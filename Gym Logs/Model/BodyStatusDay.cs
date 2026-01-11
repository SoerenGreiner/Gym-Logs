using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Logs.Model
{
    public class BodyStatusDay
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        // Front- & Back-Body Image (User zeichnet ein)
        public string? FrontImagePath { get; set; }
        public string? BackImagePath { get; set; }
    }
}
