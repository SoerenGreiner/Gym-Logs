using SQLite;

namespace Gym_Logs.Model.Database
{
    public class WorkoutDay
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime Date { get; set; }

        public bool HasStrength { get; set; }
        public bool HasCardio { get; set; }
        public bool IsCompleted { get; set; }
    }
}