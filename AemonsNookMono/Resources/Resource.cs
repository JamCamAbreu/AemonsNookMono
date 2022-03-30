using AemonsNookMono.Admin;
using AemonsNookMono.Entities;
using AemonsNookMono.GameWorld;
using Microsoft.Xna.Framework;
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
        public enum ResourceState
        {
            Raw,
            Harvestable,
            Magnetized,
            Stockpile
        }
        const int PICKUP_WIGGLE_DISTANCE = 24;
        public ResourceState State { get; set; }
        public ResourceType Type { get; set; }
        public int Value { get; set; }
        public int TileRelativeX { get; set; }
        public int TileRelativeY { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 TargetPosition { get; set; }
        public Tile TileOn { get; set; }
        public int Version { get; set; }
        public List<Collision> Collisions { get; set; }
        public int Life { get; set; }
        public abstract int MagnetOffsetX { get; }
        public abstract int MagnetOffsetY { get; }
        public List<Entity> CurrentHarvesters { get; set; }
        public Resource(int x, int y, Tile tile)
        {
            Vector2 vector2 = new Vector2(x, y);
            this.Position = vector2;
            this.TargetPosition = vector2;
            this.State = ResourceState.Raw;

            this.TileOn = tile;
            this.Collisions = new List<Collision>();
            this.CurrentHarvesters = new List<Entity>();
        }
        public virtual void Destroy()
        {
            if (this.TileOn != null)
            {
                this.TileOn.ResourcesToRemove.Add(this);
            }
            World.Current.Resources.ResourcesToRemove.Add(this);
        }
        public virtual void Update()
        {
            if (this.State == ResourceState.Harvestable && World.Current.hero != null) // TODO: Move this logic to hero?! 
            {
                int dist = Global.ApproxDist(
                    new Vector2(World.Current.hero.CenterX, World.Current.hero.CenterY),
                    this.Position
                );
                if (dist < World.Current.hero.PickupReach)
                {
                    this.State = ResourceState.Magnetized;
                    World.Current.hero.HeldResources.Add(this);
                    this.TileOn?.ResourcesToRemove.Add(this);
                }
            }

            this.Position = Global.Ease(this.Position, this.TargetPosition, 0.025f);

            if (this.State == ResourceState.Magnetized)
            {
                this.MagnetizeToHero();
            }
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
        }
        public virtual void AttackResource()
        {
            this.Life--;
            if (this.Life <= 0)
            {
                this.State = ResourceState.Harvestable;
                this.Collisions.Clear();
            }
        }
        public virtual void MagnetizeToHero()
        {
            int wigglex = World.Current.ran.Next(-PICKUP_WIGGLE_DISTANCE, PICKUP_WIGGLE_DISTANCE);
            int wiggley = World.Current.ran.Next(-PICKUP_WIGGLE_DISTANCE, PICKUP_WIGGLE_DISTANCE);
            this.TargetPosition = new Vector2(World.Current.hero.CenterX - MagnetOffsetX + wigglex, World.Current.hero.CenterY - MagnetOffsetY + wiggley);
        }
    }
}
