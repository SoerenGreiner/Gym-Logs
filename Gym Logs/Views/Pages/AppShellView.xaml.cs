using Gym_Logs.View.Pages;
using Gym_Logs.ViewModel;
using Gym_Logs.Model.System;

namespace Gym_Logs
{
    public partial class AppShellView : Shell
    {
        public AppShellView(AppShellViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            Routing.RegisterRoute(nameof(ThemeSelectionView), typeof(ThemeSelectionView));
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is AppShellViewModel vm)
            {
                await vm.CheckRegistrationStatusAsync();
            }
        }
    }
}
    
