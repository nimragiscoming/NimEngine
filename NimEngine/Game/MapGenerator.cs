using NimEngine.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NimEngine.Game
{
    public static class MapGenerator
    {
        public static Map Generate(int seed)
        {
            Map m = new Map();


            GenerateTerrain(m, seed);


            m.UpdateAllDrawCalls();

            return m;
        }

        public static void GenerateTerrain(Map m,int seed)
        {

            Tile t = new Tile(Glyph.B);
            t.col = Colour.green;
            t.Create();

            Tile t1 = new Tile(Glyph.D);
            t1.col = Colour.DarkGreen;
            t1.Create();
            OpenSimplexNoise noise = new OpenSimplexNoise(seed);


            m.Iterate((int x, int y) =>
            {
            //    Console.WriteLine(noise.Evaluate(x,y));
                if (GetTerrainHeight(noise,x, y) < 0f)
                {
                    m.SetTile(new Coord(x, y), t);
                }
                else
                {
                    m.SetTile(new Coord(x, y), t1);
                }
            });

            GenerateLakes(noise, m);
        }

        public static float GetTerrainHeight(OpenSimplexNoise noise, int x, int y)
        {
            double h0 = 0.1f;

            double h1 = 0.2f;

            double h2 = 0.5f;

            double n0 = noise.Evaluate(x * h0, y * h0);
            double n1 = noise.Evaluate(x * h1, y * h1);
            double n2 = noise.Evaluate(x * h2, y * h2);

            double n = n0 + n1 * 0.7f + n2 * 0.4f;
            return (float)n;
        }

        public static void GenerateLakes(OpenSimplexNoise noise, Map m)
        {
            Tile t = new Tile(Glyph.W);
            t.col = Colour.blue;
            t.Create();

            double h0 = 0.05f;

            double threshhold = -0.7f;

            for (int i = 0; i < Map.dataWidth; i++)
            {
                for (int j = 0; j < Map.dataWidth; j++)
                {
                    double n = noise.Evaluate(i * h0, j * h0);
                    if (n < threshhold)
                    {
                        m.SetTile(new Coord(i, j), t);
                    }
                }
            }
        }

    }
}
