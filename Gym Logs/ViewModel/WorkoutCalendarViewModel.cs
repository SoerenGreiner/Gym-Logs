using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;

namespace Gym_Logs.ViewModel
{
    public partial class WorkoutCalendarViewModel : ObservableObject
    {
        [ObservableProperty]
        string currentMonthName = DateTime.Now.ToString("MMMM yyyy");

        public ObservableCollection<DayItem> Days { get; } = new ObservableCollection<DayItem>();

        public WorkoutCalendarViewModel()
        {
            var daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

            for (int i = 1; i <= daysInMonth; i++)
            {
                Days.Add(new DayItem { DayNumber = i });
            }
        }

        [RelayCommand]
        public void SelectDay(int day)
        {
            // Workout starten
            System.Diagnostics.Debug.WriteLine($"Workout für Tag {day} starten");
        }
    }

    public class DayItem
    {
        public int DayNumber { get; set; }
    }
}