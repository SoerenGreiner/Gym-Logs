using SQLite;

namespace Gym_Logs.Model.Database
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public string? PasswordSalt { get; set; }
    }
}