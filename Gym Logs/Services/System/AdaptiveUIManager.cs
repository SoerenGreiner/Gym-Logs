using Microsoft.Maui.Devices;

namespace Gym_Logs.Services.System;

/// <summary>
/// Singleton manager that calculates and maintains the adaptive UIScale
/// for responsive layouts based on device screen width.
/// </summary>
public class AdaptiveUIManager
{
    private static AdaptiveUIManager _instance;

    /// <summary>
    /// Gets the singleton instance of <see cref="AdaptiveUIManager"/>.
    /// </summary>
    public static AdaptiveUIManager Instance =>
        _instance ??= new AdaptiveUIManager();

    /// <summary>
    /// Private constructor to enforce singleton pattern.
    /// </summary>
    private AdaptiveUIManager() { }

    /// <summary>
    /// Initializes the UIScale, registers device display events,
    /// and applies the scale to the application resources.
    /// </summary>
    public void Initialize()
    {
        CalculateScale();
        RegisterEvents();
        ApplyResources();
    }

    /// <summary>
    /// The current UI scale factor used to scale fonts, padding, widths, and heights.
    /// </summary>
    public double UIScale { get; private set; } = 1;

    /// <summary>
    /// Calculates the UIScale based on the current device screen width.
    /// </summary>
    private void CalculateScale()
    {
        var width = DeviceDisplay.MainDisplayInfo.Width /
                    DeviceDisplay.MainDisplayInfo.Density;

        UIScale = width switch
        {
            < 600 => 1.0,    // Small phones
            < 900 => 1.15,   // Large phones / small tablets
            < 1200 => 1.25,  // Tablets
            _ => 1.35        // Desktop / large screens
        };
    }

    /// <summary>
    /// Registers a callback to recalculate UIScale when the device display changes
    /// (e.g., rotation, window resizing) and reapplies resources.
    /// </summary>
    private void RegisterEvents()
    {
        DeviceDisplay.MainDisplayInfoChanged += (_, __) =>
        {
            CalculateScale();
            ApplyResources();
        };
    }

    /// <summary>
    /// Applies the current UIScale to the application's resource dictionary
    /// so that it can be bound in XAML.
    /// </summary>
    private void ApplyResources()
    {
        if (Application.Current?.Resources == null)
            return;

        Application.Current.Resources["UIScale"] = UIScale;
    }
}