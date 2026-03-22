using Gym_Logs.Services.System;
using Gym_Logs.Views.Pages;
using SQLite;
using Gym_Logs.Services.Database;
using Gym_Logs.Model.Database;

namespace Gym_Logs
{
    public partial class App : Application
    {
        public static ThemeService ThemeService { get; private set; }

        /// <summary>
        /// Globale SQLite-Verbindung (Single Source of Truth)
        /// </summary>
        public static SQLiteAsyncConnection DbConnection { get; private set; }

        /// <summary>
        /// Datenbank für User (Login / Registrierung)
        /// </summary>
        public static UserDatabase UserDb { get; private set; }

        /// <summary>
        /// Datenbank für Workout-Tage (Kalender)
        /// </summary>
        public static WorkoutDayDatabase WorkoutDayDb { get; private set; }

        /// <summary>
        /// Datenbank für Workout-Eintrag (Workout)
        /// </summary>
        public static WorkoutEntryDatabase WorkoutEntryDb { get; private set; }

        /// <summary>
        /// Datenbank für Exercise-Eintrag (Exercise)
        /// </summary>
        public static ExerciseDatabase ExerciseDb { get; private set; }

        public App(AppShellView shell)
        {
            InitializeComponent();

            AdaptiveUIManager.Instance.Initialize();

            ThemeService = new ThemeService();
            ThemeService.LoadSavedTheme();

            // ================= DATABASE INITIALIZATION =================

            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "gymlogs.db3");

            // 1️⃣ Verbindung erstellen
            DbConnection = new SQLiteAsyncConnection(dbPath);

            // 2️⃣ Datenbanken initialisieren (Tabellen werden automatisch erstellt)
            UserDb = new UserDatabase(DbConnection);
            WorkoutDayDb = new WorkoutDayDatabase(DbConnection);
            WorkoutEntryDb = new WorkoutEntryDatabase(DbConnection);
            ExerciseDb = new ExerciseDatabase(DbConnection);

            // ================= APP START =================

            MainPage = shell;
        }
    }
}