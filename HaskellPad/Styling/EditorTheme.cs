using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HaskellPad.Styling
{
	public class EditorTheme
	{
		public static EditorTheme Default = new EditorTheme();

		private static Font consolas = new Font("Consolas", 9);

		public Font Font { get; set; }
		public int TabLength { get; set; }
		public Color TabBackgroundColor { get; set; }
		public Color TabLabelBackgroundColor { get; set; }
		public Color TabLabelTextColor { get; set; }
		public Color LineNumberBackgroundColor { get; set; }
		public Color LineNumberTextColor { get; set; }
		public Color CodeBackgroundColor { get; set; }
		public Color CommentTextColor { get; set; }
		public Color PlainTextColor { get; set; }
		public Color KeywordTextColor { get; set; }
		public Color NumberTextColor { get; set; }
		public Color OperatorTextColor { get; set; }
		public Color StringTextColor { get; set; }
		public Color CharTextColor { get; set; }
		public Color TypeTextColor { get; set; }

		public EditorTheme()
		{
			Font = consolas;
			TabLength = 4;
			TabLabelBackgroundColor = Color.Black;
			TabLabelTextColor = Color.White;
			LineNumberTextColor = Color.Aquamarine;
			CodeBackgroundColor = Color.DarkGray;
			LineNumberBackgroundColor = CodeBackgroundColor;
			TabBackgroundColor = CodeBackgroundColor;
			PlainTextColor = Color.White;
			KeywordTextColor = Color.Green;
			NumberTextColor = Color.Yellow;
			OperatorTextColor = Color.LightYellow;
			StringTextColor = Color.Orange;
			CharTextColor = Color.Orange;
			TypeTextColor = Color.Blue;
		}

		public EditorTheme(string vssettings)
		{
			Font = consolas;
			TabLength = 4;
			TabLabelBackgroundColor = Color.Black;
			TabLabelTextColor = Color.White;
			LineNumberTextColor = GetColorByName(vssettings, "Line Numbers");
			CodeBackgroundColor = GetColorByName(vssettings, "Plain Text", true);
			LineNumberBackgroundColor = GetColorByName(vssettings, "Line Numbers", true);
			TabBackgroundColor = CodeBackgroundColor;
			PlainTextColor = GetColorByName(vssettings, "Plain Text");
			CommentTextColor = GetColorByName(vssettings, "Comment");
			KeywordTextColor = GetColorByName(vssettings, "Keyword");
			NumberTextColor = GetColorByName(vssettings, "Number");
			OperatorTextColor = GetColorByName(vssettings, "Operator");
			StringTextColor = GetColorByName(vssettings, "String");
			CharTextColor = GetColorByName(vssettings, "String"); //vssettings don't have a separate char color
		}

		public EditorTheme Clone()
		{
			return (EditorTheme)this.MemberwiseClone();
		}

		private Color GetColorByName(string vssettings, string name, bool background = false)
		{
			string res;
			if (!background)
			{
				res = Regex.Match(vssettings, @"<Item Name=""" + name + @""" Foreground=""([^""]*)""").Groups[1].Value.Substring(2);
			}
			else
			{
				res = Regex.Match(vssettings, @"<Item Name=""" + name + @""" Foreground=""[^""]*"" Background=""([^""]*)""").Groups[1].Value.Substring(2);
			}
			return Color.FromArgb(ReadHexString(res, 6), ReadHexString(res, 4), ReadHexString(res, 2));
		}

		private int ReadHexString(string str, int start)
		{
			return Int32.Parse(str.Substring(start, 2), System.Globalization.NumberStyles.HexNumber);
		}

		private Color MakeOpaque(Color color)
		{
			return Color.FromArgb(255, color);
		}
	}
}
