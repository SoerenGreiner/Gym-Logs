using SQLite;

namespace Gym_Logs.Services.Database
{
    public class BaseDatabase<T> where T : new()
    {
        protected readonly SQLiteAsyncConnection _db; // Muss protected sein

        public BaseDatabase(SQLiteAsyncConnection db)
        {
            _db = db;
            _db.CreateTableAsync<T>().Wait(); // Tabelle erzeugen
        }

        public virtual Task<List<T>> GetAllAsync() => _db.Table<T>().ToListAsync();

        public virtual Task<T?> GetByIdAsync(int id) => _db.FindAsync<T>(id);

        public virtual Task<int> SaveAsync(T item)
        {
            var idProperty = typeof(T).GetProperty("ID");
            if (idProperty != null)
            {
                var idValue = (int?)idProperty.GetValue(item) ?? 0;
                return idValue == 0 ? _db.InsertAsync(item) : _db.UpdateAsync(item);
            }
            return _db.InsertAsync(item);
        }

        public virtual Task<int> DeleteAsync(T item) => _db.DeleteAsync(item);
    }
}