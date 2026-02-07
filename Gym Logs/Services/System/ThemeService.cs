using System.ComponentModel;
using Microsoft.Maui.Controls; // für ResourceDictionary
using Microsoft.Maui.Storage;
using Gym_Logs.Model.System;
using System.Linq;

namespace Gym_Logs.Services.System;

public class ThemeService : INotifyPropertyChanged
{
    private AppThemeModel _currentTheme;
    public AppThemeModel CurrentTheme
    {
        get => _currentTheme;
        private set
        {
            if (_currentTheme == value) return;
            _currentTheme = value;
            OnPropertyChanged(nameof(CurrentTheme));
        }
    }

    public void LoadSavedTheme()
    {
        var name = Preferences.Default.Get("AppTheme", "Light");
        var theme = ThemeRegistry.GetAllThemes().FirstOrDefault(t => t.Name == name)
                    ?? ThemeRegistry.GetAllThemes().First();
        Apply(theme);
    }

    public void Apply(AppThemeModel theme)
    {
        if (theme?.ResourceDictionaryType == null) return;

        var dict = (ResourceDictionary)Activator.CreateInstance(theme.ResourceDictionaryType);
        Application.Current.Resources.MergedDictionaries.Clear();
        Application.Current.Resources.MergedDictionaries.Add(dict);

        CurrentTheme = theme;
        Preferences.Default.Set("AppTheme", theme.Name);
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
