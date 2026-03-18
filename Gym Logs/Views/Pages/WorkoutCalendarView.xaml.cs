using Gym_Logs.ViewModels.Pages;
using System;

namespace Gym_Logs.Views.Pages;

public partial class WorkoutCalendarView : ContentPage
{
    public WorkoutCalendarView(WorkoutCalendarViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}