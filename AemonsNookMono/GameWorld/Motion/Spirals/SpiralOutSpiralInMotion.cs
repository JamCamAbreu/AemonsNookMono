using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.GameWorld.Motion.Spirals
{
    public class SpiralOutSpiralInMotion : Motion
    {
        public SpiralOutSpiralInMotion(Vector2 startPos, bool repeat) : base(startPos, repeat) { }
        protected override void Init()
        {
            int numFrames = 60;
            int iterVal = 360 / 60;
            int radius = 1;
            int maxRadius = 100;
            int radiusPerIter = (maxRadius - radius) / numFrames;

            List<Vector2> vectorList = new List<Vector2>();
            float curX, curY;
            for (int i = 0; i <= 360; i+= iterVal)
            {
                curX = (float)(radius * Math.Cos(i));
                curY = (float)(radius * Math.Sin(i));
                vectorList.Add(new Vector2(curX, curY));
                radius += radiusPerIter;
            }

            this._Positions = vectorList.ToArray();
        }
    }
}
