using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.GameWorld.Motion.Buildings
{
    public class FallBounceMotion : Motion
    {
        public FallBounceMotion(Vector2 startPos, bool repeat) : base(startPos, repeat) { }
        protected override void Init()
        {

            #region INITIAL DROP
            List<Vector2> vectorList = new List<Vector2>();
            List<float> speedIndex = new List<float>();
            int initialHeight = -20;
            float curY;
            for (float i = 5.0f; initialHeight + i < 0; i *= 1.1f)
            {
                curY = (float)(initialHeight + i);
                vectorList.Add(new Vector2(0, curY));
                speedIndex.Add(i);
            }
            vectorList.Add(new Vector2(0, 0));
            speedIndex.Add(0);
            #endregion

            #region SHORT BOUNCE
            float bounceHeight = 3.5f;
            for (float i = 1.2f; i < bounceHeight; i *= 2.2f)
            {
                curY = (float)(-i);
                vectorList.Add(new Vector2(0, curY));
                speedIndex.Add(i);

                if (speedIndex.Count > 1000) { throw new Exception("Whoops"); }
            }
            for (float i = 1.2f; i < bounceHeight; i *= 2.2f)
            {
                curY = (float)(-bounceHeight + i);
                vectorList.Add(new Vector2(0, curY));
                speedIndex.Add(i);

                if (speedIndex.Count > 1000) { throw new Exception("Whoops"); }
            }
            vectorList.Add(new Vector2(0, 0));
            speedIndex.Add(0);
            #endregion

            this._Positions = vectorList.ToArray();
        }
    }
}
