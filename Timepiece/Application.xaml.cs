using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using twinkfrag.Timepiece.Models;
using twinkfrag.Timepiece.Models.ShortcutKey;
using twinkfrag.Timepiece.Properties;
using twinkfrag.Timepiece.Utils;
using twinkfrag.Timepiece.Views;

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

			Clock clock = null;

			var detector = new ShortcutKeyDetector().AddTo(this);
			detector.KeySetPressedAsObservable()
					.Where(_ => clock?.IsDisposed ?? true)
					.Where(args => args.ShortcutKey == Settings.Default.ModkeySetting.ToShortcutKey())
			        .Subscribe(args =>
			        {
				        args.Handled = true;
				        clock = new Clock();
			        }).AddTo(this);
		}

		protected override void OnExit(ExitEventArgs e)
		{
			base.OnExit(e);

			compositeDisposable.Dispose();
		}

		ICollection<IDisposable> IDisposableHolder.CompositeDisposable => this.compositeDisposable;

		public new static Application Current => System.Windows.Application.Current as Application;
	}
}
