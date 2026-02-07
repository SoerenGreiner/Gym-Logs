using System;
using System.Collections.Generic;
using Gym_Logs.Resources.Styles.AppThemes;

namespace Gym_Logs.Model.System;

public static class ThemeRegistry
{
    public static IEnumerable<AppThemeModel> GetAllThemes()
    {
        var themes = new List<Type>
    {
        typeof(Light),
        typeof(Dark),
        typeof(MonoBlueLight),
        typeof(MonoBlueDark)
    };

        foreach (var themeType in themes)
        {
            var dict = (ResourceDictionary)Activator.CreateInstance(themeType);

            yield return new AppThemeModel
            {
                Name = themeType.Name,
                DisplayName = themeType.Name,
                ResourceDictionaryType = themeType,

                PrimaryLight = (Color)dict["PrimaryLight"],
                PrimaryDark = (Color)dict["PrimaryDark"],
                SecondaryLight = (Color)dict["SecondaryLight"],
                SecondaryDark = (Color)dict["SecondaryDark"],
                Accent = (Color)dict["Accent"],

                PrimaryTextColor = (Color)dict["PrimaryTextColor"],
            };
        }
    }
}
