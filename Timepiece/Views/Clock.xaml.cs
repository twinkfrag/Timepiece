using System;
using System.Windows;

namespace twinkfrag.Timepiece.Views
{
	/// <summary>
	/// Clock.xaml の相互作用ロジック
	/// </summary>
	public partial class Clock : Window
	{
		public Clock()
		{
			InitializeComponent();
		}

		private void Clock_OnDeactivated(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
