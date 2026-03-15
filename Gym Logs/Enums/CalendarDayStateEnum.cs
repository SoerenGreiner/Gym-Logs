namespace Gym_Logs.Enums
{
    /// <summary>
    /// Definiert den Zustand eines Tages in der Kalenderansicht.
    /// Jeder Wert steuert Darstellung und Verhalten des Tages in der UI.
    /// </summary>
    public enum CalendarDayStateEnum
    {
        /// <summary>Header-Zelle, zeigt den Wochentag (z. B. Mo, Di, Mi).</summary>
        Header,

        /// <summary>Heutiger Tag, hervorgehoben im Kalender.</summary>
        Today,

        /// <summary>Tag, an dem ein Training geplant oder durchgeführt wurde.</summary>
        Workout,

        /// <summary>Tag, der dem Körpertraining zugeordnet ist (z. B. spezielle Markierung).</summary>
        Body,

        /// <summary>Normaler, aktiver Kalendertag ohne besondere Markierung.</summary>
        Normal,

        /// <summary>Inaktiver Tag (außerhalb des aktuellen Monats).</summary>
        Inactive
    }
}