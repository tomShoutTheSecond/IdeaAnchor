using System;
using System.Windows.Input;
using IdeaAnchor.Database;
using IdeaAnchor.Models;

namespace IdeaAnchor.ViewModels
{
	public class IdeaViewModel : BindableObject
	{
		//TODO: bind the left button icon to this bool
		//it changes from a check to a back chevron when idea is saved
		private bool _ideaIsSaved;
		public bool IdeaIsSaved
		{
			get => _ideaIsSaved;
			set
			{
				_ideaIsSaved = value;

				OnPropertyChanged(nameof(IdeaIsSaved));
			}
		}

        private readonly IdeaDatabase _db;

        public IdeaViewModel(IdeaDatabase db)
		{
			_db = db;

			IdeaContent = "";
        }

		private string _ideaContent;
		public string IdeaContent
		{
			get => _ideaContent;
			set
			{
				_ideaContent = value;

				IdeaIsSaved = false;

                OnPropertyChanged(nameof(IdeaContent));
			}
		}

		public async Task SaveIdea()
		{
			//TODO: save idea to local storage

			var idea = new Idea
			{
				Content = IdeaContent
			};

			try
			{
                await _db.SaveItemAsync(idea);

				if (idea.Content == IdeaContent)
				{
					//user has not changed the text since the save operation was invoked

					IdeaIsSaved = true;
				}
            }
			catch(Exception e)
			{
				Console.WriteLine(e.Message);
			}
        }
    }
}

