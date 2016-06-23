using System;
using System.Runtime.InteropServices;

namespace twinkfrag.Timepiece.Interop
{
	public static class NativeMethods
	{
		public const string User32Dll = "user32.dll";

		[DllImport(User32Dll)]
		public static extern int RegisterHotKey(IntPtr hWnd, WindowsMessage id, ModKeyFlags modKey, System.Windows.Forms.Keys key);

		[DllImport(User32Dll)]
		public static extern int UnregisterHotKey(IntPtr hWnd, WindowsMessage id);


		public delegate bool EnumChildCallback(IntPtr hwnd, ref IntPtr lParam);

		[DllImport(User32Dll)]
		public static extern bool EnumChildWindows(IntPtr hWndParent, EnumChildCallback lpEnumFunc, ref IntPtr lParam);

		[DllImport(User32Dll)]
		public static extern int GetClassName(IntPtr hWnd, System.Text.StringBuilder lpClassName, int nMaxCount);

		[DllImport(User32Dll)]
		public static extern bool SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

		[DllImport(User32Dll)]
		public static extern IntPtr PostMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

		[DllImport(User32Dll)]
		public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		[DllImport(User32Dll)]
		public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

		[DllImport(User32Dll)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetWindowRect(IntPtr hWnd, out Rect lpRect);


		[DllImport(User32Dll)]
		public static extern IntPtr SetCursorPos(int x, int y);

		[DllImport(User32Dll)]
		public static extern IntPtr GetCursorPos(ref Point point);

		[DllImport(User32Dll)]
		public static extern IntPtr mouse_event(MouseEventFlag dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

		public const uint HTCAPTION = 2;

		public const int MOUSEEVENTF_LEFTDOWN = 0x2;
		public const int MOUSEEVENTF_LEFTUP = 0x4;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct Rect
	{
		public int Left;
		public int Top;
		public int Right;
		public int Bottom;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct Point
	{
		public int X;
		public int Y;
	}

	[Flags]
	public enum ModKeyFlags
	{
		Alt = 0x0001,
		Control = 0x0002,
		Shift = 0x0004,
		Win = 0x0008,
	}
}
