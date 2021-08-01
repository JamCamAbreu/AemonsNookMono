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
        public Button(int x, int y)
        {
            this.ScreenX = x;
            this.ScreenY = y;
        }
        public Button(string name, int x, int y, int width, int height, ButtonSprite sprites, Color? color, Collision.CollisionShape collisionShape = Collision.CollisionShape.Rectangle)
        {
            this.Name = name;
            this.Width = width;
            this.Height = height;
            this.ScreenX = x;
            this.ScreenY = y;
            this.PrimaryColor = color;
            if (sprites != null)
            {
                this.Sprites = sprites;
                this.ButtonPan = null;
                this.PrimaryColor = color;
            }
            else
            {
                if (color == null) { throw new Exception("Whoops! Can't use a color button without a color!"); }
                this.Sprites = null;
                this.ButtonPan = new ButtonPanel(width, height, 1, (Color)color);
            }
            MyCollision = new Collision(collisionShape, x, y, width, height);
            MyTextPosition = TextPosition.Inline;
        }
        #endregion

        #region Public Properties
        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int ScreenX { get; set; }
        public int ScreenY { get; set; }
        public Collision MyCollision { get; set; }
        public ButtonSprite Sprites { get; set; }
        public ButtonPanel ButtonPan { get; set; }
        public Color? PrimaryColor { get; set; }
        public TextPosition MyTextPosition { get; set; }
        #endregion

        #region Interface
        public void Update()
        {

        }
        public void Draw()
        {
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
                        this.ButtonPan.Pressed.Draw(this.ScreenX, this.ScreenY);
                    }
                    else
                    {
                        this.ButtonPan.Hover.Draw(this.ScreenX, this.ScreenY);
                    }
                }
                else  if (this.ButtonPan != null)
                { 
                    this.ButtonPan.Normal.Draw(this.ScreenX, this.ScreenY); 
                }
            }

            if (!string.IsNullOrEmpty(Name))
            {
                int stringx = Graphics.Current.CenterStringX(this.ScreenX, this.Name, "couriernew");
                int stringy = Graphics.Current.CenterStringY(this.ScreenY, "couriernew");

                int textAdjust = 0;
                if (this.MyTextPosition == TextPosition.Above) { textAdjust = -this.Height; }
                else if (this.MyTextPosition == TextPosition.Inline) { textAdjust = 0; }
                else if (this.MyTextPosition == TextPosition.Below) { textAdjust = this.Height; }

                Graphics.Current.SpriteB.Begin();
                Graphics.Current.SpriteB.DrawString(Graphics.Current.Fonts["couriernew"], this.Name, new Vector2(stringx, stringy + textAdjust), Color.White);
                Graphics.Current.SpriteB.End();
            }
        }
        #endregion

    }
}
