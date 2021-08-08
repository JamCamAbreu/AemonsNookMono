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
                  Color.SaddleBrown,
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
            this.ButtonSpans.Clear();
            ButtonSpan buttons = new ButtonSpan(this.CenterX, this.CenterY, this.Width, this.Height, this.PadWidth, this.PadHeight, ButtonSpan.SpanType.Vertical);
            buttons.AddButton("Options", Color.DarkGreen);
            buttons.AddButton("", Color.DarkGreen);
            buttons.AddButton("", Color.DarkGreen);
            buttons.AddButton("Save / Exit Level", Color.Black);
            buttons.AddButton("Back", Color.Black);
            this.ButtonSpans.Add(buttons);
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
                    case "Options":
                        MenuManager.Current.AddMenu(new PauseOptionsMenu());
                        return true;

                    case "Save / Exit Level":
                        StateManager.Current.CurrentState = StateManager.State.Exit;
                        return true;

                    case "Back":
                        StateManager.Current.CurrentState = this.OriginalState;
                        MenuManager.Current.CloseTop();
                        return true;

                    default:
                        return base.HandleLeftClick(x, y);
                }
            }
            return base.HandleLeftClick(x, y);
        }
        #endregion
    }
}
