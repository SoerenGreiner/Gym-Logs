using SQLite;
using Gym_Logs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Logs.Services
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
