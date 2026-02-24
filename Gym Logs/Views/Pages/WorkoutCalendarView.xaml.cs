using Gym_Logs.ViewModel;
using System;

namespace Gym_Logs.View;

public partial class WorkoutCalendarView : ContentPage
{
    public WorkoutCalendarView(WorkoutCalendarViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}