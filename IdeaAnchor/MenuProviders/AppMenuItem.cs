using System;
using System.Windows.Input;

namespace IdeaAnchor.MenuProviders
{
	public class AppMenuItem
	{
		public ICommand Command { get; set; }

		public string Name { get; set; }

		public string Icon { get; set; }

		public bool IsSelected { get; set; }
    }
}

