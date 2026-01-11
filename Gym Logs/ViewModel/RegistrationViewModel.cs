using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gym_Logs.Model;
using Gym_Logs.View;
using Gym_Logs.Services;

namespace Gym_Logs.ViewModels
{
    public partial class RegistrationViewModel : ObservableObject
    {
        private readonly UserDatabase _userDb;

        [ObservableProperty]
        string email = "";

        [ObservableProperty]
        string password = "";

        public RegistrationViewModel(UserDatabase userDb)
        {
            _userDb = userDb;
        }

        [RelayCommand]
        public async Task Registrate()
        {
            var existingUser = await _userDb.GetByEmailAsync(Email);
            if (existingUser != null)
                return; // TODO: Message an UI

            var salt = SecurityHelper.GenerateSalt();
            var hash = SecurityHelper.HashPassword(Password, salt);

            var user = new User
            {
                Email = Email,
                PasswordSalt = salt,
                PasswordHash = hash
            };

            await _userDb.SaveAsync(user);
            await Shell.Current.GoToAsync(nameof(WorkoutCalendarView));
        }
    }
}
