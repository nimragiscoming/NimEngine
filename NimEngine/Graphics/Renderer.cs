using NimEngine.Game;
using NimEngine.Program;
using System.Diagnostics;
using System.Numerics;
using static SDL2.SDL;

namespace NimEngine.Graphics
{
    public static class Renderer
    {

        public static Tileset t;

        public static Texture2D texture2D;

        public static RenderBuffer renderBuf = new RenderBuffer();

        static nint renderer => Pointers.renderer;

        public static nint Init(nint window)
        {
            nint renderer = SDL_CreateRenderer(window, -1, SDL_RendererFlags.SDL_RENDERER_ACCELERATED);

            SDL_SetRenderDrawColor(renderer, 0, 0, 0,255);  
            return renderer;
        }

        public static void LoadTextures()
        {
        //    string dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"sprites","Flibber.png");
        //    t = new Tileset("base", dir, 16, 32, 16);


            string dir = "C:/Users/super/Downloads/tester/base.png";
            t = new Tileset("base", dir, 16, 256, 256);

            texture2D = t[0];

            //    BounceBall b = new BounceBall(new Vec2(1000,1000));
            //    b.Create();

            //    BounceBall b1 = new BounceBall(new Vec2(1500, 500));
            //    b1.Create();

            Map m = MapGenerator.Generate(53453);
            m.Create();
        }

        public static void Draw(DrawCall call)
        {
            renderBuf.Add(call);
        }

        public static void RenderFrame()
        {
            SDL_RenderClear(renderer);


            List<DrawCall> calls = renderBuf.Consume();
            foreach (DrawCall call in calls)
            {
                Graphics.DrawTexture(call);
            }

            SDL_RenderPresent(renderer);
        }
    }

    public class RenderBuffer
    {
        private List<DrawCall> calls = new List<DrawCall>();

        public void AddRange(IEnumerable<DrawCall> call)
        {
            calls.AddRange(call);

        }
        public void Add(DrawCall call)
        {
            calls.Add(call);

        }
        public void Add(Texture2D tex, Coord pos)
        {
            calls.Add(new DrawCall(tex, pos));
        }

        public List<DrawCall> Consume()
        {
            calls.AddRange(RenderableList.GetCalls());
            List<DrawCall> rtrn = calls;
            calls = new List<DrawCall>();
            return rtrn;
        }
    }

    public struct DrawCall
    {
        public Texture2D tex;
        public Coord pos;

        public Vec2 size;

        public Colour col = Colour.white;

        public Coord srcSize;
        public Coord srcXY;

        public void Render()
        {
            Graphics.DrawTexture(this);
        }

        public DrawCall(Texture2D _tex, Coord _pos)
        {
            tex = _tex;
            pos = _pos;
            size = new Vec2(_tex.Width, _tex.Height);
            srcXY = new Coord(tex.X, tex.Y);
            srcSize = new Coord(tex.Width, tex.Height);
        }

        public static DrawCall Create(Texture2D _tex)
        {
            return new DrawCall(_tex, Coord.Zero);
        }

        public DrawCall At(Coord _pos)
        {
            pos = _pos;
            return this;
        }

        public DrawCall Sized(Vec2 _size)
        {
            size = _size;
            return this;
        }

        public DrawCall Scaled(float scale)
        {
            size *= scale;
            return this;
        }

        public DrawCall Coloured(Colour colour)
        {
            col = colour;
            return this;
        }

        public DrawCall Clip(Coord XY, Coord size)
        {
            srcXY = XY;
            srcSize = size;
            return this;
        }

    }
}
