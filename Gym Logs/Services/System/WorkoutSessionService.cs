using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Logs.Services.System
{
    public class WorkoutSessionService
    {
        public bool IsRunning { get; private set; }
        public DateTime StartTime { get; private set; }

        public TimeSpan Elapsed =>
            IsRunning ? DateTime.Now - StartTime : TimeSpan.Zero;

        public void Start()
        {
            StartTime = DateTime.Now;
            IsRunning = true;
        }

        public void Stop()
        {
            IsRunning = false;
        }
    }
}
