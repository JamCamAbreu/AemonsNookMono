using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.GameWorld
{
    public class Tile
    {
        public enum TileType
        {
            Grass,
            Dirt,
            Water,
            Building
        }
        public int OriginX { get; set; }
        public int OriginY { get; set; }
        public int Shape { get; set; } // uses bitmap
        public TileType Type { get; set; }
        public Tile(int x, int y, TileType type)
        {
            this.OriginX = x;
            this.OriginY = y;
            this.Type = type;
        }
         
    }
}
