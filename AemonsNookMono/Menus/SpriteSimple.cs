using AemonsNookMono.Admin;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Menus
{
    public class SpriteSimple : Cell
    {
        #region Constructor
        public SpriteSimple(string sprite, int spritewidth, int spriteheight, int centerx, int centery, int width, int height, bool active = true)
        {
            this.Sprite = sprite;
            this.Width = width;
            this.Height = height;
            this.ScreenX = centerx;
            this.ScreenY = centery;
            this.Active = active;
            this.SpriteWidth = spritewidth;
            this.SpriteHeight = spriteheight;
        }
        #endregion

        #region Public Properties
        public string Sprite { get; set; }
        public int SpriteWidth { get; set; }
        public int SpriteHeight { get; set; }
        #endregion

        public override void Draw()
        {
            if (this.Active)
            {
                Graphics.Current.SpriteB.Begin();
                Graphics.Current.SpriteB.Draw(Graphics.Current.SpritesByName[this.Sprite], new Vector2(this.ScreenX - this.SpriteWidth/2, this.ScreenY - this.SpriteHeight/2), Color.White);
                Graphics.Current.SpriteB.End();
            }
        }

        public override void Refresh(int width, int height, int screenx, int screeny)
        {
            this.Width = width;
            this.Height = height;
            this.ScreenX = screenx;
            this.ScreenY = screeny;
        }

        public override void Update()
        {
            
        }
    }
}
