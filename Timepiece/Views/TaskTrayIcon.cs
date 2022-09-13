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
                new ToolStripMenuItem(x.ToString(), null, this.OnModkeyMenuClick)).ToArray();

            var cms = new ContextMenuStrip();
            cms.Items.AddRange(new[]
            {
                new ToolStripMenuItem("Style", null,
                    new ToolStripMenuItem("Windows 8 Charm Clock", null, (s, e) => { Settings.Default.ClockTypeSetting = ClockTypeSetting.Win8; })
                    {
                        Checked = Settings.Default.ClockTypeSetting == ClockTypeSetting.Win8
                    },
                    new ToolStripMenuItem("Windows Taskbar Calendar", null, (s, e) => { Settings.Default.ClockTypeSetting = ClockTypeSetting.Calendar; })
                    {
                        Checked = Settings.Default.ClockTypeSetting == ClockTypeSetting.Calendar
                    }),
                new ToolStripMenuItem("Win + C +", null, modkeyMenuItems),
                new ToolStripMenuItem("E&xit", null, (sender, args) => Application.Current.Shutdown()),
            });

            this.notifyIcon = new NotifyIcon
            {
                Text = nameof(Timepiece),
                Icon = SystemIcons.Application,
                Visible = true,
                ContextMenuStrip = cms,
            };

            // formのbinding使おうかと思ったけどさっぱりわからんぞい
            for (int i = 0; i < modkeyMenuItems.Length; i++)
            {
                modkeyMenuItems[i].Checked = Settings.Default.ModkeySetting.HasFlag(ModkeySettingHelper.AsArray[i]);
            }
        }

        private void OnModkeyMenuClick(object sender, EventArgs eventArgs)
        {
            var item = sender as ToolStripMenuItem;
            if (item == null) return;

            var index = item.GetCurrentParent().Items.IndexOf(item);
            item.Checked = Settings.Default.ToggleModkeySetting(ModkeySettingHelper.AsArray[index]);
        }

        public void Dispose()
        {
            this.notifyIcon?.Dispose();
        }
    }
}
