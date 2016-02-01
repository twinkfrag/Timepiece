using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace twinkfrag.Timepiece.Models
{
	public class NativeHotKey : IDisposable
	{
		private readonly IntPtr hWnd;
		private readonly IntPtr lParam;

		public NativeHotKey(IntPtr hWnd, ModKeys modKey, Keys key)
		{
			this.hWnd = hWnd;
			lParam = new IntPtr((int)modKey | ((int)key * 0x10000));

			NativeMethods.RegisterHotKey(hWnd, NativeMethods.WM_HOTKEY, modKey, key);
		}

		public void Dispose()
		{
			NativeMethods.UnregisterHotKey(hWnd, NativeMethods.WM_HOTKEY);
		}

		public bool IsHotKeyDown(int msg, IntPtr hotKeyLParam)
		{
			return (msg == NativeMethods.WM_HOTKEY) && (hotKeyLParam == lParam);
		}


		private static class NativeMethods
		{
			private const string User32Dll = "user32.dll";

			[DllImport(User32Dll)]
			public static extern int RegisterHotKey(IntPtr hWnd, int id, ModKeys modKey, Keys key);

			[DllImport(User32Dll)]
			public static extern int UnregisterHotKey(IntPtr hWnd, int id);

			public const int WM_HOTKEY = 0x0312;
		}
	}

	[Flags]
	public enum ModKeys
	{
		Alt = 0x0001,
		Control = 0x0002,
		Shift = 0x0004,
		Win = 0x0008,
	}
}
