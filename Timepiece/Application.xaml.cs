using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using System.Windows;
using twinkfrag.Timepiece.Models;
using twinkfrag.Timepiece.Models.ShortcutKey;
using twinkfrag.Timepiece.Utils;
using twinkfrag.Timepiece.Veiws;

namespace twinkfrag.Timepiece
{
	/// <summary>
	/// Application.xaml の相互作用ロジック
	/// </summary>
	public partial class Application : IDisposableHolder
	{
		private readonly CompositeDisposable compositeDisposable = new CompositeDisposable();

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			new TaskTrayIcon().AddTo(this);
		}

		protected override void OnExit(ExitEventArgs e)
		{
			base.OnExit(e);

			compositeDisposable.Dispose();
		}

		ICollection<IDisposable> IDisposableHolder.CompositeDisposable => this.compositeDisposable;
	}
}
