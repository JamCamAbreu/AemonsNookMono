using AemonsNookMono.Admin;
using AemonsNookMono.GameWorld;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Structures
{
    public class BuildingSelectionSquare
    {
        #region Constructor
        public BuildingSelectionSquare(int relativeX, int relativeY)
        {
            this.RelativeX = relativeX;
            this.RelativeY = relativeY;
        }
        #endregion

        #region Public Properties
        public int RelativeX { get; set; }
        public int RelativeY { get; set; }
        public Tile OriginTile { get; set; }
        #endregion

        #region Interface
        public void Draw()
        {
            Graphics.Current.SpriteB.Draw(
                Graphics.Current.SpritesByName["building-placement-green"],
                new Vector2(
                    World.Current.StartDrawX + this.OriginTile.RelativeX + (this.RelativeX * World.TILE_DIMENSION_PIXELS),
                    World.Current.StartDrawY + this.OriginTile.RelativeY + (this.RelativeY * World.TILE_DIMENSION_PIXELS)),
                Color.White);
        }
        #endregion
    }
}
