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

            // Load Categories
            foreach (var category in ThemeFactory.LoadThemeCategories())
                ThemeCategories.Add(category);

            SelectedTheme = _themeService.CurrentTheme;
        }

        [RelayCommand]
        void ThemeSelected(AppThemeModel theme)
        {
            if (theme == null)
                return;

            _themeService.ApplyTheme(theme);
            SelectedTheme = theme;

            Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        void ToggleCategory(ThemeCategoryModel category)
        {
            if (category == null)
                return;

            category.IsExpanded = !category.IsExpanded;

            foreach (var other in ThemeCategories)
            {
                if (other != category)
                    other.IsExpanded = false;
            }
        }

        [RelayCommand]
        void ToggleGroup(ThemeGroupModel group)
        {
            if (group == null)
                return;

            group.IsExpanded = !group.IsExpanded;

            foreach (var category in ThemeCategories)
            {
                foreach (var g in category.Themes)
                {
                    if (g != group)
                        g.IsExpanded = false;
                }
            }
        }
    }
}