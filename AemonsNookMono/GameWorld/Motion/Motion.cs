using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.GameWorld.Motion
{
    public abstract class Motion
    {
        #region Public Properties
        public Vector2 StartPosition { get; set; }
        public int CurrentIndex { get; set; }
        public bool Complete { get; set; }
        public int LoopCount { get; set; }
        public int NumPositions
        {
            get
            {
                return _Positions.Length;
            }
        }
        #endregion

        #region Constructor
        public Motion(Vector2 StartPos, bool repeat)
        {
            this.StartPosition = StartPos;
            this.CurrentIndex = 0;
            this.Complete = false;
            this._Repeat = repeat;
            this.LoopCount = 0;
            Init();
        }
        #endregion

        #region Game Loop
        // Init could be defined by just a set of static coordinates, 
        // or a function could be used to generate coordinates,
        // or both! Use your imagination. 
        protected abstract void Init();
        public virtual Vector2 Step()
        {
            return Step(1, 1);
        }
        public virtual Vector2 Step(float scaleX, float scaleY)
        {
            // Motion wrap
            if (this.CurrentIndex == this.NumPositions - 1)
            {
                this.CurrentIndex = 0;
                this.LoopCount++;
                if (!this._Repeat)
                {
                    this.Complete = true;
                    return this.StartPosition;
                }
            }

            Vector2 pos = this._Positions[CurrentIndex];
            this.CurrentIndex++;

            return new Vector2(this.StartPosition.X + (pos.X * scaleX), this.StartPosition.Y + (pos.Y * scaleY));
        }
        #endregion

        #region Internal
        protected virtual Vector2[] _Positions { get; set; }
        protected virtual bool _Repeat { get; set; }
        #endregion
    }
}
