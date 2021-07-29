using AemonsNookMono.GameWorld;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AemonsNookMono.Admin
{
    public sealed class Cursor
    {
        #region Singleton Implementation
        private static Cursor instance;
        private static object _lock = new object();
        private Cursor() { }
        public static Cursor Current
        {
            get
            {
                if (instance == null)
                {
                    lock (_lock)
                    {
                        if (instance == null)
                        {
                            instance = new Cursor();
                        }
                    }
                }
                return instance;
            }
        }
        #endregion

        #region HoverBox Trigger
        public int Timer;
        public int LastX;
        public int LastY;
        public int HoverTriggerFrames = 27;
        public bool Triggered;
        public Help.HoverBox CurrentHoverBox { get; set; }
        #endregion

        #region Interface
        public void Init()
        {
            this.Timer = 0;
            this.Triggered = false;

            MouseState state = Mouse.GetState();
            this.LastX = state.X;
            this.LastY = state.Y;
        }
        public int Update(GameTime gameTime)
        {
            Timer++;
            MouseState state = Mouse.GetState();

            if (state.X != LastX || state.Y != LastY)
            {
                this.MouseMove();
            }

            if (!Triggered && Timer >= this.HoverTriggerFrames)
            {
                this.AlarmTrigger();
            }

            LastX = state.X;
            LastY = state.Y;
            return Timer;
        }
        public void Draw()
        {
            if (this.CurrentHoverBox != null)
            {
                this.CurrentHoverBox.Draw();
            }
        }
        #endregion

        #region Helper Methods
        private void MouseMove()
        {
            this.Timer = 0;
            this.Triggered = false;
            this.CurrentHoverBox = null;
        }
        private void AlarmTrigger()
        {
            this.Triggered = true;

            Tile t = World.Current.TileAtPixel(this.LastX, this.LastY);
            if (t != null)
            {
                this.CurrentHoverBox = new Help.HoverBox($"Tile ({t.Column}, {t.Row})\nType: {t.Type}");
            }
            else
            {
                this.CurrentHoverBox = new Help.HoverBox("Nothing here.");
            }
        }
        #endregion
    }
}
