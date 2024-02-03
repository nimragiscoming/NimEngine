namespace NimEngine.Graphics
{
    public static class TextureLibrary
    {
        static Dictionary<string, Texture2D> _textures = new Dictionary<string, Texture2D>();

        public static Texture2D GetTexture(string ID)
        {
            return _textures[ID];
        }
        public static Texture2D AddTexture(Texture2D tex)
        {
            _textures.Add(tex.Name, tex);
            return tex;
        }

        public static bool HasTexture(string ID)
        {
            return _textures.ContainsKey(ID);
        }

        public static void DestroyTextures()
        {
            foreach (KeyValuePair<string, Texture2D> tex in _textures)
            {
                SDL2.SDL.SDL_DestroyTexture(tex.Value.Pointer);
            }
        }


    }
}
