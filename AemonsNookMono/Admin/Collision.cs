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
            Box,
            Oval
        }
        #endregion

        #region Constructor
        public Collision(int originX, int originY, int width, int height, CollisionShape s)
        {
            this.OriginX = originX;
            this.OriginY = originY;
            this.CollisionWidth = width;
            this.CollisionHeight = height;
            this.Shape = s;
        }
        #endregion

        #region Public Properties
        public CollisionShape Shape { get; set; }
        public int OriginX { get; set; }
        public int OriginY { get; set; }
        public int CollisionWidth { get; set; }
        public int CollisionHeight { get; set; }
        #endregion

        #region Interface
        public bool IsCollision(int x, int y)
        {
            throw new NotImplementedException();
            //if (this.Shape == CollisionShape.Box)
            //{
                // Simple four-point check
            //}
            //else if (this.Shape == CollisionShape.Oval)
            //{
                // To implement, use a line of circles (one per pixel height) and use the ovals width as the circle radius
                // Make sure to subtract the width from the height before determining how many circles to draw (if height == width, than only one circle is needed because it IS a cirlce)
            //}

            //return false;
        }
        #endregion
    }
}
