﻿using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using IdeaAnchor.MenuProviders;

namespace IdeaAnchor.ViewModels
{
	public class SettingsViewModel : BindableObject
	{
		private List<AppMenuItem> _settingsMenu;
		public List<AppMenuItem> SettingsMenu
		{
			get => _settingsMenu;
			set
			{
				_settingsMenu = value;
				OnPropertyChanged(nameof(SettingsMenu));
			}
		}

		public ICommand BackCommand => new AsyncRelayCommand(() => Shell.Current.GoToAsync(".."));

		public SettingsViewModel(SettingsMenuProvider settingsMenuProvider)
		{
			SettingsMenu = settingsMenuProvider.GetMenu();
		}
	}
}

