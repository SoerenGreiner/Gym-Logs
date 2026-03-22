using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gym_Logs.Model.Database;
using Gym_Logs.Views.Pages;
using Gym_Logs.Services.Database;

namespace Gym_Logs.ViewModels.Pages
{
    public partial class RegistrationViewModel : ObservableObject
    {
        private readonly UserDatabase _userDb;

        [ObservableProperty]
        private string email = "";

        [ObservableProperty]
        private string password = "";

        public RegistrationViewModel()
        {
            // Nutzt jetzt die globale DB-Connection
            _userDb = App.UserDb;
        }

        [RelayCommand]
        public async Task Registrate()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                await App.Current.MainPage.DisplayAlert("Fehler", "Bitte Email und Passwort eingeben", "OK");
                return;
            }

            var existingUser = await _userDb.GetByEmailAsync(Email);
            if (existingUser != null)
            {
                await App.Current.MainPage.DisplayAlert("Fehler", "Dieser Nutzer existiert bereits", "OK");
                return;
            }

            var salt = SecurityHelper.GenerateSalt();
            var hash = SecurityHelper.HashPassword(Password, salt);

            var user = new User
            {
                Email = Email,
                PasswordSalt = salt,
                PasswordHash = hash
            };

            await _userDb.SaveAsync(user);

            await SecureStorage.SetAsync("CurrentUserId", user.ID.ToString());

            await Shell.Current.GoToAsync($"//{nameof(WorkoutCalendarView)}");
        }
    }
}