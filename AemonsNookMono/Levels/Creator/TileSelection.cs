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
        #region Public Properties
        public TileType Type { get; set; }
        public Tile TileUnderMouse { get; set; }
        #endregion

        #region Constructor
        public TileSelection(TileType type)
        {
            this.squares = new List<TileSelectionSquare>();
            this.Type = type;
            this.TileUnderMouse = null;

            this.squares.Add(new TileSelectionSquare(type, 0, 0));
        }
        #endregion

        #region Interface
        public void Paint()
        {
            if (this.TileUnderMouse != null && this.TileUnderMouse.Type != this.Type)
            {
                Debugger.Current.AddTempString($"Painting tile {this.Type} at {this.TileUnderMouse.RelativeX},{this.TileUnderMouse.RelativeY}");
                this.TileUnderMouse.Type = this.Type;
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
        #endregion
    }
}
