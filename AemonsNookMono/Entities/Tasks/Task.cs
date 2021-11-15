using AemonsNookMono.Admin;
using AemonsNookMono.GameWorld;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Entities.Tasks
{
    public class Task
    {
        #region Constructor
        public Task(Entity entity)
        {
            Random ran = new Random();
            this.TimerLength = ran.Next(20, 100);
            this.Timer = this.TimerLength;
            this.Finished = false;
            this.Entity = entity;
        }
        #endregion

        #region Public Properties
        public Entity Entity { get; set; }
        public int TimerLength { get; set; }
        public int Timer { get; set; }
        public bool Finished { get; set; }
        #endregion



        #region Interface
        public virtual void Draw()
        {

        }
        public virtual void Update()
        {
            // Make sure each task defines itself, and when its finished.
            throw new NotImplementedException();
        }

        #endregion
    }
}
