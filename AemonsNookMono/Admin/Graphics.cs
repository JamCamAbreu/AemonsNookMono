using AemonsNookMono.GameWorld;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Admin
{
    public sealed class Graphics
    {
        #region Constants
        public const int BORDER_THICKNESS = 2;
        #endregion

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
        public GameWindow Window { get; set; }
        private bool fullscreen { get; set; }
        public bool FullScreen
        {
            get { return this.fullscreen; }
            set
            {
                this.fullscreen = value;
                if (value == true)
                {
                    this.GraphicsDM.PreferredBackBufferWidth = this.Device.DisplayMode.Width;
                    this.GraphicsDM.PreferredBackBufferHeight = this.Device.DisplayMode.Height - 72; // typical size of taskbar
                    
                    this.GraphicsDM.IsFullScreen = false;
                    this.GraphicsDM.ApplyChanges();
                    World.Current.Refresh();
                    MenuManager.Current.Refresh();
                    Debugger.Current.Refresh();
                    //this.Window.IsBorderless = true;
                    this.Window.Position = new Point(0, 32);
                    

                    // Resources need to update their collisions
                }
                else
                {
                    this.GraphicsDM.PreferredBackBufferWidth = 1400;
                    this.GraphicsDM.PreferredBackBufferHeight = 900;
                    this.GraphicsDM.IsFullScreen = false;
                    this.GraphicsDM.ApplyChanges();
                    World.Current.Refresh();
                    MenuManager.Current.Refresh();
                    Debugger.Current.Refresh();
                    this.Window.IsBorderless = false;
                }
            }
        }
        #endregion

        #region Interface
        public void Init(GraphicsDevice gd, GameWindow window, bool fullscreen = false)
        {
            this.SpritesByName = new Dictionary<string, Texture2D>();
            this.Fonts = new Dictionary<string, SpriteFont>();
            this.Device = gd;
            this.Window = window;

            this.FullScreen = fullscreen;
        }
        public int ScreenMidX { get { return Graphics.Current.Device.Viewport.Width / 2; } }
        public int ScreenMidY { get { return Graphics.Current.Device.Viewport.Height / 2; } }
        public int ScreenWidth { get { return Graphics.Current.Device.Viewport.Width; } }
        public int ScreenHeight { get { return Graphics.Current.Device.Viewport.Height; } }
        public float FontCharWidth(string font)
        {
            if (string.IsNullOrEmpty(font) || !this.Fonts.ContainsKey(font)) { throw new Exception("Cannot find specified font"); }
            switch (font)
            {
                case "debug":
                    return 7;

                case "arial":
                    return 11;

                case "couriernew":
                    return 11.3f;

                default:
                    throw new Exception("Cannot find specified font");
            }
        }
        public float FontCharHeight(string font)
        {
            if (string.IsNullOrEmpty(font) || !this.Fonts.ContainsKey(font)) { throw new Exception("Cannot find specified font"); }
            switch (font)
            {
                case "debug":
                    return 7;

                case "arial":
                    return 10;

                case "couriernew":
                    return 20f;

                default:
                    throw new Exception("Cannot find specified font");
            }
        }
        public int CenterStringX(int originX, string message, string font)
        {
            if (string.IsNullOrEmpty(message)) { return originX; }
            if (string.IsNullOrEmpty(font) || !this.Fonts.ContainsKey(font)) { throw new Exception("Cannot find specified font"); }
            return originX - ((int)((this.FontCharWidth(font) * (double)message.Length) / 2f));
        }
        public int CenterStringY(int originY, string font)
        {
            if (string.IsNullOrEmpty(font) || !this.Fonts.ContainsKey(font)) { throw new Exception("Cannot find specified font"); }
            return originY - ((int)(this.FontCharHeight(font) / 2f));
        }
        #endregion

    }
}
