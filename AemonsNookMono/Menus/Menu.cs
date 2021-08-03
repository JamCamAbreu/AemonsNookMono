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
        public Menu(string menuname, int width, int height, int x, int y, int padHeight, int padWidth, Color? color, string sprite)
        {
            if ((padHeight * 2) >= height || (padWidth * 2) >= width) { throw new Exception("Desired padding is too large and malforms Menu."); }

            this.MenuName = menuname;
            this.DynamicButtons = new List<Button>();
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
        public List<Button> DynamicButtons { get; set; }
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
            List<Button> priorDynamic = new List<Button>();
            foreach (Button b in this.DynamicButtons)
            {
                priorDynamic.Add(b);
            }

            this.DynamicButtons.Clear();
            foreach (Button b in priorDynamic)
            {
                this.AddDynamicButton(b.Name, b.Sprites, b.PrimaryColor);
            }
        }
        public void AddDynamicButton(string name, ButtonSprite sprites = null, Color? color = null)
        {
            int numButtons = this.DynamicButtons.Count + 1;
            int rowHeight = (this.Height - this.PadHeight * 2) / ((numButtons * 2) - 1);
            int buttonWidth = this.Width - this.PadWidth * 2;

            // Prior buttons:
            List<Button> priorButtons = new List<Button>();
            priorButtons.AddRange(this.DynamicButtons);
            this.DynamicButtons.Clear();
            int rowNum = 0;
            foreach (Button b in priorButtons)
            {
                if (string.IsNullOrEmpty(b.Name))
                {
                    this.DynamicButtons.Add(new Button(Graphics.Current.ScreenMidX, this.TopY + this.PadHeight + (rowHeight / 2) + (rowNum * rowHeight)));
                }
                else
                {
                    this.DynamicButtons.Add(new Button(b.Name, Graphics.Current.ScreenMidX, this.TopY + this.PadHeight + (rowHeight / 2) + (rowNum * rowHeight), buttonWidth, rowHeight, b.Sprites, b.PrimaryColor));
                }
                
                rowNum += 2;
            }

            if (string.IsNullOrEmpty(name))
            {
                this.DynamicButtons.Add(new Button(Graphics.Current.ScreenMidX, this.TopY + this.PadHeight + (rowHeight / 2) + (rowNum * rowHeight)));
            }
            else
            {
                this.DynamicButtons.Add(new Button(name, Graphics.Current.ScreenMidX, this.TopY + this.PadHeight + (rowHeight / 2) + (rowNum * rowHeight), buttonWidth, rowHeight, sprites, color));
            }
        }
        public void AddStaticButton(string name, int width, int height, int screenX, int screenY, ButtonSprite sprites = null, Color? color = null, Collision.CollisionShape shape = Collision.CollisionShape.Rectangle)
        {
            this.StaticButtons.Add(new Button(name, screenX, screenY, width, height, sprites, color, shape));
        }

        public Button CheckButtonCollisions(int x, int y)
        {
            foreach (Button b in this.DynamicButtons)
            {
                if (b.MyCollision != null && b.MyCollision.IsCollision(x, y))
                {
                    return b;
                }
            }
            foreach (Button b in this.StaticButtons)
            {
                if (b.MyCollision != null && b.MyCollision.IsCollision(x, y))
                {
                    return b;
                }
            }
            return null;
        }
        public Button GetButton(string name)
        {
            foreach (Button b in this.DynamicButtons)
            {
                if (b.Name == name) { return b; }
            }
            foreach (Button b in this.StaticButtons)
            {
                if (b.Name == name) { return b; }
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

                this.backPanel.Draw(Graphics.Current.ScreenMidX, Graphics.Current.ScreenMidY);

                foreach (Button b in this.DynamicButtons)
                {
                    b.Draw();
                }
                foreach (Button b in this.StaticButtons)
                {
                    b.Draw();
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
            //this.InitButtons();
        }
        public virtual void Update()
        {

        }
        public virtual bool HandleLeftClick(int x, int y)
        {
            Button backbutton = this.GetButton("Back");
            if (backbutton != null && backbutton.MyCollision != null && backbutton.MyCollision.IsCollision(x, y))
            {
                MenuManager.Current.CloseTop();
                return true;
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
