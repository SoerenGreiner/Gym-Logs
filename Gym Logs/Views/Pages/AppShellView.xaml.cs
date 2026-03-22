using Gym_Logs.ViewModels.Pages;

namespace Gym_Logs.Views.Pages
{
    public partial class AppShellView : Shell
    {
        public AppShellView(AppShellViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            Routing.RegisterRoute(nameof(ThemeSelectionView), typeof(ThemeSelectionView));
            Routing.RegisterRoute(nameof(WorkoutView), typeof(WorkoutView));
            Routing.RegisterRoute(nameof(ExercisePickerView), typeof(ExercisePickerView));
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
    
