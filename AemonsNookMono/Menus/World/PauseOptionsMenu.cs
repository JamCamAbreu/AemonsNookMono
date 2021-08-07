using AemonsNookMono.Admin;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Menus.World
{
    public class PauseOptionsMenu : Menu
    {
        public PauseOptionsMenu()
            : base("Options",
                  (int)((float)Graphics.Current.ScreenHeight * 0.6f),
                  (int)((float)Graphics.Current.ScreenWidth * 0.4f),
                  Graphics.Current.ScreenMidX,
                  Graphics.Current.ScreenMidY,
                  ((int)((float)Graphics.Current.ScreenHeight * 0.6f) / 16),
                  (int)((float)Graphics.Current.ScreenWidth * 0.4f) / 16,
                  Color.SaddleBrown,
                  string.Empty)
        {
            this.InitButtons();
        }
        public override void InitButtons()
        {
            this.StaticButtons.Clear();
            this.ButtonSpans.Clear();

            ButtonSpan buttonSpan = new ButtonSpan(this.CenterX, this.CenterY, this.Width, this.Height, this.PadWidth, this.PadHeight, ButtonSpan.SpanType.Vertical);
            buttonSpan.AddButton("button1", Color.Black);
            buttonSpan.AddButton("", Color.DarkGreen);
            buttonSpan.AddButton("button2", Color.Black);
            buttonSpan.AddButton("button3", Color.DarkGreen);
            buttonSpan.AddButton("Back", Color.Black);
            this.ButtonSpans.Add(buttonSpan);

            ButtonSprite bulletUnselectedSprites = new ButtonSprite("menu-bullet-unselected", "menu-bullet-unselected-hover", "menu-bullet-unselected-click");
            ButtonSprite bulletSelectedSprites = new ButtonSprite("menu-bullet-selected", "menu-bullet-selected-hover", "menu-bullet-selected-hover");
            int spriteDim = 50;
            int collisionDim = 18;
            int radioYPos = buttonSpan.Buttons[1].ScreenY;
            if (Graphics.Current.FullScreen == true)
            {
                this.AddStaticButton("Windowed", spriteDim, spriteDim, this.CenterX - (this.Width / 4), radioYPos, bulletUnselectedSprites, null, Collision.CollisionShape.Circle);
                this.AddStaticButton("Fullscreen", spriteDim, spriteDim, this.CenterX + (this.Width / 4), radioYPos, bulletSelectedSprites, null, Collision.CollisionShape.Circle);
            }
            else
            {
                this.AddStaticButton("Windowed", spriteDim, spriteDim, this.CenterX - (this.Width / 4), radioYPos, bulletSelectedSprites, null, Collision.CollisionShape.Circle);
                this.AddStaticButton("Fullscreen", spriteDim, spriteDim, this.CenterX + (this.Width / 4), radioYPos, bulletUnselectedSprites, null, Collision.CollisionShape.Circle);
            }
            this.StaticButtons[0].TitlePosition = Button.TextPosition.Above;
            this.StaticButtons[1].TitlePosition = Button.TextPosition.Above;
            this.StaticButtons[0].MyCollision = new Collision(Collision.CollisionShape.Circle, this.CenterX - (this.Width / 4), radioYPos, collisionDim, collisionDim);
            this.StaticButtons[1].MyCollision = new Collision(Collision.CollisionShape.Circle, this.CenterX + (this.Width / 4), radioYPos, collisionDim, collisionDim);
        }
        public override void Refresh()
        {
            this.Height = (int)((float)Graphics.Current.ScreenHeight * 0.6f);
            this.Width = (int)((float)Graphics.Current.ScreenWidth * 0.4f);
            this.CenterX = Graphics.Current.ScreenMidX;
            this.CenterY = Graphics.Current.ScreenMidY;
            this.PadHeight = ((int)((float)Graphics.Current.ScreenHeight * 0.6f) / 16);
            this.PadWidth = (int)((float)Graphics.Current.ScreenWidth * 0.4f) / 16;

            base.Refresh();
            this.InitButtons();
        }
        public override void Draw(bool isTop)
        {
            if (isTop)
            {
                base.Draw(isTop);
            }
        }

        public override bool HandleLeftClick(int x, int y)
        {
            Button clicked = this.CheckButtonCollisions(x, y);
            if (clicked != null)
            {
                Debugger.Current.AddTempString($"You clicked on the {clicked.Name} button!");
                switch (clicked.Name)
                {
                    case "Windowed":
                        this.GetButton("Fullscreen").Sprites = new ButtonSprite("menu-bullet-unselected", "menu-bullet-unselected-hover", "menu-bullet-unselected-click");
                        clicked.Sprites = new ButtonSprite("menu-bullet-selected", "menu-bullet-selected-hover", "menu-bullet-selected-hover");
                        //Graphics.Current.FullScreen = false;
                        return true;

                    case "Fullscreen":
                        this.GetButton("Windowed").Sprites = new ButtonSprite("menu-bullet-unselected", "menu-bullet-unselected-hover", "menu-bullet-unselected-click");
                        clicked.Sprites = new ButtonSprite("menu-bullet-selected", "menu-bullet-selected-hover", "menu-bullet-selected-hover");
                        //Graphics.Current.FullScreen = true;
                        return true;

                    default:
                        return base.HandleLeftClick(x, y);
                }
            }
            return base.HandleLeftClick(x, y);
        }
    }
}
