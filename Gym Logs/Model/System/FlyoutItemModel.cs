using System.Collections.ObjectModel;
using Gym_Logs.Enums;

namespace Gym_Logs.Model.System
{
    /// <summary>
    /// Represents a flyout menu item in the application shell.
    /// Can contain a title, icon, route, and optional sub-items.
    /// </summary>
    public class FlyoutItemModel
    {
        /// <summary>
        /// Gets or sets the display title of the flyout item.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the icon filename or resource associated with the item.
        /// Optional; can be null.
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Gets or sets the navigation route for this item.
        /// </summary>
        public FlyoutRouteEnum Route { get; set; }

        /// <summary>
        /// Gets the collection of sub-items (nested menu items) for this flyout item.
        /// </summary>
        public ObservableCollection<FlyoutItem> SubItems { get; set; } = new();

        /// <summary>
        /// Default constructor. Initializes an empty flyout item.
        /// </summary>
        public FlyoutItemModel() { }

        /// <summary>
        /// Initializes a new instance of <see cref="FlyoutItemModel"/> with the specified title, route, and optional icon.
        /// </summary>
        /// <param name="title">The display title of the item.</param>
        /// <param name="route">The navigation route associated with this item.</param>
        /// <param name="icon">Optional icon filename or resource.</param>
        public FlyoutItemModel(string title, FlyoutRouteEnum route, string icon = null)
        {
            Title = title;
            Route = route;
            Icon = icon;
        }
    }
}