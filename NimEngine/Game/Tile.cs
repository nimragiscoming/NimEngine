using NimEngine.Graphics;
using NimEngine.Interfaces;

namespace NimEngine.Game
{
    public class Tile : ITickable
    {
        public TileDef def;
        public Glyph sym;
        public Colour col = Colour.white;

        private DrawCall? cachedDefaultCall = null;

        public void Create()
        {
            TickableList.Add(this);
        }

        public void Tick()
        {
        }

        public DrawCall GetCall()
        {
            if(cachedDefaultCall == null)
            {
                cachedDefaultCall = DrawCall.Create(Renderer.t[sym.Index]).Coloured(col);
            }
            return (DrawCall)cachedDefaultCall;
        }

        public Tile(int sym)
        {
            this.sym = new Glyph(sym);
        }
    }
}
