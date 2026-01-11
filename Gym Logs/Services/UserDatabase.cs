using SQLite;
using Gym_Logs.Model;
using System.Linq;
using System.Threading.Tasks;

namespace Gym_Logs.Services
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
