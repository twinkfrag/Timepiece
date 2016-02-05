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

		private void Clock_OnLostFocus(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
