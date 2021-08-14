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
                  Color.DimGray, // foreground color
                  null) // sprite
        {
            this.InitButtons();
        }

        public override void InitButtons()
        {
            this.StaticButtons.Clear();
            this.ButtonSpans.Clear();

            ButtonSprite gear = new ButtonSprite("menu-world-gear", "menu-world-gear-hover", "menu-world-gear-hover", 32, 32);
            ButtonSprite circle = new ButtonSprite("menu-world-circle", "menu-world-circle-hover", "menu-world-circle-hover", 32, 32);
            ButtonSprite square = new ButtonSprite("menu-world-square", "menu-world-square-hover", "menu-world-square-hover", 32, 32);
            ButtonSprite diamond = new ButtonSprite("menu-world-diamond", "menu-world-diamond-hover", "menu-world-diamond-hover", 32, 32);
            ButtonSprite pentagon = new ButtonSprite("menu-world-pentagon", "menu-world-pentagon-hover", "menu-world-pentagon-hover", 32, 32);

            Span menuItems = new Span(this.CenterX, this.CenterY - 12, this.Width, this.Height, this.PadWidth, this.PadHeight, Span.SpanType.Horizontal);
            menuItems.AddButtonSprite("Pause", "Pause", gear, Collision.CollisionShape.Circle);
            menuItems.AddButtonSprite("Profile", "Profile", circle, Collision.CollisionShape.Circle);
            menuItems.AddButtonSprite("Levels", "Levels", square, Collision.CollisionShape.Rectangle);
            menuItems.AddButtonSprite("Option 4", "Option 4", diamond, Collision.CollisionShape.Circle);
            menuItems.AddButtonSprite("Option 5", "Option 5", pentagon, Collision.CollisionShape.Circle);
            this.ButtonSpans.Add(menuItems);

            foreach (Button b in menuItems.Cells)
            {
                b.TitlePosition = Button.TextPosition.Below;
            }
        }

        public override void Draw(bool isTop)
        {
            base.Draw(true);
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
                        MenuManager.Current.AddMenu(new PauseMenu(state));
                        return true;

                    case "Profile":
                        StateManager.Current.CurrentState = StateManager.State.Pause;
                        MenuManager.Current.AddMenu(new ProfileMenu(state));
                        return true;

                    case "Levels":
                        StateManager.Current.CurrentState = StateManager.State.Pause;
                        MenuManager.Current.AddMenu(new LevelSelectMenu(state));
                        return true;

                    default:
                        return true;
                }
            }
            return base.HandleLeftClick(x, y);
        }
    }
}
