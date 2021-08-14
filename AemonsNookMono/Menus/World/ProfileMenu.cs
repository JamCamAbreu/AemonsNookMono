using AemonsNookMono.Admin;
using AemonsNookMono.Player;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Menus.World
{
    public class ProfileMenu : Menu
    {
        public ProfileMenu(StateManager.State originalState) : 
            base("Profile",
                  (int)((float)Graphics.Current.ScreenWidth * 0.75f),
                  (int)((float)Graphics.Current.ScreenHeight * 0.4f),
                  Graphics.Current.ScreenMidX,
                  Graphics.Current.ScreenMidY,
                  ((int)((float)Graphics.Current.ScreenWidth * 0.75f) / 16),
                  (int)((float)Graphics.Current.ScreenHeight * 0.4f) / 16,
                  Color.SaddleBrown,
                  string.Empty)
        {
            this.InitButtons();
            this.OriginalState = originalState;
        }

        #region Public Properties
        public StateManager.State OriginalState { get; set; }
        #endregion


        #region Interface
        public override void InitButtons()
        {
            this.ButtonSpans.Clear();
            Span buttons = new Span(this.CenterX, (int)(this.CenterY - this.Height * 0.25), this.Width, this.Height / 3, this.PadWidth, this.PadHeight, Span.SpanType.Horizontal);

            Profile loaded = ProfileManager.Current.Loaded;

            List<Profile> allprofiles = SaveManager.Current.RetrieveAllSavedProfiles();
            int i = 1;
            foreach (Profile profile in allprofiles)
            {
                buttons.AddButtonColor($"Slot {i}", $"{profile.Name}", Color.RoyalBlue);
                i++;
            }
            buttons.AddButtonColor("Create New", "Create New", Color.DarkOliveGreen);


            this.ButtonSpans.Add(buttons);

            Button BackButton = new Button(
                "Back", "Back",
                this.CenterX,
                (int)(this.CenterY + this.Height * 0.25),
                this.Width - (this.PadWidth * 2),
                (int)(this.Height * 0.25m),
                null,
                Color.Black,
                Collision.CollisionShape.Rectangle,
                true);
            this.StaticButtons.Add(BackButton);
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
