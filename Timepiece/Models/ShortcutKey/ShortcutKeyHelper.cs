using System.Windows.Input;

namespace twinkfrag.Timepiece.Models.ShortcutKey
{
	public static class ShortcutKeyHelper
	{
		public static bool IsModifyKey(this Key key)
		{
			switch (key)
			{
				case Key.LeftAlt:
					return true;
				case Key.RightAlt:
					return true;
				case Key.LeftCtrl:
					return true;
				case Key.RightCtrl:
					return true;
				case Key.LWin:
					return true;
				case Key.RWin:
					return true;
				case Key.LeftShift:
					return true;
				case Key.RightShift:
					return true;
				default:
					return false;
			}
		}
	}
}