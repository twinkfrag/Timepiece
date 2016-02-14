using System;
using System.Windows;
using System.Windows.Input;

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

			var desktop = System.Windows.Forms.Screen.FromPoint(System.Windows.Forms.Cursor.Position).Bounds;
			this.Left = desktop.Left + DISPLAY_MARGIN;
			this.Top = desktop.Bottom - this.Height - DISPLAY_MARGIN;

			this.Show();
			this.Activate();
		}

		private const double DISPLAY_MARGIN = 100d;

		private void Clock_OnDeactivated(object sender, EventArgs e)
		{
			this.Dispose();
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
