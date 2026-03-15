using Microsoft.Maui.Devices;

namespace Gym_Logs.Services.System;

/// <summary>
/// Calculates scaled UI values (FontSize, Padding, Width/Height) and provides 
/// adaptive grid information for responsive layouts in MAUI applications.
/// </summary>
public class LayoutCalculator
{
    private readonly AdaptiveUIManager _uiManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="LayoutCalculator"/> class
    /// and binds the singleton <see cref="AdaptiveUIManager"/> instance.
    /// </summary>
    public LayoutCalculator()
    {
        _uiManager = AdaptiveUIManager.Instance;
    }

    /// <summary>
    /// Scales a base value (e.g., FontSize, Padding, Width/Height) according to the UIScale.
    /// </summary>
    /// <param name="baseValue">The base value to scale.</param>
    /// <returns>The scaled value based on the current UIScale.</returns>
    public double Scale(double baseValue)
    {
        return baseValue * _uiManager.UIScale;
    }

    /// <summary>
    /// Calculates the optimal number of columns for Grids or CollectionViews
    /// based on the screen width.
    /// </summary>
    /// <returns>The recommended number of columns for the current device screen.</returns>
    public int GetGridColumns()
    {
        var width = DeviceDisplay.MainDisplayInfo.Width /
                    DeviceDisplay.MainDisplayInfo.Density;

        return width switch
        {
            < 600 => 1,     // Small phones
            < 900 => 2,     // Large phones / small tablets
            < 1200 => 3,    // Tablets
            _ => 4          // Desktop / large screens
        };
    }

    /// <summary>
    /// Calculates the dynamic item width for Grids or CollectionViews.
    /// </summary>
    /// <param name="totalWidth">Total width of the container (e.g., Grid or CollectionView).</param>
    /// <param name="columns">Number of columns.</param>
    /// <param name="spacing">Spacing between items.</param>
    /// <returns>The calculated width of each item.</returns>
    public double GetItemWidth(double totalWidth, int columns, double spacing = 0)
    {
        if (columns <= 0) columns = 1; // Prevent division by zero
        return (totalWidth - (columns - 1) * spacing) / columns;
    }

    /// <summary>
    /// Calculates the item height either as a scaled base value or proportionally to the item width.
    /// </summary>
    /// <param name="baseHeight">The base height (optional if proportional=false).</param>
    /// <param name="itemWidth">The width of the item (optional if proportional=true).</param>
    /// <param name="proportional">Whether the height should be proportional to the width.</param>
    /// <param name="ratio">Height/width ratio when proportional=true.</param>
    /// <returns>The calculated height of the item.</returns>
    public double GetItemHeight(double baseHeight = 100, double itemWidth = 100, bool proportional = false, double ratio = 0.6)
    {
        if (proportional)
        {
            return itemWidth * ratio;
        }
        else
        {
            return Scale(baseHeight);
        }
    }
}
