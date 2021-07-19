using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Help
{
    public class HoverBox : IDisposable
    {
        public string Message { get; set; }

        private int transparency { get; set; }
        private bool kill { get; set; }
        public HoverBox(string message)
        {
            this.Message = message;
            this.transparency = 0;
        }
        
        public void Draw()
        {
            int xpos = Cursor.Current().LastX;
            int ypos = Cursor.Current().LastY;

            #region Kill
            if (kill && transparency > 0)
            {
                transparency--;
            }
            if (transparency <= 0)
            {
                this.Dispose();
            }
            #endregion

            Graphics.Current().SpriteB.Begin();
            Graphics.Current().SpriteB.DrawString(Graphics.Current().Fonts["debug"], Message, new Vector2(xpos + 12, ypos + 12), Color.White);
            Graphics.Current().SpriteB.End();

        }

        public void Kill()
        {
            this.kill = true;
        }

        public void Dispose()
        {
        }
    }
}
