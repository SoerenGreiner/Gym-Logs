using Gym_Logs.View;
using Gym_Logs.Services;
using Gym_Logs.Resources.Styles.AppThemes;
using Gym_Logs.ViewModel;

namespace Gym_Logs
{
    public partial class App : Application
    {
        public App(AppShell shell)
        {
            InitializeComponent();

            var savedTheme = Preferences.Default.Get("AppTheme", "Light");
            var themeModel = new SettingsViewModel().AvailableThemes.FirstOrDefault(t => t.Name == savedTheme);
            if (themeModel?.ResourceDictionaryType != null)
            {
                var dict = (ResourceDictionary)Activator.CreateInstance(themeModel.ResourceDictionaryType);
                Application.Current.Resources.MergedDictionaries.Clear();
                Application.Current.Resources.MergedDictionaries.Add(dict);
            }

            MainPage = shell;
        }
    }
}