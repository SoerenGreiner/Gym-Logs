using SQLite;
using Gym_Logs.Model.Database;

namespace Gym_Logs.Services.Database
{
    public class UserDatabase : BaseDatabase<User>
    {
        public UserDatabase(SQLiteAsyncConnection db) : base(db) { }

        public Task<User?> GetByEmailAsync(string email)
            => _db.Table<User>()
                  .Where(u => u.Email == email)
                  .FirstOrDefaultAsync();
    }
}