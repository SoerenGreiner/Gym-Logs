using Gym_Logs.Enums;
using Gym_Logs.Model.System;
using System.ComponentModel;

namespace Gym_Logs.Services.System
{
    /// <summary>
    /// Service for managing application themes, brightness, and brush inversion.
    /// Updates application resources dynamically and persists user preferences.
    /// </summary>
    public class ThemeService : INotifyPropertyChanged
    {
        private const string PrefKey = "SelectedTheme";
        private const string BrightnessKey = "ThemeBrightness";

        /// <summary>
        /// List of all available themes across categories.
        /// </summary>
        public IReadOnlyList<AppThemeModel> Themes { get; }

        /// <summary>
        /// List of all theme categories with their groups and themes.
        /// </summary>
        public IReadOnlyList<ThemeCategoryModel> ThemeCategories { get; }

        private ThemeBrightnessEnum _brightness = ThemeBrightnessEnum.Normal;

        /// <summary>
        /// Gets the current brightness level.
        /// </summary>
        public ThemeBrightnessEnum Brightness
        {
            get => _brightness;
            private set
            {
                if (_brightness == value) return;
                _brightness = value;
                OnPropertyChanged(nameof(Brightness));
            }
        }

        private bool _isBrushInverted;

        /// <summary>
        /// Indicates if the primary and secondary brushes should be inverted.
        /// Changing this property automatically reapplies the current theme.
        /// </summary>
        public bool IsBrushInverted
        {
            get => _isBrushInverted;
            set
            {
                if (_isBrushInverted == value) return;
                _isBrushInverted = value;
                OnPropertyChanged(nameof(IsBrushInverted));
                if (CurrentTheme != null) ApplyTheme(CurrentTheme);
            }
        }

        private bool _isGradientDirectionInverted;

        /// <summary>
        /// Indicates if gradient directions should be inverted.
        /// Changing this property automatically reapplies the current theme.
        /// </summary>
        public bool IsGradientDirectionInverted
        {
            get => _isGradientDirectionInverted;
            set
            {
                if (_isGradientDirectionInverted == value) return;
                _isGradientDirectionInverted = value;
                OnPropertyChanged(nameof(IsGradientDirectionInverted));
                if (CurrentTheme != null) ApplyTheme(CurrentTheme);
            }
        }

        private AppThemeModel _currentTheme;

        /// <summary>
        /// The currently applied theme.
        /// </summary>
        public AppThemeModel CurrentTheme
        {
            get => _currentTheme;
            private set
            {
                if (_currentTheme == value) return;
                _currentTheme = value;
                OnPropertyChanged(nameof(CurrentTheme));
            }
        }

        /// <summary>
        /// Initializes the service, loads theme categories and all available themes,
        /// and sets the last saved brightness level.
        /// </summary>
        public ThemeService()
        {
            var categories = ThemeFactory.LoadThemeCategories().ToList();

            ThemeCategories = categories;

            Themes = categories
                .SelectMany(c => c.Themes)
                .SelectMany(g => g.Themes)
                .ToList();

            var savedBrightness = Preferences.Default.Get(BrightnessKey, ThemeBrightnessEnum.Normal.ToString());

            if (Enum.TryParse(savedBrightness, out ThemeBrightnessEnum parsed))
                Brightness = parsed;
        }

        /// <summary>
        /// Loads the last saved theme from preferences or defaults to the first available theme.
        /// </summary>
        public void LoadSavedTheme()
        {
            var saved = Preferences.Default.Get<string>(PrefKey, null);

            var theme = Themes.FirstOrDefault(t => t.Name == saved)
                        ?? Themes.FirstOrDefault();

            ApplyTheme(theme);
        }

        /// <summary>
        /// Sets the global brightness and persists it in preferences.
        /// Reapplies the current theme with the new brightness.
        /// </summary>
        /// <param name="brightness">The desired brightness level.</param>
        public void SetBrightness(ThemeBrightnessEnum brightness)
        {
            if (Brightness == brightness) return;

            Brightness = brightness;
            Preferences.Default.Set(BrightnessKey, brightness.ToString());

            if (CurrentTheme != null)
                ApplyTheme(CurrentTheme);
        }

