using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Logs.Services.System;

public class LayoutCalculator
{
    private readonly AdaptiveUIManager _uiManager;

    public LayoutCalculator()
    {
        _uiManager = AdaptiveUIManager.Instance;
    }

    public double Scale(double baseValue)
    {
        return baseValue * _uiManager.UIScale;
    }

    public int GetGridColumns()
    {
        var width = DeviceDisplay.MainDisplayInfo.Width /
                    DeviceDisplay.MainDisplayInfo.Density;

        return width switch
        {
            < 600 => 1,
            < 900 => 2,
            _ => 3
        };
    }
}
