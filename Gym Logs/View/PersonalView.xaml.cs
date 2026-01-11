using Gym_Logs.ViewModel;

namespace Gym_Logs.View;
public partial class PersonalView : ContentPage
{
	public PersonalView(PersonalViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}