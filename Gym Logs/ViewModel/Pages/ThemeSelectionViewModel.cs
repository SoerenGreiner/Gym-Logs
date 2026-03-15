using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gym_Logs.Model.System;
using Gym_Logs.Services.System;
using System.Collections.ObjectModel;

namespace Gym_Logs.ViewModel.Pages
{
    /// <summary>
    /// ViewModel for the Theme Selection page.
    /// Manages theme categories, groups, and the currently selected theme.
    /// </summary>
    public partial class ThemeSelectionViewModel : ObservableObject
    {
        private readonly ThemeService _themeService;

        /// <summary>
        /// Collection of theme categories displayed in the UI.
        /// </summary>
        public ObservableCollection<ThemeCategoryModel> ThemeCategories { get; } = new();

        /// <summary>
        /// The currently selected theme.
        /// Changes automatically when a user selects a new theme.
        /// </summary>
        [ObservableProperty]
        AppThemeModel selectedTheme;

        /// <summary>
        /// Initializes a new instance of <see cref="ThemeSelectionViewModel"/>.
        /// Loads theme categories and sets the selected theme from the ThemeService.
        /// </summary>
        public ThemeSelectionViewModel()
        {
            _themeService = App.ThemeService;

            // Load Categories
            foreach (var category in ThemeFactory.LoadThemeCategories())
                ThemeCategories.Add(category);

            SelectedTheme = _themeService.CurrentTheme;
        }

        // ================= Commands =================

        /// <summary>
        /// Applies the selected theme and navigates back to the previous page.
        /// </summary>
        /// <param name="theme">The theme to apply.</param>
        [RelayCommand]
        void ThemeSelected(AppThemeModel theme)
        {
            if (theme == null)
                return;

            _themeService.ApplyTheme(theme);
            SelectedTheme = theme;

            Shell.Current.GoToAsync("..");
        }

        /// <summary>
        /// Toggles the expansion state of a theme category.
        /// Collapses all other categories.
        /// </summary>
        /// <param name="category">The category to toggle.</param>
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

        /// <summary>
        /// Toggles the expansion state of a theme group within a category.
        /// Collapses all other groups in all categories.
        /// </summary>
        /// <param name="group">The group to toggle.</param>
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
