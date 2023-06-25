using System;
using IdeaAnchor.Database;
using IdeaAnchor.Models;
using IdeaAnchor.ItemViewModels;
using System.Windows.Input;

namespace IdeaAnchor.ViewModels
{
    public class IdeasListViewModel : BindableObject
    {
        public string SearchButtonIcon => IsSearchVisible ? "\uf00d" : "\uf002";

        private bool _isSearchVisible;
        public bool IsSearchVisible
        {
            get => _isSearchVisible;
            set
            {
                _isSearchVisible = value;
                OnPropertyChanged(nameof(IsSearchVisible));
                OnPropertyChanged(nameof(SearchButtonIcon));

                if (_isSearchVisible == false)
                {
                    SearchText = "";
                    _searchTimer.Stop();
                    VisibleIdeas = _allIdeas;
                }
            }
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));

                //restart timer
                _searchTimer.Stop();
                _searchTimer.Start();
            }
        }

        private List<IdeaItemViewModel> _visibleIdeas;
        public List<IdeaItemViewModel> VisibleIdeas
        {
            get => _visibleIdeas;
            set
            {
                _visibleIdeas = value;
                OnPropertyChanged(nameof(VisibleIdeas));
            }
        }

        private readonly IdeaDatabase _db;
        private readonly System.Timers.Timer _searchTimer;
        private List<IdeaItemViewModel> _allIdeas;

        public IdeasListViewModel(IdeaDatabase db)
        {
            _db = db;

            _searchTimer = new System.Timers.Timer(250);
            _searchTimer.Elapsed += (s, e) => SearchIdeas();

            _ = LoadIdeas();
        }

        public async Task LoadIdeas()
        {
            try
            {
                var ideaModels = await _db.GetIdeasAsync();

                _allIdeas = ideaModels
                    .Select(i => new IdeaItemViewModel { Idea = i })
                    .Reverse() //reverse is called as an optimisation, as it will reduce the length of the time it takes to put the items in the correct order
                    .OrderByDescending(i => i.LastUpdatedDateTime)
                    .ToList();

                VisibleIdeas = _allIdeas;
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

        private void SearchIdeas()
        {
            var searchQuery = SearchText.ToLowerInvariant();

            VisibleIdeas = _allIdeas.Where(i => i.Idea.Title.ToLowerInvariant().IndexOf(searchQuery) > -1 || i.Idea.Content.ToLowerInvariant().IndexOf(searchQuery) > -1).ToList();
        }
    }
}

