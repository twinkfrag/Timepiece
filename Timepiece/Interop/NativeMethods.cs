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
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(out System.Drawing.Point lpPoint);

        [DllImport(User32Dll)]
        public static extern IntPtr mouse_event(MouseEventFlag dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        [DllImport(User32Dll)]
        public static extern IntPtr MonitorFromWindow(IntPtr hWnd, int dwFlags);

        [DllImport(User32Dll)]
        internal static extern IntPtr MonitorFromPoint(System.Drawing.Point pt, MonitorDwFlags dwFlags);

        [DllImport(User32Dll)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetMonitorInfo(IntPtr hMonitor, ref MonitorInfo lpmi);

        [DllImport("Shcore.dll")]
        internal static extern HResult GetDpiForMonitor(IntPtr hMonitor, MonitorDpiType dpiType, out uint dpiX, out uint dpiY);
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
    internal struct MonitorInfo
    {
        public uint cbSize = (uint)Marshal.SizeOf<MonitorInfo>();
        public Rect rcMonitor;
        public Rect rcWork;
        public uint dwFlags;

        public MonitorInfo() { }
    }

    [Flags]
    public enum ModKeyFlags
    {
        Alt = 0x0001,
        Control = 0x0002,
        Shift = 0x0004,
        Win = 0x0008,
    }

    [Flags]
    internal enum MonitorDwFlags : uint { DefaultToNull, DefaultToPrimary, DefaultToNearest }

    internal enum MonitorDpiType : uint { Effective, Angular, Raw, Default = Effective }

    internal enum HResult : uint
    {
        S_OK = 0, E_InvalidArg
    }
}
