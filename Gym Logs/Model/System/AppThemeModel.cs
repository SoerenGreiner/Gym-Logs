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

        [JsonPropertyName("isDark")]
        public bool IsDark { get; set; }

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

        [JsonPropertyName("accent")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaletteColorKey Accent { get; set; }

        [JsonPropertyName("primaryTextColor")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaletteColorKey PrimaryTextColor { get; set; }

        [JsonPropertyName("headerStart")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaletteColorKey HeaderStart { get; set; }

        [JsonPropertyName("headerEnd")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaletteColorKey HeaderEnd { get; set; }

        [JsonPropertyName("normalStart")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaletteColorKey NormalStart { get; set; }

        [JsonPropertyName("normalEnd")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaletteColorKey NormalEnd { get; set; }

        [JsonPropertyName("inactiveStart")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaletteColorKey InactiveStart { get; set; }

        [JsonPropertyName("inactiveEnd")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaletteColorKey InactiveEnd { get; set; }

        [JsonPropertyName("todayStart")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaletteColorKey TodayStart { get; set; }

        [JsonPropertyName("todayEnd")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaletteColorKey TodayEnd { get; set; }

        [JsonPropertyName("workoutStart")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaletteColorKey WorkoutStart { get; set; }

        [JsonPropertyName("workoutEnd")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaletteColorKey WorkoutEnd { get; set; }

        [JsonPropertyName("bodyStart")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaletteColorKey BodyStart { get; set; }

        [JsonPropertyName("bodyEnd")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaletteColorKey BodyEnd { get; set; }

        // ===== COLOR HELPERS =====
        [JsonIgnore] public string LocalizedDisplayName => AppResources.ResourceManager.GetString(Name) ?? DisplayName;
        [JsonIgnore] public Color PrimaryStartColor => GetColor(PrimaryStart);
        [JsonIgnore] public Color PrimaryEndColor => GetColor(PrimaryEnd);
        [JsonIgnore] public Color SecondaryStartColor => GetColor(SecondaryStart);
        [JsonIgnore] public Color SecondaryEndColor => GetColor(SecondaryEnd);
        [JsonIgnore] public Color AccentColor => GetColor(Accent);
        [JsonIgnore] public Color PrimaryTextColorColor => GetColor(PrimaryTextColor);
        [JsonIgnore] public Color HeaderStartColor => GetColor(HeaderStart);
        [JsonIgnore] public Color HeaderEndColor => GetColor(HeaderEnd);
        [JsonIgnore] public Color NormalStartColor => GetColor(NormalStart);
        [JsonIgnore] public Color NormalEndColor => GetColor(NormalEnd);
        [JsonIgnore] public Color InactiveStartColor => GetColor(InactiveStart);
        [JsonIgnore] public Color InactiveEndColor => GetColor(InactiveEnd);
        [JsonIgnore] public Color TodayStartColor => GetColor(TodayStart);
        [JsonIgnore] public Color TodayEndColor => GetColor(TodayEnd);
        [JsonIgnore] public Color WorkoutStartColor => GetColor(WorkoutStart);
        [JsonIgnore] public Color WorkoutEndColor => GetColor(WorkoutEnd);
        [JsonIgnore] public Color BodyStartColor => GetColor(BodyStart);
        [JsonIgnore] public Color BodyEndColor => GetColor(BodyEnd);

        // ===== SEMANTISCHE TEXTFARBEN (optional über Palette) =====
        [JsonIgnore] public Color CalendarTodayText => GetColor(PaletteColorKey.Yellow);
        [JsonIgnore] public Color CalendarWorkoutText => GetColor(PaletteColorKey.Red);
        [JsonIgnore] public Color CalendarBodyText => GetColor(PaletteColorKey.Green);

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