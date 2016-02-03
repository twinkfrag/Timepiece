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

		private readonly Subject<ShortcutKeyPressedEventArgs> keySubject = new Subject<ShortcutKeyPressedEventArgs>();
		private readonly HashSet<Key> pressedModifiers = new HashSet<Key>();

		public bool RequireModifier { get; set; } = false;

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

			if (key.IsModifyKey())
			{
				this.pressedModifiers.Add(key);
			}
			else if (this.pressedModifiers.Count > 0 || !this.RequireModifier)
			{
				var shortcut = new ShortcutKeyPressedEventArgs(new ShortcutKey(key, new HashSet<Key>(this.pressedModifiers)));
				this.keySubject.OnNext(shortcut);
				args.SuppressKeyPress = !shortcut.Handled;
			}
		}

		private void InterceptorOnKeyUp(object sender, System.Windows.Forms.KeyEventArgs args)
		{
			if(!this.pressedModifiers.Any()) return;

			var key = KeyInterop.KeyFromVirtualKey((int)args.KeyCode);
			this.pressedModifiers.Remove(key);
		}

		public IObservable<ShortcutKeyPressedEventArgs> KeySetPressedAsObservable() => this.keySubject.AsObservable();

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
