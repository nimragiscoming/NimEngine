using NimEngine.Game;
using NimEngine.Graphics;


namespace NimEngine.Interfaces
{
    public interface IRenderable
    {
        public abstract void Draw();

        public abstract void OnRenderTargetReset();
    }
}
