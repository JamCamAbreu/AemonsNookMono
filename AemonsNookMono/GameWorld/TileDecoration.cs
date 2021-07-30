using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.GameWorld
{
    public class TileDecoration
    {
        #region Constructor
        public TileDecoration(string sprite, Vector2 pos, float r, Vector2 spriteMidpoint)
        {
            this.Coord = pos;
            this.Mid = spriteMidpoint;
            this.Rotation = r;
            this.Sprite = sprite;
        }
        #endregion

        #region Public Properties
        public string Sprite { get; set; }
        public Vector2 Coord { get; set; }
        public Vector2 Mid { get; set; }
        public float Rotation { get; set; }
        #endregion
    }
}
