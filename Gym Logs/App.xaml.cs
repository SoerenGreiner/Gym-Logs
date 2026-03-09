using Gym_Logs.Services.System;

namespace Gym_Logs
{
    public partial class App : Application
    {
        public static ThemeService ThemeService { get; private set; }

        public App(AppShellView shell)
        {
            InitializeComponent();

            AdaptiveUIManager.Instance.Initialize();

            ThemeService = new ThemeService();
            ThemeService.LoadSavedTheme();

            MainPage = shell;
        }
    }
}