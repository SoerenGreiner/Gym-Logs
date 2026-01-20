using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gym_Logs.Model;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Gym_Logs.ViewModel;

public partial class WorkoutCalendarViewModel : ObservableObject
{
    [ObservableProperty]
    DateTime displayedMonth = new(DateTime.Now.Year, DateTime.Now.Month, 1);

    [ObservableProperty]
    string currentMonthName;

    public ObservableCollection<CalendarDay> Days { get; } = new();

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
        if (!day.IsEnabled)
            return;

        Debug.WriteLine($"Ausgewählt: {day.Date:d}");
    }

    private void BuildCalendar()
    {
        Days.Clear();
        CurrentMonthName = DisplayedMonth.ToString("MMMM yyyy");

        // 1️⃣ Headerzeile: Mo-So
        foreach (var wd in WeekDays)
        {
            Days.Add(new CalendarDay
            {
                IsHeader = true,
                DayName = wd
            });
        }

        // 2️⃣ Berechnung Startdatum (inklusive Tage des Vormonats)
        var firstOfMonth = DisplayedMonth;
        int offset = ((int)firstOfMonth.DayOfWeek + 6) % 7; // Montag = 0
        var startDate = firstOfMonth.AddDays(-offset);

        // 3️⃣ 6 Wochen (42 Tage) anzeigen
        for (int i = 0; i < 42; i++)
        {
            var date = startDate.AddDays(i);

            Days.Add(new CalendarDay
            {
                Date = date,
                IsCurrentMonth = date.Month == DisplayedMonth.Month
            });
        }
    }
}
