using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gym_Logs.Model.System;
using Gym_Logs.Enums;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Gym_Logs.ViewModel
{
    /// <summary>
    /// ViewModel for the Workout Calendar page.
    /// Manages the displayed month, calendar days, and user interactions with the calendar.
    /// </summary>
    public partial class WorkoutCalendarViewModel : ObservableObject
    {
        /// <summary>
        /// The month currently displayed in the calendar.
        /// </summary>
        [ObservableProperty]
        private DateTime displayedMonth = new(DateTime.Now.Year, DateTime.Now.Month, 1);

        /// <summary>
        /// The name of the current month, formatted for display (e.g., "March 2026").
        /// </summary>
        [ObservableProperty]
        private string currentMonthName;

        /// <summary>
        /// Collection of calendar day entries displayed in the UI.
        /// Includes headers, inactive days, today, and normal days.
        /// </summary>
        public ObservableCollection<CalendarDay> Days { get; private set; } = new();

        /// <summary>
        /// Weekday names used for the calendar header (Monday-first).
        /// </summary>
        private readonly string[] WeekDays = { "Mo", "Di", "Mi", "Do", "Fr", "Sa", "So" };

        /// <summary>
        /// Initializes a new instance of <see cref="WorkoutCalendarViewModel"/>
        /// and builds the initial calendar for the current month.
        /// </summary>
        public WorkoutCalendarViewModel()
        {
            BuildCalendar();
        }

        // ================= Commands =================

        /// <summary>
        /// Moves the calendar to the next month and rebuilds the calendar.
        /// </summary>
        [RelayCommand]
        void NextMonth()
        {
            DisplayedMonth = DisplayedMonth.AddMonths(1);
            BuildCalendar();
        }

        /// <summary>
        /// Moves the calendar to the previous month and rebuilds the calendar.
        /// </summary>
        [RelayCommand]
        void PreviousMonth()
        {
            DisplayedMonth = DisplayedMonth.AddMonths(-1);
            BuildCalendar();
        }

        /// <summary>
        /// Handles the selection of a calendar day.
        /// </summary>
        /// <param name="day">The day that was tapped by the user.</param>
        [RelayCommand]
        void SelectDay(CalendarDay day)
        {
            if (!day.IsEnabled) return;

            Debug.WriteLine($"Selected: {day.Date:d}");
            // Additional actions for the selected day can be added here
        }

        // ================= Calendar Construction =================

        /// <summary>
        /// Builds the calendar days collection for the currently displayed month.
        /// Includes a header row with weekdays and 6 weeks of calendar cells (42 total).
        /// Marks inactive days, today, and normal days.
        /// </summary>
        private void BuildCalendar()
        {
            var tempList = new List<CalendarDay>();

            // Set the month label
            CurrentMonthName = DisplayedMonth.ToString("MMMM yyyy");

            // 1️⃣ Header row for weekdays
            for (int col = 0; col < 7; col++)
            {
                tempList.Add(new CalendarDay
                {
                    DayName = WeekDays[col],
                    State = CalendarDayStateEnum.Header
                });
            }

            // 2️⃣ Calendar days (6 weeks × 7 days = 42 cells)
            var firstOfMonth = new DateTime(DisplayedMonth.Year, DisplayedMonth.Month, 1);
            int offset = ((int)firstOfMonth.DayOfWeek + 6) % 7; // Monday = 0
            var startDate = firstOfMonth.AddDays(-offset);

            for (int i = 0; i < 42; i++)
            {
                var date = startDate.AddDays(i);

                CalendarDayStateEnum state =
                    date.Month < DisplayedMonth.Month || date.Month > DisplayedMonth.Month ? CalendarDayStateEnum.Inactive :
                    date.Date == DateTime.Today ? CalendarDayStateEnum.Today :
                    CalendarDayStateEnum.Normal;

                tempList.Add(new CalendarDay
                {
                    Date = date,
                    State = state
                });
            }

            Days = new ObservableCollection<CalendarDay>(tempList);
            OnPropertyChanged(nameof(Days));
        }
    }
}