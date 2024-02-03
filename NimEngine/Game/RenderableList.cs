using NimEngine.Graphics;
using NimEngine.Interfaces;
using System.Diagnostics;

namespace NimEngine.Game
{
    public static class RenderableList
    {
        static List<IRenderable> _active = new List<IRenderable>();

        public static void Add(IRenderable render)
        {
            _active.Add(render);
        }

        public static void Remove(IRenderable render)
        {
            _active.Remove(render);
        }

        public static void Clear()
        {
            _active.Clear();
        }

        public static List<DrawCall> GetCalls()
        {
            List<DrawCall> cal = new List<DrawCall>();
            foreach (IRenderable render in _active)
            {
                render.Draw();
            }
            return cal;
        }

        public static void OnRenderTargetReset()
        {
            foreach (IRenderable render in _active)
            {
                render.OnRenderTargetReset();
            }
        }
    }
}
