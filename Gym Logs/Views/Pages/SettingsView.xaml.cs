using Gym_Logs.ViewModels.Pages;

namespace Gym_Logs.Views.Pages;

public partial class SettingsView : ContentPage
{
	public SettingsView(SettingsViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}