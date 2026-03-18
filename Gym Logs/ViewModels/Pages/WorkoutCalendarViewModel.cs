using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gym_Logs.Model.System;
using Gym_Logs.Enums;
using Gym_Logs.Views.Controls;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Gym_Logs.ViewModels.Pages
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
        /// Indicates whether the bottom sheet with available actions is visible.
        /// </summary>
        [ObservableProperty]
        private bool isBottomSheetVisible = false;

        /// <summary>
        /// Collection of options displayed inside the bottom sheet.
        /// Each option represents an action the user can perform for a selected day.
        /// </summary>
        [ObservableProperty]
        private ObservableCollection<BottomSheetOptionModel> bottomSheetOptions = new ObservableCollection<BottomSheetOptionModel>();

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
        /// Creates and displays a list of available actions for the selected day.
        /// </summary>
        /// <param name="day">The day that was tapped by the user.</param>
        [RelayCommand]
        void SelectDay(CalendarDay day)
        {
            if (!day.IsEnabled) return;

            bool hasWorkout = false;
            bool hasBody = false;
            bool isToday = day.Date.Date == DateTime.Today;

            BottomSheetOptions.Clear();

            BottomSheetOptions.Add(new BottomSheetOptionModel
            {
                Text = "Workout erstellen",
                Action = CalendarActionEnum.AddWorkout,
                Icon = "💪",
                ImagePath = "workout.png",
                IsVisible = true,
                Command = new Command(() => ExecuteAction(CalendarActionEnum.AddWorkout))
            });

            BottomSheetOptions.Add(new BottomSheetOptionModel
            {
                Text = "Workout bearbeiten",
                Action = CalendarActionEnum.EditWorkout,
                Icon = "✏",
                ImagePath = "edit_workout.png",
                IsVisible = hasWorkout,
                Command = new Command(() => ExecuteAction(CalendarActionEnum.EditWorkout))
            });

            BottomSheetOptions.Add(new BottomSheetOptionModel
            {
                Text = "Body erstellen",
                Action = CalendarActionEnum.AddBody,
                Icon = "📊",
                ImagePath = "body.png",
                IsVisible = true,
                Command = new Command(() => ExecuteAction(CalendarActionEnum.AddBody))
            });

            BottomSheetOptions.Add(new BottomSheetOptionModel
            {
                Text = "Body bearbeiten",
                Action = CalendarActionEnum.EditBody,
                Icon = "✏",
                ImagePath = "edit_body.png",
                IsVisible = hasBody,
                Command = new Command(() => ExecuteAction(CalendarActionEnum.EditBody))
            });

            BottomSheetOptions.Add(new BottomSheetOptionModel
            {
                Text = "Workout vorplanen",
                Action = CalendarActionEnum.PlanWorkout,
                Icon = "📅",
                ImagePath = "calendar.png",
                IsVisible = !isToday,
                Command = new Command(() => ExecuteAction(CalendarActionEnum.PlanWorkout))
            });

            if (BottomSheetOptions.Count > 0)
            {
                IsBottomSheetVisible = true;
            }
        }

        /// <summary>
        /// Closes the bottom sheet and hides all available actions.
        /// </summary>
        [RelayCommand]
        private void CloseBottomSheet()
        {
            IsBottomSheetVisible = false;
        }

        /// <summary>
        /// Executes the selected calendar action and navigates to the corresponding page.
        /// </summary>
        /// <param name="action">The action selected by the user.</param>
        private async void ExecuteAction(CalendarActionEnum action)
        {
            IsBottomSheetVisible = false;

            switch (action)
            {
                case CalendarActionEnum.AddWorkout:
                    //await Shell.Current.GoToAsync("WorkoutCreatePage");
                    break;

                case CalendarActionEnum.EditWorkout:
                    //await Shell.Current.GoToAsync("WorkoutEditPage");
                    break;

                case CalendarActionEnum.AddBody:
                    //await Shell.Current.GoToAsync("BodyCreatePage");
                    break;

                case CalendarActionEnum.EditBody:
                    //await Shell.Current.GoToAsync("BodyEditPage");
                    break;

                case CalendarActionEnum.PlanWorkout:
                    //await Shell.Current.GoToAsync("WorkoutPlanPage");
                    break;

                default:
                    Debug.WriteLine("Keine passende Action gefunden!");
                    break;
            }
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

            // Header row for weekdays
            for (int col = 0; col < 7; col++)
            {
                tempList.Add(new CalendarDay
                {
                    DayName = WeekDays[col],
                    State = CalendarDayStateEnum.Header
                });
            }

            // Calendar days (6 weeks × 7 days = 42 cells)
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