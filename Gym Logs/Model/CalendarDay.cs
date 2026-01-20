namespace Gym_Logs.Model
{
    public class CalendarDay
    {
        public DateTime Date { get; set; }
        public bool IsCurrentMonth { get; set; }
        public bool IsHeader { get; set; }
        public string DayName { get; set; }

        public string DayText => IsHeader ? DayName : Date.Day.ToString("D2");

        public bool IsEnabled => !IsHeader && IsCurrentMonth;

        public Brush BackgroundBrush
        {
            get
            {
                if (IsHeader)
                {
                    return new RadialGradientBrush
                    {
                        Center = new Point(0.0, 0.0),   // Mitte des Buttons
                        Radius = 1,
                        GradientStops = new GradientStopCollection
                {
                    new GradientStop(Color.FromArgb("#00BFFF"), 0f),
                    new GradientStop(Color.FromArgb("#00688B"), 1f)
                }
                    };
                }

                if (Date.Date == DateTime.Today)
                {
                    return new RadialGradientBrush
                    {
                        Center = new Point(0.0, 0.0),   // Mitte des Buttons
                        Radius = 1.5,
                        GradientStops = new GradientStopCollection
                {
                    new GradientStop(Color.FromArgb("#FFA54F"), 0f),
                    new GradientStop(Color.FromArgb("#8B5A2B"), 1f)
                }
                    };
                }

                if (IsCurrentMonth)
                {
                    return new RadialGradientBrush
                    {
                        Center = new Point(0.0, 0.0),
                        Radius = 1,
                        GradientStops = new GradientStopCollection
                {
                    new GradientStop(Color.FromArgb("#F2F2F2"), 0f),
                    new GradientStop(Color.FromArgb("#BFBFBF"), 1f)
                }
                    };
                }

                return new RadialGradientBrush
                {
                    Center = new Point(0.0, 0.0),
                    Radius = 1,
                    GradientStops = new GradientStopCollection
            {
                new GradientStop(Color.FromArgb("#B3B3B3"), 0f),
                new GradientStop(Color.FromArgb("#737373"), 1f)
            }
                };
            }
        }
    }
}
