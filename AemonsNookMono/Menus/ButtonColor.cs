using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Menus
{
    public class ButtonColor
    {
        public ButtonColor(int width, int height, int transparency, Color primary)
        {
            Color hover = Color.Lerp(primary, Color.White, 0.25f);
            Color pressed = Color.Lerp(primary, Color.Black, 0.25f);

            this.Normal = new Panel(width, height, Color.Black, primary, transparency);
            this.Hover = new Panel(width, height, Color.Black, hover, transparency);
            this.Pressed = new Panel(width, height, Color.Black, pressed, transparency);
        }

        public Panel Normal { get; set; }
        public Panel Hover { get; set; }
        public Panel Pressed { get; set; }
    }
}
