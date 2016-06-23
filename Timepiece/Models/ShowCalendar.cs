using System;
using System.ComponentModel;
using System.Text;
using twinkfrag.Timepiece.Interop;

namespace twinkfrag.Timepiece.Models
{
	public class ShowCalendar
	{

		private const string TrayWndClassName = "Shell_TrayWnd";
		private const string TrayNotifyWndClassName = "TrayNotifyWnd";
		private const string ClockWndClassName = "TrayClockWClass";

		public static void Show()
		{
			var hWndTray = NativeMethods.FindWindow(TrayWndClassName, string.Empty);
			if (hWndTray == IntPtr.Zero)
			{
				throw new Win32Exception();
			}

			var hWndTrayNotify = NativeMethods.FindWindowEx(hWndTray, IntPtr.Zero, TrayNotifyWndClassName, string.Empty);
			if (hWndTrayNotify == IntPtr.Zero)
			{
				throw new Win32Exception();
			}

			// search clock window
			var cb = new NativeMethods.EnumChildCallback((IntPtr hWndChild, ref IntPtr lParam) =>
			{
				var className = new StringBuilder(128);
				NativeMethods.GetClassName(hWndChild, className, 128);

				if (className.ToString() != ClockWndClassName) return true;

				lParam = hWndChild;
				return false;
			});

			var hWndClock = IntPtr.Zero;
			NativeMethods.EnumChildWindows(hWndTray, cb, ref hWndClock);
			if (hWndClock == IntPtr.Zero)
			{
				throw new Win32Exception();
			}

			// get clock window position
			Rect rect;
			if (!NativeMethods.GetWindowRect(hWndClock, out rect))
			{
				throw new Win32Exception();
			}

			// send click, lParam contains window position
			//IntPtr wParam = new IntPtr(HTCAPTION);
			//IntPtr lParam = new IntPtr(rect.Top << 16 | rect.Left);
			//var ret = SendMessage(hWndTray, WM_LBUTTONDOWN, wParam, lParam);
			//SendMessage(hWndTray, WM_LBUTTONUP, wParam, lParam);

			var current = new Point();
			NativeMethods.GetCursorPos(ref current);
			NativeMethods.SetCursorPos(rect.Left + 10, rect.Top + 10);
			NativeMethods.mouse_event(MouseEventFlag.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
			NativeMethods.mouse_event(MouseEventFlag.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
			NativeMethods.SetCursorPos(current.X, current.Y);

			//Console.ReadKey();
		}
	}
}