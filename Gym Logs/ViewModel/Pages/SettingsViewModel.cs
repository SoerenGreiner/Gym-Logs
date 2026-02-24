using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gym_Logs.Model.System;
using Gym_Logs.View.Pages;
using Gym_Logs.Services.System;
using Gym_Logs.Enums;
using Microsoft.Maui.Storage;
using System;

namespace Gym_Logs.ViewModel;

public partial class SettingsViewModel : ObservableObject
{
    private const string BrightnessPrefKey = "BrightnessLevel";
    private const string InvertBrushPrefKey = "InvertBrush";
    private const string InvertStartPointsPrefKey = "InvertBrushStartPoints";

    private readonly ThemeService _themeService;

    [ObservableProperty]
    private AppThemeModel currentTheme;

    [ObservableProperty]
    bool isLightSelected;

    [ObservableProperty]
    bool isNormalSelected;

    [ObservableProperty]
    bool isDarkSelected;

    [ObservableProperty]
    private bool isBrushInverted;

    [ObservableProperty]
    private bool areBrushStartPointsInverted;

    public SettingsViewModel()
    {
        _themeService = App.ThemeService;

        if (_themeService.CurrentTheme == null)
            _themeService.LoadSavedTheme();

        CurrentTheme = _themeService.CurrentTheme;

        _themeService.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(_themeService.CurrentTheme))
                CurrentTheme = _themeService.CurrentTheme;
        };

        // ===== Load Preferences =====

        IsBrushInverted = Preferences.Default.Get(InvertBrushPrefKey, false);
        AreBrushStartPointsInverted = Preferences.Default.Get(InvertStartPointsPrefKey, false);

        var savedBrightness = Preferences.Default.Get(BrightnessPrefKey, ThemeBrightness.Normal.ToString());

        if (!Enum.TryParse(savedBrightness, out ThemeBrightness brightness))
            brightness = ThemeBrightness.Normal;

        switch (brightness)
        {
            case ThemeBrightness.Light:
                IsLightSelected = true;
                break;

            case ThemeBrightness.Normal:
                IsNormalSelected = true;
                break;

            case ThemeBrightness.Dark:
                IsDarkSelected = true;
                break;
        }

        // Sync ThemeService State
        _themeService.IsBrushInverted = IsBrushInverted;
        _themeService.IsGradientDirectionInverted = AreBrushStartPointsInverted;

        if (CurrentTheme != null)
            _themeService.ApplyTheme(CurrentTheme);
    }

    // ------------------ Brightness Changes ------------------
    partial void OnIsLightSelectedChanged(bool value)
    {
        if (value)
        {
            _themeService.SetBrightness(ThemeBrightness.Light);
            Preferences.Default.Set(BrightnessPrefKey, ThemeBrightness.Light.ToString());
        }
    }

    partial void OnIsNormalSelectedChanged(bool value)
    {
        if (value)
        {
            _themeService.SetBrightness(ThemeBrightness.Normal);
            Preferences.Default.Set(BrightnessPrefKey, ThemeBrightness.Normal.ToString());
        }
    }

    partial void OnIsDarkSelectedChanged(bool value)
    {
        if (value)
        {
            _themeService.SetBrightness(ThemeBrightness.Dark);
            Preferences.Default.Set(BrightnessPrefKey, ThemeBrightness.Dark.ToString());
        }
    }

    // ------------------ Invert Brush Toggle ------------------
    partial void OnIsBrushInvertedChanged(bool value)
    {
        _themeService.IsBrushInverted = value;

        if (CurrentTheme != null)
            _themeService.ApplyTheme(CurrentTheme);

        Preferences.Default.Set(InvertBrushPrefKey, value);
    }

    // ------------------ Invert Brush Start Points ------------------
    partial void OnAreBrushStartPointsInvertedChanged(bool value)
    {
        _themeService.IsGradientDirectionInverted = value;

        if (CurrentTheme != null)
            _themeService.ApplyTheme(CurrentTheme);

        Preferences.Default.Set(InvertStartPointsPrefKey, value);
    }

    // ------------------ Commands ------------------
    [RelayCommand]
    void OpenThemeSelection()
    {
        Shell.Current.GoToAsync(nameof(ThemeSelectionView));
    }
}


