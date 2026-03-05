using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System.Text.Json.Serialization;
using Gym_Logs.Enums;

namespace Gym_Logs.Model.System
{
    public class AppThemeModel
    {
        // ===== JSON ENUMS =====
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("primaryStart")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaletteColorKey PrimaryStart { get; set; }

        [JsonPropertyName("primaryEnd")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaletteColorKey PrimaryEnd { get; set; }

        [JsonPropertyName("secondaryStart")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaletteColorKey SecondaryStart { get; set; }

        [JsonPropertyName("secondaryEnd")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaletteColorKey SecondaryEnd { get; set; }

        [JsonPropertyName("primaryAccent")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaletteColorKey PrimaryAccent { get; set; }

        [JsonPropertyName("secondaryAccent")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaletteColorKey SecondaryAccent { get; set; }

        [JsonPropertyName("primaryTextColor")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaletteColorKey PrimaryTextColor { get; set; }

        // ===== COLOR HELPERS =====
        [JsonIgnore] public string LocalizedDisplayName => AppResources.ResourceManager.GetString(Name) ?? DisplayName;
        [JsonIgnore] public Color PrimaryStartColor => GetColor(PrimaryStart);
        [JsonIgnore] public Color PrimaryEndColor => GetColor(PrimaryEnd);
        [JsonIgnore] public Color SecondaryStartColor => GetColor(SecondaryStart);
        [JsonIgnore] public Color SecondaryEndColor => GetColor(SecondaryEnd);
        [JsonIgnore] public Color PrimaryAccentColor => GetColor(PrimaryAccent);
        [JsonIgnore] public Color SecondaryAccentColor => GetColor(SecondaryAccent);
        [JsonIgnore] public Color PrimaryTextColorColor => GetColor(PrimaryTextColor);

        // ===== METHODEN =====
        public Color GetColor(PaletteColorKey key)
        {
            var resourceKey = key.ToString();
            if (Application.Current?.Resources.TryGetValue(resourceKey, out var obj) == true && obj is Color c)
                return c;

            return Colors.Transparent;
        }

        public RadialGradientBrush CreateBrush(PaletteColorKey start, PaletteColorKey end)
        {
            return new RadialGradientBrush
            {
                Center = new Point(0, 0),
                Radius = 1,
                GradientStops =
                {
                    new GradientStop { Color = GetColor(start), Offset = 0 },
                    new GradientStop { Color = GetColor(end), Offset = 1 }
                }
            };
        }
    }
}