using System;
using System.Windows;
using System.Windows.Input;
using twinkfrag.Timepiece.Interop;

namespace twinkfrag.Timepiece.Views
{
    /// <summary>
    /// Clock.xaml の相互作用ロジック
    /// </summary>
    public partial class Clock : Window, IDisposable
    {
        public Clock()
        {
            InitializeComponent();

            if (!NativeMethods.GetCursorPos(out var cursorPos)) return;
            var currentMonitor = NativeMethods.MonitorFromPoint(cursorPos, MonitorDwFlags.DefaultToNearest);
            if (currentMonitor == IntPtr.Zero) return;
            if (NativeMethods.GetDpiForMonitor(
                currentMonitor, MonitorDpiType.Effective, out var dpiX, out var dpiY
                ) != HResult.S_OK) return;
            var monitorInfo = new MonitorInfo();
            if (!NativeMethods.GetMonitorInfo(currentMonitor, ref monitorInfo)) return;
            var dpiScaleX = dpiX / 96.0f;
            var dpiScaleY = dpiY / 96.0f;

            this.Left = (monitorInfo.rcWork.Left + DISPLAY_MARGIN) / dpiScaleX;
            this.Top = (monitorInfo.rcWork.Bottom - this.Height - DISPLAY_MARGIN) / dpiScaleY;

            this.Show();
            this.Activate();
        }

        private const double DISPLAY_MARGIN = 100d;

        private void Clock_OnDeactivated(object sender, EventArgs e)
        {
            //this.Dispose();
        }

        private void Clock_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                e.Handled = true;
                this.Dispose();
            }
        }

        public bool IsDisposed { get; private set; } = false;

        public void Dispose()
        {
            if (this.IsDisposed) return;

            this.IsDisposed = true;
            this.Close();
        }
    }
}
