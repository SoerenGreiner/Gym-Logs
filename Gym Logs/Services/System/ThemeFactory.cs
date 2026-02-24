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
            .Where(n => n.EndsWith(".json") && n.Contains("Resources.Styles.AppThemes"))
            .ToList();

        var categories = new List<ThemeCategoryModel>();

        foreach (var resourceName in resourceNames)
        {
            using var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null) continue;

            using var reader = new StreamReader(stream);
            var json = reader.ReadToEnd();

            var themes = JsonSerializer.Deserialize<List<AppThemeModel>>(json);
            if (themes == null || themes.Count == 0) continue;

            var nameParts = resourceName.Split('.');
            var categoryName = nameParts[^2];

            categories.Add(new ThemeCategoryModel
            {
                Name = categoryName,
                Themes = new ObservableCollection<AppThemeModel>(themes)
            });
        }

        return categories;
    }
}

