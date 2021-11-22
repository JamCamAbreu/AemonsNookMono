using AemonsNookMono.GameWorld;
using AemonsNookMono.Resources;
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
        public int Timer { get; set; }
        public int LastX { get; set; }
        public int LastY { get; set; }
        public int LastWorldX { get; set; }
        public int LastWorldY { get; set; }
        public int CurDistanceFromCenter { get; set; }
        public int HoverTriggerFrames { get; set; } = 25;
        public bool Triggered { get; set; }
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

            Vector2 worldpos = Camera.Current.ScreenToWorld(new Vector2(this.LastX, this.LastY));
            this.LastWorldX = (int)worldpos.X;
            this.LastWorldY = (int)worldpos.Y;

            this.CurDistanceFromCenter = Global.ApproxDist(
                new Vector2(Graphics.Current.ScreenMidX, Graphics.Current.ScreenMidY),
                new Vector2(Cursor.Current.LastX, Cursor.Current.LastY)
                );
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

            this.LastX = state.X;
            this.LastY = state.Y;

            Vector2 worldpos = Camera.Current.ScreenToWorld(new Vector2(this.LastX, this.LastY));
            this.LastWorldX = (int)worldpos.X;
            this.LastWorldY = (int)worldpos.Y;

            this.CurDistanceFromCenter = Global.ApproxDist(
                new Vector2(Graphics.Current.ScreenMidX, Graphics.Current.ScreenMidY),
                new Vector2(this.LastX, this.LastY)
                );

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

            foreach (Resource r in World.Current.Resources.Sorted.Values)
            {
                if (r.IsCollision(this.LastX, this.LastY))
                {
                    this.CurrentHoverBox = new Help.HoverBox($"{r.Type}");
                    return;
                }
            }

            Tile t = World.Current.TileAtPixel(this.LastWorldX, this.LastWorldY);
            if (t != null)
            {
                this.CurrentHoverBox = new Help.HoverBox($"{t.Type} Tile");
                return;
            }
            else
            {
                this.CurrentHoverBox = new Help.HoverBox("Nothing here.");
                return;
            }
        }
        #endregion
    }
}
