using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gym_Logs.Model.System;
using Gym_Logs.View.Pages;
using Gym_Logs.Services.System;
using Microsoft.Maui.Controls;

namespace Gym_Logs.ViewModel;

public partial class SettingsViewModel : ObservableObject
{
    private readonly ThemeService _themeService;

    [ObservableProperty]
    private AppThemeModel currentTheme;

    public SettingsViewModel()
    {
        _themeService = App.ThemeService;

        CurrentTheme = _themeService.CurrentTheme;

        _themeService.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(_themeService.CurrentTheme))
            {
                CurrentTheme = _themeService.CurrentTheme;
            }
        };
    }

    [RelayCommand]
    void OpenThemeSelection()
    {
        Shell.Current.GoToAsync(nameof(ThemeSelectionView));
    }
}
