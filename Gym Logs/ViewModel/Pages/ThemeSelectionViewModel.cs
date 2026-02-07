using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gym_Logs.Model.System;
using Gym_Logs.Services.System;
using System.Collections.ObjectModel;

namespace Gym_Logs.ViewModel.Pages
{
    public partial class ThemeSelectionViewModel : ObservableObject
    {
        private readonly ThemeService _themeService;

        public ObservableCollection<AppThemeModel> AvailableThemes { get; }
        = new ObservableCollection<AppThemeModel>(ThemeRegistry.GetAllThemes());

        [ObservableProperty]
        AppThemeModel selectedTheme;

        public ThemeSelectionViewModel()
        {
            _themeService = App.ThemeService;

            AvailableThemes = new ObservableCollection<AppThemeModel>(
                ThemeRegistry.GetAllThemes());

            SelectedTheme = _themeService.CurrentTheme;
        }

        [RelayCommand]
        void ThemeSelected(AppThemeModel theme)
        {
            _themeService.Apply(theme);
            SelectedTheme = theme;

            Shell.Current.GoToAsync("..");
        }
    }
}
