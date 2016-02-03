using System;

namespace twinkfrag.Timepiece.Models.ShortcutKey
{
	public class ShortcutKeyPressedEventArgs : EventArgs
	{
		public ShortcutKey ShortcutKey { get; set; }

		public bool Handled { get; set; }

		public ShortcutKeyPressedEventArgs(ShortcutKey shortcutKey)
		{
			this.ShortcutKey = shortcutKey;
		}

		public override string ToString()
		{
			return $"Pressed {ShortcutKey}";
		}
	}
}