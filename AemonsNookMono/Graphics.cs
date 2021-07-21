using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono
{
    public sealed class Graphics
    {
        #region Singleton Implementation
        private static Graphics instance;
        private static object _lock = new object();
        private Graphics() { }
        public static Graphics Current()
        {
            lock (_lock)
            {
                if (instance == null)
                {
                    instance = new Graphics();
                }
            }
            return instance;
        }
        #endregion

        public void Init(GraphicsDevice gd)
        {
            this.Sprites = new Dictionary<string, Texture2D>();
            this.Fonts = new Dictionary<string, SpriteFont>();
            this.Device = gd;

            // WINDOWED:
            this.GraphicsDM.PreferredBackBufferWidth = 1400;
            this.GraphicsDM.PreferredBackBufferHeight = 900;
            this.GraphicsDM.ApplyChanges();

            // FULL SCREEN:
            //Graphics.Current().GraphicsDM.PreferredBackBufferWidth = this.Device.DisplayMode.Width;
            //Graphics.Current().GraphicsDM.PreferredBackBufferHeight = this.Device.DisplayMode.Height;
            //Graphics.Current().GraphicsDM.IsFullScreen = true;
            //Graphics.Current().GraphicsDM.ApplyChanges();
        }

        public Dictionary<string, Texture2D> Sprites { get; set; }
        public Dictionary<string, SpriteFont> Fonts { get; set; }
        public GraphicsDeviceManager GraphicsDM { get; set; }
        public SpriteBatch SpriteB { get; set; }
        public GraphicsDevice Device { get; set; }

    }
}
