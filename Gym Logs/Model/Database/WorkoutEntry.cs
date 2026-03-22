using SQLite;
using System;

namespace Gym_Logs.Model.Database
{
    /// <summary>
    /// Repräsentiert einen Eintrag einer Übung an einem bestimmten WorkoutDay.
    /// Speichert nur die tagesbezogenen Werte.
    /// Statische Übungsinformationen gehören in die Exercise-Klasse.
    /// </summary>
    public class WorkoutEntry
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int WorkoutDayId { get; set; }
        public int ExerciseId { get; set; }
        public double? Weight { get; set; }
        public int? Sets { get; set; }
        public int? Reps { get; set; }
        public int? DurationMinutes { get; set; }
        public double? DistanceKm { get; set; }
        public string? Notes { get; set; } // <-- hier
        public int Order { get; set; }
    }
}