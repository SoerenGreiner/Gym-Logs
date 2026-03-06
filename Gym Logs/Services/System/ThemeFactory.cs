using Gym_Logs.Model.System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text.Json;

public static class ThemeFactory
{
    public static IEnumerable<ThemeCategoryModel> LoadThemeCategories()
    {
        var assembly = Assembly.GetExecutingAssembly();

        var resourceNames = assembly.GetManifestResourceNames()
            .Where(n => n.EndsWith(".json") &&
                        n.Contains("Resources.Styles.AppThemes"))
            .ToList();

        var categoryMap = new Dictionary<string, ThemeCategoryModel>();

        foreach (var resourceName in resourceNames)
        {
            using var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
                continue;

            using var reader = new StreamReader(stream);
            var json = reader.ReadToEnd();

            var themes = JsonSerializer.Deserialize<List<AppThemeModel>>(json);
            if (themes == null || themes.Count == 0)
                continue;

            /*
             Naming Convention:
             Resources.Styles.AppThemes.Category.Group.File.json
            */

            var parts = resourceName.Split('.');

            if (parts.Length < 3)
                continue;

            var categoryName = parts[^3];
            var groupName = parts[^2];

            if (!categoryMap.TryGetValue(categoryName, out var category))
            {
                category = new ThemeCategoryModel
                {
                    Name = categoryName,
                    Themes = new ObservableCollection<ThemeGroupModel>()
                };

                categoryMap[categoryName] = category;
            }

            var group = category.Themes
                .FirstOrDefault(g => g.Name == groupName);

            if (group == null)
            {
                group = new ThemeGroupModel
                {
                    Name = groupName,
                    Themes = new ObservableCollection<AppThemeModel>()
                };

                category.Themes.Add(group);
            }

            foreach (var theme in themes)
                group.Themes.Add(theme);
        }

        return categoryMap.Values;
    }
}