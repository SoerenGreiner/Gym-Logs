using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gym_Logs.Model.Database;

namespace Gym_Logs.Services.Database
{
    public class BodyStatusDayDatabase : BaseDatabase<BodyStatusDay>
    {
        public BodyStatusDayDatabase(SQLiteAsyncConnection db) : base(db) { }

        public Task<BodyStatusDay?> GetByDateAsync(DateTime date)
            => _db.Table<BodyStatusDay>()
                  .Where(b => b.Date == date.Date)
                  .FirstOrDefaultAsync();
    }
}
