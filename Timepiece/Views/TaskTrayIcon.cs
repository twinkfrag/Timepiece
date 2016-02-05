using System;
using System.Drawing;
using System.Windows.Forms;

namespace twinkfrag.Timepiece.Views
{
	class TaskTrayIcon : IDisposable
	{
		private readonly NotifyIcon notifyIcon;

		public TaskTrayIcon()
		{
			this.notifyIcon = new NotifyIcon
			{
				Text = nameof(Timepiece),
				Icon = SystemIcons.Application,
				Visible = true,
				ContextMenu = new ContextMenu(new[]
				{
					new MenuItem("E&xit", (sender, args) => Application.Current.Shutdown()),
				}),
			};
		}

		public void Dispose()
		{
			this.notifyIcon?.Dispose();
		}
	}
}
