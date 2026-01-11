using SQLite;
using Gym_Logs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Logs.Services
{
    public class WorkoutDayDatabase : BaseDatabase<WorkoutDay>
    {
        public WorkoutDayDatabase(SQLiteAsyncConnection db) : base(db) { }

        public Task<WorkoutDay?> GetByDateAsync(DateTime date)
            => _db.Table<WorkoutDay>()
                  .Where(d => d.Date == date.Date)
                  .FirstOrDefaultAsync();

        public Task<List<WorkoutDay>> GetByMonthAsync(int year, int month)
            => _db.Table<WorkoutDay>()
                  .Where(d => d.Date.Year == year && d.Date.Month == month)
                  .ToListAsync();
    }
}
