using Gym_Logs.ViewModel;

namespace Gym_Logs.View;

public partial class StatisticView : ContentPage
{
	public StatisticView(StatisticsViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}