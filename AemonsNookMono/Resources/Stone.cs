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
        public Stone(int x, int y, Tile tile) : base(x, y, tile)
        {
            this.Type = ResourceType.Stone;

            Random ran = new Random();
            this.Version = ran.Next(1, 6);

            this.SetCollisions();
        }
        public override void Draw()
        {
            string spritestring = $"stone-{this.Version}";
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
                            Graphics.Current.SpriteB.Draw(Graphics.Current.SpritesByName["cursor-redx"], new Vector2(pos.X, pos.Y), Color.White);
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

            Debugger.Current.AddTempString($"You clicked on a Stone!");
            this.Life--;
            if (this.Life <= 0)
            {
                this.TileOn.ResourcesToRemove.Add(this);
            }

            Stockpile pile = Buildings.Current.GetClosestStockpile(this.TileOn);
            if (pile != null)
            {
                pile.NumStone++;
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
            Collision RockCollision = new Collision(Collision.CollisionShape.Circle, this.PosX + 8, this.PosY + 8, radius, radius);
            this.Collisions.Add(RockCollision);
        }
    }
}
