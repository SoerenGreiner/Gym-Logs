using Gym_Logs.Enums;
using Gym_Logs.Model.System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Gym_Logs.Services.System;

public class ThemeService : INotifyPropertyChanged
{
    private const string PrefKey = "SelectedTheme";
    private const string BrightnessKey = "ThemeBrightness";

    public IReadOnlyList<AppThemeModel> Themes { get; }
    public IReadOnlyList<ThemeCategoryModel> ThemeCategories { get; }

    private ThemeBrightness _brightness = ThemeBrightness.Normal;
    public ThemeBrightness Brightness
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

    public ThemeService()
    {
        var categories = ThemeFactory.LoadThemeCategories().ToList();

        ThemeCategories = categories;

        Themes = categories
            .SelectMany(c => c.Themes)
            .SelectMany(g => g.Themes)
            .ToList();

        var savedBrightness = Preferences.Default.Get(BrightnessKey, ThemeBrightness.Normal.ToString());

        if (Enum.TryParse(savedBrightness, out ThemeBrightness parsed))
            Brightness = parsed;
    }

    public void LoadSavedTheme()
    {
        var saved = Preferences.Default.Get<string>(PrefKey, null);

        var theme = Themes.FirstOrDefault(t => t.Name == saved)
                    ?? Themes.FirstOrDefault();

        ApplyTheme(theme);
    }

    public void SetBrightness(ThemeBrightness brightness)
    {
        if (Brightness == brightness) return;

        Brightness = brightness;
        Preferences.Default.Set(BrightnessKey, brightness.ToString());

        if (CurrentTheme != null)
            ApplyTheme(CurrentTheme);
    }

    private Color GetContrastingText(Color bg)
    {
        double luminance = 0.2126 * bg.Red + 0.7152 * bg.Green + 0.0722 * bg.Blue;
        return luminance > 0.6 ? Colors.Black : Colors.White;
    }

    public void ApplyTheme(AppThemeModel theme, ThemeBrightness? brightness = null)
    {
        if (theme == null || Application.Current == null) return;

        if (brightness.HasValue)
            SetBrightness(brightness.Value);

        var r = Application.Current.Resources;

        Color Adjust(Color c)
        {
            float factor = Brightness switch
            {
                ThemeBrightness.Light => 1.2f,
                ThemeBrightness.Normal => 1f,
                ThemeBrightness.Dark => 0.8f,
                _ => 1f
            };

            return Color.FromRgba(
                Math.Clamp(c.Red * factor, 0, 1),
                Math.Clamp(c.Green * factor, 0, 1),
                Math.Clamp(c.Blue * factor, 0, 1),
                c.Alpha
            );
        }

        Color C(PaletteColorKey key)
        {
            if (r.TryGetValue(key.ToString(), out var obj) && obj is Color c)
                return Adjust(c);

            throw new Exception($"Palette color '{key}' not found.");
        }

        // ===== Farb-Invertierung =====

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

        // ===== Richtungs-Invertierung =====

        var startPoint = IsGradientDirectionInverted ? new Point(1, 0) : new Point(0, 0);
        var endPoint = IsGradientDirectionInverted ? new Point(0, 0) : new Point(1, 0);

        // ===== Primary & Secondary Brush aus ResourceDictionary überschreiben =====

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

    public event PropertyChangedEventHandler PropertyChanged;
    private void OnPropertyChanged(string name) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
