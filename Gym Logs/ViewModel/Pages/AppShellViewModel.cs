using Gym_Logs.Model.System;
using Gym_Logs.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Gym_Logs.ViewModel
{
    public partial class AppShellViewModel : ObservableObject
    {
        public ObservableCollection<FlyoutItemModel> Items { get; }

        public AppShellViewModel()
        {
            Items = new ObservableCollection<FlyoutItemModel>
            {
                new("Calendar", FlyoutRoute.WorkoutCalendarView, "calendar.png"),
                new("Personal", FlyoutRoute.PersonalView, "personal.png"),
                new("Statistics", FlyoutRoute.StatisticView, "stats.png"),
                new("Settings", FlyoutRoute.SettingsView, "settings.png"),
            };

            Routing.RegisterRoute(FlyoutRoute.WorkoutCalendarView.ToString(), typeof(WorkoutCalendarView));
            Routing.RegisterRoute(FlyoutRoute.PersonalView.ToString(), typeof(PersonalView));
            Routing.RegisterRoute(FlyoutRoute.StatisticView.ToString(), typeof(StatisticView));
            Routing.RegisterRoute(FlyoutRoute.SettingsView.ToString(), typeof(SettingsView));
        }

        [RelayCommand]
        async Task Navigate(FlyoutItemModel item)
        {
            if (item == null) return;

            await Shell.Current.GoToAsync(item.Route.ToString());
            Shell.Current.FlyoutIsPresented = false;
        }

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
                await Shell.Current.GoToAsync($"//{FlyoutRoute.RegistrationView}");
            }
            else
            {
                await Shell.Current.GoToAsync($"//{FlyoutRoute.WorkoutCalendarView}");
            }
        }
    }
}
