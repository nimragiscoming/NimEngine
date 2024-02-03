namespace NimEngine.Game
{
    public struct Coord
    {
        public int x;
        public int y;

        public Coord(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        static Coord _zero = new Coord(0, 0);

        public static Coord Zero => _zero;

        public Vec2 ToVec2 => new Vec2(x, y);

        public static Coord operator +(Coord a, Coord b) => new Coord(a.x + b.x, a.y + b.y);
        public static Coord operator *(Coord a, Coord b) => new Coord(a.x * b.x, a.y * b.y);

    }

    public struct Vec2
    {
        public float x;
        public float y;

        public Vec2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }


        static Vec2 _zero = new Vec2(0, 0);

        public static Vec2 Zero => _zero;


        public Coord ToCoord => new Coord((int)x, (int)y);

        public static Vec2 operator +(Vec2 a, Vec2 b) => new Vec2(a.x + b.x, a.y + b.y);
        public static Vec2 operator *(Vec2 a, Vec2 b) => new Vec2(a.x * b.x, a.y * b.y);

        public static Vec2 operator *(Vec2 a, float b) => new Vec2(a.x * b, a.y * b);
        public static Vec2 operator *(float b, Vec2 a) => new Vec2(a.x * b, a.y * b);

    }

}
