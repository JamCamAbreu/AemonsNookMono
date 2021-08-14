using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Menus
{
    public abstract class Cell
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int ScreenX { get; set; }
        public int ScreenY { get; set; }
        public bool Active { get; set; }
        public abstract void Refresh(int width, int height, int screenx, int screeny);
        public abstract void Draw();
        public abstract void Update();
    }
}
