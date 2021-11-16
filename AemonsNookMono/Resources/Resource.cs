using AemonsNookMono.Admin;
using AemonsNookMono.GameWorld;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Resources
{
    public abstract class Resource
    {
        public enum ResourceType
        {
            Tree,
            Stone
        }
        public ResourceType Type { get; set; }
        public int TileRelativeX { get; set; }
        public int TileRelativeY { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public Tile TileOn { get; set; }
        public int Version { get; set; }
        public List<Collision> Collisions { get; set; }
        public int Life { get; set; }
        public bool Collectible { get; set; }
        public Resource(int x, int y, Tile tile)
        {
            this.PosX = x;
            this.PosY = y;
            this.TileOn = tile;
            this.Collisions = new List<Collision>();
            this.Life = 1;
        }
        public virtual void Update()
        {

        }
        public abstract void Draw();
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
        public virtual void HandleLeftClick()
        {
            this.Life--;
            if (this.Life <= 0)
            {
                this.TileOn.ResourcesToRemove.Add(this);
            }
        }
    }
}
