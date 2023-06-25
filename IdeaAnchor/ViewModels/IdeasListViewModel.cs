using System;
using IdeaAnchor.Database;
using IdeaAnchor.Models;
using IdeaAnchor.ItemViewModels;
using System.Windows.Input;

namespace IdeaAnchor.ViewModels
{
    public class IdeasListViewModel : BindableObject
    {
        public ICommand ToggleSearchVisibilityCommand => new Command(ToggleSearchVisibility);

        private bool _isSearchVisible;
        public bool IsSearchVisible
        {
            get => _isSearchVisible;
            set
            {
                _isSearchVisible = value;
                OnPropertyChanged(nameof(IsSearchVisible));
            }
        }

        private List<IdeaItemViewModel> _ideas;
        public List<IdeaItemViewModel> Ideas
        {
            get => _ideas;
            set
            {
                _ideas = value;
                OnPropertyChanged(nameof(Ideas));
            }
        }

        private readonly IdeaDatabase _db;

        public IdeasListViewModel(IdeaDatabase db)
        {
            _db = db;

            _ = LoadIdeas();
        }

        public async Task LoadIdeas()
        {
            try
            {
                var ideaModels = await _db.GetIdeasAsync();

                Ideas = ideaModels
                    .Select(i => new IdeaItemViewModel { Idea = i })
                    .Reverse() //reverse is called as an optimisation, as it will reduce the length of the time it takes to put the items in the correct order
                    .OrderByDescending(i => i.LastUpdatedDateTime)
                    .ToList();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void ToggleSearchVisibility()
        {
            IsSearchVisible = !IsSearchVisible;
        }
    }
}

