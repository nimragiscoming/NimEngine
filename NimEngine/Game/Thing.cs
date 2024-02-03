using NimEngine.Graphics;
using NimEngine.Interfaces;

namespace NimEngine.Game
{
    public abstract class Thing : ITickable, IRenderable
    {
        public Coord pos = Coord.Zero;

        Texture2D tex;

        public void Create()
        {
            TickableList.Add(this);
            RenderableList.Add(this);
        }

        public void Destroy()
        {
            TickableList.Remove(this);
            RenderableList.Remove(this);
        }

        public virtual void Tick()
        {
        }

        public virtual void Draw()
        {
            Renderer.Draw(new DrawCall(tex, pos));
        }

        public virtual void OnRenderTargetReset()
        {
        }
    }
}
