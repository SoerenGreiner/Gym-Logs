using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace Gym_Logs.Model.System
{
    public partial class ThemeCategoryModel : ObservableObject
    {
        public string Name { get; set; }

        public ObservableCollection<AppThemeModel> Themes { get; set; } = new();

        [ObservableProperty]
        bool isExpanded;
    }
}
