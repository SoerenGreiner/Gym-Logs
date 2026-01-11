using Gym_Logs.View;

namespace Gym_Logs
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(RegistrationView), typeof(RegistrationView));
            Routing.RegisterRoute(nameof(WorkoutCalendarView), typeof(WorkoutCalendarView));


            CheckRegistrationStatus();
        }

        private async void CheckRegistrationStatus()
        {
            bool isRegistered = false;

            try
            {
                var value = await SecureStorage.GetAsync("IsRegistered");
                isRegistered = value == "true";
            }
            catch
            {
                isRegistered = false;
            }

            if (!isRegistered)
            {
                await GoToAsync($"//{nameof(RegistrationView)}");
            }
            else
            {
                await GoToAsync($"//{nameof(WorkoutCalendarView)}");
            }
        }
    }
}