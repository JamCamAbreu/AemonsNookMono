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
        public Menu()
        {
            this.Buttons = new List<Button>();
        }
        #endregion

        #region Public Properties
        public List<Button> Buttons { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int InnerPad { get; set; }
        public int TopY { get; set; }
        #endregion

        #region Interface
        public void AddButton(string name, ButtonSprite sprites = null, Color? color = null)
        {
            int numButtons = this.Buttons.Count + 1;
            int rowHeight = (this.Height - this.InnerPad * 2) / ((numButtons * 2) - 1);
            int buttonWidth = this.Width - this.InnerPad * 2;

            // Prior buttons:
            List<Button> priorButtons = new List<Button>();
            priorButtons.AddRange(this.Buttons);
            this.Buttons.Clear();
            int rowNum = 0;
            foreach (Button b in priorButtons)
            {
                this.Buttons.Add(new Button(b.Name, Graphics.Current.ScreenMidX, this.TopY + this.InnerPad + (rowHeight / 2) + (rowNum * rowHeight), buttonWidth, rowHeight, b.Sprites, b.PrimaryColor));
                rowNum += 2;
            }

            this.Buttons.Add(new Button(name, Graphics.Current.ScreenMidX, this.TopY + this.InnerPad + (rowHeight / 2) + (rowNum * rowHeight), buttonWidth, rowHeight, sprites, color));
        }
        public Button GetButton(string name)
        {
            foreach (Button b in this.Buttons)
            {
                if (b.Name == name) { return b; }
            }
            return null;
        }
        public virtual void Draw(bool isTop) { }
        public virtual void Update() { }
        #endregion
    }
}
