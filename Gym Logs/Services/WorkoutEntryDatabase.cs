using SQLite;
using Gym_Logs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Logs.Services
{
    public class WorkoutEntryDatabase : BaseDatabase<WorkoutEntry>
    {
        public WorkoutEntryDatabase(SQLiteAsyncConnection db) : base(db) { }

        public Task<List<WorkoutEntry>> GetByWorkoutDayAsync(int workoutDayId)
            => _db.Table<WorkoutEntry>()
                  .Where(e => e.WorkoutDayId == workoutDayId)
                  .OrderBy(e => e.Order)
                  .ToListAsync();
    }
}
