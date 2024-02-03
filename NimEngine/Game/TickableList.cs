using NimEngine.Interfaces;

namespace NimEngine.Game
{
    public static class TickableList
    {
        static List<ITickable> _active = new List<ITickable>();

        public static void Add(ITickable ticker)
        {
            _active.Add(ticker);
        }

        public static void Remove(ITickable ticker)
        {
            _active.Remove(ticker);
        }

        public static void Clear()
        {
            _active.Clear();
        }

        public static void UpdateAll()
        {
            foreach (ITickable ticker in _active)
            {
                ticker.Tick();
            }
        }

    }
}
