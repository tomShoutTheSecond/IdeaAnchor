using System;
using IdeaAnchor.Models;
using SQLite;

namespace IdeaAnchor.Database
{
    public class IdeaDatabase
    {
        private SQLiteAsyncConnection _database;

        private async Task Init()
        {
            if (_database is not null)
                return;

            _database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await _database.CreateTableAsync<Idea>();
        }

        public async Task<List<Idea>> GetIdeasAsync()
        {
            await Init();
            return await _database.Table<Idea>().ToListAsync();
        }

        public async Task<Idea> GetIdeaAsync(string id)
        {
            await Init();
            return await _database.Table<Idea>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveItemAsync(Idea item)
        {
            await Init();
            if (item.Id == null)
            {
                //new idea
                item.Id = Guid.NewGuid().ToString();
                return await _database.InsertAsync(item);
            }

            //existing idea
            return await _database.UpdateAsync(item);
        }

        public async Task<int> DeleteItemAsync(Idea item)
        {
            await Init();
            return await _database.DeleteAsync(item);
        }
    }
}

