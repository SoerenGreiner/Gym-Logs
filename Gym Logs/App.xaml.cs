using Gym_Logs.View;

namespace Gym_Logs
{
    public partial class App : Application
    {
        public App(AppShell shell)
        {
            InitializeComponent();
            MainPage = shell;
        }
    }
}