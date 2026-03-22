using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Gym_Logs.Model.Database;
using Gym_Logs.Model.System;
using Gym_Logs.Services.Database;
using Gym_Logs.Views.Pages;
using Gym_Logs;

public partial class WorkoutViewModel : ObservableObject
{
    private readonly WorkoutDayDatabase _workoutDayDb;
    private readonly WorkoutEntryDatabase _entryDb;

    private int _currentUserId;
    private WorkoutDay _currentWorkoutDay;

    [ObservableProperty]
    private ObservableCollection<WorkoutEntry> entries = new();

    [ObservableProperty]
    private ObservableCollection<ExerciseDisplayModel> exercises = new();

    public WorkoutViewModel()
    {
        _workoutDayDb = App.WorkoutDayDb;
        _entryDb = App.WorkoutEntryDb;

        Init();
    }

    /// <summary>
    /// Initialisiert das Workout für den gewählten Tag.
    /// Lädt den WorkoutDay oder legt ihn neu an, dann lädt alle Einträge.
    /// </summary>
    private async void Init()
    {
        try
        {
            var userIdStr = await SecureStorage.GetAsync("CurrentUserId");
            if (string.IsNullOrWhiteSpace(userIdStr))
            {
                Debug.WriteLine("❌ Kein UserId gefunden in SecureStorage!");
                return;
            }

            _currentUserId = int.Parse(userIdStr);

            // Datum aus Query oder heute als Default
            var date = GetDateFromQuery();

            // WorkoutDay laden oder neu erstellen
            _currentWorkoutDay = await _workoutDayDb.GetByDateAsync(_currentUserId, date);
            if (_currentWorkoutDay == null)
            {
                _currentWorkoutDay = new WorkoutDay
                {
                    UserId = _currentUserId,
                    Date = date
                };
                await _workoutDayDb.SaveAsync(_currentWorkoutDay);
            }

            // WorkoutEntries laden
            await LoadEntries();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"❌ Init Fehler: {ex.Message}");
        }
    }

    /// <summary>
    /// Lädt alle Einträge für den aktuellen WorkoutDay.
    /// </summary>
    private async Task LoadEntries()
    {
        var entriesList = await _entryDb.GetByWorkoutDayAsync(_currentWorkoutDay.Id);

        var displayList = new List<ExerciseDisplayModel>();

        foreach (var entry in entriesList)
        {
            var exercise = await App.ExerciseDb.GetByIdAsync(entry.ExerciseId);
            if (exercise != null)
            {
                displayList.Add(new ExerciseDisplayModel
                {
                    Exercise = exercise,
                    Entry = entry
                });
            }
        }

        Exercises = new ObservableCollection<ExerciseDisplayModel>(displayList);
    }

    /// <summary>
    /// Fügt einen neuen WorkoutEntry hinzu.
    /// Speichert nur die tagesbezogene Referenz zur Exercise.
    /// </summary>
    [RelayCommand]
    public async Task AddEntry(int exerciseId)
    {
        if (_currentWorkoutDay == null)
            return;

        var entry = new WorkoutEntry
        {
            WorkoutDayId = _currentWorkoutDay.Id,
            ExerciseId = exerciseId,
            Order = Entries.Count
        };

        await _entryDb.SaveAsync(entry);
        Entries.Add(entry);
    }

    /// <summary>
    /// Hilfsmethode, um das Datum aus der Query-Parameterübergabe der Shell zu holen.
    /// </summary>
    private DateTime GetDateFromQuery()
    {
        // TODO: Hier QueryProperty aus Shell abgreifen, aktuell einfach heute
        return DateTime.Today;
    }

    [RelayCommand]
    private async Task AddNewEntry()
    {
        // Navigation zu ExercisePickerView, um eine Übung auszuwählen
        await Shell.Current.GoToAsync(nameof(ExercisePickerView));
    }
}