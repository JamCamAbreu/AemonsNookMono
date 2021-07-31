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
        #region Constructor
        public Button(string name, int x, int y, int width, int height, ButtonSprite sprites, Color? color)
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
            MyCollision = new Collision(Collision.CollisionShape.Rectangle, x, y, width, height);
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
                // check mouse
            }
            else
            {
                if (this.MyCollision.IsCollision(mouse.X, mouse.Y))
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
                else { this.ButtonPan.Normal.Draw(this.ScreenX, this.ScreenY); }
                
            }

            int stringx = Graphics.Current.CenterStringX(this.ScreenX, this.Name, "couriernew");
            int stringy = Graphics.Current.CenterStringY(this.ScreenY, "couriernew");
            Graphics.Current.SpriteB.Begin();
            Graphics.Current.SpriteB.DrawString(Graphics.Current.Fonts["couriernew"], this.Name, new Vector2(stringx, stringy), Color.White);
            Graphics.Current.SpriteB.End();
        }
        #endregion

    }
}
