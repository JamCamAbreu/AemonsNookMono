﻿using AemonsNookMono.GameWorld;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Structures
{
    public class BuildingSelection
    {
        #region Public Properties
        public BuildingInfo.Type Type { get; set; }
        public Tile LastTileUnderMouse { get; set; }
        #endregion

        #region Constructor
        public BuildingSelection(BuildingInfo.Type type)
        {
            this.Squares = new List<BuildingSelectionSquare>();
            this.Type = type;
            this.LastTileUnderMouse = World.Current.TileAt(0, 0);

            List<Tuple<int, int>> relativeCoordinates = BuildingInfo.RetrieveRelativeCoordinates(type);
            foreach (Tuple<int, int> coord in relativeCoordinates)
            {
                BuildingSelectionSquare square = new BuildingSelectionSquare(coord.Item1, coord.Item2);
                this.Squares.Add(square);
            }
        }
        #endregion

        #region Interface
        public void Build()
        {
            Buildings.Current.AddBuilding(LastTileUnderMouse.RelativeX, LastTileUnderMouse.RelativeY, this.Type);
        }
        public void Update()
        {
            this.FollowMouse();
        }
        public void Draw()
        {
            foreach (BuildingSelectionSquare square in this.Squares)
            {
                square.Draw();
            }
        }
        #endregion

        #region Internal
        private List<BuildingSelectionSquare> Squares { get; set; }
        private void FollowMouse()
        {
            Tile t = World.Current.TileAtPixel(Cursor.Current.LastX, Cursor.Current.LastY);
            if (t != null && this.LastTileUnderMouse != t)
            {
                this.LastTileUnderMouse = t;
                foreach (BuildingSelectionSquare square in this.Squares)
                {
                    square.OriginTile = t;
                }
            }
        }
        #endregion
    }
}
