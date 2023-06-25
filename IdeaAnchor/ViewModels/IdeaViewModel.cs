using System;
using System.Windows.Input;
using IdeaAnchor.Database;
using IdeaAnchor.Models;
using IdeaAnchor.Helper;
using CommunityToolkit.Mvvm.Input;

namespace IdeaAnchor.ViewModels
{
	public class IdeaViewModel : BindableObject, IQueryAttributable
	{
        private const string DefaultTitle = "Untitled Note";

        private readonly IdeaDatabase _db;

        public Idea ExistingIdea { get; set; }

        public string SaveButtonIcon => IdeaIsSaved ? "\uf104" : "\uf00c";

        public Color SaveButtonColor => IdeaIsSaved ? Colors.White : ThemeColors.Primary;

        public ICommand SaveOrGoBackCommand => new AsyncRelayCommand(SaveOrGoBack);

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
                OnPropertyChanged(nameof(SaveButtonIcon));
                OnPropertyChanged(nameof(SaveButtonColor));
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

        private async Task SaveOrGoBack()
        {
            if (IdeaIsSaved)
            {
                await GoBack();
            }
            else
            {
                await SaveIdea();
            }
        }

        private async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async Task SaveIdea()
		{
            IdeaTitle = IdeaTitle == null ? null : IdeaTitle.Trim();
            IdeaContent = IdeaContent == null ? null : IdeaContent.Trim();

            var idea = new Idea
            {
                Id = ExistingIdea?.Id,
                Title = IdeaTitle,
                Content = IdeaContent,
                LastUpdatedTime = TimeHelper.GetTimeStamp()
            };

            if (idea.Title.IsNullOrWhiteSpace() && idea.Content.IsNullOrWhiteSpace())
            {
                //don't save an empty idea

                if (idea.Id != null)
                {
                    //delete existing idea

                    await _db.DeleteIdeaAsync(idea);

                    ExistingIdea = null;
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

                await _db.SaveIdeaAsync(idea);

                ExistingIdea = idea;

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
                ExistingIdea = existingIdeaObject as Idea;

                IdeaTitle = ExistingIdea.Title;
                IdeaContent = ExistingIdea.Content;

                IdeaIsSaved = true;
            }
        }

        public async Task DeleteIdea()
        {
            if (ExistingIdea == null)
                return;

            await _db.DeleteIdeaAsync(ExistingIdea);

            await GoBack();
        }
    }
}

