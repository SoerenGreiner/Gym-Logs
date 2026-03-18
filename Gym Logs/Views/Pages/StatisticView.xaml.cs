using Gym_Logs.ViewModels.Pages;

namespace Gym_Logs.Views.Pages;

public partial class StatisticView : ContentPage
{
	public StatisticView(StatisticsViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}