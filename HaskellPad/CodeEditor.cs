using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HaskellPad.Styling;
using System.Text.RegularExpressions;
using Common;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace HaskellPad
{
	public partial class CodeEditor : UserControl, IInjectable
	{
		private EditorTheme currentTheme;

		/// <summary>
		/// Used to measure the width of a space character
		/// </summary>
		private RichTextBox measureRtb;

		private int tabLength;
		[Description("The number of spaces inserted when tab is pressed"), Category("Data"), DefaultValue(4)]
		public int TabLength
		{
			get { return tabLength; }
			set
			{
				tabLength = value;
				OnFontDataChanged();
			}
		}

		public new Font Font
		{
			get { return currentTheme.Font; }
			set
			{
				UpdateFont(value);
				OnFontDataChanged();
			}
		}

		[Dependency("diff")]
		public Func<string[], string[], List<int>> Diff;

		[Dependency("tagLines")]
		public Func<List<Line>, List<Line>> TagLines;

		public List<Line> Lines;

		public CodeEditor()
		{
			InitializeComponent();
			measureRtb = new RichTextBox();
			Lines = new List<Line>();

			//Defaults
			LoadTheme(new EditorTheme());
		}

		public void LoadTheme(EditorTheme theme)
		{
			currentTheme = theme.Clone();
			UpdateFont(currentTheme.Font);
			UpdateCodeBackgroundColor(currentTheme.CodeBackgroundColor);
			UpdateTabLength(currentTheme.TabLength);
		}

		public void AfterInjection()
		{
			//Hooks for sub controls
			string[] lastText = new string[0];
			rtbCodeView.TextChanged += (s, args) =>
			{
				var diff = Diff(lastText, rtbCodeView.Lines);
				lastText = rtbCodeView.Lines;
				var newLines = new List<Line>();
				for (var i = 0; i < diff.Count; ++i)
				{
					if (diff[i] != -1) newLines.Add(Lines[diff[i]]);
					else newLines.Add(Line.NewRaw(rtbCodeView.Lines[i]));
				}
				Lines = TagLines(newLines);
				UpdateColoring();
			};
			rtbCodeView.VScroll += (s, args) => UpdateColoring();
			rtbCodeView.ClientSizeChanged += (s, args) => UpdateColoring();
		}

		private bool updatingColors = false;
		protected void UpdateColoring()
		{
			if (updatingColors) return;
			updatingColors = true;
			IntPtr eventMask = IntPtr.Zero;
			try
			{
				//Stop redrawing
				SendMessage(rtbCodeView.Handle, WM_SETREDRAW, 0, IntPtr.Zero);
				//Stop sending of events
				eventMask = SendMessage(rtbCodeView.Handle, EM_GETEVENTMASK, 0, IntPtr.Zero);

				//Apply coloring
				int selectionStart = rtbCodeView.SelectionStart, selectionLength = rtbCodeView.SelectionLength;
				var firstLine = rtbCodeView.GetLineFromCharIndex(rtbCodeView.GetCharIndexFromPosition(Point.Empty));
				var lastLine = rtbCodeView.GetLineFromCharIndex(rtbCodeView.GetCharIndexFromPosition(new Point(rtbCodeView.ClientSize)));
				for (var i = firstLine; i <= lastLine && i < Lines.Count; ++i)
				{
					var currentIndex = rtbCodeView.GetFirstCharIndexFromLine(i);
					var line = Lines[i];
					if (!(line is Line.Tagged)) throw new Exception("A line is unexpectedly untagged.");
					var tagged = line as Line.Tagged;
					for (var j = 0; j < tagged.Item2.Count; ++j)
					{
						rtbCodeView.Select(currentIndex, tagged.Item2[j].Item1.Length);
						rtbCodeView.SelectionColor = GetColorFromTagName(tagged.Item2[j].Item2);

						currentIndex += tagged.Item2[j].Item1.Length;
					}
				}
				lFocusLabel.Focus();
				SendMessage(rtbCodeView.Handle, EM_HIDESELECTION, 1, IntPtr.Zero);
				rtbCodeView.Select(selectionStart, selectionLength);
				SendMessage(rtbCodeView.Handle, EM_HIDESELECTION, 0, IntPtr.Zero);
				rtbCodeView.Select();
			}
			finally
			{
				//Turn on events
				SendMessage(rtbCodeView.Handle, EM_SETEVENTMASK, 0, eventMask);
				//Turn on redrawing
				SendMessage(rtbCodeView.Handle, WM_SETREDRAW, 1, IntPtr.Zero);
				updatingColors = false;

			}
			rtbCodeView.Refresh();
		}

		protected Color GetColorFromTagName(string name)
		{
			var dict = new Dictionary<string, Color>
			{
				{"text", currentTheme.PlainTextColor},
				{"identifier", currentTheme.PlainTextColor}
			};
			if (dict.ContainsKey(name)) return dict[name];
			else
			{
				return (Color)(typeof(EditorTheme)
					.GetProperty(Char.ToUpper(name[0]) + name.Substring(1) + "TextColor")
					.GetValue(currentTheme));
			}
		}

		protected void OnFontDataChanged()
		{
			if (Font == null) return;
			using (var gfx = this.CreateGraphics())
			{
				measureRtb.Font = Font;
				measureRtb.Text = "  ";
				var pos1 = measureRtb.GetPositionFromCharIndex(0);
				var pos2 = measureRtb.GetPositionFromCharIndex(1);
				var spaceWidthInPx = pos2.X - pos1.X;
				var spaceWidthInPoints = spaceWidthInPx * 72F / gfx.DpiX;
				var spaceWidthInTwips = (int)Math.Round(spaceWidthInPoints * 20F);
				var tabWidthInTwips = spaceWidthInTwips * TabLength;
				if (rtbCodeView.Rtf.IndexOf("deftab") != -1)
				{
					rtbCodeView.Rtf = Regex.Replace(rtbCodeView.Rtf, "deftab[0-9]+", "deftab" + tabWidthInTwips);
				}
				else
				{
					rtbCodeView.Rtf = rtbCodeView.Rtf.Insert(rtbCodeView.Rtf.IndexOf('{', 1), "\\deftab" + tabWidthInTwips);
				}
			}
		}

		protected void UpdateFont(Font font)
		{
			rtbCodeView.Font = font;
			currentTheme.Font = font;
		}

		protected void UpdateCodeBackgroundColor(Color color)
		{
			rtbCodeView.BackColor = color;
			currentTheme.CodeBackgroundColor = color;
		}

		protected void UpdateTabLength(int length)
		{
			TabLength = length;
			currentTheme.TabLength = length;
		}

		//These are used to prevent selection fickering when syntax coloring is working
		private const int WM_SETREDRAW = 0x000B;
		private const int WM_USER = 0x400;
		private const int EM_GETEVENTMASK = (WM_USER + 59);
		private const int EM_SETEVENTMASK = (WM_USER + 69);
		private const int EM_HIDESELECTION = WM_USER + 63;

		[DllImport("user32", CharSet = CharSet.Auto)]
		private extern static IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam);
	}
}
