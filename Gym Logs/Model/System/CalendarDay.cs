using System;
using System.ComponentModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace Gym_Logs.Model.System
{
    public enum CalendarDayState
    {
        Header,
        Today,
        Workout,
        Body,
        Normal,
        Inactive
    }

    public class CalendarDay : INotifyPropertyChanged
    {
        public DateTime Date { get; set; }
        public string DayName { get; set; }

        private CalendarDayState _state;
        public CalendarDayState State
        {
            get => _state;
            set
            {
                if (_state == value) return;
                _state = value;
                OnPropertyChanged(nameof(State));
                OnPropertyChanged(nameof(DayText));
                OnPropertyChanged(nameof(IsEnabled));
                OnPropertyChanged(nameof(BackgroundBrush));
            }
        }

        public string DayText =>
            State == CalendarDayState.Header ? DayName : Date.Day.ToString("D2");

        public bool IsEnabled => State != CalendarDayState.Header && State != CalendarDayState.Inactive;

        public Brush BackgroundBrush
        {
            get
            {
                if (Application.Current?.Resources == null)
                    return Colors.Transparent;

                string key = State switch
                {
                    CalendarDayState.Header => "CalendarHeaderBrush",
                    CalendarDayState.Today => "CalendarTodayBrush",
                    CalendarDayState.Workout => "CalendarWorkoutBrush",
                    CalendarDayState.Body => "CalendarBodyBrush",
                    CalendarDayState.Inactive => "CalendarInactiveBrush",
                    _ => "CalendarNormalBrush"
                };

                if (Application.Current.Resources.TryGetValue(key, out var brush))
                    return brush as Brush;

                return Colors.Transparent;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public void RaiseThemeChanged()
        {
            OnPropertyChanged(nameof(BackgroundBrush));
        }
    }
}
