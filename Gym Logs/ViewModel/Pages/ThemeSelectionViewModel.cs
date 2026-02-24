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

        public ObservableCollection<ThemeCategoryModel> ThemeCategories { get; } = new();

        [ObservableProperty]
        AppThemeModel selectedTheme;

        public ThemeSelectionViewModel()
        {
            _themeService = App.ThemeService;

            var categories = ThemeFactory.LoadThemeCategories();
            foreach (var category in categories)
            {
                ThemeCategories.Add(category);
            }

            SelectedTheme = _themeService.CurrentTheme;
        }

        [RelayCommand]
        void ThemeSelected(AppThemeModel theme)
        {
            _themeService.ApplyTheme(theme);
            SelectedTheme = theme;
            Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        void ToggleCategory(ThemeCategoryModel category)
        {
            category.IsExpanded = !category.IsExpanded;
        }
    }
}


