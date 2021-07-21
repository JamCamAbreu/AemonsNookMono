using AemonsNookMono.GameWorld;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Resources
{
    public abstract class Resource
    {
        public int PosX { get; set; }
        public int PosY { get; set; }
        public Tile TileOn { get; set; }
        public int Version { get; set; }
        public Resource(int x, int y, Tile tile)
        {
            this.PosX = x;
            this.PosY = y;
            this.TileOn = tile;
        }
        public abstract void Draw(int startDrawX, int startDrawY);
    }
}
