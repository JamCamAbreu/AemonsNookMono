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
        public static Graphics Current
        {
            get
            {
                if (instance == null)
                {
                    lock (_lock)
                    {
                        if (instance == null)
                        {
                            instance = new Graphics();
                        }
                    }
                }
                return instance;
            }
        }
        #endregion

        #region Public Properties
        public Dictionary<string, Texture2D> SpritesByName { get; set; }
        public Dictionary<string, SpriteFont> Fonts { get; set; }
        public GraphicsDeviceManager GraphicsDM { get; set; }
        public SpriteBatch SpriteB { get; set; }
        public GraphicsDevice Device { get; set; }
        #endregion

        #region Interface
        public void Init(GraphicsDevice gd, bool fullscreen = false)
        {
            this.SpritesByName = new Dictionary<string, Texture2D>();
            this.Fonts = new Dictionary<string, SpriteFont>();
            this.Device = gd;

            if (fullscreen)
            {
                this.GraphicsDM.PreferredBackBufferWidth = this.Device.DisplayMode.Width;
                this.GraphicsDM.PreferredBackBufferHeight = this.Device.DisplayMode.Height;
                this.GraphicsDM.IsFullScreen = true;
                this.GraphicsDM.ApplyChanges();
            }
            else
            {
                this.GraphicsDM.PreferredBackBufferWidth = 1400;
                this.GraphicsDM.PreferredBackBufferHeight = 900;
                this.GraphicsDM.ApplyChanges();
            }

        }
        #endregion
    }
}
