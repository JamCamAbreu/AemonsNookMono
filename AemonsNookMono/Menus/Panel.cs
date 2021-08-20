using AemonsNookMono.Admin;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Menus
{
    public class Panel : Cell
    {
        #region Constructor
        public Panel(int width, int height, int centerx, int centery, Color? backColor = null, Color? frontColor = null, float transparency = 1)
        {
            this.Width = width;
            this.Height = height;
            this.ScreenX = centerx;
            this.ScreenY = centery;
            this.BackgroundColor = backColor == null ? Color.Black : (Color)backColor;
            this.AccentColor = ProfileManager.Current.ColorPrimary;
            this.ForegroundColor = frontColor == null ? Color.Gray : (Color)frontColor;
            this.Transparency = transparency;

            if (width > 1 && height > 1)
            {
                Color[] data;
                this.background = new Texture2D(Graphics.Current.Device, width, height);
                data = new Color[width * height];
                for (int i = 0; i < data.Length; ++i) data[i] = this.BackgroundColor;
                this.background.SetData(data);

                int fwidth = width - Graphics.BORDER_THICKNESS;
                int fheight = height - Graphics.BORDER_THICKNESS;
                this.accent = new Texture2D(Graphics.Current.Device, fwidth, fheight);
                data = new Color[fwidth * fheight];
                for (int i = 0; i < data.Length; ++i) data[i] = this.AccentColor;
                this.accent.SetData(data);

                fwidth = width - Graphics.BORDER_THICKNESS * 2;
                fheight = height - Graphics.BORDER_THICKNESS * 2;
                this.foreground = new Texture2D(Graphics.Current.Device, fwidth, fheight);
                data = new Color[fwidth * fheight];
                for (int i = 0; i < data.Length; ++i) data[i] = this.ForegroundColor;
                this.foreground.SetData(data);
            }
            else
            {
                this.foreground = null;
                this.background = null;
            }

        }
        #endregion

        #region Public Properties
        public Color BackgroundColor { get; set; }
        public Color AccentColor { get; set; }
        public Color ForegroundColor { get; set; }
        public float Transparency { get; set; }
        #endregion

        #region Interface
        public override void Draw()
        {
            if (this.background == null || this.foreground == null) { return; }

            int centerBackX = this.ScreenX - this.Width/2;
            int centerBackY = this.ScreenY - this.Height/2;
            int centerAccentX = centerBackX + Graphics.BORDER_THICKNESS / 2;
            int centerAccentY = centerBackY + Graphics.BORDER_THICKNESS / 2;
            int centerFrontX = centerBackX + Graphics.BORDER_THICKNESS;
            int centerFrontY = centerBackY + Graphics.BORDER_THICKNESS;

            Graphics.Current.SpriteB.Begin();
            Graphics.Current.SpriteB.Draw(this.background, new Vector2(centerBackX, centerBackY), Color.White);
            Graphics.Current.SpriteB.Draw(this.accent, new Vector2(centerAccentX, centerAccentY), Color.White);
            Graphics.Current.SpriteB.Draw(this.foreground, new Vector2(centerFrontX, centerFrontY), Color.White);
            Graphics.Current.SpriteB.End();
        }

        public override void Refresh(int width, int height, int screenx, int screeny)
        {
            this.Width = width;
            this.Height = height;
            this.ScreenX = screenx;
            this.ScreenY = screeny;

            if (width > 1 && height > 1)
            {
                Color[] data;
                this.background = new Texture2D(Graphics.Current.Device, width, height);
                data = new Color[width * height];
                for (int i = 0; i < data.Length; ++i) data[i] = this.BackgroundColor;
                this.background.SetData(data);

                int fwidth = width - Graphics.BORDER_THICKNESS / 2;
                int fheight = height - Graphics.BORDER_THICKNESS / 2;
                this.accent = new Texture2D(Graphics.Current.Device, fwidth, fheight);
                data = new Color[fwidth * fheight];
                for (int i = 0; i < data.Length; ++i) data[i] = this.AccentColor;
                this.accent.SetData(data);

                fwidth = width - Graphics.BORDER_THICKNESS;
                fheight = height - Graphics.BORDER_THICKNESS;
                this.foreground = new Texture2D(Graphics.Current.Device, fwidth, fheight);
                data = new Color[fwidth * fheight];
                for (int i = 0; i < data.Length; ++i) data[i] = this.ForegroundColor;
                this.foreground.SetData(data);
            }
            else
            {
                this.foreground = null;
                this.background = null;
            }
        }

        public override void Update()
        {
        }
        #endregion

        #region Internal
        private Texture2D background { get; set; }
        private Texture2D accent { get; set; }
        private Texture2D foreground { get; set; }
        #endregion
    }
}
