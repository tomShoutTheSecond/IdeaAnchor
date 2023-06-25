using System;
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

		public SettingsViewModel(SettingsMenuProvider settingsMenuProvider)
		{
			SettingsMenu = settingsMenuProvider.GetMenu();
		}
	}
}

