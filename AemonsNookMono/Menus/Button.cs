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
            this.Title = name;
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
                if (collisionShape == Collision.CollisionShape.Circle)
                {
                    if (sprites.SpriteWidth > sprites.SpriteHeight) { MyCollision = new Collision(collisionShape, x, y, sprites.SpriteHeight, sprites.SpriteHeight); }
                    else { MyCollision = new Collision(collisionShape, x, y, sprites.SpriteWidth, sprites.SpriteWidth); }
                }
                else
                {
                    MyCollision = new Collision(collisionShape, x, y, sprites.SpriteWidth, sprites.SpriteHeight);
                }
            }
            else if (color != null)
            {
                this.Sprites = null;
                if (width > 0 && height > 0)
                {
                    this.ButtonColor = new ButtonColor(width, height, 1, (Color)color);
                }
                MyCollision = new Collision(collisionShape, x, y, width, height);
            }
            else
            {
                this.Sprites = null;
                this.ButtonColor = null;
            }
            this.Shape = collisionShape;
            
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
        private Collision myCollision;
        public Collision MyCollision
        {
            get { return myCollision; }
            protected set { myCollision = value; }
        }
        public Collision.CollisionShape Shape { get; set; }
        public ButtonSprite Sprites { get; set; }
        public ButtonColor ButtonColor { get; set; }
        public Color? PrimaryColor { get; set; }
        public string Title { get; set; }
        public bool DisplayTitle { get; set; }
        public TextPosition TitlePosition { get; set; }
        #endregion

        #region Interface
        public void InitCollision(Collision.CollisionShape shape, int x, int y, int width, int height)
        {
            if (shape == Collision.CollisionShape.Circle)
            {
                if (width > height) { this.MyCollision = new Collision(shape, x, y, height, height); }
                else { this.MyCollision = new Collision(shape, x, y, width, width); }
            }
            else
            {
                this.MyCollision = new Collision(shape, x, y, width, height);
            }
        }
        public void Update()
        {

        }
        public void Draw()
        {
            if (!this.Active) { return; }

            MouseState mouse = Mouse.GetState();
            if (this.Sprites != null)
            {
                int spriteX = this.ScreenX - (this.Sprites.SpriteWidth / 2);
                int spriteY = this.ScreenY - (this.Sprites.SpriteHeight / 2);
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

            if (DisplayTitle && !string.IsNullOrEmpty(Title))
            {
                int buttonHeight = this.Height;
                if (this.Sprites != null) { buttonHeight = this.Sprites.SpriteHeight; }

                int stringx = Graphics.Current.CenterStringX(this.ScreenX, this.Title, "couriernew");
                int stringy = Graphics.Current.CenterStringY(this.ScreenY, "couriernew");

                int textAdjust = 0;
                if (this.TitlePosition == TextPosition.Above) { textAdjust = -buttonHeight; }
                else if (this.TitlePosition == TextPosition.Inline) { textAdjust = 0; }
                else if (this.TitlePosition == TextPosition.Below) { textAdjust = buttonHeight; }

                Graphics.Current.SpriteB.Begin();
                Graphics.Current.SpriteB.DrawString(Graphics.Current.Fonts["couriernew"], this.Title, new Vector2(stringx, stringy + textAdjust), Color.White);
                Graphics.Current.SpriteB.End();
            }

            if (Debugger.Current.ShowCircleCollisions && this.MyCollision != null && this.MyCollision.Shape == Collision.CollisionShape.Circle)
            {
                int spriteX = this.MyCollision.CenterX - (this.Sprites.SpriteWidth / 2);
                int spriteY = this.MyCollision.CenterY - (this.Sprites.SpriteHeight / 2);

                Collision col = this.MyCollision;
                float scale = Math.Max((float)this.MyCollision.Width / 10f, 1);
                Graphics.Current.SpriteB.Begin();
                Graphics.Current.SpriteB.Draw(Graphics.Current.SpritesByName["debug-circle"], new Vector2(spriteX, spriteY), null, Color.White, 0, Vector2.Zero, scale, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 1);
                Graphics.Current.SpriteB.End();
            }
        }
        #endregion

    }
}
