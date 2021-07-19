﻿using Microsoft.Xna.Framework;
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
        private int sizeX = 0;
        private int sizeY = 0;
        private int startDrawX = 0;
        private int startDrawY = 0;

        public Tile[,] Tiles;
        public int Width { get; set; }
        public int Height { get; set; }
        
        public World(int width, int height)
        {
            initTiles(width, height);
            this.sizeX = width * TILE_DIMENSION_PIXELS;
            this.sizeY = height * TILE_DIMENSION_PIXELS;
            this.startDrawX = (Graphics.Current().Device.Viewport.Width / 2) - (this.sizeX / 2);
            this.startDrawY = (Graphics.Current().Device.Viewport.Height / 2) - (this.sizeY / 2);
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
            Tile cur;
            Graphics.Current().SpriteB.Begin();
            Graphics.Current().GraphicsDM.GraphicsDevice.Clear(Color.Black);

            for (int row = 0; row < this.Height; row++)
            {
                for (int col = 0; col < this.Width; col++)
                {
                    cur = this.Tiles[row, col];
                    switch (cur.Type)
                    {
                        case Tile.TileType.Grass:
                            Graphics.Current().SpriteB.Draw(Graphics.Current().Sprites["grass-a"], new Vector2(this.startDrawX + (cur.OriginX * TILE_DIMENSION_PIXELS), this.startDrawY + (cur.OriginY * TILE_DIMENSION_PIXELS)), Color.White);
                            break;

                        default:
                            throw new Exception("Attempt to draw a tile that is not supported yet!");
                    }
                }
            }
            Graphics.Current().SpriteB.End();
        }
    }
}
