using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace twinkfrag.Timepiece.Models
{
	[Serializable]
	[Flags]
	public enum ModkeySetting
	{
		None = 0x00,
		Shift = 0x01,
		Ctrl = 0x02,
		Alt = 0x04,
	}

	public static class ModkeySettingHelper
	{
		public static ModkeySetting[] AsArray { get; } = { ModkeySetting.Shift, ModkeySetting.Ctrl, ModkeySetting.Alt, };

		public static Key[] ToKeys(this ModkeySetting modkeySetting)
		{
			var list = new List<Key> { Key.LWin };
			if (modkeySetting.HasFlag(ModkeySetting.Shift)) list.Add(Key.LeftShift);
			if (modkeySetting.HasFlag(ModkeySetting.Ctrl)) list.Add(Key.LeftCtrl);
			if (modkeySetting.HasFlag(ModkeySetting.Alt)) list.Add(Key.LeftAlt);
			return list.ToArray();
		}

		public static ShortcutKey.ShortcutKey ToShortcutKey(this ModkeySetting modkey)
		{
			ShortcutKey.ShortcutKey value;
			if (!shortcutkeysCache.TryGetValue(modkey, out value))
			{
				value = new ShortcutKey.ShortcutKey(Key.C, modkey.ToKeys());
				shortcutkeysCache.Add(modkey, value);
			}
			return value;
		}

		private static Dictionary<ModkeySetting, ShortcutKey.ShortcutKey> shortcutkeysCache
			= new Dictionary<ModkeySetting, ShortcutKey.ShortcutKey>();
	}
}