using Gym_Logs.ViewModels.Pages;

namespace Gym_Logs.Views.Pages;

public partial class ThemeSelectionView : ContentPage
{
	public ThemeSelectionView(ThemeSelectionViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}