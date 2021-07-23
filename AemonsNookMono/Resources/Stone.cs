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
        }
        public override void Draw(int startDrawX, int startDrawY)
        {
            string spritestring = $"stone-{this.Version}";
            Graphics.Current.SpriteB.Draw(Graphics.Current.SpritesByName[spritestring], new Vector2(startDrawX + PosX, startDrawY + PosY), Color.White);
        }
    }
}
