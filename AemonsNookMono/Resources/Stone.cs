using AemonsNookMono.Admin;
using AemonsNookMono.GameWorld;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Resources
{
    public class Stone : Resource
    {
        public Stone(int x, int y, Tile tile) : base(x, y, tile)
        {
            Random ran = new Random();
            this.Version = ran.Next(1, 6);

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
            Collision RockCollision = new Collision(Collision.CollisionShape.Circle, x + 8, y + 8, radius, radius);
            this.Collisions.Add(RockCollision);
        }
        public override void Draw()
        {
            string spritestring = $"stone-{this.Version}";
            Graphics.Current.SpriteB.Draw(Graphics.Current.SpritesByName[spritestring], new Vector2(World.Current.StartDrawX + PosX, World.Current.StartDrawY + PosY), Color.White);
        }
        public override void HandleLeftClick()
        {
            Debugger.Current.AddTempString($"You clicked on a Stone!");
            base.HandleLeftClick();
        }
    }
}
