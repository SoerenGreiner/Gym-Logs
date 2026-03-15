using System.Text.Json.Serialization;
using Gym_Logs.Enums;

namespace Gym_Logs.Model.System
{
    /// <summary>
    /// Represents a theme configuration for the app, including primary, secondary, and accent colors.
    /// Properties can be loaded from JSON and mapped to <see cref="PaletteColorKey"/> enums.
    /// </summary>
    public class AppThemeModel
    {
        // ===== JSON PROPERTIES =====

        /// <summary>
        /// Internal name of the theme used for JSON serialization and identification.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// User-friendly display name for the theme, optionally localized.
        /// </summary>
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        /// <summary>Start color of the primary gradient.</summary>
        [JsonPropertyName("primaryStart")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaletteColorKeyEnum PrimaryStart { get; set; }

        /// <summary>End color of the primary gradient.</summary>
        [JsonPropertyName("primaryEnd")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaletteColorKeyEnum PrimaryEnd { get; set; }

        /// <summary>Start color of the secondary gradient.</summary>
        [JsonPropertyName("secondaryStart")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaletteColorKeyEnum SecondaryStart { get; set; }

        /// <summary>End color of the secondary gradient.</summary>
        [JsonPropertyName("secondaryEnd")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaletteColorKeyEnum SecondaryEnd { get; set; }

        /// <summary>Primary accent color for highlights and borders.</summary>
        [JsonPropertyName("primaryAccent")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaletteColorKeyEnum PrimaryAccent { get; set; }

        /// <summary>Secondary accent color for highlights and borders.</summary>
        [JsonPropertyName("secondaryAccent")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaletteColorKeyEnum SecondaryAccent { get; set; }

        /// <summary>Primary text color key used for readable text over backgrounds.</summary>
        [JsonPropertyName("primaryTextColor")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaletteColorKeyEnum PrimaryTextColor { get; set; }

        // ===== COLOR HELPERS =====

        /// <summary>
        /// Returns a localized display name using app resources if available, otherwise returns <see cref="DisplayName"/>.
        /// </summary>
        [JsonIgnore]
        public string LocalizedDisplayName => AppResources.ResourceManager.GetString(Name) ?? DisplayName;

        /// <summary>Resolved <see cref="Color"/> for <see cref="PrimaryStart"/> key.</summary>
        [JsonIgnore] public Color PrimaryStartColor => GetColor(PrimaryStart);

        /// <summary>Resolved <see cref="Color"/> for <see cref="PrimaryEnd"/> key.</summary>
        [JsonIgnore] public Color PrimaryEndColor => GetColor(PrimaryEnd);

        /// <summary>Resolved <see cref="Color"/> for <see cref="SecondaryStart"/> key.</summary>
        [JsonIgnore] public Color SecondaryStartColor => GetColor(SecondaryStart);

        /// <summary>Resolved <see cref="Color"/> for <see cref="SecondaryEnd"/> key.</summary>
        [JsonIgnore] public Color SecondaryEndColor => GetColor(SecondaryEnd);

        /// <summary>Resolved <see cref="Color"/> for <see cref="PrimaryAccent"/> key.</summary>
        [JsonIgnore] public Color PrimaryAccentColor => GetColor(PrimaryAccent);

        /// <summary>Resolved <see cref="Color"/> for <see cref="SecondaryAccent"/> key.</summary>
        [JsonIgnore] public Color SecondaryAccentColor => GetColor(SecondaryAccent);

        /// <summary>Resolved <see cref="Color"/> for <see cref="PrimaryTextColor"/> key.</summary>
        [JsonIgnore] public Color PrimaryTextColorColor => GetColor(PrimaryTextColor);

        // ===== METHODS =====

        /// <summary>
        /// Retrieves the actual <see cref="Color"/> from the application's resource dictionary for the given <see cref="PaletteColorKey"/>.
        /// Returns <see cref="Colors.Transparent"/> if the key is not found.
        /// </summary>
        /// <param name="key">The palette color key to resolve.</param>
        /// <returns>The resolved <see cref="Color"/>.</returns>
        public Color GetColor(PaletteColorKeyEnum key)
        {
            var resourceKey = key.ToString();
            if (Application.Current?.Resources.TryGetValue(resourceKey, out var obj) == true && obj is Color c)
                return c;

            return Colors.Transparent;
        }

        /// <summary>
        /// Creates a <see cref="RadialGradientBrush"/> using the specified start and end color keys.
        /// </summary>
        /// <param name="start">The starting <see cref="PaletteColorKey"/> of the gradient.</param>
        /// <param name="end">The ending <see cref="PaletteColorKey"/> of the gradient.</param>
        /// <returns>A <see cref="RadialGradientBrush"/> with the resolved colors.</returns>
        public RadialGradientBrush CreateBrush(PaletteColorKeyEnum start, PaletteColorKeyEnum end)
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