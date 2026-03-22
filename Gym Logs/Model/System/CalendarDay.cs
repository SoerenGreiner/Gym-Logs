using System.ComponentModel;
using Gym_Logs.Enums;

namespace Gym_Logs.Model.System
{
    /// <summary>
    /// Represents a single day in the workout calendar, including its display state and appearance.
    /// Implements <see cref="INotifyPropertyChanged"/> for dynamic UI updates.
    /// </summary>
    public class CalendarDay : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the date of this calendar day.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the name of the day (e.g., "Mo", "Di") used for header rows.
        /// </summary>
        public string DayName { get; set; }

        public bool HasWorkout { get; set; }
        public bool IsCompleted { get; set; }

        private CalendarDayStateEnum _state;

        /// <summary>
        /// Gets or sets the current state of the day (e.g., Header, Today, Workout, Inactive).
        /// Updating the state automatically triggers property change notifications for dependent properties.
        /// </summary>
        public CalendarDayStateEnum State
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
                OnPropertyChanged(nameof(CalendarTextColor));
            }
        }

        /// <summary>
        /// Gets the display text for the day.
        /// Returns <see cref="DayName"/> if the day is a header, otherwise the day of the month as a zero-padded string.
        /// </summary>
        public string DayText => State == CalendarDayStateEnum.Header ? DayName : Date.Day.ToString("D2");

        /// <summary>
        /// Determines if the day is interactive (enabled).
        /// Header and Inactive days are not enabled.
        /// </summary>
        public bool IsEnabled => State != CalendarDayStateEnum.Header && State != CalendarDayStateEnum.Inactive;

        /// <summary>
        /// Gets the background brush for the day based on its state.
        /// Looks up the brush in the application resources and falls back to transparent if not found.
        /// </summary>
        public Brush BackgroundBrush
        {
            get
            {
                string key = State switch
                {
                    CalendarDayStateEnum.Header => "CalendarHeaderBrush",
                    CalendarDayStateEnum.Today => "CalendarTodayBrush",
                    CalendarDayStateEnum.Workout => "CalendarWorkoutBrush",
                    CalendarDayStateEnum.Body => "CalendarBodyBrush",
                    CalendarDayStateEnum.Inactive => "CalendarInactiveBrush",
                    _ => "CalendarNormalBrush"
                };

                if (Application.Current.Resources.TryGetValue(key, out var brush))
                    return brush as Brush;

                return Colors.Transparent;
            }
        }

        /// <summary>
        /// Gets the text color for the day based on its state.
        /// Looks up the color in the application resources and falls back to black if not found.
        /// </summary>
        public Color CalendarTextColor
        {
            get
            {
                string key = State switch
                {
                    CalendarDayStateEnum.Header => "CalendarHeaderTextColor",
                    CalendarDayStateEnum.Today => "CalendarTodayTextColor",
                    CalendarDayStateEnum.Workout => "CalendarWorkoutTextColor",
                    CalendarDayStateEnum.Body => "CalendarBodyTextColor",
                    CalendarDayStateEnum.Inactive => "CalendarInactiveTextColor",
                    _ => "CalendarNormalTextColor"
                };

                if (Application.Current.Resources.TryGetValue(key, out var color))
                    return (Color)color;

                return Colors.Black;
            }
        }

        /// <summary>
        /// Event triggered whenever a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event for the specified property.
        /// </summary>
        /// <param name="name">The name of the property that changed.</param>
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        /// <summary>
        /// Triggers UI updates for theme-dependent properties, such as background and text color.
        /// Should be called when the app theme changes.
        /// </summary>
        public void RaiseThemeChanged()
        {
            OnPropertyChanged(nameof(BackgroundBrush));
            OnPropertyChanged(nameof(CalendarTextColor));
        }
    }
}