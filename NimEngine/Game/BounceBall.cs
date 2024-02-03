using NimEngine.Graphics;
using NimEngine.Program;
using System.Drawing;

namespace NimEngine.Game
{
    public class BounceBall : Thing
    {
        Vec2 velocity = new Vec2(1000, 1000);

        Vec2 fPos = new Vec2(0, 0);

        const float G = 0; //2000f;

        const float damping = 1; // 0.8f; 

        int texIndex;

        float size = 300;

        Colour col = Colour.white;

        public BounceBall(Vec2 vel)
        {
            velocity= vel;
        }

        public override void Tick()
        {
            base.Tick();

            fPos += velocity * Time.timestep;
            //    Console.WriteLine(velocity.X + " " + velocity.Y);

            velocity.y += Time.timestep * G;

            SDL2.SDL.SDL_DisplayMode SDm;

            int w;
            int h;



            SDL2.SDL.SDL_GetWindowSize(Pointers.window, out w, out h);

            if (fPos.x < 0)
            {
                fPos.x = 0;
                velocity.x *= -damping;

                texIndex = 1 - texIndex;
                Console.Beep(1000, 100);
                col = Colour.red;
            }
            if (fPos.x > w - size / 2)
            {
                fPos.x = w - size / 2;
                velocity.x *= -damping;
                texIndex = 1 - texIndex;
                Console.Beep(1000, 100);
                col = Colour.green;
            }

            if (fPos.y < 0)
            {
                fPos.y = 0;
                velocity.y *= -damping;
                texIndex = 1 - texIndex;
                Console.Beep(1000, 100);
                col = Colour.blue;
            }
            if (fPos.y > h - size/2)
            {
                fPos.y = h - size / 2;
                velocity.y *= -damping;
                texIndex = 1 - texIndex;
                Console.Beep(5000, 100);
                col = Colour.yellow;
            }

            pos = new Coord((int)fPos.x, (int)fPos.y);
        }

        public override void Draw()
        {
            Renderer.Draw(DrawCall.Create(Renderer.t[texIndex]).At(pos).Sized(new Vec2(size, size)).Coloured(col));
        }
    }
}
