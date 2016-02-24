using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using twinkfrag.Timepiece.Models;
using twinkfrag.Timepiece.Properties;

namespace twinkfrag.Timepiece.Views
{
	class TaskTrayIcon : IDisposable
	{
		private readonly NotifyIcon notifyIcon;

		public TaskTrayIcon()
		{
			var modkeyMenuItems = ModkeySettingHelper.AsArray.Select(x =>
				new MenuItem(x.ToString(), this.OnModkeyMenuClick)).ToArray();

			this.notifyIcon = new NotifyIcon
			{
				Text = nameof(Timepiece),
				Icon = SystemIcons.Application,
				Visible = true,
				ContextMenu = new ContextMenu(new[]
				{
					new MenuItem("Win + C +", modkeyMenuItems),
					new MenuItem("E&xit", (sender, args) => Application.Current.Shutdown()),
				}),
			};

			// formのbinding使おうかと思ったけどさっぱりわからんぞい
			for (int i = 0; i < modkeyMenuItems.Length; i++)
			{
				modkeyMenuItems[i].Checked = Settings.Default.ModkeySetting.HasFlag(ModkeySettingHelper.AsArray[i]);
			}
		}

		private void OnModkeyMenuClick(object sender, EventArgs eventArgs)
		{
			var item = sender as MenuItem;
			if (item == null) return;

			item.Checked = Settings.Default.ToggleModkeySetting(ModkeySettingHelper.AsArray[item.Index]);
		}

		public void Dispose()
		{
			this.notifyIcon?.Dispose();
		}
	}
}
