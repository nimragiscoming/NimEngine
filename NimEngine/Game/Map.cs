using NimEngine.Graphics;
using System;
using System.Diagnostics;
using SDL2;
using NimEngine.Program;
using static System.Net.Mime.MediaTypeNames;
using NimEngine.Interfaces;

namespace NimEngine.Game
{
    public class Map : Thing
    {
        //larger draw rect on screen
        public const int drawSize = 1600;

        //amount of tiles in the array
        public const int dataWidth = 150;

        //how big each tile is in pixels
        public int tileWidth = 16;

        //for rendering an inner rect
        public Coord leftTopCell = new Coord(0,0);

        public int tilesWidthOnScreen = 40;

        public int viewportSize => tilesWidthOnScreen * tileWidth;

        public int ScreenTileSize => (int)(((float)drawSize / (float)tileWidth)*((float)drawSize/ (float)viewportSize));

        public Vec2 tileSize => new Vec2(tileWidth, tileWidth);

        private Tile[,] tileData = new Tile[dataWidth,dataWidth];

        public Tile[,] TileData => tileData;

        List<DrawCall> drawCallsToAdd = new List<DrawCall>();
         
        bool shouldUpdateCalls => drawCallsToAdd.Count> 0;

        public Texture2D MapTex;

        public Map()
        {
            Tile t = new Tile(Glyph.At);
            t.col = Colour.blue;
            t.Create();

            CreateMapTex();

        //    Fill(t);

        //    UpdateAllDrawCalls();
            //    UpdateDrawTile(new Coord(0, 0), t);
        }


        public override void Tick()
        {
            if(Time.Ticks % 5 != 0) return;

            Console.WriteLine(ScreenTileSize);

            if (Input.GetKey(SDL.SDL_Keycode.SDLK_d))
            {
                leftTopCell = new Coord((int)(leftTopCell.x + tileWidth), leftTopCell.y);
            }
            if (Input.GetKey(SDL.SDL_Keycode.SDLK_a))
            {
                leftTopCell = new Coord((int)(leftTopCell.x - tileWidth), leftTopCell.y);
            }
            if (Input.GetKey(SDL.SDL_Keycode.SDLK_w))
            {
                leftTopCell = new Coord(leftTopCell.x, (int)(leftTopCell.y - tileWidth));
            }
            if (Input.GetKey(SDL.SDL_Keycode.SDLK_s))
            {
                leftTopCell = new Coord(leftTopCell.x, (int)(leftTopCell.y + tileWidth));
            }

            if (Input.GetKey(SDL.SDL_Keycode.SDLK_z))
            {
                tilesWidthOnScreen -= 1;
            }
            if (Input.GetKey(SDL.SDL_Keycode.SDLK_x))
            {
                tilesWidthOnScreen += 1;
            }

            tilesWidthOnScreen = Math.Clamp(tilesWidthOnScreen, 20, drawSize / tileWidth);

            leftTopCell.x = Math.Clamp(leftTopCell.x, 0, drawSize - tileWidth);
            leftTopCell.y = Math.Clamp(leftTopCell.y, 0, drawSize - tileWidth);
        }


        public override void Draw()
        {
            Stopwatch sw = Stopwatch.StartNew();

            if(shouldUpdateCalls)
            {
                UpdateDrawTiles();
            }

            Renderer.Draw(DrawCall.Create(MapTex).At(new Coord(0,0)).Sized(new Vec2(drawSize, drawSize)).Clip(leftTopCell,new Coord(viewportSize,viewportSize)));
            //Renderer.renderBuf.AddRange(drawCalls);
            sw.Stop();

        //    Console.WriteLine(sw.ElapsedMilliseconds + " send draw ms");
        }

        public override void OnRenderTargetReset()
        {
            UpdateAllDrawCalls();
        }


        public void Fill(Tile tile)
        {
            Iterate((int x, int y) =>
            {
                SetTile(new Coord(x, y), tile);
            });
        }

        public void UpdateAllDrawCalls()
        {
            Stopwatch sw = Stopwatch.StartNew();

            drawCallsToAdd.Clear();

            Iterate((int x, int y) =>
            {
                drawCallsToAdd.Add(GetTileCall(x,y).At(GetDrawCoord(x, y)));
            });

            sw.Stop();

            Console.WriteLine(sw.ElapsedMilliseconds + "update draw calls ms");
        }

        public Coord GetDrawCoord(int x, int y)
        {
            return new Coord(x*tileWidth, y*tileWidth);
        }

        public Tile GetTile(int x, int y)
        {
            return tileData[x, y];
        }

        public DrawCall GetTileCall(int x, int y)
        {
            return tileData[x, y].GetCall();
        }



        public Texture2D GetTileTex(int x, int y)
        {
            Tile t = TileData[x, y];

            return Renderer.t[t.sym.Index];
        }


        public void SetTile(Coord pos, Tile tile)
        {
            tileData[pos.x, pos.y] = tile;
        //    Console.WriteLine(pos.x + " " + pos.y);

            drawCallsToAdd.Add(tile.GetCall().At(GetDrawCoord(pos.x, pos.y)));
        }

        public unsafe void UpdateDrawTiles()
        {
            Stopwatch sw = Stopwatch.StartNew();

            SDL.SDL_SetRenderTarget(Pointers.renderer, MapTex.Pointer);

        //    Console.WriteLine(drawCallsToAdd.Count);
            foreach (DrawCall call in drawCallsToAdd)
            {
                Graphics.Graphics.DrawTexture(call);
            }

            drawCallsToAdd.Clear();


            SDL.SDL_SetRenderTarget(Pointers.renderer, (IntPtr)null);

            sw.Stop();

            Console.WriteLine(sw.ElapsedMilliseconds + "blit tex ms");

        }

        public void CreateMapTex()
        {
            nint ptr = SDL.SDL_CreateTexture(Pointers.renderer, SDL.SDL_PIXELFORMAT_ABGR8888, (int)SDL.SDL_TextureAccess.SDL_TEXTUREACCESS_TARGET,dataWidth * tileWidth, dataWidth * tileWidth);
            MapTex = Graphics.Graphics.CreateTexture(ptr,"map", dataWidth * tileWidth, dataWidth * tileWidth);
        }
        public void Iterate(Action<int, int> action)
        {
            for (int i = 0; i < dataWidth; i++) // X
            {
                for (int j = 0; j < dataWidth; j++)// Y
                {
                    action(i, j);
                }
            }
        }

        public void IterateParallelRow(Action<int, int> action)
        {
            Parallel.For(0, dataWidth, (i) =>// X
            {
                for (int j = 0; j < dataWidth; j++)// Y
                {
                    action(i, j);
                }
            });
        }

        public void IterateParallel(Action<int, int> action)
        {
            Parallel.For(0, dataWidth, (i) =>// X
            {
                Parallel.For(0, dataWidth, (j) =>//Y
                {

                    action(i, j);

                });
            });
        }
    }
}
