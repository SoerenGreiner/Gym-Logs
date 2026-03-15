using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace Gym_Logs.Model.System
{
    /// <summary>
    /// Represents a group of related themes within a category.
    /// Each group contains multiple <see cref="AppThemeModel"/> instances.
    /// </summary>
    public partial class ThemeGroupModel : ObservableObject
    {
        /// <summary>
        /// The display name of the group (e.g., "Dark Variants").
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Collection of themes that belong to this group.
        /// </summary>
        public ObservableCollection<AppThemeModel> Themes { get; set; } = new();

        /// <summary>
        /// Indicates whether the group is currently expanded in the UI.
        /// Useful for controlling visibility in a hierarchical theme list.
        /// </summary>
        [ObservableProperty]
        private bool isExpanded;
    }
}