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
                  (int)((float)Graphics.Current.ScreenHeight * 0.1f), // height
                  (int)((float)Graphics.Current.ScreenWidth * 0.25f), // x
                  (int)((float)Graphics.Current.ScreenHeight * 0.05f), // y
                  32, // pad height 
                  32, // pad width
                  null, // foreground color
                  null) // sprite
        {
            this.InitButtons();
        }

        public override void InitButtons()
        {
            this.StaticCells.Clear();
            this.CellGroupings.Clear();

            ButtonSprite gear = new ButtonSprite("menu-world-gear", "menu-world-gear-hover", "menu-world-gear-hover", 32, 32);
            ButtonSprite circle = new ButtonSprite("menu-world-circle", "menu-world-circle-hover", "menu-world-circle-hover", 32, 32);
            ButtonSprite square = new ButtonSprite("menu-world-square", "menu-world-square-hover", "menu-world-square-hover", 32, 32);
            ButtonSprite diamond = new ButtonSprite("menu-world-diamond", "menu-world-diamond-hover", "menu-world-diamond-hover", 32, 32);
            ButtonSprite pentagon = new ButtonSprite("menu-world-pentagon", "menu-world-pentagon-hover", "menu-world-pentagon-hover", 32, 32);

            Span menuItems = new Span(this.CenterX, this.CenterY - 12, this.Width, this.Height, this.PadWidth, this.PadHeight, Span.SpanType.Horizontal);
            menuItems.AddSpriteButton("Pause", "Pause", gear, Collision.CollisionShape.Circle);
            menuItems.AddSpriteButton("Profile", "Profile", circle, Collision.CollisionShape.Circle);
            menuItems.AddSpriteButton("Levels", "Levels", square, Collision.CollisionShape.Rectangle);
            menuItems.AddSpriteButton("Test", "Test", diamond, Collision.CollisionShape.Circle);
            //menuItems.AddButtonSprite("Option 5", "Option 5", pentagon, Collision.CollisionShape.Circle);
            this.CellGroupings.Add(menuItems);

            foreach (Button b in menuItems.Cells)
            {
                b.TitlePosition = Button.TextPosition.Below;
            }
        }

        public override void Draw()
        {
            base.Draw();
        }
        public override void Refresh()
        {
            this.Width = (int)((float)Graphics.Current.ScreenWidth * 0.5f);
            this.Height = (int)((float)Graphics.Current.ScreenHeight * 0.1f);
            this.CenterX = (int)((float)Graphics.Current.ScreenWidth * 0.25f);
            this.CenterY = (int)((float)Graphics.Current.ScreenHeight * 0.05f);
            base.Refresh();
            this.InitButtons();
        }
        public override bool HandleLeftClick(int x, int y)
        {
            Button clicked = this.CheckButtonCollisions(x, y);
            if (clicked != null)
            {
                Debugger.Current.AddTempString($"You clicked on the {clicked.ButtonCode} button!");
                StateManager.State state = StateManager.Current.CurrentState;
                switch (clicked.ButtonCode)
                {
                    case "Pause":
                        StateManager.Current.CurrentState = StateManager.State.Pause;
                        MenuManager.Current.AddMenu(new PauseMenu(state), false, false);
                        return true;

                    case "Profile":
                        StateManager.Current.CurrentState = StateManager.State.Pause;
                        MenuManager.Current.AddMenu(new ProfileMenu(state), false, false);
                        return true;

                    case "Levels":
                        StateManager.Current.CurrentState = StateManager.State.Pause;
                        MenuManager.Current.AddMenu(new LevelSelectMenu(state), false, false);
                        return true;

                    case "Test":
                        StateManager.Current.CurrentState = StateManager.State.Pause;
                        MenuManager.Current.AddMenu(new TestMenu(state), false, false);
                        return true;

                    default:
                        return false;
                }
            }
            return false;
        }
    }
}
