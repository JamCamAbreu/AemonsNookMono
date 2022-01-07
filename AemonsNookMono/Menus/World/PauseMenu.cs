using AemonsNookMono.Admin;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Menus.World
{
    public class PauseMenu : Menu
    {
        #region Constructors
        public PauseMenu(StateManager.State originalState) 
            : base("Pause",
                  (int)((float)Graphics.Current.ScreenWidth * 0.4f), 
                  (int)((float)Graphics.Current.ScreenHeight * 0.6f),
                  Graphics.Current.ScreenMidX,
                  Graphics.Current.ScreenMidY,
                  ((int)((float)Graphics.Current.ScreenWidth * 0.4f)/16),
                  (int)((float)Graphics.Current.ScreenHeight * 0.6f)/16,
                  null,
                  string.Empty)
        {
            this.OriginalState = originalState;
            this.InitButtons();
            Cursor.Current.CurrentHoverBox = null;
        }
        #endregion

        #region Public Properties
        public StateManager.State OriginalState { get; set; }
        #endregion

        #region Interface
        public override void InitButtons()
        {
            this.CellGroupings.Clear();
            Span cells = new Span(this.CenterX, this.CenterY, this.Width, this.Height, this.PadWidth, this.PadHeight, Span.SpanType.Vertical);
            cells.AddColorButton("Options", "Options", ProfileManager.Current.ColorPrimary);
            cells.AddText("Here is some test text.");
            cells.AddText("Aemon's nook is a really neat game! This game has things in it that you have never seen before! Step right up folks!");
            cells.AddColorButton("Save / Exit Level", "Save / Exit Level", Color.Black);
            cells.AddColorButton("Back", "Back", Color.Black);
            this.CellGroupings.Add(cells);
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
                Debugger.Current.AddTempString($"You clicked on the {clicked.ButtonCode} button!");
                switch (clicked.ButtonCode)
                {
                    case "Options":
                        MenuManager.Current.AddMenu(new PauseOptionsMenu());
                        return true;

                    case "Save / Exit Level":
                        SaveManager.Current.SaveProfile(ProfileManager.Current.Loaded);
                        StateManager.Current.CurrentState = StateManager.State.Exit;
                        return true;

                    case "Back":
                        SaveManager.Current.SaveProfile(ProfileManager.Current.Loaded);
                        StateManager.Current.CurrentState = this.OriginalState;
                        MenuManager.Current.CloseTop();
                        return true;

                    default:
                        return false;
                }
            }
            return false;
        }
        #endregion
    }
}
