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

namespace HaskellPad
{
	public partial class CodeEditor : UserControl
	{
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

		[Description("The color behind the tab buttons"), Category("Appearance")]
		public Color TabBackgroundColor { get; set; }

		[Description("The color of the tab buttons"), Category("Appearance")]
		public Color TabLabelBackgroundColor { get; set; }

		[Description("The color of the tab button text"), Category("Appearance")]
		public Color TabLabelTextColor { get; set; }

		public Color LineNumberBackgroundColor { get; set; }

		public Color LineNumberTextColor { get; set; }

		public Color CodeBackgroundColor { get; set; }

		private Font font;
		public new Font Font
		{
			get { return font; }
			set
			{
				font = value;
				OnFontDataChanged();
			}
		}
		
		public CodeEditor()
		{
			InitializeComponent();
			bndCodeEditor.DataSource = this;
			measureRtb = new RichTextBox();

			//Hooks for sub controls
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
	}
}
