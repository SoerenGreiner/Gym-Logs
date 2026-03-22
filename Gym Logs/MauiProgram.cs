using Gym_Logs.Services.Database;
using Gym_Logs.Views.Controls;
using Gym_Logs.Views.Pages;
using Gym_Logs.Services.System;
using Gym_Logs.ViewModels.Pages;
using Microsoft.Extensions.Logging;
using SQLite;

namespace Gym_Logs
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            // === SQLite Setup ===
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "gym_logs.db3");
            var connection = new SQLiteAsyncConnection(dbPath);

            builder.Services.AddSingleton(connection);

            builder.Services.AddSingleton<UserDatabase>();
            builder.Services.AddSingleton<ExerciseDatabase>();
            builder.Services.AddSingleton<WorkoutDayDatabase>();
            builder.Services.AddSingleton<WorkoutEntryDatabase>();
            builder.Services.AddSingleton<BodyStatusDayDatabase>();

            builder.Services.AddSingleton<AppShellView>();
            builder.Services.AddSingleton<AppShellViewModel>();
            builder.Services.AddTransient<RegistrationView>();
            builder.Services.AddTransient<RegistrationViewModel>();
            builder.Services.AddTransient<WorkoutCalendarView>();
            builder.Services.AddTransient<WorkoutCalendarViewModel>();
            builder.Services.AddTransient<PersonalView>();
            builder.Services.AddTransient<PersonalViewModel>();
            builder.Services.AddTransient<SettingsView>();
            builder.Services.AddTransient<SettingsViewModel>();
            builder.Services.AddTransient<ThemeSelectionView>();
            builder.Services.AddTransient<ThemeSelectionViewModel>();
            builder.Services.AddTransient<StatisticView>();
            builder.Services.AddTransient<StatisticsViewModel>();
            builder.Services.AddTransient<WorkoutView>();
            builder.Services.AddTransient<WorkoutViewModel>();
            builder.Services.AddTransient<ExercisePickerView>();
            builder.Services.AddTransient<ExercisePickerViewModel>();

            builder.Services.AddSingleton<ThemeService>();
            builder.Services.AddSingleton<WorkoutSessionService>();
#if DEBUG

#if DEBUG
            builder.Logging.AddDebug();
#endif


            return builder.Build();
        }
    }
}
#endif