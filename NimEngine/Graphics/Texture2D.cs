namespace NimEngine.Graphics
{
    public class Texture2D
    {
        private nint _pointer;

        private string _name = "UNNAMED_TEXTURE";

        private int _x;

        private int _y;

        private int _width;

        private int _height;

        public string Name => _name;
        public int Width => _width;
        public int Height => _height;

        public int X => _x;

        public int Y => _y;

        public nint Pointer => _pointer;

        public Texture2D(nint pointer, string name, int width, int height, int x = 0, int y = 0)
        {
            _pointer = pointer;
            _name = name;
            _width = width;
            _height = height;
            _x = x;
            _y = y;
        }

    }
}
