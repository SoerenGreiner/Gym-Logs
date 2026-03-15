using Gym_Logs.Model.System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text.Json;

namespace Gym_Logs.Services.System
{
    /// <summary>
    /// Factory class to load theme categories and groups from embedded JSON resources.
    /// </summary>
    public static class ThemeFactory
    {
        /// <summary>
        /// Loads all theme categories from embedded JSON resources following the naming convention:
        /// <c>Resources.Styles.AppThemes.Category.Group.File.json</c>.
        /// </summary>
        /// <returns>
        /// A collection of <see cref="ThemeCategoryModel"/> containing groups and themes.
        /// </returns>
        public static IEnumerable<ThemeCategoryModel> LoadThemeCategories()
        {
            // Get the currently executing assembly
            var assembly = Assembly.GetExecutingAssembly();

            // Filter embedded resources ending with .json in the AppThemes folder
            var resourceNames = assembly.GetManifestResourceNames()
                .Where(n => n.EndsWith(".json") &&
                            n.Contains("Resources.Styles.AppThemes"))
                .ToList();

            // Dictionary to collect categories by name
            var categoryMap = new Dictionary<string, ThemeCategoryModel>();

            foreach (var resourceName in resourceNames)
            {
                using var stream = assembly.GetManifestResourceStream(resourceName);
                if (stream == null)
                    continue;

                using var reader = new StreamReader(stream);
                var json = reader.ReadToEnd();

                // Deserialize the JSON file into a list of themes
                var themes = JsonSerializer.Deserialize<List<AppThemeModel>>(json);
                if (themes == null || themes.Count == 0)
                    continue;

                /*
                 Naming convention for resources:
                 Resources.Styles.AppThemes.Category.Group.File.json
                */

                var parts = resourceName.Split('.');
                if (parts.Length < 3)
                    continue;

                var categoryName = parts[^3]; // third-to-last part = category
                var groupName = parts[^2];    // second-to-last part = group

                // Get or create category
                if (!categoryMap.TryGetValue(categoryName, out var category))
                {
                    category = new ThemeCategoryModel
                    {
                        Name = categoryName,
                        Themes = new ObservableCollection<ThemeGroupModel>()
                    };

                    categoryMap[categoryName] = category;
                }

                // Get or create group within the category
                var group = category.Themes.FirstOrDefault(g => g.Name == groupName);
                if (group == null)
                {
                    group = new ThemeGroupModel
                    {
                        Name = groupName,
                        Themes = new ObservableCollection<AppThemeModel>()
                    };

                    category.Themes.Add(group);
                }

                // Add each theme to the group
                foreach (var theme in themes)
                    group.Themes.Add(theme);
            }

            // Return all categories
            return categoryMap.Values;
        }
    }
}
