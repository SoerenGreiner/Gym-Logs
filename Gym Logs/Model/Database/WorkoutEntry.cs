using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Logs.Model.Database
{
    public class WorkoutEntry
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        // Beziehung
        public int WorkoutDayId { get; set; }
        public int ExerciseId { get; set; }

        // Krafttraining
        public double? Weight { get; set; }
        public int? Sets { get; set; }
        public int? Reps { get; set; }

        // Cardio
        public int? DurationMinutes { get; set; }
        public double? DistanceKm { get; set; }

        // Reihenfolge im Workout
        public int Order { get; set; }
    }
}
