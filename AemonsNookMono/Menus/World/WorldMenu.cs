using AemonsNookMono.Admin;
using AemonsNookMono.Menus.World;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Menus.World
{
    public class WorldMenu : Menu
    {
        public WorldMenu()
            : base(
                  "WorldMenu", // menu name
                  (int)((float)Graphics.Current.ScreenWidth * 0.5f), // width 
                  (int)((float)Graphics.Current.ScreenHeight * 0.08f), // height
                  (int)((float)Graphics.Current.ScreenWidth * 0.25f), // x
                  (int)((float)Graphics.Current.ScreenHeight * 0.04f), // y
                  4, // pad height 
                  4, // pad width
                  Color.DimGray, // foreground color
                  null) // sprite
        {
            this.InitButtons();
            
        }

        public override void InitButtons()
        {
            this.StaticButtons.Clear();

            int numButtons = 5;
            int buttonSize = 32;
            int buttonWidth = (this.Width - (this.PadWidth * 2)) / ((numButtons));
            int centerCol = buttonWidth / 2;
            int startX = this.LeftX + this.PadWidth + centerCol - (buttonSize / 2);

            ButtonSprite gear = new ButtonSprite("menu-world-gear", "menu-world-gear-hover", "menu-world-gear-hover");
            ButtonSprite circle = new ButtonSprite("menu-world-circle", "menu-world-circle-hover", "menu-world-circle-hover");
            ButtonSprite square = new ButtonSprite("menu-world-square", "menu-world-square-hover", "menu-world-square-hover");
            ButtonSprite diamond = new ButtonSprite("menu-world-diamond", "menu-world-diamond-hover", "menu-world-diamond-hover");
            ButtonSprite pentagon = new ButtonSprite("menu-world-pentagon", "menu-world-pentagon-hover", "menu-world-pentagon-hover");

            int buttonYadjust = 13;
            this.AddStaticButton("Pause", buttonSize, buttonSize, startX + (buttonWidth * 0), this.CenterY - buttonYadjust, gear, null, Collision.CollisionShape.Circle);
            this.AddStaticButton("Profile", buttonSize, buttonSize, startX + (buttonWidth * 1), this.CenterY - buttonYadjust, circle, null, Collision.CollisionShape.Circle);
            this.AddStaticButton("Levels", buttonSize, buttonSize, startX + (buttonWidth * 2), this.CenterY - buttonYadjust, square, null, Collision.CollisionShape.Circle);
            this.AddStaticButton("Option 4", buttonSize, buttonSize, startX + (buttonWidth * 3), this.CenterY - buttonYadjust, diamond, null, Collision.CollisionShape.Circle);
            this.AddStaticButton("Option 5", buttonSize, buttonSize, startX + (buttonWidth * 4), this.CenterY - buttonYadjust, pentagon, null, Collision.CollisionShape.Circle);
            foreach (Button b in this.StaticButtons)
            {
                b.TitlePosition = Button.TextPosition.Below;
            }
        }

        public override void Draw(bool isTop)
        {
            base.Draw(true);
        }

        public override bool HandleLeftClick(int x, int y)
        {
            Button clicked = this.CheckButtonCollisions(x, y);
            if (clicked != null)
            {
                Debugger.Current.AddTempString($"You clicked on the {clicked.Name} button!");
                StateManager.State state = StateManager.Current.CurrentState;
                switch (clicked.Name)
                {
                    case "Pause":
                        
                        if (state == StateManager.State.World || state == StateManager.State.BuildSelection)
                        {
                            StateManager.Current.CurrentState = StateManager.State.Pause;
                            MenuManager.Current.AddMenu(new PauseMenu(state));
                        }
                        return true;

                    case "Profile":
                        if (state == StateManager.State.World || state == StateManager.State.BuildSelection)
                        {
                            StateManager.Current.CurrentState = StateManager.State.Pause;
                            MenuManager.Current.AddMenu(new ProfileMenu(state));
                        }
                        return true;

                    case "Levels":
                        if (state == StateManager.State.World || state == StateManager.State.BuildSelection)
                        {
                            StateManager.Current.CurrentState = StateManager.State.Pause;
                            MenuManager.Current.AddMenu(new LevelSelectMenu(state));
                        }
                        return true;

                    default:
                        return true;
                }
            }
            return base.HandleLeftClick(x, y);
        }
    }
}
