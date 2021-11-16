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
            this.Collectible = false;

            Random ran = new Random();
            this.Version = ran.Next(1, 6);

            this.SetCollisions();
        }
        #endregion

        #region Interface
        public override void Draw()
        {
            string spritestring = $"tree-{this.Version}";
            Vector2 pos = new Vector2(PosX, PosY);
            Graphics.Current.SpriteB.Draw(Graphics.Current.SpritesByName[spritestring], pos, Color.White);

            if (this.Collisions != null)
            {
                foreach (var collision in this.Collisions)
                {
                    if (collision.IsCollision(Cursor.Current.LastWorldX, Cursor.Current.LastWorldY))
                    {
                        if (Cursor.Current.CurDistanceFromCenter <= World.Current.hero.Reach)
                        {
                            Graphics.Current.DrawOutlineSprite(spritestring, pos, Color.Lerp(Color.White, Color.Red, 0.5f));
                        }
                        else
                        {
                            Graphics.Current.SpriteB.Draw(Graphics.Current.SpritesByName["cursor-redx"], new Vector2(pos.X + 8, pos.Y + 8), Color.White);
                        }
                    }
                }
            }
        }
        public override void HandleLeftClick()
        {
            if (Cursor.Current.CurDistanceFromCenter > World.Current.hero.Reach)
            {
                return;
            }

            Debugger.Current.AddTempString($"You clicked on a Tree!");
            this.Life--;
            if (this.Life <= 0)
            {
                this.TileOn.ResourcesToRemove.Add(this);
            }

            Stockpile pile = Buildings.Current.GetClosestStockpile(this.TileOn);
            if (pile != null)
            {
                pile.NumWood++;
            }
        }
        public void SetCollisions()
        {
            this.Collisions.Clear();
            Collision TrunkCollision = new Collision(Collision.CollisionShape.Rectangle, this.PosX + 16, this.PosY + 16, 6, 28);
            Collision BranchesCollision = new Collision(Collision.CollisionShape.Circle, this.PosX + 16, this.PosY + 8, 16, 16);
            this.Collisions.Add(TrunkCollision);
            this.Collisions.Add(BranchesCollision);
        }
        #endregion
    }
}
