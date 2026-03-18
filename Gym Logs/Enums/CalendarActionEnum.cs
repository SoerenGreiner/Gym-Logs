namespace Gym_Logs.Enums
{
    /// <summary>
    /// Defines the available actions that can be triggered from the calendar bottom sheet.
    /// </summary>
    public enum CalendarActionEnum
    {
        /// <summary>
        /// Creates a new workout entry for the selected day.
        /// </summary>
        AddWorkout,

        /// <summary>
        /// Edits an existing workout entry for the selected day.
        /// </summary>
        EditWorkout,

        /// <summary>
        /// Creates a new body measurement entry for the selected day.
        /// </summary>
        AddBody,

        /// <summary>
        /// Edits an existing body measurement entry for the selected day.
        /// </summary>
        EditBody,

        /// <summary>
        /// Plans a workout for a future day.
        /// </summary>
        PlanWorkout
    }
}