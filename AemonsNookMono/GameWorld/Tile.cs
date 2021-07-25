using AemonsNookMono.GameWorld.Effects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.GameWorld
{
    public class Tile
    {
        #region Static Methods And Enums
        public static string ShapeToString(int shape)
        {
            switch (shape)
            {
                case 0:
                    return "intersection";

                case 1:
                case 8:
                case 9:
                    return "horizontal";

                case 2:
                case 4:
                case 6:
                    return "vertical";

                case 12:
                    return "corner-topright";

                case 5:
                    return "corner-topleft";

                case 3:
                    return "corner-bottomleft";

                case 10:
                    return "corner-bottomright";

                case 7:
                    return "ts-left";

                case 11:
                    return "ts-bottom";

                case 14:
                    return "ts-right";

                case 13:
                    return "ts-top";

                case 15:
                    return "intersection";

                default:
                    throw new Exception($"Whoops. Somehow this tile's shape is {shape}, which is not a supported bit shape");
            }
        }
        public enum TileType
        {
            Grass,
            Dirt,
            Water,
            Building,
            Tree,
            Stone
        }
        #endregion

        #region Public Properties
        public int Column { get; set; }
        public int Row { get; set; }
        public int RelativeX { get; set; }
        public int RelativeY { get; set; }
        public int Shape { get; set; } // uses bitmap
        public TileType Type { get; set; }
        public bool IsMapEdge { get; set; }
        public int MapEdgeId { get; set; }
        public bool IsPath { get; set; }
        public Tile TileAbove { get; set; }
        public Tile TileRight { get; set; }
        public Tile TileBelow { get; set; }
        public Tile TileLeft { get; set; }
        #endregion

        #region Constructors
        public Tile(int x, int y, TileType type, int tileDimension)
        {
            this.Column = x;
            this.Row = y;
            this.RelativeX = x * tileDimension;
            this.RelativeY = y * tileDimension;
            this.Type = type;
            this.IsMapEdge = false;
            this.MapEdgeId = -1;
            this.ran = new Random();
        }
        #endregion

        #region Interface
        public void Update()
        {
        }
        public void Draw()
        {
            Vector2 pos = new Vector2(World.Current.StartDrawX + RelativeX, World.Current.StartDrawY + RelativeY);
            string spritename;
            switch (this.Type)
            {
                case Tile.TileType.Grass:
                    Graphics.Current.SpriteB.Draw(Graphics.Current.SpritesByName["grass-a"], pos, Color.White);
                    break;

                case Tile.TileType.Dirt:
                    // Underneath:
                    Graphics.Current.SpriteB.Draw(Graphics.Current.SpritesByName["grass-a"], pos, Color.White);

                    spritename = $"dirt-{Tile.ShapeToString(this.Shape)}";
                    Graphics.Current.SpriteB.Draw(Graphics.Current.SpritesByName[spritename], pos, Color.White);
                    break;

                case Tile.TileType.Water:
                    Graphics.Current.SpriteB.Draw(Graphics.Current.SpritesByName["grass-a"], pos, Color.White);

                    // Underneath:
                    spritename = $"dirt-{Tile.ShapeToString(this.Shape)}";
                    Graphics.Current.SpriteB.Draw(Graphics.Current.SpritesByName[spritename], pos, Color.White);

                    spritename = $"water-{Tile.ShapeToString(this.Shape)}";
                    Graphics.Current.SpriteB.Draw(Graphics.Current.SpritesByName[spritename], pos, Color.White);
                    break;

                case TileType.Tree:
                    Graphics.Current.SpriteB.Draw(Graphics.Current.SpritesByName["grass-a"], pos, Color.White);
                    break;

                case TileType.Stone:
                    Graphics.Current.SpriteB.Draw(Graphics.Current.SpritesByName["grass-a"], pos, Color.White);
                    break;

                default:
                    throw new Exception("Attempt to draw a tile that is not supported yet!");
            }
            if (Debugger.Current.DrawTileShapes) { Graphics.Current.SpriteB.DrawString(Graphics.Current.Fonts["debug"], this.Shape.ToString(), pos, Color.White); }
        }
        #endregion

        #region Internal Properties
        Random ran { get; set; }
        #endregion
    }
}
