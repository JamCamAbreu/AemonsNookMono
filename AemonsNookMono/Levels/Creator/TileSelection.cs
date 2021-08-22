using AemonsNookMono.Admin;
using AemonsNookMono.GameWorld;
using System;
using System.Collections.Generic;
using System.Text;
using static AemonsNookMono.GameWorld.Tile;

namespace AemonsNookMono.Levels.Creator
{
    public class TileSelection
    {
        #region Enums
        public enum BrushShape
        {
            OneByOne,
            TwoByTwo,
            Cross
        }
        #endregion

        #region Public Properties
        public TileType Type { get; set; }
        public BrushShape Shape { get; set; }
        public Tile TileUnderMouse { get; set; }
        #endregion

        #region Constructor
        public TileSelection(TileType type)
        {
            this.squares = new List<TileSelectionSquare>();
            this.Type = type;
            this.Shape = BrushShape.OneByOne;
            this.TileUnderMouse = null;

            this.squares.Add(new TileSelectionSquare(type, 0, 0));
        }
        public TileSelection(TileType type, BrushShape shape)
        {
            this.squares = new List<TileSelectionSquare>();
            this.Type = type;
            this.Shape = shape;
            this.TileUnderMouse = null;

            List<Tuple<int, int>> relativeCoordinates = this.RetrieveBrushShape(shape);
            foreach (Tuple<int, int> coord in relativeCoordinates)
            {
                TileSelectionSquare square = new TileSelectionSquare(type, coord.Item1, coord.Item2);
                this.squares.Add(square);
            }
        }
        #endregion

        #region Interface
        public void Paint()
        {
            bool dirty = false;
            foreach (TileSelectionSquare square in this.squares)
            {
                Tile under = World.Current.TileAt(
                    Math.Min(World.Current.Width - 1, Math.Max(0, square.OriginTile.Column + square.RelativeX)), 
                    Math.Min(World.Current.Height - 1, Math.Max(0, square.OriginTile.Row + square.RelativeY)));
                if (under != null && under.Type != this.Type)
                {
                    under.Type = this.Type;
                    dirty = true;
                }
            }
            if (dirty)
            {
                GameWorld.World.Current.RefreshTiles(true);
            }
        }
        public void Update()
        {
            this.FollowMouse();
        }
        public void Draw()
        {
            foreach (TileSelectionSquare square in this.squares)
            {
                square.Draw();
            }
        }
        #endregion

        #region Internal
        private List<TileSelectionSquare> squares { get; set; }
        private void FollowMouse()
        {
            Tile t = World.Current.TileAtPixel(Cursor.Current.LastX, Cursor.Current.LastY);
            if (t != null)
            {
                if (this.TileUnderMouse != t)
                {
                    this.TileUnderMouse = t;
                    foreach (TileSelectionSquare square in this.squares)
                    {
                        square.OriginTile = t;
                    }
                }
            }
            else
            {
                this.TileUnderMouse = null;
            }
        }
        private List<Tuple<int, int>> RetrieveBrushShape(BrushShape shape)
        {
            List<Tuple<int, int>> relativeCoordinates = new List<Tuple<int, int>>();
            switch (shape)
            {
                case BrushShape.OneByOne:
                    relativeCoordinates.Add(Tuple.Create(0, 0)); // (x, y)
                    break;

                case BrushShape.TwoByTwo:
                    relativeCoordinates.Add(Tuple.Create(0, 0)); // (x, y)
                    relativeCoordinates.Add(Tuple.Create(0, 1));
                    relativeCoordinates.Add(Tuple.Create(1, 0));
                    relativeCoordinates.Add(Tuple.Create(1, 1));
                    break;

                case BrushShape.Cross:
                    relativeCoordinates.Add(Tuple.Create(0, 0)); // (x, y)
                    relativeCoordinates.Add(Tuple.Create(-1, 0));
                    relativeCoordinates.Add(Tuple.Create(0, -1));
                    relativeCoordinates.Add(Tuple.Create(1, 0));
                    relativeCoordinates.Add(Tuple.Create(0, 1));
                    break;
            }
            return relativeCoordinates;
        }
        #endregion
    }
}
