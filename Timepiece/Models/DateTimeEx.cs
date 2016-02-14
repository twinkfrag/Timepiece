using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using twinkfrag.Timepiece.Utils;

namespace twinkfrag.Timepiece.Models
{
	public static class DateTimeEx
	{
		private static readonly CompositeDisposable compositeDisposable = new CompositeDisposable();

		private static readonly Subject<DateTime> dateTimeSubject = new Subject<DateTime>();

		static DateTimeEx()
		{
			SetObservables();
			compositeDisposable.AddTo(Application.Current);
		}

		private static async void SetObservables()
		{
			var start = DateTime.Now;
			await Task.Run(() =>
			{
				while(DateTime.Now.Second == start.Second) { }
				new Timer(_ => dateTimeSubject.OnNext(DateTime.Now), null, 0, 1000).AddTo(compositeDisposable);
			});
		}

		public static IObservable<DateTime> DateTimeAsObservable()
		{
			return dateTimeSubject;
		} 
	}
}
