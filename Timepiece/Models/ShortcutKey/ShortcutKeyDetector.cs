using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Windows.Input;
using Open.WinKeyboardHook;

namespace twinkfrag.Timepiece.Models.ShortcutKey
{
	public class ShortcutKeyDetector : IDisposable
	{
		private readonly IKeyboardInterceptor interceptor = new KeyboardInterceptor();

		private readonly Subject<ISet<Key>> keySubject = new Subject<ISet<Key>>();
		private readonly HashSet<Key> pressedKeys = new HashSet<Key>();

		public ShortcutKeyDetector()
		{
			this.interceptor.KeyDown += this.InterceptorOnKeyDown;
			this.interceptor.KeyUp += this.InterceptorOnKeyUp;

			this.Start();
		}

		public void Start()
		{
			this.interceptor.StartCapturing();
		}

		public void Stop()
		{
			this.interceptor.StopCapturing();
		}

		public IDisposable Suspend()
		{
			this.Stop();
			return Disposable.Create(this.Start);
		}

		private void InterceptorOnKeyDown(object sender, System.Windows.Forms.KeyEventArgs args)
		{
			var key = KeyInterop.KeyFromVirtualKey((int)args.KeyCode);
			this.pressedKeys.Add(key);
		}

		private void InterceptorOnKeyUp(object sender, System.Windows.Forms.KeyEventArgs args)
		{
			var key = KeyInterop.KeyFromVirtualKey((int)args.KeyCode);
			this.keySubject.OnNext(new HashSet<Key>(this.pressedKeys));
			this.pressedKeys.Remove(key);
		}

		public IObservable<ISet<Key>> KeySetPressedAsObservable() => this.keySubject.AsObservable();

		public void Dispose()
		{
			this.Stop();

			this.interceptor.KeyDown -= this.InterceptorOnKeyDown;
			this.interceptor.KeyUp -= this.InterceptorOnKeyUp;

			this.keySubject.OnCompleted();
			this.keySubject.Dispose();
		}
	}
}
