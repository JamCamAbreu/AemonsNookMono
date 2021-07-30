using AemonsNookMono.Admin;
using AemonsNookMono.GameWorld;
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
            Random ran = new Random();
            this.Version = ran.Next(1, 6);

            Collision TrunkCollision = new Collision(Collision.CollisionShape.Rectangle, x + 16, y + 16, 6, 28);
            Collision BranchesCollision = new Collision(Collision.CollisionShape.Circle, x + 16, y + 8, 16, 16);
            this.Collisions.Add(TrunkCollision);
            this.Collisions.Add(BranchesCollision);
        }
        public override void Draw()
        {
            string spritestring = $"tree-{this.Version}";
            Graphics.Current.SpriteB.Draw(Graphics.Current.SpritesByName[spritestring], new Vector2(World.Current.StartDrawX + PosX, World.Current.StartDrawY + PosY), Color.White);
        }
        public override void HandleLeftClick()
        {
            Debugger.Current.AddTempString($"You clicked on a Tree!");
            base.HandleLeftClick();
        }
    }
}
