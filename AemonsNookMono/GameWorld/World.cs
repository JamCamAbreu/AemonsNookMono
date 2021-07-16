using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.GameWorld
{
    public class World
    {
        private const int MAX_WORLD_WIDTH = 40;
        private const int MAX_WORLD_HEIGHT = 40;
        private const int TILE_DIMENSION_PIXELS = 32;
        private Dictionary<string, Texture2D> sprites { get; set; } // TODO: Move this to a singleton!
        private SpriteBatch SB { get; set; }

        public Tile[,] Tiles;
        public int Width { get; set; }
        public int Height { get; set; }
        
        public World(int width, int height, Dictionary<string, Texture2D> _sprites, SpriteBatch sb)
        {
            initTiles(width, height);
            this.sprites = _sprites;
            this.SB = sb;
        }

        private void initTiles(int width, int height)
        {
            this.Tiles = new Tile[height,width];
            this.Width = width;
            this.Height = height;
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    this.Tiles[row, col] = new Tile(col, row, Tile.TileType.Grass);
                }
            }
        }

        public Tile TileAt(int x, int y)
        {
            if (x < 0 || y < 0 || x > this.Width || y > this.Height) { throw new Exception("Attempt to retrieve Tile out of bounds!"); }
            return this.Tiles[y, x];
        }

        public void Draw()
        {
            if (SB == null) { return; }
            Tile cur;
            SB.Begin();
            for (int row = 0; row < this.Height; row++)
            {
                for (int col = 0; col < this.Width; col++)
                {
                    cur = this.Tiles[row, col];
                    switch (cur.Type)
                    {
                        case Tile.TileType.Grass:
                            SB.Draw(sprites["grass-a"], new Vector2(cur.OriginX * TILE_DIMENSION_PIXELS, cur.OriginY * TILE_DIMENSION_PIXELS), Color.White);
                            break;

                        default:
                            throw new Exception("Attempt to draw a tile that is not supported yet!");
                            break;
                    }
                }
            }
            SB.End();
        }
    }
}
