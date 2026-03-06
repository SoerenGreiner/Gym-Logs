using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Logs.Model.System
{
    public partial class ThemeGroupModel : ObservableObject
    {
        public string Name { get; set; }

        public ObservableCollection<AppThemeModel> Themes { get; set; } = new();

        [ObservableProperty]
        bool isExpanded;
    }
}
