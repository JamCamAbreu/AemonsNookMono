using AemonsNookMono.Admin;
using AemonsNookMono.GameWorld;
using AemonsNookMono.Structures;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Resources
{
    public class Stone : Resource
    {
        #region Constructor
        public Stone(int x, int y, Tile tile) : base(x, y, tile)
        {
            this.Type = ResourceType.Stone;

            Random ran = new Random();
            this.Version = ran.Next(1, 6);

            this.SetCollisions();
        }
        #endregion

        #region Public Properties
        public override int MagnetOffsetX { get { return 0; } }
        public override int MagnetOffsetY { get { return 4; } }
        #endregion

        #region Interface
        public override void Update()
        {
            base.Update();
        }
        public override void Draw()
        {
            string spritestring;
            if (this.State != ResourceState.Raw) { spritestring = "stone-harvest"; }
            else { spritestring = $"stone-{this.Version}"; }
            Graphics.Current.SpriteB.Draw(Graphics.Current.SpritesByName[spritestring], this.Position, Color.White);

            if (this.Collisions != null && World.Current.hero != null)
            {
                foreach (var collision in this.Collisions)
                {
                    if (collision.IsCollision(Cursor.Current.LastWorldX, Cursor.Current.LastWorldY))
                    {
                        if (Cursor.Current.CurDistanceFromCenter <= World.Current.hero.InteractReach)
                        {
                            Graphics.Current.DrawOutlineSprite(spritestring, this.Position, Color.Lerp(Color.White, Color.Red, 0.5f));
                        }
                        else
                        {
                            Graphics.Current.SpriteB.Draw(Graphics.Current.SpritesByName["cursor-redx"], new Vector2(this.Position.X, this.Position.Y), Color.White);
                        }
                    }
                }
            }
        }
        public override void HandleLeftClick()
        {
            if (Cursor.Current.CurDistanceFromCenter > World.Current.hero.InteractReach)
            {
                Debugger.Current.AddTempString($"You need to get closer to harvest this stone.");
                return;
            }

            Debugger.Current.AddTempString($"You chip away at the stone!");
            this.Life--;
            if (this.Life <= 0)
            {
                this.State = ResourceState.Harvestable;
                this.Collisions.Clear();
            }
        }
        public void SetCollisions()
        {
            this.Collisions.Clear();
            int radius;
            switch (this.Version)
            {
                case 1:
                case 2:
                case 5:
                case 6:
                    radius = 6;
                    break;
                case 3:
                    radius = 4;
                    break;
                case 4:
                    radius = 3;
                    break;
                default:
                    radius = 6;
                    break;
            }
            Collision RockCollision = new Collision(Collision.CollisionShape.Circle, (int)this.Position.X + 8, (int)this.Position.Y + 8, radius, radius);
            this.Collisions.Add(RockCollision);
        }
        #endregion
    }
}
