using Gym_Logs.Model.System;
using Gym_Logs.View;
using Gym_Logs.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Gym_Logs.ViewModel
{
    /// <summary>
    /// ViewModel for the AppShell.  
    /// Manages the Flyout menu items, navigation, and initial registration check.
    /// </summary>
    public partial class AppShellViewModel : ObservableObject
    {
        /// <summary>
        /// Collection of items shown in the Flyout menu.
        /// </summary>
        public ObservableCollection<FlyoutItemModel> Items { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="AppShellViewModel"/>.
        /// Registers routes and populates the Flyout menu items.
        /// </summary>
        public AppShellViewModel()
        {
            // Initialize Flyout menu items
            Items = new ObservableCollection<FlyoutItemModel>
            {
                new("Calendar", FlyoutRouteEnum.WorkoutCalendarView, "calendar.png"),
                new("Personal", FlyoutRouteEnum.PersonalView, "personal.png"),
                new("Statistics", FlyoutRouteEnum.StatisticView, "stats.png"),
                new("Settings", FlyoutRouteEnum.SettingsView, "settings.png"),
            };

            // Register Shell routes for navigation
            Routing.RegisterRoute(FlyoutRouteEnum.WorkoutCalendarView.ToString(), typeof(WorkoutCalendarView));
            Routing.RegisterRoute(FlyoutRouteEnum.PersonalView.ToString(), typeof(PersonalView));
            Routing.RegisterRoute(FlyoutRouteEnum.StatisticView.ToString(), typeof(StatisticView));
            Routing.RegisterRoute(FlyoutRouteEnum.SettingsView.ToString(), typeof(SettingsView));
        }

        /// <summary>
        /// Command to navigate to a selected Flyout item.
        /// Closes the Flyout menu after navigation.
        /// </summary>
        /// <param name="item">The Flyout menu item to navigate to.</param>
        [RelayCommand]
        async Task Navigate(FlyoutItemModel item)
        {
            if (item == null) return;

            await Shell.Current.GoToAsync(item.Route.ToString());
            Shell.Current.FlyoutIsPresented = false;
        }

        /// <summary>
        /// Checks whether the user is registered.  
        /// Redirects to RegistrationView if not registered, otherwise to WorkoutCalendarView.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task CheckRegistrationStatusAsync()
        {
            bool isRegistered = false;

            try
            {
                var value = await SecureStorage.GetAsync("IsRegistered");
                isRegistered = value == "true";
            }
            catch
            {
                isRegistered = false;
            }

            if (!isRegistered)
            {
                await Shell.Current.GoToAsync($"//{FlyoutRouteEnum.RegistrationView}");
            }
            else
            {
                await Shell.Current.GoToAsync($"//{FlyoutRouteEnum.WorkoutCalendarView}");
            }
        }
    }
}