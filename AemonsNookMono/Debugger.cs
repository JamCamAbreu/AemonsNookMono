using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AemonsNookMono
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
        public GameWorld.World CurrentWorld { get; set; }
        #endregion

        #region Interface
        public void Init()
        {
            fps = new FrameCounter();
            screenWidthPixels = Graphics.Current.Device.Viewport.Width;
            this.DrawTileShapes = false;
        }
        public void Update(GameTime gameTime)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            this.fps.Update(deltaTime);
        }
        public void Draw(GameTime gameTime)
        {
            Graphics.Current.SpriteB.Begin();
            List<string> debugMessages = new List<string>();

            #region FPS
            debugMessages.Add($"FPS: {this.fps.AverageFramesPerSecond}");
            #endregion

            #region Mouse Hover
            debugMessages.Add($"Mouse: {Cursor.Current.LastX}, {Cursor.Current.LastY}");
            debugMessages.Add($"MouseTm: {Cursor.Current.Timer}");
            debugMessages.Add($"MouseTrig: " + (Cursor.Current.Triggered ? "True" : "False"));
            #endregion

            #region World
            if (CurrentWorld != null)
            {
                if (CurrentWorld.Resources != null)
                {
                    debugMessages.Add($"Resource Count: {CurrentWorld.Resources.Sorted.Count}");
                }
            }
            #endregion

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

            Graphics.Current.SpriteB.End();
        }
        #endregion

        #region Private Properties
        private FrameCounter fps { get; set; }
        private int screenWidthPixels { get; set; }
        private const int ROW_HEIGHT = 16;
        private const int PAD = 4;
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
