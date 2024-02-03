using System.Collections.Generic;

namespace NimEngine.Graphics
{
    public class Tileset
    {
        public string path = "C:/Users/super/Downloads/tester/testsprite.png";

        nint _srctex;

        public nint srctex => _srctex;

        List<Texture2D> tex = new List<Texture2D>();

        public Texture2D this[int index]
        {
            get { return tex[index]; }
        }


        public Tileset(string name, string path, int cellSize, int ImgWidth, int ImgHeight)
        {
            this.path = path;

            GetTileTextures(name,path,cellSize,ImgWidth,ImgHeight);
        }

        void GetTileTextures(string name, string path, int cellSize, int ImgWidth, int ImgHeight)
        {
            _srctex = Graphics.LoadTexture(path);

            int CellsX = ImgWidth / cellSize;
            int CellsY = ImgHeight / cellSize;

            for (int j = 0; j < CellsY; j++)
            {
                for (int i = 0; i < CellsX; i++)
                {
                    tex.Add(Graphics.CreateTexture(srctex, name+ ":" + i + j * CellsX, cellSize, cellSize, i*cellSize, j*cellSize));
                }
            }
        }
    }
}
