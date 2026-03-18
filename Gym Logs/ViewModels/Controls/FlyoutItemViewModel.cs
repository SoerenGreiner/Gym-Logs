using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gym_Logs.ViewModels.Pages;
using Gym_Logs.Model.System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace Gym_Logs.ViewModels
{
    /// <summary>
    /// ViewModel für ein einzelnes Flyout-Item in der AppShell.
    /// Kapselt das <see cref="FlyoutItemModel"/> und ermöglicht Navigation beim Tippen.
    /// </summary>
    public partial class FlyoutItemViewModel : ObservableObject
    {
        /// <summary>
        /// Das zugrunde liegende Flyout-Item-Modell.
        /// </summary>
        public FlyoutItemModel Model { get; }

        /// <summary>
        /// Zeigt an, ob das Item gerade gedrückt wird (für visuelles Feedback).
        /// </summary>
        [ObservableProperty]
        private bool isPressed;

        /// <summary>
        /// Der angezeigte Titel des Flyout-Items.
        /// </summary>
        public string Title => Model.Title;

        /// <summary>
        /// Das Symbol des Flyout-Items.
        /// </summary>
        public string Icon => Model.Icon;

        /// <summary>
        /// Erstellt eine neue Instanz des FlyoutItemViewModels basierend auf dem gegebenen Modell.
        /// </summary>
        /// <param name="model">Das Flyout-Item-Modell, das angezeigt und navigiert werden soll.</param>
        public FlyoutItemViewModel(FlyoutItemModel model)
        {
            Model = model;
        }

        /// <summary>
        /// Führt die Navigation zum Ziel des Flyout-Items aus.
        /// Setzt kurzzeitig <see cref="IsPressed"/> auf true für visuelles Feedback.
        /// </summary>
        [RelayCommand]
        public async Task Navigate()
        {
            IsPressed = true;

            // Navigation zu Route des Modells
            await Shell.Current.GoToAsync(Model.Route.ToString());

            // Flyout schließen
            Shell.Current.FlyoutIsPresented = false;

            // Kurzes Delay für visuelles Feedback
            await Task.Delay(120);
            IsPressed = false;
        }
    }
}