using NimEngine.Program;
using SDL2;
using static SDL2.SDL;
using static SDL2.SDL_image;

namespace NimEngine.Graphics
{
    public static class Graphics
    {
        static nint renderer => Pointers.renderer;

        //scary unsafe points and shit
        public static unsafe nint LoadTexture(string path)
        {
            SDL_Surface* surf = (SDL_Surface*)IMG_Load(path);

            SDL_SetColorKey((IntPtr)surf, 1, SDL_MapRGB(surf->format, 0, 0, 0));

            return SDL_CreateTextureFromSurface(renderer, (IntPtr)surf);
        }

        public static Texture2D CreateTexture(string path, string name, int W, int H, int X = 0, int Y = 0)
        {
            nint pntr = LoadTexture(path);

            return CreateTexture(pntr, name, W, H,X,Y);
        }

        public static Texture2D CreateTexture(nint pointer, string name, int W, int H, int X = 0, int Y = 0)
        {
            if (TextureLibrary.HasTexture(name))
            {
                Console.WriteLine($"Texture {name} has been defined twice, overwriting the older version. Was this intentional?");
            }

            Texture2D texture = new Texture2D(pointer, name, W, H,X,Y);

            TextureLibrary.AddTexture(texture);
            return texture;
        }

        public static void DrawBox(int x, int y, int h, int w)
        {
            SDL_Rect drect = new SDL_Rect
            {
                x = x,
                y = y,
                h = h,
                w = w,

            };
            SDL_RenderFillRect(renderer,ref drect);

        }

        public static void DrawTexture(DrawCall call)
        {

            SDL_Rect srect = new SDL_Rect
            {
                x = call.srcXY.x,
                y = call.srcXY.y,
                h = call.srcSize.y,
                w = call.srcSize.x,

            };
            SDL_Rect drect = new SDL_Rect
            {
                x = call.pos.x,
                y = call.pos.y,
                h = (int)call.size.y,
                w = (int)call.size.x,

            };

            SDL_SetTextureColorMod(call.tex.Pointer, call.col.r, call.col.g, call.col.b);

            SDL_RenderCopy(renderer, call.tex.Pointer, ref srect, ref drect);
            //    SDL_RenderCopy(renderer, tex.Pointer, IntPtr.Zero, IntPtr.Zero);
        }
    }
}
