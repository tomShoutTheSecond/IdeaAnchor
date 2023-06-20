using System;
using System.Windows.Input;
using IdeaAnchor.Database;
using IdeaAnchor.Models;

namespace IdeaAnchor.ViewModels
{
	public class IdeaViewModel : BindableObject
	{
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
            }
			catch(Exception e)
			{
				Console.WriteLine(e.Message);
			}
        }
    }
}

