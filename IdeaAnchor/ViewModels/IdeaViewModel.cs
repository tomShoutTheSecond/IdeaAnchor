using System;
using System.Windows.Input;
using IdeaAnchor.Database;
using IdeaAnchor.Models;
using IdeaAnchor.Helper;

namespace IdeaAnchor.ViewModels
{
	public class IdeaViewModel : BindableObject, IQueryAttributable
	{
        private const string DefaultTitle = "Untitled Note";

        private readonly IdeaDatabase _db;

        private Idea _existingIdea;

        public IdeaViewModel(IdeaDatabase db)
		{
			_db = db;
        }

        //left icon changes from a check to a back chevron when idea is saved
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

        private string _ideaTitle;
        public string IdeaTitle
        {
            get => _ideaTitle;
            set
            {
                _ideaTitle = value;

                IdeaIsSaved = false;

                OnPropertyChanged(nameof(IdeaTitle));
            }
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
            var idea = new Idea
            {
                Id = _existingIdea?.Id,
				Title = IdeaTitle,
				Content = IdeaContent
			};

            if (idea.Title.IsNullOrWhiteSpace() && idea.Content.IsNullOrWhiteSpace())
            {
                //don't save an empty idea

                if (idea.Id != null)
                {
                    //delete existing idea

                    await _db.DeleteItemAsync(idea);

                    _existingIdea = null;
                }

                IdeaIsSaved = true;
                return;
            }

			try
			{
                if (IdeaTitle.IsNullOrWhiteSpace())
                {
                    IdeaTitle = DefaultTitle;
                    idea.Title = DefaultTitle;
                }

                await _db.SaveItemAsync(idea);

                _existingIdea = idea;

				if (idea.Content == IdeaContent && idea.Title == IdeaTitle)
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

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query != null && query.TryGetValue("ExistingIdea", out var existingIdeaObject))
            {
                _existingIdea = existingIdeaObject as Idea;

                IdeaTitle = _existingIdea.Title;
                IdeaContent = _existingIdea.Content;
            }
        }
    }
}

