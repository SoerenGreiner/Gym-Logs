using Gym_Logs.ViewModel.Pages;

namespace Gym_Logs.View.Pages;

public partial class ThemeSelectionView : ContentPage
{
	public ThemeSelectionView(ThemeSelectionViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}