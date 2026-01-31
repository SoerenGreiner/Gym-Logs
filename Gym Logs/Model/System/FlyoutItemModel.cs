using System.Collections.ObjectModel;

namespace Gym_Logs.Model.System
{
    public enum FlyoutRoute
    {
        RegistrationView,
        WorkoutCalendarView,
        PersonalView,
        StatisticView,
        SettingsView
    }

    public class FlyoutItemModel
    {
        public string Title { get; set; }
        public string Icon { get; set; }
        public FlyoutRoute Route { get; set; }

        public ObservableCollection<FlyoutItem> SubItems { get; set; } = new();

        public FlyoutItemModel() { }

        public FlyoutItemModel(string title, FlyoutRoute route, string icon = null)
        {
            Title = title;
            Route = route;
            Icon = icon;
        }
    }
}
