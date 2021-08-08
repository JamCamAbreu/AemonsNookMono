using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Menus
{
    public class ButtonSprite
    {
        public ButtonSprite(string spriteNormal, string spriteHover, string spriteClick, int spriteWidth, int spriteHeight)
        {
            if (string.IsNullOrEmpty(spriteNormal) || string.IsNullOrEmpty(spriteHover) || string.IsNullOrEmpty(spriteClick))
            {
                throw new Exception("Misuse of Button Sprite!");
            }
            this.SpriteNormal = spriteNormal;
            this.SpriteHover = spriteHover;
            this.SpriteClick = spriteClick;
            this.SpriteWidth = spriteWidth;
            this.SpriteHeight = spriteHeight;
        }

        #region Internal
        public string SpriteNormal { get; set; }
        public string SpriteHover { get; set; }
        public string SpriteClick { get; set; }
        public int SpriteWidth { get; set; }
        public int SpriteHeight { get; set; }
        #endregion
    }
}
