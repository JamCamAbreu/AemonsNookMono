using AemonsNookMono.Admin;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Menus
{
    public class Button
    {
        #region Enums
        public enum TextPosition
        {
            Above,
            Inline,
            Below
        }
        #endregion

        #region Constructor
        public Button(string name, int x, int y, int width, int height, ButtonSprite sprites, Color? color, Collision.CollisionShape collisionShape = Collision.CollisionShape.Rectangle, bool active = true)
        {
            this.Name = name;
            this.Active = active;
            this.Width = width;
            this.Height = height;
            this.ScreenX = x;
            this.ScreenY = y;
            this.PrimaryColor = color;
            if (sprites != null)
            {
                this.Sprites = sprites;
                this.ButtonColor = null;
                this.PrimaryColor = color;
            }
            else
            {
                if (color == null) { throw new Exception("Whoops! Can't use a color button without a color!"); }
                this.Sprites = null;
                this.ButtonColor = new ButtonColor(width, height, 1, (Color)color);
            }
            this.Shape = collisionShape;
            MyCollision = new Collision(collisionShape, x, y, width, height);
            this.TitlePosition = TextPosition.Inline;
            this.DisplayTitle = true;
        }
        #endregion

        #region Public Properties
        public string Name { get; set; }
        public bool Active { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int ScreenX { get; set; }
        public int ScreenY { get; set; }
        public Collision MyCollision { get; set; }
        public Collision.CollisionShape Shape { get; set; }
        public ButtonSprite Sprites { get; set; }
        public ButtonColor ButtonColor { get; set; }
        public Color? PrimaryColor { get; set; }
        public bool DisplayTitle { get; set; }
        public TextPosition TitlePosition { get; set; }
        #endregion

        #region Interface
        public void Update()
        {

        }
        public void Draw()
        {
            if (!this.Active) { return; }

            MouseState mouse = Mouse.GetState();
            if (this.Sprites != null)
            {
                int spriteX = this.ScreenX - (this.Width / 2);
                int spriteY = this.ScreenY - (this.Height / 2);
                if (this.MyCollision != null && this.MyCollision.IsCollision(mouse.X, mouse.Y))
                {
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        Graphics.Current.SpriteB.Begin();
                        Graphics.Current.SpriteB.Draw(Graphics.Current.SpritesByName[this.Sprites.SpriteClick], new Vector2(spriteX, spriteY), Color.White);
                        Graphics.Current.SpriteB.End();
                    }
                    else
                    {
                        Graphics.Current.SpriteB.Begin();
                        Graphics.Current.SpriteB.Draw(Graphics.Current.SpritesByName[this.Sprites.SpriteHover], new Vector2(spriteX, spriteY), Color.White);
                        Graphics.Current.SpriteB.End();
                    }
                }
                else if (this.Sprites != null)
                {
                    Graphics.Current.SpriteB.Begin();
                    Graphics.Current.SpriteB.Draw(Graphics.Current.SpritesByName[this.Sprites.SpriteNormal], new Vector2(spriteX, spriteY), Color.White);
                    Graphics.Current.SpriteB.End();
                }
            }
            else
            {
                if (this.MyCollision != null && this.MyCollision.IsCollision(mouse.X, mouse.Y))
                {
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        this.ButtonColor.Pressed.Draw(this.ScreenX, this.ScreenY);
                    }
                    else
                    {
                        this.ButtonColor.Hover.Draw(this.ScreenX, this.ScreenY);
                    }
                }
                else  if (this.ButtonColor != null)
                { 
                    this.ButtonColor.Normal.Draw(this.ScreenX, this.ScreenY); 
                }
            }

            if (DisplayTitle && !string.IsNullOrEmpty(Name))
            {
                int stringx = Graphics.Current.CenterStringX(this.ScreenX, this.Name, "couriernew");
                int stringy = Graphics.Current.CenterStringY(this.ScreenY, "couriernew");

                int textAdjust = 0;
                if (this.TitlePosition == TextPosition.Above) { textAdjust = -this.Height; }
                else if (this.TitlePosition == TextPosition.Inline) { textAdjust = 0; }
                else if (this.TitlePosition == TextPosition.Below) { textAdjust = this.Height; }

                Graphics.Current.SpriteB.Begin();
                Graphics.Current.SpriteB.DrawString(Graphics.Current.Fonts["couriernew"], this.Name, new Vector2(stringx, stringy + textAdjust), Color.White);
                Graphics.Current.SpriteB.End();
            }
        }
        #endregion

    }
}
