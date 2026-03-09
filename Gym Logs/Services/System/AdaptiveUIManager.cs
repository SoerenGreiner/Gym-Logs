using Microsoft.Maui.Devices;

namespace Gym_Logs.Services.System;

public class AdaptiveUIManager
{
    private static AdaptiveUIManager _instance;

    public static AdaptiveUIManager Instance =>
        _instance ??= new AdaptiveUIManager();

    private AdaptiveUIManager() { }

    public void Initialize()
    {
        CalculateScale();
        RegisterEvents();
        ApplyResources();
    }

    public double UIScale { get; private set; } = 1;

    private void CalculateScale()
    {
        var width = DeviceDisplay.MainDisplayInfo.Width /
                    DeviceDisplay.MainDisplayInfo.Density;

        UIScale = width switch
        {
            < 600 => 1.0,
            < 900 => 1.15,
            < 1200 => 1.25,
            _ => 1.35
        };
    }

    private void RegisterEvents()
    {
        DeviceDisplay.MainDisplayInfoChanged += (_, __) =>
        {
            CalculateScale();
            ApplyResources();
        };
    }

    private void ApplyResources()
    {
        if (Application.Current?.Resources == null)
            return;

        Application.Current.Resources["UIScale"] = UIScale;
    }
}
