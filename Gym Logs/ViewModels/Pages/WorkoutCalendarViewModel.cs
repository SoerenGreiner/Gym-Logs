using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gym_Logs.Model.System;
using Gym_Logs.Enums;
using Gym_Logs.Views.Pages;
using Gym_Logs.Services.Database;
using Gym_Logs.Model.Database;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Gym_Logs.ViewModels.Pages
{
    /// <summary>
    /// ViewModel for the Workout Calendar page.
    /// 
    /// Responsibilities:
    /// - Display a monthly calendar view
    /// - Load workout data from the database for the current user
    /// - Map database entries to UI-friendly calendar days
    /// - Handle user interaction (day selection, navigation)
    /// - Provide actions via BottomSheet (Create, Edit, Plan)
    /// 
    /// Architecture Notes:
    /// - Uses SQLite via WorkoutDayDatabase
    /// - Uses SecureStorage to retrieve the current user
    /// - Keeps UI (CalendarDay) and DB model (WorkoutDay) separated
    /// </summary>
    public partial class WorkoutCalendarViewModel : ObservableObject
    {
        /// <summary>
        /// Database access for WorkoutDay entities.
        /// </summary>
        private readonly WorkoutDayDatabase _workoutDayDb;

        /// <summary>
        /// Currently logged-in user's ID (loaded from SecureStorage).
        /// </summary>
        private int _currentUserId;

        /// <summary>
        /// The month currently displayed in the calendar.
        /// Always normalized to the first day of the month.
        /// </summary>
        [ObservableProperty]
        private DateTime displayedMonth = new(DateTime.Now.Year, DateTime.Now.Month, 1);

        /// <summary>
        /// Formatted name of the current month (e.g., "March 2026").
        /// </summary>
        [ObservableProperty]
        private string currentMonthName;

        /// <summary>
        /// Controls visibility of the BottomSheet overlay.
        /// </summary>
        [ObservableProperty]
        private bool isBottomSheetVisible = false;

        /// <summary>
        /// Collection of actions shown in the BottomSheet.
        /// Each entry represents a possible action for a selected day.
        /// </summary>
        [ObservableProperty]
        private ObservableCollection<BottomSheetOptionModel> bottomSheetOptions = new();

        /// <summary>
        /// Collection of all calendar entries (including header + days).
        /// Bound to the UI CollectionView.
        /// </summary>
        public ObservableCollection<CalendarDay> Days { get; private set; } = new();

        /// <summary>
        /// Weekday labels (Monday-first).
        /// </summary>
        private readonly string[] WeekDays = { "Mo", "Di", "Mi", "Do", "Fr", "Sa", "So" };

        /// <summary>
        /// Constructor initializes database and loads initial data.
        /// </summary>
        public WorkoutCalendarViewModel()
        {
            _workoutDayDb = App.WorkoutDayDb;

            Init();
        }

        /// <summary>
        /// Initializes the ViewModel by loading the current user ID
        /// and fetching calendar data.
        /// </summary>
        private async void Init()
        {
            try
            {
                var userIdStr = await SecureStorage.GetAsync("CurrentUserId");

                if (string.IsNullOrWhiteSpace(userIdStr))
                {
                    Debug.WriteLine("❌ Kein User gefunden");
                    return;
                }

                _currentUserId = int.Parse(userIdStr);

                await LoadData();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ Init Fehler: {ex.Message}");
            }
        }

        // ================= Commands =================

        /// <summary>
        /// Navigates to the next month and reloads calendar data.
        /// </summary>
        [RelayCommand]
        async Task NextMonth()
        {
            DisplayedMonth = DisplayedMonth.AddMonths(1);
            await LoadData();
        }

        /// <summary>
        /// Navigates to the previous month and reloads calendar data.
        /// </summary>
        [RelayCommand]
        async Task PreviousMonth()
        {
            DisplayedMonth = DisplayedMonth.AddMonths(-1);
            await LoadData();
        }

        /// <summary>
        /// Handles user tapping on a calendar day.
        /// Ensures a WorkoutDay exists and opens action menu.
        /// </summary>
        /// <param name="day">The selected calendar day.</param>
        [RelayCommand]
        async Task SelectDay(CalendarDay day)
        {
            if (!day.IsEnabled) return;

            // Ensure a WorkoutDay exists in DB
            var existing = await _workoutDayDb.GetByDateAsync(_currentUserId, day.Date);

            if (existing == null)
            {
                existing = new WorkoutDay
                {
                    UserId = _currentUserId,
                    Date = day.Date,
                    HasStrength = false,
                    HasCardio = false,
                    IsCompleted = false
                };

                await _workoutDayDb.SaveAsync(existing);
            }

            // Determine state for UI logic
            bool hasWorkout = existing.HasStrength || existing.HasCardio;
            bool isToday = day.Date.Date == DateTime.Today;

            BottomSheetOptions.Clear();

            // Create Workout
            BottomSheetOptions.Add(new BottomSheetOptionModel
            {
                Text = "Workout erstellen",
                Action = CalendarActionEnum.AddWorkout,
                Icon = "💪",
                ImagePath = "workout.png",
                IsVisible = true,
                Command = new Command(() => ExecuteAction(CalendarActionEnum.AddWorkout, day.Date))
            });

            // Edit Workout (only if exists)
            BottomSheetOptions.Add(new BottomSheetOptionModel
            {
                Text = "Workout bearbeiten",
                Action = CalendarActionEnum.EditWorkout,
                Icon = "✏",
                ImagePath = "edit_workout.png",
                IsVisible = hasWorkout,
                Command = new Command(() => ExecuteAction(CalendarActionEnum.EditWorkout, day.Date))
            });

            // Plan Workout (only for future days)
            BottomSheetOptions.Add(new BottomSheetOptionModel
            {
                Text = "Workout vorplanen",
                Action = CalendarActionEnum.PlanWorkout,
                Icon = "📅",
                ImagePath = "calendar.png",
                IsVisible = !isToday,
                Command = new Command(() => ExecuteAction(CalendarActionEnum.PlanWorkout, day.Date))
            });

            if (BottomSheetOptions.Count > 0)
                IsBottomSheetVisible = true;
        }

        /// <summary>
        /// Closes the BottomSheet.
        /// </summary>
        [RelayCommand]
        private void CloseBottomSheet()
        {
            IsBottomSheetVisible = false;
        }

        /// <summary>
        /// Executes the selected calendar action and navigates to the WorkoutView.
        /// Passes date and action mode via query parameters.
        /// </summary>
        private async void ExecuteAction(CalendarActionEnum action, DateTime date)
        {
            IsBottomSheetVisible = false;

            string route = $"{nameof(WorkoutView)}?date={date:yyyy-MM-dd}&mode={action}";
            await Shell.Current.GoToAsync(route);
        }

        // ================= Data Handling =================

        /// <summary>
        /// Loads workout data for the currently displayed month from the database
        /// and rebuilds the calendar UI.
        /// </summary>
        private async Task LoadData()
        {
            var dbDays = await _workoutDayDb.GetByMonthAsync(
                _currentUserId,
                DisplayedMonth.Year,
                DisplayedMonth.Month
            );

            BuildCalendar(dbDays);
        }

        // ================= Calendar Construction =================

        /// <summary>
        /// Builds the calendar grid (7 columns × 6 rows + header).
        /// Maps database entries to UI elements.
        /// </summary>
        /// <param name="dbDays">WorkoutDay entries for the current month.</param>
        private void BuildCalendar(List<WorkoutDay> dbDays)
        {
            var tempList = new List<CalendarDay>();

            // Set month label
            CurrentMonthName = DisplayedMonth.ToString("MMMM yyyy");

            // Header row (weekdays)
            for (int col = 0; col < 7; col++)
            {
                tempList.Add(new CalendarDay
                {
                    DayName = WeekDays[col],
                    State = CalendarDayStateEnum.Header
                });
            }

            // Calculate first visible date in grid
            var firstOfMonth = new DateTime(DisplayedMonth.Year, DisplayedMonth.Month, 1);
            int offset = ((int)firstOfMonth.DayOfWeek + 6) % 7;
            var startDate = firstOfMonth.AddDays(-offset);

            // Fill 6 weeks (42 days)
            for (int i = 0; i < 42; i++)
            {
                var date = startDate.AddDays(i);

                var workoutDay = dbDays.FirstOrDefault(d => d.Date.Date == date.Date);

                CalendarDayStateEnum state =
                    date.Month != DisplayedMonth.Month ? CalendarDayStateEnum.Inactive :
                    date.Date == DateTime.Today ? CalendarDayStateEnum.Today :
                    CalendarDayStateEnum.Normal;

                tempList.Add(new CalendarDay
                {
                    Date = date,
                    State = state,

                    // UI flags derived from DB
                    HasWorkout = workoutDay != null,
                    IsCompleted = workoutDay?.IsCompleted ?? false
                });
            }

            Days = new ObservableCollection<CalendarDay>(tempList);
            OnPropertyChanged(nameof(Days));
        }
    }
}