using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Logs.UI.Prototyping
{
    internal class CustomTabBarPrototypCode
    {
        /*[ObservableProperty]
        private bool isTabBarVisible;

        public ObservableCollection<TabBarItemModel> Tabs { get; set; } = new();

        public WorkoutViewModel()
        {
            BuildTabs();
            CurrentContent = workoutCreateView; // initial
        }

        private void SelectTab(TabBarItemModel tab)
        {
            foreach (var t in Tabs)
                t.IsSelected = false;

            tab.IsSelected = true;

            CurrentContent = tab.Action switch
            {
                TabBarActionEnum.WorkoutCreate => workoutCreateView,
                TabBarActionEnum.WorkoutLibrary => workoutLibraryView,
                _ => null
            };
        }

        private void BuildTabs()
        {
            Tabs.Clear();

            Tabs.Add(new TabBarItemModel
            {
                Title = "Home",
                Icon = "🏠",
                Action = TabBarActionEnum.WorkoutCreate,
                IsVisible = true,
                IsSelected = true,
                Command = new RelayCommand<TabBarItemModel>(SelectTab)
            });

            Tabs.Add(new TabBarItemModel
            {
                Title = "Stats",
                Icon = "📊",
                Action = TabBarActionEnum.WorkoutLibrary,
                IsVisible = true,
                IsSelected = false,
                Command = new RelayCommand<TabBarItemModel>(SelectTab)
            });

            IsTabBarVisible = Tabs.Count > 0;
        }*/
    }
}
