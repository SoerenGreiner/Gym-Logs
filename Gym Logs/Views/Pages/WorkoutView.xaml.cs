namespace Gym_Logs.Views.Pages;

public partial class WorkoutView : ContentPage
{
	public WorkoutView()
	{
		InitializeComponent();
        BindingContext = new WorkoutViewModel();
    }
}