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
            this.Collisions = new List<Collision>();
            this.Capacity = BuildingInfo.RetrieveCapacity(t);
            this.Height = BuildingInfo.RetrieveHeight(t);
            this.Width = BuildingInfo.RetrieveWidth(t);
            this.Name = BuildingInfo.RetrieveName(t);
            this.Sprite = BuildingInfo.RetrieveSprite(t);

            Tile tile;
            for (int w = 0; w < this.Width; w++)
            {
                for (int h = 0; h < this.Height; h++)
                {
                    tile = World.Current.TileAtPixel(World.Current.StartDrawX + x + (w * World.TILE_DIMENSION_PIXELS), World.Current.StartDrawY + y + (h * World.TILE_DIMENSION_PIXELS));
                    if (tile != null)
                    {
                        this.TilesUnderneath.Add(tile);
                    }
                }
            }

            this.CreateCollisions();
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
        public List<Collision> Collisions { get; set; }
        public string Sprite { get; set; }
        #endregion

        #region Interface
        public virtual void HandleLeftClick()
        {
            Debugger.Current.AddTempString($"You clicked on the {Name}.");
        }
        public virtual void Update()
        {

        }
        public virtual void Draw()
        {
            Graphics.Current.SpriteB.Draw(Graphics.Current.SpritesByName[this.Sprite], new Vector2(World.Current.StartDrawX + this.OriginX, World.Current.StartDrawY + this.OriginY), Color.White);
        }
        public virtual void CreateCollisions()
        {
            this.Collisions.Clear();
            foreach (Tile t in this.TilesUnderneath)
            {
                if (t != null )
                {
                    Collision collision = new Collision(
                        Collision.CollisionShape.Rectangle,
                        World.Current.StartDrawX + (int)t.RelativeX + World.TILE_DIMENSION_PIXELS / 2,
                        World.Current.StartDrawY + (int)t.RelativeY + World.TILE_DIMENSION_PIXELS / 2,
                        World.TILE_DIMENSION_PIXELS, World.TILE_DIMENSION_PIXELS);
                    this.Collisions.Add(collision);
                }
            }
        }
        public bool IsCollision(int x, int y)
        {
            foreach (Collision c in this.Collisions)
            {
                if (c.IsCollision(x, y))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Internal

        #endregion
    }
}
