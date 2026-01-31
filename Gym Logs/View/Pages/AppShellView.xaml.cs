using Gym_Logs.View;
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
    
