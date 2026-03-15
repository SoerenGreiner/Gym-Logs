using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gym_Logs.Model.System;
using Gym_Logs.View.Pages;
using Gym_Logs.Services.System;
using Gym_Logs.Enums;
using Microsoft.Maui.Storage;
using System;

namespace Gym_Logs.ViewModel
{
    /// <summary>
    /// ViewModel for the Settings page.
    /// Handles theme selection, brightness level, brush inversion, and gradient direction preferences.
    /// </summary>
    public partial class SettingsViewModel : ObservableObject
    {
        // ===== Preference Keys =====
        private const string BrightnessPrefKey = "BrightnessLevel";
        private const string InvertBrushPrefKey = "InvertBrush";
        private const string InvertStartPointsPrefKey = "InvertBrushStartPoints";

        private readonly ThemeService _themeService;

        // ===== Observable Properties =====

        /// <summary>
        /// The currently active theme.
        /// Updates automatically when the ThemeService changes.
        /// </summary>
        [ObservableProperty]
        private AppThemeModel currentTheme;

        /// <summary>
        /// Whether the Light brightness option is selected.
        /// </summary>
        [ObservableProperty]
        bool isLightSelected;

        /// <summary>
        /// Whether the Normal brightness option is selected.
        /// </summary>
        [ObservableProperty]
        bool isNormalSelected;

        /// <summary>
        /// Whether the Dark brightness option is selected.
        /// </summary>
        [ObservableProperty]
        bool isDarkSelected;

        /// <summary>
        /// Whether the brush colors are inverted.
        /// </summary>
        [ObservableProperty]
        private bool isBrushInverted;

        /// <summary>
        /// Whether the gradient start points are inverted.
        /// </summary>
        [ObservableProperty]
        private bool areBrushStartPointsInverted;

        /// <summary>
        /// Initializes a new instance of <see cref="SettingsViewModel"/>.
        /// Loads saved theme and preferences, subscribes to ThemeService changes.
        /// </summary>
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

            var savedBrightness = Preferences.Default.Get(BrightnessPrefKey, ThemeBrightnessEnum.Normal.ToString());
            if (!Enum.TryParse(savedBrightness, out ThemeBrightnessEnum brightness))
                brightness = ThemeBrightnessEnum.Normal;

            switch (brightness)
            {
                case ThemeBrightnessEnum.Light:
                    IsLightSelected = true;
                    break;
                case ThemeBrightnessEnum.Normal:
                    IsNormalSelected = true;
                    break;
                case ThemeBrightnessEnum.Dark:
                    IsDarkSelected = true;
                    break;
            }

            // Sync ThemeService state
            _themeService.IsBrushInverted = IsBrushInverted;
            _themeService.IsGradientDirectionInverted = AreBrushStartPointsInverted;

            if (CurrentTheme != null)
                _themeService.ApplyTheme(CurrentTheme);
        }

        // ================= Brightness Changes =================

        /// <summary>
        /// Called when <see cref="IsLightSelected"/> changes.
        /// Updates the ThemeService and saves preference.
        /// </summary>
        /// <param name="value">True if selected.</param>
        partial void OnIsLightSelectedChanged(bool value)
        {
            if (value)
            {
                _themeService.SetBrightness(ThemeBrightnessEnum.Light);
                Preferences.Default.Set(BrightnessPrefKey, ThemeBrightnessEnum.Light.ToString());
            }
        }

        /// <summary>
        /// Called when <see cref="IsNormalSelected"/> changes.
        /// Updates the ThemeService and saves preference.
        /// </summary>
        /// <param name="value">True if selected.</param>
        partial void OnIsNormalSelectedChanged(bool value)
        {
            if (value)
            {
                _themeService.SetBrightness(ThemeBrightnessEnum.Normal);
                Preferences.Default.Set(BrightnessPrefKey, ThemeBrightnessEnum.Normal.ToString());
            }
        }

        /// <summary>
        /// Called when <see cref="IsDarkSelected"/> changes.
        /// Updates the ThemeService and saves preference.
        /// </summary>
        /// <param name="value">True if selected.</param>
        partial void OnIsDarkSelectedChanged(bool value)
        {
            if (value)
            {
                _themeService.SetBrightness(ThemeBrightnessEnum.Dark);
                Preferences.Default.Set(BrightnessPrefKey, ThemeBrightnessEnum.Dark.ToString());
            }
        }

        // ================= Brush Inversion =================

        /// <summary>
        /// Called when <see cref="IsBrushInverted"/> changes.
        /// Updates ThemeService and saves preference.
        /// </summary>
        /// <param name="value">True if brush inversion is enabled.</param>
        partial void OnIsBrushInvertedChanged(bool value)
        {
            _themeService.IsBrushInverted = value;

            if (CurrentTheme != null)
                _themeService.ApplyTheme(CurrentTheme);

            Preferences.Default.Set(InvertBrushPrefKey, value);
        }

        // ================= Gradient Direction =================

        /// <summary>
        /// Called when <see cref="AreBrushStartPointsInverted"/> changes.
        /// Updates ThemeService and saves preference.
        /// </summary>
        /// <param name="value">True if gradient start points inversion is enabled.</param>
        partial void OnAreBrushStartPointsInvertedChanged(bool value)
        {
            _themeService.IsGradientDirectionInverted = value;

            if (CurrentTheme != null)
                _themeService.ApplyTheme(CurrentTheme);

            Preferences.Default.Set(InvertStartPointsPrefKey, value);
        }

        // ================= Commands =================

        /// <summary>
        /// Opens the Theme Selection page.
        /// </summary>
        [RelayCommand]
        void OpenThemeSelection()
        {
            Shell.Current.GoToAsync(nameof(ThemeSelectionView));
        }
    }
}