using AemonsNookMono.Admin;
using AemonsNookMono.GameWorld;
using AemonsNookMono.Structures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Resources
{
    public class Tree : Resource
    {
        #region Constructor
        public Tree(int x, int y, Tile tile) : base(x, y, tile)
        {
            this.Type = ResourceType.Tree;

            Random ran = new Random();
            this.Version = ran.Next(1, 6);

            this.Value = ran.Next(2, 6);
            this.Life = this.Value / 2;

            this.SetCollisions();
        }
        #endregion

        #region Public Properties
        public override int MagnetOffsetX { get { return 8; } }
        public override int MagnetOffsetY { get { return 16; } }
        #endregion

        #region Interface
        public override void Update()
        {
           base.Update();
        }
        public override void Draw()
        {
            string spritestring;
            if (this.State != ResourceState.Raw) { spritestring = "tree-harvest"; }
            else { spritestring = $"tree-{this.Version}"; }
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
                            Graphics.Current.SpriteB.Draw(Graphics.Current.SpritesByName["cursor-redx"], new Vector2(this.Position.X + 8, this.Position.Y + 8), Color.White);
                        }
                    }
                }
            }
        }
        public override void HandleLeftClick()
        {
            if (World.Current.hero != null && Cursor.Current.CurDistanceFromCenter > World.Current.hero.InteractReach)
            {
                Debugger.Current.AddTempString($"You need to get closer to harvest this tree.");
                return;
            }

            Debugger.Current.AddTempString($"You hack away at the Tree.");
            this.AttackResource();
        }
        public void SetCollisions()
        {
            this.Collisions.Clear();
            Collision TrunkCollision = new Collision(Collision.CollisionShape.Rectangle, (int)this.Position.X + 16, (int)this.Position.Y + 16, 6, 28);
            Collision BranchesCollision = new Collision(Collision.CollisionShape.Circle, (int)this.Position.X + 16, (int)this.Position.Y + 8, 16, 16);
            this.Collisions.Add(TrunkCollision);
            this.Collisions.Add(BranchesCollision);
        }
        #endregion
    }
}
