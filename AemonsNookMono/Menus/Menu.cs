using AemonsNookMono.Admin;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Menus
{
    public class Menu
    {
        #region Constructor
        public Menu(string menuname, int width, int height, int x, int y, int padWidth, int padHeight, Color? color, string sprite)
        {
            if ((padHeight * 2) >= height || (padWidth * 2) >= width) { throw new Exception("Desired padding is too large and malforms Menu."); }

            this.MenuName = menuname;
            this.ButtonSpans = new List<ButtonSpan>();
            this.StaticButtons = new List<Button>();
            this.Width = width;
            this.Height = height;
            this.CenterX = x;
            this.CenterY = y;
            this.PadHeight = padHeight;
            this.PadWidth = padWidth;

            this.ForegroundColor = color;
            this.Sprite = sprite;

            this.TopY = this.CenterY - this.Height / 2;
            this.LeftX = this.CenterX - this.Width / 2;

            if (color != null)
            {
                this.backPanel = new Panel(width, height, Color.Black, color, 1);
            }
            if (!string.IsNullOrEmpty(sprite))
            {
                this.backPanel = null;
            }
        }
        #endregion

        #region Public Properties
        public string MenuName { get; set; }
        public List<ButtonSpan> ButtonSpans { get; set; }
        public List<Button> StaticButtons { get; set; }
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

        public void AddStaticButton(string name, int width, int height, int screenX, int screenY, ButtonSprite sprites = null, Color? color = null, Collision.CollisionShape shape = Collision.CollisionShape.Rectangle)
        {
            this.StaticButtons.Add(new Button(name, screenX, screenY, width, height, sprites, color, shape));
        }

        public Button CheckButtonCollisions(int x, int y)
        {
            foreach (Button b in this.StaticButtons)
            {
                if (b.MyCollision != null && b.MyCollision.IsCollision(x, y))
                {
                    return b;
                }
            }

            foreach (ButtonSpan span in this.ButtonSpans)
            {
                Button b;
                b = span.CheckButtonCollisions(x, y);
                if (b != null) { return b; }
            }

            return null;
        }
        public Button GetButton(string name)
        {
            foreach (Button b in this.StaticButtons)
            {
                if (b.Name == name) 
                { 
                    return b; 
                }
            }

            foreach (ButtonSpan span in this.ButtonSpans)
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
                int titlex = Graphics.Current.CenterStringX(Graphics.Current.ScreenMidX, this.MenuName, "couriernew");
                int titley = this.TopY - 32;
                Graphics.Current.SpriteB.DrawString(Graphics.Current.Fonts["couriernew"], this.MenuName, new Vector2(titlex, titley), Color.White);
                Graphics.Current.SpriteB.End();

                this.backPanel.Draw(this.CenterX, this.CenterY);

                foreach (Button b in this.StaticButtons)
                {
                    b.Draw();
                }

                foreach (ButtonSpan span in this.ButtonSpans)
                {
                    span.Draw();
                }
            }
        }
        public virtual void Refresh()
        {
            if (this.ForegroundColor != null)
            {
                this.backPanel = new Panel(this.Width, this.Height, Color.Black, this.ForegroundColor, 1);
            }
            if (!string.IsNullOrEmpty(this.Sprite))
            {
                this.backPanel = null;
            }
        }
        public virtual void Update()
        {

        }
        public virtual bool HandleLeftClick(int x, int y)
        {
            Button clicked = this.CheckButtonCollisions(x, y);
            if (clicked != null)
            {
                switch (clicked.Name)
                {
                    case "Back":
                        MenuManager.Current.CloseTop();
                        return true;

                    default:
                        Debugger.Current.AddTempString($"Button {clicked.Name} has no defined functionality yet.");
                        return true;
                }
            }

            return false;
        }
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
