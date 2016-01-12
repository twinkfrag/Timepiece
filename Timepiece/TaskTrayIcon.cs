using System;
using System.Drawing;
using System.Windows.Forms;

namespace Timepiece
{
	class TaskTrayIcon : IDisposable
	{
		private readonly NotifyIcon notifyIcon;

		public TaskTrayIcon()
		{
			notifyIcon = new NotifyIcon
			{
				Text = nameof(Timepiece),
				Icon = SystemIcons.Application,
				Visible = true,
				ContextMenu = new ContextMenu(new[]
				{
					new MenuItem("E&xit", (sender, args) => App.Current.Shutdown()),
				}),
			};
		}

		public void Dispose()
		{
			notifyIcon?.Dispose();
		}
	}
}
