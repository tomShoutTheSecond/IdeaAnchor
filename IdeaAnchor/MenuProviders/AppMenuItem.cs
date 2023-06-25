using System;
using System.Windows.Input;

namespace IdeaAnchor.MenuProviders
{
	public class AppMenuItem : BindableObject
	{
		public ICommand Command { get; set; }

		public string Name { get; set; }

		public string Icon { get; set; }

		private bool _isSelected;
        public bool IsSelected
		{
			get => _isSelected;
			set
			{
				_isSelected = value;

				OnPropertyChanged(nameof(IsSelected));
			}
		}
    }
}

