﻿using System.Configuration;
using twinkfrag.Timepiece.Models;
// ReSharper disable CheckNamespace

namespace twinkfrag.Timepiece.Properties
{
	// このクラスでは設定クラスでの特定のイベントを処理することができます:
	//  SettingChanging イベントは、設定値が変更される前に発生します。
	//  PropertyChanged イベントは、設定値が変更された後に発生します。
	//  SettingsLoaded イベントは、設定値が読み込まれた後に発生します。
	//  SettingsSaving イベントは、設定値が保存される前に発生します。
	internal sealed partial class Settings
	{

		public Settings()
		{
			// // 設定の保存と変更のイベント ハンドラーを追加するには、以下の行のコメントを解除します:
			//
			// this.SettingChanging += this.SettingChangingEventHandler;
			//
			// this.SettingsSaving += this.SettingsSavingEventHandler;
			//
			this.PropertyChanged += (_, __) => this.Save();

			if (this[nameof(this.ModkeySetting)] == null) this.ModkeySetting = ModkeySetting.None;
			if (this[nameof(this.ClockTypeSetting)] == null) this.ClockTypeSetting = ClockTypeSetting.Win8;
		}

		private void SettingChangingEventHandler(object sender, System.Configuration.SettingChangingEventArgs e)
		{
			// SettingChangingEvent イベントを処理するコードをここに追加してください。
		}

		private void SettingsSavingEventHandler(object sender, System.ComponentModel.CancelEventArgs e)
		{
			// SettingsSaving イベントを処理するコードをここに追加してください。
		}

		[UserScopedSetting]
		public ModkeySetting ModkeySetting
		{
			get { return (ModkeySetting)this[nameof(this.ModkeySetting)]; }
			set { this[nameof(this.ModkeySetting)] = value; }
		}

		/// <summary>
		/// <see cref="ModkeySetting"/>のFlagを逆転させます。
		/// </summary>
		/// <param name="key">逆転させるFlag</param>
		/// <returns>逆転させた結果の<see cref="System.Enum.HasFlag"/></returns>
		public bool ToggleModkeySetting(ModkeySetting key)
		{
			if (this.ModkeySetting.HasFlag(key))
			{
				this.ModkeySetting -= key;
				return false;
			}
			else
			{
				this.ModkeySetting += (int)key;
				return true;
			}
		}

		[UserScopedSetting]
		public ClockTypeSetting ClockTypeSetting
		{
			get { return (ClockTypeSetting)this[nameof(this.ClockTypeSetting)]; }
			set { this[nameof(this.ClockTypeSetting)] = value; }
		}
	}
}
