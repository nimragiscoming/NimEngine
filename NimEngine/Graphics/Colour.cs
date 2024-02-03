using System.Numerics;

namespace NimEngine.Graphics
{
    public struct Colour
    {
        private byte[] value = new byte[4];

        public byte r => value[0];
        public byte g => value[1];
        public byte b => value[2];
        public byte a => value[3];

        public Colour(byte r, byte g, byte b)
        {
            value[0] = r;
            value[1] = g;
            value[2] = b;
            value[3] = byte.MaxValue;
        }

        public Colour(byte r, byte g, byte b, byte a)
        {
            value[0] = r;
            value[1] = g;
            value[2] = b;
            value[3] = a;
        }

        static Colour Multiply(Colour col, float val)
        {
            byte r = (byte)(col.r * val);
            byte g = (byte)(col.g * val);
            byte b = (byte)(col.b * val);
            byte a = (byte)(col.a * val);
            return new Colour(r, g, b, a);
        }

        static float Clamp01(float x)
        {
            return Math.Clamp(x, 0, 1);
        }
        static Vector4 Clamp01(Vector4 v)
        {
            v.X = Clamp01(v.X);
            v.Y = Clamp01(v.Y);
            v.Z = Clamp01(v.Z);
            v.W = Clamp01(v.W);
            return v;
        }

        public static Colour white => new Colour(255, 255, 255);
        public static Colour black => new Colour(0, 0, 0);
        public static Colour red => new Colour(255, 0, 0);
        public static Colour green => new Colour(0, 255, 0);
        public static Colour blue => new Colour(0, 0, 255);

        public static Colour yellow => new Colour(255, 255, 0);
        public static Colour cyan => new Colour(0, 255, 255);
        public static Colour magenta => new Colour(255, 0, 255);

        public static Colour DarkGreen => new Colour(0, 150, 0);

        public static Colour transparent => new Colour(0, 0, 0, 0);

        public static Colour operator *(Colour lhs, float rhs) => Multiply(lhs,rhs);
        public static Colour operator /(Colour lhs, float rhs) => Multiply(lhs, 1/rhs);

    }
}
