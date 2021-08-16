using AemonsNookMono.Admin;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Menus
{
    public class SpriteAnimated : Cell
    {
        #region Constructor
        public SpriteAnimated(List<string> sprites, int spritewidth, int spriteheight, int framesTillUpdate, int centerx, int centery, int width, int height, bool active = true)
        {
            this.Sprites = sprites;
            this.spriteIndex = 0;
            this.FramesTillUpdate = framesTillUpdate; // sprite speed
            this.spriteAlarm = framesTillUpdate;
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
        List<string> Sprites { get; set; }
        public int FramesTillUpdate { get; set; }
        public int SpriteWidth { get; set; }
        public int SpriteHeight { get; set; }
        #endregion

        public override void Draw()
        {
            if (this.Active)
            {
                Graphics.Current.SpriteB.Begin();
                Graphics.Current.SpriteB.Draw(Graphics.Current.SpritesByName[this.Sprites[this.spriteIndex]], new Vector2(this.ScreenX - this.SpriteWidth / 2, this.ScreenY - this.SpriteHeight / 2), Color.White);
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
            this.spriteAlarm--;
            if (this.spriteAlarm <= 0)
            {
                this.spriteIndex++;
                if (this.spriteIndex > this.Sprites.Count - 1) { this.spriteIndex = 0; }
                this.spriteAlarm = this.FramesTillUpdate;
            }
        }

        #region Internal
        protected int spriteIndex { get; set; }
        protected int spriteAlarm { get; set; }
        #endregion
    }
}
