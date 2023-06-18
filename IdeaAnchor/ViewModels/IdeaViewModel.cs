using System;
using System.Windows.Input;

namespace IdeaAnchor.ViewModels
{
	public class IdeaViewModel : BindableObject
	{
		public IdeaViewModel()
		{
			IdeaContent = "";
        }

		private string _ideaContent;
		public string IdeaContent
		{
			get => _ideaContent;
			set
			{
				_ideaContent = value;

				OnPropertyChanged(nameof(IdeaContent));
			}
		}

		public void SaveIdea()
		{
			//TODO: save idea to local storage
		}
    }
}

