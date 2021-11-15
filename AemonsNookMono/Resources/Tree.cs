using AemonsNookMono.Admin;
using AemonsNookMono.GameWorld;
using AemonsNookMono.Structures;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Resources
{
    public class Tree : Resource
    {
        public Tree(int x, int y, Tile tile) : base(x, y, tile)
        {
            this.Type = ResourceType.Tree;

            Random ran = new Random();
            this.Version = ran.Next(1, 6);

            this.SetCollisions();
        }
        public override void Draw()
        {
            string spritestring = $"tree-{this.Version}";
            Graphics.Current.SpriteB.Draw(Graphics.Current.SpritesByName[spritestring], new Vector2(PosX, PosY), Color.White);
        }
        public override void HandleLeftClick()
        {
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
    }
}
