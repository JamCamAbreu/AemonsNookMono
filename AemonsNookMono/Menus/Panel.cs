using AemonsNookMono.Admin;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Menus
{
    public class Panel
    {
        #region Constructor
        public Panel(int width, int height, Color? backColor = null, Color? frontColor = null, float transparency = 1)
        {
            this.Width = width;
            this.Height = height;
            this.BackgroundColor = backColor == null ? Color.Black : (Color)backColor;
            this.ForegroundColor = frontColor == null ? Color.Gray : (Color)frontColor;
            this.Transparency = transparency;

            Color[] data;
            this.background = new Texture2D(Graphics.Current.Device, width, height);
            data = new Color[width * height];
            for (int i = 0; i < data.Length; ++i) data[i] = this.BackgroundColor;
            this.background.SetData(data);

            int fwidth = width - Graphics.BORDER_THICKNESS*2;
            int fheight = height - Graphics.BORDER_THICKNESS*2;
            this.foreground = new Texture2D(Graphics.Current.Device, fwidth, fheight);
            data = new Color[fwidth * fheight];
            for (int i = 0; i < data.Length; ++i) data[i] = this.ForegroundColor;
            this.foreground.SetData(data);
        }
        #endregion

        #region Public Properties
        public int Width { get; set; }
        public int Height { get; set; }
        public Color BackgroundColor { get; set; }
        public Color ForegroundColor { get; set; }
        public float Transparency { get; set; }
        #endregion

        #region Interface
        public void Draw(int x, int y)
        {
            int centerBackX = x - this.Width / 2;
            int centerBackY = y - this.Height / 2;
            int centerFrontX = centerBackX + Graphics.BORDER_THICKNESS;
            int centerFrontY = centerBackY + Graphics.BORDER_THICKNESS;

            Graphics.Current.SpriteB.Begin();
            Graphics.Current.SpriteB.Draw(this.background, new Vector2(centerBackX, centerBackY), Color.White);
            Graphics.Current.SpriteB.Draw(this.foreground, new Vector2(centerFrontX, centerFrontY), Color.White);
            Graphics.Current.SpriteB.End();
        }
        #endregion

        #region Internal
        private Texture2D background { get; set; }
        private Texture2D foreground { get; set; }
        #endregion
    }
}
