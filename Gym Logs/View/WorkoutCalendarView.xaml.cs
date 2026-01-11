using Gym_Logs.ViewModel;

namespace Gym_Logs.View;

public partial class WorkoutCalendarView : ContentPage
{
	public WorkoutCalendarView(WorkoutCalendarViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;

        PopulateCalendar(viewModel);
    }

    private void PopulateCalendar(WorkoutCalendarViewModel viewModel)
    {
        int row = 1; // Zeile 0 = Wochentage
        int col = 0;

        foreach (var day in viewModel.Days)
        {
            var btn = new Button
            {
                Text = day.DayNumber.ToString(),
                BackgroundColor = Colors.LightBlue,
                CornerRadius = 8
            };

            btn.Clicked += (s, e) => viewModel.SelectDayCommand.Execute(day.DayNumber);

            CalendarGrid.Add(btn, col, row);

            col++;
            if (col > 6)
            {
                col = 0;
                row++;
            }
        }
    }
}