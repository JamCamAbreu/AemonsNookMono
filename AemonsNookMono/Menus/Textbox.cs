using AemonsNookMono.Admin;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Menus
{
    public class Textbox : Cell
    {
        #region Enums
        public enum HorizontalAlign
        {
            Left,
            Center,
            Right
        }
        public enum VerticalAlign
        {
            Top,
            Center,
            Bottom
        }
        #endregion

        #region Constructor
        public Textbox(string text, int centerx, int centery, int width, int height, string fontname, Color? background, HorizontalAlign horzAlign, VerticalAlign vertAlign, bool active = true)
        {
            this.Text = text;
            this.Width = width;
            this.Height = height;
            this.ScreenX = centerx;
            this.ScreenY = centery;
            this.Font = fontname;
            this.Background = background;
            this.HorizontalAlignment = horzAlign;
            this.VerticalAlignment = vertAlign;
            this.Active = active;

            this.SquishText();
        }
        #endregion

        #region Public Properties
        public string Text { get; set; }
        
        public Color? Background { get; set; }
        public string Font { get; set; }
        public VerticalAlign VerticalAlignment { get; set; }
        public HorizontalAlign HorizontalAlignment { get; set; }
        #endregion

        #region Interface
        public void SquishText()
        {
            string[] words = this.Text.Split(' ');
            StringBuilder squished = new StringBuilder();
            int curWidth = 0;
            int wordWidth = 0;
            int sizeSpace = Graphics.Current.StringWidth(" ", this.Font);
            foreach (string word in words)
            {
                wordWidth = Graphics.Current.StringWidth(word, this.Font);
                if (curWidth + wordWidth <= this.Width) 
                { 
                    squished.Append(word);
                    squished.Append(" ");
                    curWidth += wordWidth;
                    curWidth += sizeSpace;
                }
                else 
                { 
                    squished.Append('\n');
                    squished.Append(word);
                    squished.Append(" ");
                    curWidth = wordWidth;
                    curWidth += sizeSpace;
                }
            }
            this.formattedText = squished.ToString();
        }
        public override void Refresh(int width, int height, int screenx, int screeny)
        {
            this.Width = width;
            this.Height = height;
            this.ScreenX = screenx;
            this.ScreenY = screeny;
            this.SquishText();
        }
        public override void Update()
        {

        }
        public override void Draw()
        {
            if (!this.Active) { return; }
            
            int stringx;
            int stringy = Graphics.Current.CenterStringY(this.ScreenY, this.formattedText, this.Font);

            if (this.HorizontalAlignment == HorizontalAlign.Left)
            {
                stringx = this.ScreenX;
                stringy = this.ScreenY;
            }
            else if (this.HorizontalAlignment == HorizontalAlign.Right)
            {
                stringx = Graphics.Current.RightAlignStringX(this.ScreenX, this.formattedText, this.Font);
            }
            else // (Center)
            {
                stringx = Graphics.Current.CenterStringX(this.ScreenX, this.formattedText, this.Font);
            }

            int vertAdjust = 0;
            if (this.VerticalAlignment == VerticalAlign.Top) { vertAdjust = -this.Height/2; }
            else if (this.VerticalAlignment == VerticalAlign.Center) { vertAdjust = 0; }
            else if (this.VerticalAlignment == VerticalAlign.Bottom) { vertAdjust = this.Height / 2; }

            Graphics.Current.SpriteB.Begin();
            Graphics.Current.SpriteB.DrawString(Graphics.Current.Fonts[this.Font], this.formattedText, new Vector2(stringx, stringy + vertAdjust), Color.White);
            Graphics.Current.SpriteB.End();

        }
        #endregion

        #region Internal
        public string formattedText { get; set; }
        #endregion
    }
}
