using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaskellPad.Styling
{
	class EditorTheme
	{
		public static EditorTheme Default = new EditorTheme();

		private static Font consolas = new Font("Consolas", 9);

		public Font Font { get { return consolas; } }
		public int TabLength { get { return 4; } }
		public Color TabBackgroundColor { get { return CodeBackgroundColor; } }
		public Color TabLabelBackgroundColor { get { return Color.Black; } }
		public Color TabLabelTextColor { get { return Color.White; } }
		public Color LineNumberBackgroundColor { get { return CodeBackgroundColor; } }
		public Color LineNumberTextColor { get { return Color.Aquamarine; } }
		public Color CodeBackgroundColor { get { return Color.DarkGray; } }
	}
}
