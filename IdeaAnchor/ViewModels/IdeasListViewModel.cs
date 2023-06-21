using System;
using IdeaAnchor.Database;
using IdeaAnchor.Models;

namespace IdeaAnchor.ViewModels
{
    public class IdeasListViewModel : BindableObject
    {
        private List<Idea> _ideas;
        public List<Idea> Ideas
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
                Ideas = await _db.GetIdeasAsync();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

