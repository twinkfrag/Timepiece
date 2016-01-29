using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using twinkfrag.Timepiece.Veiws;

namespace twinkfrag.Timepiece
{
	/// <summary>
	/// Application.xaml の相互作用ロジック
	/// </summary>
	public partial class Application : System.Windows.Application
	{
		private TaskTrayIcon taskTrayIcon;

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			taskTrayIcon = new TaskTrayIcon();
		}

		protected override void OnExit(ExitEventArgs e)
		{
			base.OnExit(e);

			taskTrayIcon.Dispose();
		}
	}
}
