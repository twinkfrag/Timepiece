using System;
using System.Collections;
using System.Collections.Generic;

namespace twinkfrag.Timepiece.Utils
{
	public static class DisposableHelper
	{
		public static T AddTo<T>(this T disposable, IDisposableHolder holder) where T : IDisposable
		{
			// TODO: if CompositeDisposable.Disposed
			if (holder == null)
			{
				disposable.Dispose();
			}
			else
			{
				holder.CompositeDisposable.Add(disposable);
			}
			return disposable;
		}

		public static T AddTo<T>(this T disposable, ICollection<IDisposable> compositeDisposable) where T : IDisposable
		{
			compositeDisposable?.Add(disposable);
			return disposable;
		}
	}

	public interface IDisposableHolder
	{
		ICollection<IDisposable> CompositeDisposable { get; }
	}
}