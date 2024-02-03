using SDL2;

namespace NimEngine.Program
{
    public static class Input
    {
        public static List<SDL.SDL_Keycode> Down = new List<SDL.SDL_Keycode>();
        public static List<SDL.SDL_Keycode> Up = new List<SDL.SDL_Keycode>();

        public static List<SDL.SDL_Keycode> Cur = new List<SDL.SDL_Keycode>();

        static List<SDL.SDL_Keycode> BufDown = new List<SDL.SDL_Keycode>();

        static List<SDL.SDL_Keycode> BufUp = new List<SDL.SDL_Keycode>();

        public static bool GetKey(SDL.SDL_Keycode keycode)
        {
            return Cur.Contains(keycode);
        }

        public static void KeyDown(SDL.SDL_Keycode key)
        {
            if (!Cur.Contains(key))
            {
                BufDown.Add(key);
                Console.WriteLine(key.ToString());
            }
        }

        public static void KeyUp(SDL.SDL_Keycode key)
        {
            BufUp.Add(key);
        }

        public static void Update()
        {
            Down.Clear();
            Up.Clear();


            foreach (var key in BufUp)
            {
                if (Cur.Contains(key))
                {
                    Up.Add(key);
                    Cur.Remove(key);
                }
            }

            foreach (var key in BufDown)
            {
                if (!Cur.Contains(key))
                {
                    Down.Add(key);
                    Cur.Add(key);
                }

            }


            ClearBuffers();
        }

        static void ClearBuffers()
        {
            BufDown.Clear();
            BufUp.Clear();
        }
    }
}
