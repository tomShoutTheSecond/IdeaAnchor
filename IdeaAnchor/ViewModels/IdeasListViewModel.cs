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
                Ideas = ideaModels.Select(i => new IdeaItemViewModel { Idea = i }).ToList();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

