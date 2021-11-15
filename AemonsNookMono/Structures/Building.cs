using AemonsNookMono.Admin;
using AemonsNookMono.GameWorld;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Structures
{
    public class Building
    {
        #region Constructor
        public Building(int x, int y, BuildingInfo.Type t)
        {
            this.OriginX = x;
            this.OriginY = y;
            this.Type = t;
            this.TilesUnderneath = new List<Tile>();
            this.Capacity = BuildingInfo.RetrieveCapacity(t);
            this.Height = BuildingInfo.RetrieveHeight(t);
            this.Width = BuildingInfo.RetrieveWidth(t);
            this.Name = BuildingInfo.RetrieveName(t);
            this.Sprite = BuildingInfo.RetrieveSprite(t);

            for (int w = 0; w < this.Width; w++)
            {
                for (int h = 0; h < this.Height; h++)
                {
                    this.TilesUnderneath.Add(World.Current.TileAtPixel(x + w, y + h));
                }
            }
        }
        #endregion

        #region Public Properties
        public BuildingInfo.Type Type { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int OriginX { get; set; }
        public int OriginY { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public List<Tile> TilesUnderneath { get; set; }
        public string Sprite { get; set; }
        #endregion

        #region Interface
        public virtual void Update()
        {

        }
        public virtual void Draw()
        {
            Graphics.Current.SpriteB.Draw(Graphics.Current.SpritesByName[this.Sprite], new Vector2(World.Current.StartDrawX + this.OriginX, World.Current.StartDrawY + this.OriginY), Color.White);
        }
        #endregion

        #region Internal
        
        #endregion
    }
}
