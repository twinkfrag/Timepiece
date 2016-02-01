using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Windows.Input;
using Open.WinKeyboardHook;

namespace twinkfrag.Timepiece.Models.ShortcutKey
{
	public class ShortcutKeyDetector : IDisposable
	{
		private readonly IKeyboardInterceptor interceptor = new KeyboardInterceptor();

		private readonly Subject<Key> keyDownSubject = new Subject<Key>();
		private readonly Subject<Key> keyUpSubject = new Subject<Key>();

		public ShortcutKeyDetector()
		{
			this.interceptor.KeyDown += InterceptorOnKeyDown;
			this.interceptor.KeyUp += InterceptorOnKeyUp;
		}

		public void Start()
		{
			this.interceptor.StartCapturing();
		}

		public void Stop()
		{
			this.interceptor.StopCapturing();
		}

		private void InterceptorOnKeyDown(object sender, System.Windows.Forms.KeyEventArgs args)
		{
			var key = KeyInterop.KeyFromVirtualKey((int)args.KeyCode);
			keyDownSubject.OnNext(key);
		}

		private void InterceptorOnKeyUp(object sender, System.Windows.Forms.KeyEventArgs args)
		{
			var key = KeyInterop.KeyFromVirtualKey((int)args.KeyCode);
			keyUpSubject.OnNext(key);
		}

		public IObservable<Key> OnKeyDownAsObservable() => keyDownSubject;

		public IObservable<Key> OnKeyUpAsObservable() => keyUpSubject;

		public void Dispose()
		{
			Stop();

			keyDownSubject.OnCompleted();
			keyDownSubject.Dispose();
			keyUpSubject.OnCompleted();
			keyUpSubject.Dispose();
		}
	}
}
