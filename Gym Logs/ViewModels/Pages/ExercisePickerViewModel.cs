using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Gym_Logs.Model.Database;
using Gym_Logs.Services.Database;
using Gym_Logs.Model.System;

namespace Gym_Logs.ViewModels.Pages
{
    public partial class ExercisePickerViewModel : ObservableObject
    {
        private readonly ExerciseDatabase _exerciseDb;

        [ObservableProperty]
        private string searchText;

        [ObservableProperty]
        private ObservableCollection<Exercise> filteredExercises = new();

        public ObservableCollection<Exercise> AllExercises { get; set; } = new();

        [ObservableProperty]
        private ObservableCollection<ExerciseDisplayModel> exercises = new();

        public ExercisePickerViewModel(ExerciseDatabase exerciseDb)
        {
            _exerciseDb = exerciseDb;
            Init();
        }

        /// <summary>
        /// Lädt alle aktiven Exercises aus der DB.
        /// </summary>
        private async void Init()
        {
            var list = await _exerciseDb.GetActiveAsync();
            AllExercises = new ObservableCollection<Exercise>(list);
            FilteredExercises = new ObservableCollection<Exercise>(list);
        }

        /// <summary>
        /// Filtert die Exercises nach dem Suchtext.
        /// Wird automatisch beim Ändern von SearchText aufgerufen.
        /// </summary>
        [RelayCommand]
        partial void OnSearchTextChanged(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                FilteredExercises = new ObservableCollection<Exercise>(AllExercises);
            }
            else
            {
                FilteredExercises = new ObservableCollection<Exercise>(
                    AllExercises.Where(x => x.Name.Contains(value, StringComparison.OrdinalIgnoreCase))
                );
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Nutzer eine Exercise auswählt.
        /// Rückgabe an WorkoutView erfolgt über Navigation oder Event.
        /// </summary>
        [RelayCommand]
        public void SelectExercise(Exercise exercise)
        {
            // TODO: Navigation zurück oder Event an WorkoutViewModel
        }

        private async Task LoadExercises()
        {
            var list = await App.ExerciseDb.GetActiveAsync();
            Exercises = new ObservableCollection<ExerciseDisplayModel>(
                list.Select(e => new ExerciseDisplayModel { Exercise = e })
            );
        }
    }
}
