using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Menus
{
    public class ButtonSprite
    {
        public ButtonSprite(string spriteNormal, string spriteHover, string spriteClick)
        {
            if (!string.IsNullOrEmpty(spriteNormal) || !string.IsNullOrEmpty(spriteHover) || !string.IsNullOrEmpty(spriteClick))
            {
                throw new Exception("Misuse of Button Sprite!");
            }
            this.SpriteNormal = spriteNormal;
            this.SpriteHover = spriteHover;
            this.SpriteClick = spriteClick;
        }

        #region Internal
        public string SpriteNormal { get; set; }
        public string SpriteHover { get; set; }
        public string SpriteClick { get; set; }
        #endregion
    }
}
