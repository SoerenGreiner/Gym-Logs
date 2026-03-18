using Gym_Logs.ViewModels.Pages;

namespace Gym_Logs.Views.Pages;
public partial class PersonalView : ContentPage
{
	public PersonalView(PersonalViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}