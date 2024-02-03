namespace NimEngine.Program
{
    public static class Pointers
    {
        public static nint window { get; private set; }

        public static nint renderer { get; private set; }

        public static void Set(nint _window, nint _renderer)
        {
            window = _window;
            renderer = _renderer;
        }
    }
}
