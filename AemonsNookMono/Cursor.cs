using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AemonsNookMono
{
    public class Cursor
    {
        private int timer;

        public int LastX;
        public int LastY;
        public int HoverTriggerFrames = 250;

        public bool Triggered;

        public Cursor()
        {
            this.timer = 0;
            this.Triggered = false;

            MouseState state = Mouse.GetState();
            this.LastX = state.X;
            this.LastY = state.Y;
        }

        public int Update(GameTime gameTime)
        {
            timer++;
            MouseState state = Mouse.GetState();

            if (state.X != LastX || state.Y != LastY)
            {
                this.timer = 0;
                this.Triggered = false;
            }

            if (!Triggered && timer >= this.HoverTriggerFrames)
            {
                this.Triggered = true;
                this.Trigger();
            }

            LastX = state.X;
            LastY = state.Y;
            return timer;
        }

        public void Trigger()
        {
            Debug.WriteLine($"Triggered mouse event");
        }
    }
}
