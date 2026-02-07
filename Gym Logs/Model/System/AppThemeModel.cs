using System;
using Microsoft.Maui.Graphics;

namespace Gym_Logs.Model.System;

public class AppThemeModel
{
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public Type ResourceDictionaryType { get; set; }

    public Color PrimaryLight { get; set; }
    public Color PrimaryDark { get; set; }
    public Color SecondaryLight { get; set; }
    public Color SecondaryDark { get; set; }
    public Color Accent { get; set; }

    public Color PrimaryTextColor { get; set; }

    public string LocalizedDisplayName
    {
        get
        {
            var localized = AppResources.ResourceManager.GetString(Name);
            return string.IsNullOrEmpty(localized) ? DisplayName : localized;
        }
    }
}