        /// <summary>
        /// Computes a high-contrast text color based on a background color.
        /// </summary>
        /// <param name="bg">The background color.</param>
        /// <returns>Black or White, depending on luminance.</returns>
        private Color GetContrastingText(Color bg)
        {
            double luminance = 0.2126 * bg.Red + 0.7152 * bg.Green + 0.0722 * bg.Blue;
            return luminance > 0.6 ? Colors.Black : Colors.White;
        }

        /// <summary>
        /// Applies a theme to the application resources, adjusting colors for brightness,
        /// optionally inverting brush colors and gradient directions.
        /// Persists the selected theme in preferences.
        /// </summary>
        /// <param name="theme">The theme to apply.</param>
        /// <param name="brightness">Optional brightness override.</param>
        public void ApplyTheme(AppThemeModel theme, ThemeBrightnessEnum? brightness = null)
        {
            if (theme == null || Application.Current == null) return;

            if (brightness.HasValue)
                SetBrightness(brightness.Value);

            var r = Application.Current.Resources;

            // Helper: Adjust color by brightness
            Color Adjust(Color c)
            {
                float factor = Brightness switch
                {
                    ThemeBrightnessEnum.Light => 1.2f,
                    ThemeBrightnessEnum.Normal => 1f,
                    ThemeBrightnessEnum.Dark => 0.8f,
                    _ => 1f
                };

                return Color.FromRgba(
                    Math.Clamp(c.Red * factor, 0, 1),
                    Math.Clamp(c.Green * factor, 0, 1),
                    Math.Clamp(c.Blue * factor, 0, 1),
                    c.Alpha
                );
            }

            // Helper: Get color from ResourceDictionary and adjust
            Color C(PaletteColorKeyEnum key)
            {
                if (r.TryGetValue(key.ToString(), out var obj) && obj is Color c)
                    return Adjust(c);

                throw new Exception($"Palette color '{key}' not found.");
            }

            // ===== Handle brush inversion =====
            var primaryStart = IsBrushInverted ? theme.SecondaryStart : theme.PrimaryStart;
            var primaryEnd = IsBrushInverted ? theme.SecondaryEnd : theme.PrimaryEnd;

            var secondaryStart = IsBrushInverted ? theme.PrimaryStart : theme.SecondaryStart;
            var secondaryEnd = IsBrushInverted ? theme.PrimaryEnd : theme.SecondaryEnd;

            r["PrimaryStart"] = C(primaryStart);
            r["PrimaryEnd"] = C(primaryEnd);
            r["SecondaryStart"] = C(secondaryStart);
            r["SecondaryEnd"] = C(secondaryEnd);
            r["PrimaryAccent"] = C(theme.PrimaryAccent);
            r["SecondaryAccent"] = C(theme.SecondaryAccent);

            r["PrimaryTextColor"] = GetContrastingText(C(primaryStart));

            // ===== Handle gradient direction inversion =====
            var startPoint = IsGradientDirectionInverted ? new Point(1, 0) : new Point(0, 0);
            var endPoint = IsGradientDirectionInverted ? new Point(0, 0) : new Point(1, 0);

            // ===== Update brushes in ResourceDictionary =====
            r["PrimaryBrush"] = new LinearGradientBrush
            {
                StartPoint = startPoint,
                EndPoint = endPoint,
                GradientStops =
                {
                    new GradientStop { Color = C(primaryStart), Offset = 0 },
                    new GradientStop { Color = C(primaryEnd), Offset = 1 }
                }
            };

            r["SecondaryBrush"] = new LinearGradientBrush
            {
                StartPoint = startPoint,
                EndPoint = endPoint,
                GradientStops =
                {
                    new GradientStop { Color = C(secondaryStart), Offset = 0 },
                    new GradientStop { Color = C(secondaryEnd), Offset = 1 }
                }
            };

            CurrentTheme = theme;
            Preferences.Default.Set(PrefKey, theme.Name);
        }

        /// <summary>
        /// Event triggered when a property changes to update bindings.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Helper to raise <see cref="PropertyChanged"/> events.
        /// </summary>
        /// <param name="name">The name of the property that changed.</param>
        private void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}