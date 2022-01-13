using AemonsNookMono.Admin;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.GameWorld.Effects
{
    public class TempEffect
    {
        #region Implement Properties
        protected virtual string spriteBaseName { get; set; }
        protected virtual int spriteNumFrames { get; set; }
        #endregion

        #region Public Properties
        public bool Dead { get; set; }
        #endregion

        #region Constructors
        public TempEffect(int x, int y, int life, int speed, string spritename = "cursor-redx", int spriteFrames = 1)
        {
            this.xpos = x;
            this.ypos = y;
            this.life = life;
            this.speed = speed;
            this.curFrame = 1;
            this.spriteAlarm = speed;
            this.Dead = false;

            this.spriteBaseName = spritename;
            this.spriteNumFrames = spriteFrames;
        }
        #endregion

        #region Interface
        public virtual void Update()
        {
            #region Kill
            if (life > 0)
            {
                life--;
            }
            if (life <= 0)
            {
                this.Dead = true;
            }

            this.spriteAlarm--;
            if (this.spriteAlarm <= 0)
            {
                this.IncrementFrame();
                this.spriteAlarm = this.speed;
            }
            #endregion
        }

        public virtual void Draw()
        {
            if (spriteNumFrames > 1)
            {
                Graphics.Current.SpriteB.Draw(Graphics.Current.SpritesByName[$"{this.spriteBaseName}-{this.curFrame}"], new Vector2(World.Current.StartDrawX + xpos, World.Current.StartDrawY + ypos), Color.White);
            }
            else
            {
                Graphics.Current.SpriteB.Draw(Graphics.Current.SpritesByName[$"{this.spriteBaseName}"], new Vector2(World.Current.StartDrawX + xpos, World.Current.StartDrawY + ypos), Color.White);
            }
        }
        #endregion

        #region Internal Properties
        private int life { get; set; }
        private int xpos { get; set; }
        private int ypos { get; set; }
        private int speed { get; set; }
        private bool kill { get; set; }
        private int curFrame { get; set; }
        private int spriteAlarm { get; set; }
        #endregion

        #region Helper Methods
        private void IncrementFrame()
        {
            this.curFrame++;
            if (this.curFrame > this.spriteNumFrames)
            {
                this.curFrame = 1;
            }
        }
        #endregion
    }
}
