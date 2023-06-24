using System;
using IdeaAnchor.Database;
using IdeaAnchor.Models;
using IdeaAnchor.ItemViewModels;

namespace IdeaAnchor.ViewModels
{
    public class IdeasListViewModel : BindableObject
    {
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
    }
}

