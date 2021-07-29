using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.GameWorld
{
    public class TileDecoration
    {
        #region Constructor
        public TileDecoration(string sprite, int x, int y, int r)
        {
            this.Coord = new Vector2(x, y);
            this.RotationDegrees = r;
            this.Sprite = sprite;
        }
        #endregion

        #region Public Properties
        public string Sprite { get; set; }
        public Vector2 Coord { get; set; }
        public int RotationDegrees { get; set; }
        #endregion
    }
}
