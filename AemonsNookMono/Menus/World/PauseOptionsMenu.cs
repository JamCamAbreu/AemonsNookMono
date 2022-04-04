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
                  null,
                  string.Empty)
        {
            this.InitButtons();
        }
        public override void InitButtons()
        {
            this.StaticCells.Clear();
            this.CellGroupings.Clear();

            Span buttonSpan = new Span(this.CenterX, this.CenterY, this.Width, this.Height, this.PadWidth, this.PadHeight, Span.SpanType.Vertical);
            buttonSpan.AddColorButton("Show Circle Collisions", "Show Circle Collisions", ProfileManager.Current.ColorPrimary);
            buttonSpan.AddColorButton("", "", null, false);
            buttonSpan.AddColorButton("", "", null, false);
            buttonSpan.AddColorButton("", "", null, false);
            buttonSpan.AddColorButton("Back", "Back", Color.Black);
            this.CellGroupings.Add(buttonSpan);

            ButtonSprite bulletUnselectedSprites = new ButtonSprite("menu-bullet-unselected", "menu-bullet-unselected-hover", "menu-bullet-unselected-click", 50, 50);
            ButtonSprite bulletSelectedSprites = new ButtonSprite("menu-bullet-selected", "menu-bullet-selected-hover", "menu-bullet-selected-hover", 50, 50);
            int spriteDim = 50;
            int collisionDim = 50;
            int radioYPos = buttonSpan.Cells[1].ScreenY;
            if (Graphics.Current.FullScreen == true)
            {
                this.AddStaticButton("Windowed", "Windowed", spriteDim, spriteDim, this.CenterX - (this.Width / 4), radioYPos, bulletUnselectedSprites, null, Collision.CollisionShape.Circle);
                this.AddStaticButton("Fullscreen", "Fullscreen", spriteDim, spriteDim, this.CenterX + (this.Width / 4), radioYPos, bulletSelectedSprites, null, Collision.CollisionShape.Circle);
            }
            else
            {
                this.AddStaticButton("Windowed", "Windowed", spriteDim, spriteDim, this.CenterX - (this.Width / 4), radioYPos, bulletSelectedSprites, null, Collision.CollisionShape.Circle);
                this.AddStaticButton("Fullscreen", "Fullscreen", spriteDim, spriteDim, this.CenterX + (this.Width / 4), radioYPos, bulletUnselectedSprites, null, Collision.CollisionShape.Circle);
            }
            (this.StaticCells[0] as Button).TitlePosition = Button.TextPosition.Above;
            (this.StaticCells[1] as Button).TitlePosition = Button.TextPosition.Above;
            (this.StaticCells[0] as Button).InitCollision(Collision.CollisionShape.Circle, this.CenterX - (this.Width / 4), radioYPos, collisionDim, collisionDim);
            (this.StaticCells[1] as Button).InitCollision(Collision.CollisionShape.Circle, this.CenterX + (this.Width / 4), radioYPos, collisionDim, collisionDim);
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
        public override void Draw()
        {
            base.Draw();

        }

        public override bool HandleLeftClick(int x, int y)
        {
            Button clicked = this.CheckButtonCollisions(x, y);
            if (clicked != null)
            {
                Debugger.Current.AddTempString($"You clicked on the {clicked.ButtonCode} button!");
                switch (clicked.ButtonCode)
                {
                    case "Back":
                        MenuManager.Current.CloseMenuType<PauseOptionsMenu>();
                        return true;

                    case "Windowed":
                        this.GetButton("Fullscreen").Sprites = new ButtonSprite("menu-bullet-unselected", "menu-bullet-unselected-hover", "menu-bullet-unselected-click", 50, 50);
                        clicked.Sprites = new ButtonSprite("menu-bullet-selected", "menu-bullet-selected-hover", "menu-bullet-selected-hover", 50, 50);
                        Graphics.Current.FullScreen = false;
                        return true;

                    case "Fullscreen":
                        this.GetButton("Windowed").Sprites = new ButtonSprite("menu-bullet-unselected", "menu-bullet-unselected-hover", "menu-bullet-unselected-click", 50, 50);
                        clicked.Sprites = new ButtonSprite("menu-bullet-selected", "menu-bullet-selected-hover", "menu-bullet-selected-hover", 50, 50);
                        Graphics.Current.FullScreen = true;
                        return true;

                    case "Show Circle Collisions":
                        if (Debugger.Current.ShowCircleCollisions == true) { Debugger.Current.ShowCircleCollisions = false; }
                        else { Debugger.Current.ShowCircleCollisions = true; }
                        return true;

                    default:
                        return false;
                }
            }
            return false;
        }
    }
}
