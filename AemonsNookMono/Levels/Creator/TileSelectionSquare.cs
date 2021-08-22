using AemonsNookMono.Admin;
using AemonsNookMono.GameWorld;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using static AemonsNookMono.GameWorld.Tile;

namespace AemonsNookMono.Levels.Creator
{
    public class TileSelectionSquare
    {
        #region Constructor
        public TileSelectionSquare(TileType type, int relativeX, int relativeY)
        {
            this.RelativeX = relativeX;
            this.RelativeY = relativeY;
            this.Type = type;
            switch (type)
            {
                case Tile.TileType.Grass:
                    this.Sprite = "grass-a";
                    break;

                case Tile.TileType.Dirt:
                    this.Sprite = $"dirt-intersection";
                    break;

                case Tile.TileType.Water:
                    this.Sprite = $"water-intersection";
                    break;

                case TileType.Tree:
                    this.Sprite = "tree-1";
                    break;

                case TileType.Stone:
                    this.Sprite = "stone-1";
                    break;
            }
        }
        #endregion

        #region Public Properties
        public int RelativeX { get; set; }
        public int RelativeY { get; set; }
        public TileType Type { get; set; }
        public Tile OriginTile { get; set; }
        public string Sprite { get; set; }
        #endregion

        #region Interface
        public void Draw()
        {
            if (this.OriginTile != null)
            {
                Graphics.Current.SpriteB.Begin();
                Graphics.Current.SpriteB.Draw(
                    Graphics.Current.SpritesByName[this.Sprite],
                    new Vector2(
                        World.Current.StartDrawX + this.OriginTile.RelativeX + (this.RelativeX * World.TILE_DIMENSION_PIXELS),
                        World.Current.StartDrawY + this.OriginTile.RelativeY + (this.RelativeY * World.TILE_DIMENSION_PIXELS)),
                    Color.White * 0.8f);
                Graphics.Current.SpriteB.Draw(
                    Graphics.Current.SpritesByName["tile-outline"],
                    new Vector2(
                        World.Current.StartDrawX + this.OriginTile.RelativeX + (this.RelativeX * World.TILE_DIMENSION_PIXELS),
                        World.Current.StartDrawY + this.OriginTile.RelativeY + (this.RelativeY * World.TILE_DIMENSION_PIXELS)),
                    Color.White);
                Graphics.Current.SpriteB.End();
            }
        }
        #endregion
    }
}
