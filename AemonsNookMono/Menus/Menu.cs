using AemonsNookMono.Admin;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AemonsNookMono.Menus
{
    public abstract class Menu
    {
        #region Constructor
        public Menu(string menuname, int width, int height, int x, int y, int padWidth, int padHeight, Color? colorOverride, string sprite)
        {
            if ((padHeight * 2) >= height || (padWidth * 2) >= width) { throw new Exception("Desired padding is too large and malforms Menu."); }

            this.MenuName = menuname;
            this.Spans = new List<Span>();
            this.PagingSpans = new List<PagingSpan>();
            this.StaticCells = new List<Cell>();
            this.Width = width;
            this.Height = height;
            this.CenterX = x;
            this.CenterY = y;
            this.PadHeight = padHeight;
            this.PadWidth = padWidth;

            this.ForegroundColor = colorOverride;
            this.Sprite = sprite;

            this.TopY = this.CenterY - this.Height / 2;
            this.LeftX = this.CenterX - this.Width / 2;

            if (colorOverride != null)
            {
                this.backPanel = new Panel(width, height, this.CenterX, this.CenterY, Color.Black, colorOverride, 2);
            }
            else
            {
                this.backPanel = new Panel(width, height, this.CenterX, this.CenterY, Color.Black, Color.SaddleBrown, 2);
            }
            if (!string.IsNullOrEmpty(sprite))
            {
                this.backPanel = null;
            }
        }
        #endregion

        #region Public Properties
        public string MenuName { get; set; }
        public List<Span> Spans { get; set; }
        public List<PagingSpan> PagingSpans { get; set; }
        public List<Cell> StaticCells { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int CenterX { get; set; }
        public int CenterY { get; set; }
        public int PadHeight { get; set; }
        public int PadWidth { get; set; }
        public int TopY { get; set; }
        public int LeftX { get; set; }
        public Color? ForegroundColor { get; set; }
        public string Sprite { get; set; }
        #endregion

        #region Interface
        public virtual void InitButtons()
        {
        }

        public void AddStaticButton(string name, string title, int width, int height, int screenX, int screenY, ButtonSprite sprites = null, Color? color = null, Collision.CollisionShape shape = Collision.CollisionShape.Rectangle)
        {
            this.StaticCells.Add(new Button(name, title, screenX, screenY, width, height, sprites, color, shape));
        }
        public void AddStaticSpan(int centerx, int centery, int width, int height, int padwidth, int padheight, Span.SpanType type)
        {
            this.StaticCells.Add(new Span(centerx, centery, width, height, padwidth, padheight, type));
        }
        public void AddStaticSpan(Span span)
        {
            this.StaticCells.Add(span);
        }

        public Button CheckButtonCollisions(int x, int y)
        {
            foreach (Button b in this.StaticCells.Where(cell => cell is Button))
            {
                if (b.MyCollision != null && b.MyCollision.IsCollision(x, y))
                {
                    return b;
                }
            }
            foreach (Span span in this.StaticCells.Where(cell => cell is Span))
            {
                Button b;
                b = span.CheckButtonCollisions(x, y);
                if (b != null) { return b; }
            }

            foreach (Span span in this.Spans)
            {
                Button b;
                b = span.CheckButtonCollisions(x, y);
                if (b != null) { return b; }
            }
            foreach (PagingSpan span in this.PagingSpans)
            {
                Button b;
                b = span.CheckButtonCollisions(x, y);
                if (b != null) { return b; }
            }

            return null;
        }
        public Button GetButton(string name)
        {
            foreach (Button b in this.StaticCells.Where(cell => cell is Button))
            {
                if (b.ButtonCode == name) 
                { 
                    return b; 
                }
            }
            foreach (Span span in this.StaticCells.Where(cell => cell is Span))
            {
                if (span.ContainsButton(name))
                {
                    return span.GetButton(name);
                }
            }

            foreach (Span span in this.Spans)
            {
                if (span.ContainsButton(name))
                {
                    return span.GetButton(name);
                }
            }
            foreach (PagingSpan span in this.PagingSpans)
            {
                if (span.ContainsButton(name))
                {
                    return span.GetButton(name);
                }
            }

            return null;
        }
        public virtual void Draw(bool isTop)
        {
            if (isTop)
            {
                Graphics.Current.SpriteB.Begin();
                int titlex = Graphics.Current.CenterStringX(this.CenterX, this.MenuName, "couriernew");
                int titley = this.TopY - 32;
                Graphics.Current.SpriteB.DrawString(Graphics.Current.Fonts["couriernew"], this.MenuName, new Vector2(titlex, titley), Color.White);
                Graphics.Current.SpriteB.End();

                this.backPanel.Draw();

                foreach (Cell c in this.StaticCells)
                {
                    c.Draw();
                }

                foreach (Span span in this.Spans)
                {
                    span.Draw();
                }
                foreach (PagingSpan span in this.PagingSpans)
                {
                    span.Draw();
                }
            }
        }
        public virtual void Refresh()
        {
            if (this.ForegroundColor != null)
            {
                this.backPanel = new Panel(this.Width, this.Height, this.CenterX, this.CenterY, Color.Black, this.ForegroundColor, 1);
            }
            if (!string.IsNullOrEmpty(this.Sprite))
            {
                this.backPanel = null;
            }
        }
        public virtual void Update()
        {
            foreach (Cell cell in this.StaticCells)
            {
                cell.Update();
            }
            foreach (Span span in this.Spans)
            {
                span.Update();
            }
            foreach (PagingSpan span in this.PagingSpans)
            {
                span.Update();
            }
        }
        public abstract bool HandleLeftClick(int x, int y);
        //{
        //    Button clicked = this.CheckButtonCollisions(x, y);
        //    if (clicked != null)
        //    {
        //        switch (clicked.ButtonCode)
        //        {
        //            case "Back":
        //                MenuManager.Current.CloseTop();
        //                return true;

        //            default:
        //                Debugger.Current.AddTempString($"Button {clicked.ButtonCode} has no defined functionality yet.");
        //                return true;
        //        }
        //    }

        //    return false;
        //}
        public virtual bool HandleEscape()
        {
            MenuManager.Current.CloseTop();
            return true;
        }
    #endregion

    #region Internal
    protected Panel backPanel { get; set; }
        #endregion
    }
}
