using NimEngine.Program;
using NimEngine.Game;
using NimEngine.Graphics;
using SDL2;
using System.Runtime.InteropServices;

internal class Program
{
    [DllImport("kernel32.dll")]
    static extern IntPtr GetConsoleWindow();

    [DllImport("user32.dll")]
    static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    const int SW_HIDE = 5;
    const int SW_SHOW = 0;

    static bool ShowConsole = true;


    private static void Main(string[] args)
    {
        var handle = GetConsoleWindow();
        if (ShowConsole)
        {

            ShowWindow(handle, SW_HIDE);
        }
        else
        {
            ShowWindow(handle, SW_SHOW);
        }

        InitSDL();

        Renderer.LoadTextures();


        Update();


        DestroySDL();

    }

    private static void InitSDL()
    {
        Console.WriteLine("Initializing SDL...");
        SDL.SDL_SetHint(SDL.SDL_HINT_WINDOWS_DISABLE_THREAD_NAMING, "1");
        if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO) < 0)
        {
            Console.WriteLine($"Unable to initialise SDL. Error {SDL.SDL_GetError()}");
            return;
        }

        Console.WriteLine("Creating Window...");

        nint window = IntPtr.Zero;
        window = SDL.SDL_CreateWindow(
            "TestingWindow",
            SDL.SDL_WINDOWPOS_CENTERED,
            SDL.SDL_WINDOWPOS_CENTERED,
            800,
            800,
            SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE
            );

        if (window == IntPtr.Zero)
        {
            Console.WriteLine($"Unable to create window. Error {SDL.SDL_GetError()}");
            return;
        }

        nint renderer = Renderer.Init(window);

        Pointers.Set(window, renderer);
    }

    private static void Update()
    {
        SDL.SDL_Event e;
        bool quit = false;

        Time.StartTime();

        while (!quit)
        {

            while (SDL.SDL_PollEvent(out e) != 0)
            {

                switch (e.type)
                {
                    case SDL.SDL_EventType.SDL_QUIT:
                        quit = true;
                        break;
                    case SDL.SDL_EventType.SDL_KEYDOWN:
                        Input.KeyDown(e.key.keysym.sym);
                        break;
                    case SDL.SDL_EventType.SDL_KEYUP:
                        Input.KeyUp(e.key.keysym.sym);
                        break;
                    case SDL.SDL_EventType.SDL_RENDER_TARGETS_RESET:
                        RenderableList.OnRenderTargetReset();
                        break;

                }

            }
            //Input
            //Objects - Handled on separate independant timer
            //Processing - not Implemented
            //Rendering
            //Time Update/Reset

            Input.Update();

            UpdateableList.UpdateAll();

            Renderer.RenderFrame();
            //    Console.WriteLine(Input.Cur.Count);

            Time.UpdateTime();

        }

    }

    private static void DestroySDL()
    {
        TextureLibrary.DestroyTextures();
        SDL.SDL_DestroyRenderer(Pointers.renderer);
        SDL.SDL_DestroyWindow(Pointers.window);
        SDL.SDL_Quit();
    }
}