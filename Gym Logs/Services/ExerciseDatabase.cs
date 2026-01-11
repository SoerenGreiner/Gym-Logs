using SQLite;
using Gym_Logs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Logs.Services
{
    public class ExerciseDatabase : BaseDatabase<Exercise>
    {
        public ExerciseDatabase(SQLiteAsyncConnection db) : base(db) { }

        public Task<List<Exercise>> GetActiveAsync()
            => _db.Table<Exercise>()
                  .Where(e => !e.IsArchived)
                  .ToListAsync();
    }
}
