using AemonsNookMono.Admin;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Menus
{
    public class ButtonSpan
    {
        #region Enums
        public enum SpanType
        {
            Horizontal,
            Vertical
        }
        #endregion

        #region Constructor
        public ButtonSpan(int x, int y, int width, int height, int padWidth, int padHeight, SpanType type)
        {
            this.Type = type;
            this.CenterX = x;
            this.CenterY = y;
            this.Width = width;
            this.Height = height;
            this.PadWidth = padWidth;
            this.PadHeight = padHeight;
            this.Buttons = new List<Button>();
        }
        #endregion

        #region Public Properties
        public SpanType Type { get; set; }
        public int CenterX { get; set; }
        public int CenterY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int PadWidth { get; set; }
        public int PadHeight { get; set; }
        public List<Button> Buttons { get; set; }
        public int TopY { get { return this.CenterY - (this.Height / 2); } }
        public int LeftX { get { return this.CenterX - (this.Width / 2); } }
        #endregion

        #region Interface
        public void AddButton(Button button)
        {
            int numButtons = this.Buttons.Count + 1;

            int cellHeight = this.Height - (this.PadHeight * 2); // default
            int cellWidth = this.Width - this.PadWidth * 2; // default
            if (this.Type == SpanType.Vertical)
            {
                cellHeight = (this.Height - this.PadHeight * 2) / ((numButtons * 2) - 1);
            }
            else if (this.Type == SpanType.Horizontal)
            {
                cellWidth = (this.Width - this.PadWidth * 2) / ((numButtons * 2) - 1);
            }

            #region Prior Buttons
            int cellNum = 0;
            foreach (Button priorButton in this.Buttons)
            {
                if (this.Type == SpanType.Vertical)
                {
                    priorButton.ScreenX = this.CenterX;
                    priorButton.ScreenY = this.TopY + this.PadHeight + (cellHeight / 2) + (cellNum * cellHeight);
                }
                else if (this.Type == SpanType.Horizontal)
                {
                    priorButton.ScreenX = this.LeftX + this.PadWidth + (cellWidth / 2) + (cellNum * cellWidth);
                    priorButton.ScreenY = this.CenterY;
                }

                priorButton.Width = cellWidth;
                priorButton.Height = cellHeight;

                if (priorButton.Active)
                {
                    priorButton.MyCollision = new Collision(priorButton.Shape, priorButton.ScreenX, priorButton.ScreenY, cellWidth, cellHeight);
                    if (priorButton.Sprites != null)
                    {
                        throw new NotImplementedException();
                    }
                    else if (priorButton.PrimaryColor != null)
                    {
                        priorButton.ButtonColor = new ButtonColor(cellWidth, cellHeight, 1, (Color)priorButton.PrimaryColor);
                    }
                }

                cellNum += 2;
            }
            #endregion

            #region New Button
            if (button != null)
            {
                if (this.Type == SpanType.Vertical)
                {
                    button.ScreenX = this.CenterX;
                    button.ScreenY = this.TopY + this.PadHeight + (cellHeight / 2) + (cellNum * cellHeight);
                }
                else if (this.Type == SpanType.Horizontal)
                {
                    button.ScreenX = this.LeftX + this.PadWidth + (cellWidth / 2) + (cellNum * cellWidth);
                    button.ScreenY = this.CenterY;
                }

                button.Width = cellWidth;
                button.Height = cellHeight;
                button.MyCollision = new Collision(button.Shape, button.ScreenX, button.ScreenY, cellWidth, cellHeight);
                if (button.Sprites != null)
                {
                    throw new NotImplementedException();
                }
                else if (button.PrimaryColor != null)
                {
                    button.ButtonColor = new ButtonColor(cellWidth, cellHeight, 1, (Color)button.PrimaryColor);
                }
                
                this.Buttons.Add(button);
            }
            #endregion
        }
        public void AddButton(string name, Color? primaryColor)
        {
            Button created = new Button(name, 0, 0, this.PadWidth * 2, this.PadHeight * 2, null, primaryColor, Collision.CollisionShape.Rectangle, !string.IsNullOrEmpty(name));
            this.AddButton(created);
        }
        public void AddButton(string name, ButtonSprite sprites, Collision.CollisionShape shape)
        {
            Button created = new Button(name, 0, 0, this.PadWidth * 2, this.PadHeight * 2, sprites, null, shape, !string.IsNullOrEmpty(name));
            this.AddButton(created);
        }
        public void Draw()
        {
            foreach (Button b in this.Buttons)
            {
                b.Draw();
            }
        }
        public Button GetButton(string name)
        {
            foreach (Button b in this.Buttons)
            {
                if (b.Name == name) { return b; }
            }
            return null;
        }
        public bool ContainsButton(string name)
        {
            foreach (Button b in this.Buttons)
            {
                if (b.Name == name) { return true; }
            }
            return false;
        }
        public Button CheckButtonCollisions(int x, int y)
        {
            foreach (Button b in this.Buttons)
            {
                if (b.Active && b.MyCollision != null && b.MyCollision.IsCollision(x, y))
                {
                    return b;
                }
            }
            return null;
        }
        #endregion
    }
}
