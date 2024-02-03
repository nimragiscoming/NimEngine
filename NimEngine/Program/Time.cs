using NimEngine.Game;
using System.Diagnostics;
using System.Timers;

namespace NimEngine.Program
{
    public static class Time
    {
        //Game Ticks
        private static int _ticks = 0;
        public static int Ticks => _ticks;

        public const int tps = 120;

        public const float timestep = 1 / (float)tps;

        public const float timestepms = timestep * 1000;


        //Global Ticks
        public static float delta { get; private set; } // is the actual time since last frame was rendered


        private static Stopwatch frameCounter;

        private static System.Timers.Timer ticktimer;

        public static void SetDelta(float _delta)
        {
            delta = _delta;
        }

        public static void StartTime()
        {
            frameCounter = Stopwatch.StartNew();

            ticktimer = new System.Timers.Timer(timestepms);

            // Hook up the Elapsed event for the timer. 
            ticktimer.Elapsed += Update;
            ticktimer.AutoReset = true;
            ticktimer.Enabled = true;
        }

        public static void UpdateTime()
        {
            delta = frameCounter.ElapsedTicks / Stopwatch.Frequency;

        //    Console.WriteLine(GC.GetTotalMemory(true) / 1000000 + "mb");

         //   Console.WriteLine(frameCounter.ElapsedMilliseconds + " MS");

            frameCounter.Restart();
        }

        public static void Increment()
        {
            _ticks++;
        }

        public static void Update(object source, ElapsedEventArgs e)
        {
            Increment();
            TickableList.UpdateAll();
        }
    }
}
