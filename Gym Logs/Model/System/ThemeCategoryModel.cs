using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace Gym_Logs.Model.System
{
    /// <summary>
    /// Represents a category of themes in the application.
    /// Each category contains multiple <see cref="ThemeGroupModel"/> groups.
    /// </summary>
    public partial class ThemeCategoryModel : ObservableObject
    {
        /// <summary>
        /// The display name of the category (e.g., "Light Themes", "Dark Themes").
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Collection of theme groups contained within this category.
        /// </summary>
        public ObservableCollection<ThemeGroupModel> Themes { get; set; } = new();

        /// <summary>
        /// Indicates whether the category is currently expanded in the UI.
        /// This controls visibility of its contained <see cref="ThemeGroupModel"/> items.
        /// </summary>
        [ObservableProperty]
        private bool isExpanded;
    }
}