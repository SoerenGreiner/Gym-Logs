using SQLite;
using Gym_Logs.Model.Database;

namespace Gym_Logs.Services.Database
{
    public class WorkoutDayDatabase : BaseDatabase<WorkoutDay>
    {
        public WorkoutDayDatabase(SQLiteAsyncConnection db) : base(db) { }

        /// <summary>
        /// Returns a WorkoutDay for a specific date and user.
        /// Uses a safe date range instead of direct equality to avoid SQLite issues.
        /// </summary>
        public Task<WorkoutDay?> GetByDateAsync(int userId, DateTime date)
        {
            var start = date.Date;
            var end = start.AddDays(1);

            return _db.Table<WorkoutDay>()
                .Where(d => d.UserId == userId &&
                            d.Date >= start &&
                            d.Date < end)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Returns all WorkoutDays for a specific month and user.
        /// Uses a date range instead of Year/Month (SQLite limitation).
        /// </summary>
        public Task<List<WorkoutDay>> GetByMonthAsync(int userId, int year, int month)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1);

            return _db.Table<WorkoutDay>()
                .Where(d => d.UserId == userId &&
                            d.Date >= startDate &&
                            d.Date < endDate)
                .ToListAsync();
        }
    }
}