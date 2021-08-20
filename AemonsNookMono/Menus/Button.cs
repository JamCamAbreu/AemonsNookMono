using AemonsNookMono.Admin;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Menus
{
    public class Button : Cell
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
        public Button(string name, string title, int x, int y, int width, int height, ButtonSprite sprites, Color? color, Collision.CollisionShape collisionShape = Collision.CollisionShape.Rectangle, bool active = true)
        {
            this.ButtonCode = name;
            this.Title = title;
            this.Active = active;
            this.Width = width;
            this.Height = height;
            this.ScreenX = x;
            this.ScreenY = y;
            this.PrimaryColor = color;
            this.TitleColor = Color.White;
            this.Selected = false;
            if (this.PrimaryColor == Color.White) { this.TitleColor = Color.Black; }
            if (sprites != null)
            {
                this.Sprites = sprites;
                this.ButtonColor = null;
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
            else if (this.PrimaryColor != null)
            {
                this.Sprites = null;
                if (width > 0 && height > 0)
                {
                    this.ButtonColor = new ButtonColor(width, height, this.ScreenX, this.ScreenY, 1, (Color)color);
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
        }
        #endregion

        #region Public Properties
        public string ButtonCode { get; set; }
        
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
        public Color TitleColor { get; set; }
        public bool Selected { get; set; }
        #endregion

        #region Interface
        public override void Refresh(int width, int height, int screenx, int screeny)
        {
            this.Width = width;
            this.Height = height;
            this.ScreenX = screenx;
            this.ScreenY = screeny;

            if (this.Active)
            {
                int collisionWidth = width;
                int collisionHeight = height;
                if (this.Sprites != null)
                {
                    collisionWidth = this.Sprites.SpriteWidth;
                    collisionHeight = this.Sprites.SpriteHeight;
                }
                this.InitCollision(this.Shape, this.ScreenX, this.ScreenY, collisionWidth, collisionHeight);
                if (this.PrimaryColor != null)
                {
                    this.ButtonColor = new ButtonColor(width, height, this.ScreenX, this.ScreenY, 1, (Color)this.PrimaryColor);
                }
            }
        }
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
        public override void Update()
        {

        }
        public override void Draw()
        {
            if (!this.Active) { return; }

            MouseState mouse = Mouse.GetState();
            if (this.Sprites != null)
            {
                int spriteX = this.ScreenX - (this.Sprites.SpriteWidth / 2);
                int spriteY = this.ScreenY - (this.Sprites.SpriteHeight / 2);
                if (this.Selected)
                {
                    Graphics.Current.SpriteB.Begin();
                    Graphics.Current.SpriteB.Draw(Graphics.Current.SpritesByName[this.Sprites.SpriteClick], new Vector2(spriteX, spriteY), Color.White);
                    Graphics.Current.SpriteB.End();
                }
                else if (this.MyCollision != null && this.MyCollision.IsCollision(mouse.X, mouse.Y))
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
                if (this.Selected)
                {
                    this.ButtonColor.Pressed.Draw();
                }
                else if (this.MyCollision != null && this.MyCollision.IsCollision(mouse.X, mouse.Y))
                {
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        this.ButtonColor.Pressed.Draw();
                    }
                    else
                    {
                        this.ButtonColor.Hover.Draw();
                    }
                }
                else if (this.ButtonColor != null)
                { 
                    this.ButtonColor.Normal.Draw(); 
                }
            }

            if (!string.IsNullOrEmpty(Title))
            {
                int buttonHeight = this.Height;
                if (this.Sprites != null) { buttonHeight = this.Sprites.SpriteHeight; }

                int stringx = Graphics.Current.CenterStringX(this.ScreenX, this.Title, "couriernew");
                int stringy = Graphics.Current.CenterStringY(this.ScreenY, this.Title, "couriernew");

                int textAdjust = 0;
                if (this.TitlePosition == TextPosition.Above) { textAdjust = -buttonHeight; }
                else if (this.TitlePosition == TextPosition.Inline) { textAdjust = 0; }
                else if (this.TitlePosition == TextPosition.Below) { textAdjust = buttonHeight; }

                Graphics.Current.SpriteB.Begin();
                Graphics.Current.SpriteB.DrawString(Graphics.Current.Fonts["couriernew"], this.Title, new Vector2(stringx, stringy + textAdjust), this.TitleColor);
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
