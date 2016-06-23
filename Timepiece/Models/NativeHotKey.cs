using System;
using twinkfrag.Timepiece.Interop;

namespace twinkfrag.Timepiece.Models
{
	// ライブラリを使うように変更したので今は使用されていない
	public class NativeHotKey : IDisposable
	{
		private readonly IntPtr hWnd;
		private readonly IntPtr lParam;

		public NativeHotKey(IntPtr hWnd, ModKeyFlags modKey, System.Windows.Forms.Keys key)
		{
			this.hWnd = hWnd;
			this.lParam = new IntPtr((int)modKey | ((int)key * 0x10000));

			NativeMethods.RegisterHotKey(hWnd, WindowsMessage.WM_HOTKEY, modKey, key);
		}

		public void Dispose()
		{
			NativeMethods.UnregisterHotKey(this.hWnd, WindowsMessage.WM_HOTKEY);
		}

		public bool IsHotKeyDown(WindowsMessage msg, IntPtr hotKeyLParam)
		{
			return (msg == WindowsMessage.WM_HOTKEY) && (hotKeyLParam == this.lParam);
		}

	}
}
