using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gym_Logs.Model.System;
using Gym_Logs.ViewModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Gym_Logs.ViewModel;

public partial class SettingsViewModel : ObservableObject
{
    [ObservableProperty]
    AppThemeModel selectedTheme;

    public ObservableCollection<AppThemeModel> AvailableThemes { get; } = new()
    {
        new AppThemeModel
        {
            Name = "Light",
            DisplayName = "Light",
            ResourceDictionaryType = typeof(Gym_Logs.Resources.Styles.AppThemes.Light)
        },
        new AppThemeModel
        {
            Name = "Dark",
            DisplayName = "Dark",
            ResourceDictionaryType = typeof(Gym_Logs.Resources.Styles.AppThemes.Dark)
        },
        new AppThemeModel
        {
            Name = "MonoBlueLight",
            DisplayName = "Mono Blue (Hell)",
            ResourceDictionaryType = typeof(Gym_Logs.Resources.Styles.AppThemes.MonoBlueLight)
        },
        new AppThemeModel
        {
            Name = "MonoBlueDark",
            DisplayName = "Mono Blue (Dunkel)",
            ResourceDictionaryType = typeof(Gym_Logs.Resources.Styles.AppThemes.MonoBlueDark)
        }
    };

    public SettingsViewModel()
    {
        var savedThemeName = Preferences.Default.Get("AppTheme", "Light");
        SelectedTheme = AvailableThemes.FirstOrDefault(t => t.Name == savedThemeName) ?? AvailableThemes[0];
    }

    [RelayCommand]
    void ApplySelectedTheme()
    {
        if (SelectedTheme?.ResourceDictionaryType == null)
            return;

        var themeDict = (ResourceDictionary)Activator.CreateInstance(SelectedTheme.ResourceDictionaryType);

        Application.Current.Resources.MergedDictionaries.Clear();
        Application.Current.Resources.MergedDictionaries.Add(themeDict);

        Preferences.Default.Set("AppTheme", SelectedTheme.Name);
    }
}
