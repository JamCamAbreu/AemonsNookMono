using AemonsNookMono.Admin;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Menus
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

            this.AddDynamicButton("Option 1", null, Color.Blue);
            this.AddDynamicButton("");
            this.AddDynamicButton("Option 3", null, Color.Blue);
            this.AddDynamicButton("Option 4", null, Color.Blue);
            this.AddDynamicButton("Back", null, Color.Magenta);

            ButtonSprite bulletUnselectedSprites = new ButtonSprite("menu-bullet-unselected", "menu-bullet-unselected-hover", "menu-bullet-unselected-click");
            ButtonSprite bulletSelectedSprites = new ButtonSprite("menu-bullet-selected", "menu-bullet-selected-hover", "menu-bullet-selected-hover");
            if (Graphics.Current.FullScreen == true)
            {
                this.AddStaticButton("Windowed", 50, 50, this.CenterX - (this.Width / 4) - 25, this.DynamicButtons[1].ScreenY, bulletUnselectedSprites, null, Collision.CollisionShape.Circle);
                this.AddStaticButton("Fullscreen", 50, 50, this.CenterX + (this.Width / 4) - 25, this.DynamicButtons[1].ScreenY, bulletSelectedSprites, null, Collision.CollisionShape.Circle);
                this.StaticButtons[0].MyTextPosition = Button.TextPosition.Above;
                this.StaticButtons[1].MyTextPosition = Button.TextPosition.Above;
            }
            else
            {
                this.AddStaticButton("Windowed", 50, 50, this.CenterX - this.Width / 4, this.DynamicButtons[1].ScreenY, bulletSelectedSprites, null, Collision.CollisionShape.Circle);
                this.AddStaticButton("Fullscreen", 50, 50, this.CenterX + (this.Width / 4), this.DynamicButtons[1].ScreenY, bulletUnselectedSprites, null, Collision.CollisionShape.Circle);
                this.StaticButtons[0].MyTextPosition = Button.TextPosition.Above;
                this.StaticButtons[1].MyTextPosition = Button.TextPosition.Above;
            }

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
                    case "Back":
                        MenuManager.Current.CloseTop();
                        return true;

                    case "Option 1":
                        return true;

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

                    case "Option 4":
                        return true;

                    default:
                        break;
                }
            }
            return base.HandleLeftClick(x, y);
        }
    }
}
