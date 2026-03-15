namespace Gym_Logs.Enums
{
    /// <summary>
    /// Definiert die Routen für Flyout-Menüeinträge in der App.
    /// Jeder Wert entspricht einer Seite, zu der im Shell-Navigationssystem navigiert werden kann.
    /// </summary>
    public enum FlyoutRouteEnum
    {
        /// <summary>Registrierungsseite für neue Benutzer.</summary>
        RegistrationView,

        /// <summary>Kalenderansicht für Trainingsplanung.</summary>
        WorkoutCalendarView,

        /// <summary>Persönliche Übersicht des Benutzers.</summary>
        PersonalView,

        /// <summary>Statistik- und Auswertungsseite.</summary>
        StatisticView,

        /// <summary>Einstellungen der App.</summary>
        SettingsView
    }
}
