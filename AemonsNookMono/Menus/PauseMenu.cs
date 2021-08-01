using AemonsNookMono.Admin;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Menus
{
    public class PauseMenu : Menu
    {
        #region Constructors
        public PauseMenu(StateManager.State originalState) 
            : base("Pause",
                  (int)((float)Graphics.Current.ScreenHeight * 0.6f), 
                  (int)((float)Graphics.Current.ScreenWidth * 0.4f),
                  Graphics.Current.ScreenMidX,
                  Graphics.Current.ScreenMidY,
                  ((int)((float)Graphics.Current.ScreenHeight * 0.6f)/16),
                  (int)((float)Graphics.Current.ScreenWidth * 0.4f)/16,
                  Color.SaddleBrown,
                  string.Empty)
        {
            this.OriginalState = originalState;

            this.AddDynamicButton("Continue", null, Color.Red);
            this.AddDynamicButton("Profile", null, Color.Blue);
            this.AddDynamicButton("Options", null, Color.Purple);
            this.AddDynamicButton("Save / Exit Level", null, Color.Green);

            Cursor.Current.CurrentHoverBox = null;
        }
        #endregion

        #region Public Properties
        public StateManager.State OriginalState { get; set; }
        #endregion

        #region Interface
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
                    case "Continue":
                        StateManager.Current.CurrentState = this.OriginalState;
                        MenuManager.Current.CloseTop();
                        return true;

                    case "Profile":
                        Menu profileMenu = new Menu("Profile", 400, 400, Graphics.Current.ScreenMidX, Graphics.Current.ScreenMidY, 16, 16, Color.Purple, string.Empty);
                        profileMenu.AddDynamicButton("Option 1", null, Color.DarkOliveGreen);
                        profileMenu.AddDynamicButton("Option 2", null, Color.DarkOliveGreen);
                        profileMenu.AddDynamicButton("Option 3", null, Color.DarkOliveGreen);
                        profileMenu.AddDynamicButton("Option 4", null, Color.DarkOliveGreen);
                        profileMenu.AddDynamicButton("Option 5", null, Color.DarkOliveGreen);
                        profileMenu.AddDynamicButton("Back", null, Color.DarkOliveGreen);
                        MenuManager.Current.AddMenu(profileMenu);
                        return true;

                    case "Options":
                        MenuManager.Current.AddMenu(new PauseOptionsMenu());
                        return true;

                    case "Save / Exit Level":
                        StateManager.Current.CurrentState = StateManager.State.Exit;
                        return true;

                    default:
                        break;
                }
            }
            return base.HandleLeftClick(x, y);
        }
        #endregion
    }
}
