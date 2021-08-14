using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Menus
{
    public struct CellDimension
    {
        public CellDimension(int width, int height, int centerX, int centerY)
        {
            this.Width = width;
            this.Height = height;
            this.CenterX = centerX;
            this.CenterY = centerY;
        }
        public int Width { get; set; }
        public int Height{ get; set; }
        public int CenterX { get; set; }
        public int CenterY { get; set; }
    }
}
