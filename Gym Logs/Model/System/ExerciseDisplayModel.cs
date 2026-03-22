using Gym_Logs.Model.Database;

namespace Gym_Logs.Model.System
{
    /// <summary>
    /// Kombiniert Exercise-Daten und optional Tageswerte aus WorkoutEntry.
    /// Kann sowohl in der Library/Exercise-Seite als auch im Workout verwendet werden.
    /// </summary>
    public class ExerciseDisplayModel
    {
        public Exercise Exercise { get; set; }
        public WorkoutEntry? Entry { get; set; }

        public string Name => Exercise?.Name ?? "Unbekannte Übung";
        public bool IsStrength => Exercise?.IsStrength ?? false;
        public bool IsCardio => Exercise?.IsCardio ?? false;
        public string? ImagePath => Exercise?.ImagePath;

        // Tageswerte
        public double? Weight => Entry?.Weight;
        public int? Sets => Entry?.Sets;
        public int? Reps => Entry?.Reps;
        public int? DurationMinutes => Entry?.DurationMinutes;
        public double? DistanceKm => Entry?.DistanceKm;

        // Notizen für den Tag
        public string? Notes => Entry?.Notes;
    }
}
