using AemonsNookMono.GameWorld;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Admin
{
    public class Collision
    {
        #region Enums
        public enum CollisionShape
        {
            Rectangle,
            Circle
        }
        #endregion

        #region Constructor
        public Collision(CollisionShape s, int centerX, int centerY, int width, int height)
        {
            if (s == CollisionShape.Circle && width != height)
            {
                throw new Exception($"Bro. Ovals are not great, let's chill with that idea and stick with perfect circles. Remember, opt for multiple simple collisions rather than a single complex collision");
            }

            this.CenterX = centerX;
            this.CenterY = centerY;
            this.Width = width;
            this.Height = height;
            this.Shape = s;
        }
        #endregion

        #region Public Properties
        public CollisionShape Shape { get; set; }
        public int CenterX { get; set; }
        public int CenterY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        #endregion

        #region Interface
        public bool IsCollision(int x, int y)
        {
            if (this.Shape == CollisionShape.Rectangle)
            {
                if (x >= (this.CenterX - this.Width/2) && x <= (this.CenterX + this.Width/2) &&
                    y >= (this.CenterY - this.Height/2) && y <= (this.CenterY + this.Height/2)) 
                { return true; }
            }
            else if (this.Shape == CollisionShape.Circle)
            {
                if (this.WithinRadius(this.Width, x, y, this.CenterX, this.CenterY)) { return true; }
                else return false;
            }

            return false;
        }
        #endregion

        #region Internal
        private bool WithinRadius(int radius, int x1, int y1, int x2, int y2)
        {
            if (radius <= 1) { throw new Exception("Bro, seriously?"); }
            int approxDist = IntSqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1)); // Pythagorean brah.
            if (radius >= approxDist) { return true; }
            else return false;
        }
        private int IntSqrt(int num)
        {
            if (0 == num) { return 0; }  // Avoid zero divide  
            int n = (num / 2) + 1;       // Initial estimate, never low  
            int n1 = (n + (num / n)) / 2;
            while (n1 < n)
            {
                n = n1;
                n1 = (n + (num / n)) / 2;
            } // end while  
            return n;
        }
        #endregion

    }
}
