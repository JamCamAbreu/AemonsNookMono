using AemonsNookMono;
using AemonsNookMono.GameWorld;
using AemonsNookMono.GameWorld.Effects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AemonsNookMono.Admin
{
    public sealed class Debugger
    {
        #region Singleton Implementation
        private static Debugger instance;
        private static object _lock = new object();
        private Debugger() { }
        public static Debugger Current
        {
            get
            {
                if (instance == null)
                {
                    lock (_lock)
                    {
                        if (instance == null)
                        {
                            instance = new Debugger();
                        }
                    }
                }
                return instance;
            }
        }
        #endregion

        #region Public Properties
        public bool DrawTileShapes { get; set; }
        public World CurrentWorld { get; set; }
        public bool ShowCircleCollisions { get; set; }
        #endregion

        #region Interface
        public void AddTempString(string message)
        {
            this.tempStrings.Insert(0, message);
            if (this.tempStrings.Count > 6)
            {
                this.tempStrings.RemoveRange(6, this.tempStrings.Count - 6);
            }
        }
        public void Refresh()
        {
            this.screenWidthPixels = Graphics.Current.Device.Viewport.Width;
            this.screenHeightPixels = Graphics.Current.Device.Viewport.Height;
        }
        public void Init()
        {
            fps = new FrameCounter();
            this.screenWidthPixels = Graphics.Current.Device.Viewport.Width;
            this.screenHeightPixels = Graphics.Current.Device.Viewport.Height;
            this.DrawTileShapes = false;
            this.tempStrings = new List<string>();
            this.maxTempStrings = 6;
            this.ShowCircleCollisions = false;
        }
        public void Update(GameTime gameTime)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            this.fps.Update(deltaTime);
        }
        public void Draw(GameTime gameTime)
        {
            Graphics.Current.SpriteB.Begin();

            #region Top Right Panel
            List<string> debugMessages = new List<string>();
            debugMessages.Add($"----------- DEBUGGER -----------");

            #region FPS
            debugMessages.Add($"FPS: {this.fps.AverageFramesPerSecond}");
            #endregion

            #region Mouse Hover
            debugMessages.Add($"Mouse: {Mouse.GetState().X}, {Mouse.GetState().Y}");
            #endregion

            #region World
            if (CurrentWorld != null)
            {
                if (CurrentWorld.Resources != null)
                {
                    debugMessages.Add($"Resource Count: {CurrentWorld.Resources.Sorted.Count}");
                }
                debugMessages.Add($"Temp Effects Count: {EffectsGenerator.Current.CountTotalEffects()}");
            }
            #endregion

            #region State Manager
            debugMessages.Add($"Game State: {StateManager.Current.CurrentState}");
            #endregion

            #region Menu Manager
            debugMessages.Add($"Menu Count: {MenuManager.Current.Count}");

            string menuname = MenuManager.Current.Top?.MenuName ?? "None";
            debugMessages.Add($"Top Menu: {menuname}");
            #endregion

            #region Peeps
            debugMessages.Add($"Peep Count: {World.Current.Peeps.Count}");
            #endregion

            debugMessages.Add($"--------------------------------");

            int maxLength = 0;
            foreach (string message in debugMessages)
            {
                if (message.Length > maxLength) { maxLength = message.Length; }
            }
            int requiredWidth = 16 + maxLength * 7;

            int row = 0;
            foreach (string message in debugMessages)
            {
                Graphics.Current.SpriteB.DrawString(Graphics.Current.Fonts["debug"], message, new Vector2(screenWidthPixels - requiredWidth, PAD + (ROW_HEIGHT * row)), Color.White);
                row++;
            }
            #endregion

            #region Bottom Console
            row = 1;
            foreach (string message in tempStrings)
            {
                string msg;
                if (row == 1) { msg = $">> {message}"; }
                else { msg = message; }
                float transparency = 1.0f - ((float)(row - 1) / (float)maxTempStrings);
                Color stringcolor = Color.White * transparency;
                Graphics.Current.SpriteB.DrawString(Graphics.Current.Fonts["debug"], msg, new Vector2(8, screenHeightPixels - PAD - (ROW_HEIGHT*row)), stringcolor);
                row++;
            }
            #endregion

            Graphics.Current.SpriteB.End();
        }
        #endregion

        #region Private Properties
        private FrameCounter fps { get; set; }
        private int screenWidthPixels { get; set; }
        private int screenHeightPixels { get; set; }
        private const int ROW_HEIGHT = 16;
        private const int PAD = 4;
        private List<string> tempStrings { get; set; }
        private int maxTempStrings { get; set; }
        #endregion
    }
    public class FrameCounter
    {
        public FrameCounter()
        {
        }

        public long TotalFrames { get; private set; }
        public float TotalSeconds { get; private set; }
        public float AverageFramesPerSecond { get; private set; }
        public float CurrentFramesPerSecond { get; private set; }

        public const int MAXIMUM_SAMPLES = 100;

        private Queue<float> _sampleBuffer = new Queue<float>();

        public bool Update(float deltaTime)
        {
            CurrentFramesPerSecond = 1.0f / deltaTime;

            _sampleBuffer.Enqueue(CurrentFramesPerSecond);

            if (_sampleBuffer.Count > MAXIMUM_SAMPLES)
            {
                _sampleBuffer.Dequeue();
                AverageFramesPerSecond = _sampleBuffer.Average(i => i);
            }
            else
            {
                AverageFramesPerSecond = CurrentFramesPerSecond;
            }

            TotalFrames++;
            TotalSeconds += deltaTime;
            return true;
        }
    }
}
