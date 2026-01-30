using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Logs.Model.Database
{
    public class Exercise
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; } = "";

        // Optional: Gerätenummer im Studio
        public string? MachineNumber { get; set; }

        // Foto vom Gerät (vom Nutzer aufgenommen)
        public string? ImagePath { get; set; }

        // Typ
        public bool IsStrength { get; set; }
        public bool IsCardio { get; set; }
        public bool IsArchived { get; set; } = false;

        // Meta
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
