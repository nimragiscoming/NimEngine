using NimEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NimEngine.Game
{
    public static class UpdateableList
    {
        static List<IUpdateable> _active = new List<IUpdateable>();

        public static void Add(IUpdateable ticker)
        {
            _active.Add(ticker);
        }

        public static void Remove(IUpdateable ticker)
        {
            _active.Remove(ticker);
        }

        public static void Clear()
        {
            _active.Clear();
        }

        public static void UpdateAll()
        {
            foreach (IUpdateable ticker in _active)
            {
                ticker.Update();
            }
        }
    }
}
