using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectHB
{
    public class Databases
    {
        readonly SQLiteAsyncConnection _database;

        public Databases(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Data.ContactPreset>().Wait();
            _database.CreateTableAsync<Data.MessagePreset>().Wait();
        }

        public Task<List<Data.ContactPreset>> GetAsyncContact()
        {
            return _database.Table<Data.ContactPreset>().ToListAsync();
        }

        public Task<List<Data.MessagePreset>> GetAsyncMessage()
        {
            return _database.Table<Data.MessagePreset>().ToListAsync();
        }

        public Task<int> SaveAsyncContact(Data.ContactPreset contact)
        {
            return _database.InsertAsync(contact);
        }

        public Task<int> SaveAsyncMessage(Data.MessagePreset message)
        {
            return _database.InsertAsync(message);
        }

        public Task<int> DeleteAsyncContact(Data.ContactPreset contact)
        {
            return _database.DeleteAsync(contact);
        }

        public Task<int> DeleteAsyncMessage(Data.MessagePreset message)
        {
            return _database.DeleteAsync(message);
        }
    }
}
