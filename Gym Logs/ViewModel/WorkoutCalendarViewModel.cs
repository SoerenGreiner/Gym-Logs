using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gym_Logs.Model.System;
using Gym_Logs.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace Gym_Logs.ViewModel;

public partial class WorkoutCalendarViewModel : ObservableObject
{
    [ObservableProperty]
    private DateTime displayedMonth = new(DateTime.Now.Year, DateTime.Now.Month, 1);

    [ObservableProperty]
    private string currentMonthName;

    public ObservableCollection<CalendarDay> Days { get; private set; } = new();

    private readonly string[] WeekDays = { "Mo", "Di", "Mi", "Do", "Fr", "Sa", "So" };

    public WorkoutCalendarViewModel()
    {
        BuildCalendar();
    }

    [RelayCommand]
    void NextMonth()
    {
        DisplayedMonth = DisplayedMonth.AddMonths(1);
        BuildCalendar();
    }

    [RelayCommand]
    void PreviousMonth()
    {
        DisplayedMonth = DisplayedMonth.AddMonths(-1);
        BuildCalendar();
    }

    [RelayCommand]
    void SelectDay(CalendarDay day)
    {
        if (!day.IsEnabled) return;

        Debug.WriteLine($"Ausgewählt: {day.Date:d}");
        // hier weitere Aktionen möglich
    }

    private void BuildCalendar()
    {
        var tempList = new List<CalendarDay>();

        CurrentMonthName = DisplayedMonth.ToString("MMMM yyyy");

        // 1️⃣ Header
        for (int col = 0; col < 7; col++)
        {
            tempList.Add(new CalendarDay
            {
                DayName = WeekDays[col],
                State = CalendarDayState.Header
            });
        }

        // 2️⃣ Monate (42 Zellen = 6 Wochen à 7 Tage)
        var firstOfMonth = new DateTime(DisplayedMonth.Year, DisplayedMonth.Month, 1);
        int offset = ((int)firstOfMonth.DayOfWeek + 6) % 7; // Montag = 0
        var startDate = firstOfMonth.AddDays(-offset);

        for (int i = 0; i < 42; i++)
        {
            var date = startDate.AddDays(i);

            CalendarDayState state =
                date.Month < DisplayedMonth.Month || date.Month > DisplayedMonth.Month ? CalendarDayState.Inactive :
                date.Date == DateTime.Today ? CalendarDayState.Today :
                CalendarDayState.Normal;

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
