using AemonsNookMono.Admin;
using AemonsNookMono.Player;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AemonsNookMono.Menus.World
{
    public class ProfileLoadMenu : Menu
    {
        #region Constructor
        public ProfileLoadMenu() :
            base("Load Profile",
                  (int)((float)Graphics.Current.ScreenWidth * 0.7f),
                  (int)((float)Graphics.Current.ScreenHeight * 0.7f),
                  Graphics.Current.ScreenMidX,
                  Graphics.Current.ScreenMidY,
                  ((int)((float)Graphics.Current.ScreenWidth * 0.7f) / 16),
                  (int)((float)Graphics.Current.ScreenHeight * 0.7f) / 16,
                  null,
                  string.Empty)
        {
            this.InitButtons();
        }
        #endregion

        #region Public Properties
        Dictionary<string, Profile> ProfileButtonCodes { get; set; }
        #endregion

        #region Interface
        public override void InitButtons()
        {
            this.CellGroupings.Clear();
            int bottomRowHeight = this.Height / 10;
            Span columns = new Span(this.CenterX, this.CenterY - (bottomRowHeight / 2), this.Width, this.Height - bottomRowHeight, this.PadWidth, this.PadHeight, Span.SpanType.Horizontal);
            columns.InnerPadScale = 0.2f;

            Span leftColumn = new Span(Span.SpanType.Vertical);
            leftColumn.InnerPadScale = 0;
            leftColumn.AddText("Profile");

            Span rightColumn = new Span(Span.SpanType.Vertical);
            rightColumn.InnerPadScale = 0;
            rightColumn.AddText("Total Time Played:");

            this.ProfileButtonCodes = new Dictionary<string, Profile>();
            List<Profile> allprofiles = SaveManager.Current.RetrieveAllSavedProfiles();
            int i = 1;
            foreach (Profile profile in allprofiles.OrderByDescending(profile => profile.TotalTimePlayedSeconds))
            {
                leftColumn.AddColorButton($"Slot {i}", $"{profile.Name}", ProfileManager.Current.GetPrimaryColor(profile.Theme));
                rightColumn.AddText(ProfileManager.Current.GetTotalPlaytimeString(profile), "couriernew", null, Textbox.HorizontalAlign.Left);
                this.ProfileButtonCodes.Add($"Slot {i}", profile);
                i++;
            }

            while (i < 8)
            {
                leftColumn.AddBlank();
                rightColumn.AddBlank();
                i++;
            }

            columns.AddSpan(leftColumn);
            columns.AddSpan(rightColumn);

            this.CellGroupings.Add(columns);
            foreach (Span span in this.CellGroupings)
            {
                span.Refresh();
            }

            Button BackButton = new Button(
                "Back", "Back",
                this.CenterX,                                       // x
                this.TopY + (int)(this.Height - (bottomRowHeight)), // y
                this.Width - (this.PadWidth * 2),                   // width
                bottomRowHeight,                                    // height
                null,                                               // sprite
                Color.Black,                                        // color
                Collision.CollisionShape.Rectangle,
                true);
            this.StaticCells.Add(BackButton);
        }
        public override void Refresh()
        {
            this.Height = (int)((float)Graphics.Current.ScreenHeight * 0.7f); // Todo: match constructor
            this.Width = (int)((float)Graphics.Current.ScreenWidth * 0.7f);
            this.CenterX = Graphics.Current.ScreenMidX;
            this.CenterY = Graphics.Current.ScreenMidY;
            this.PadHeight = ((int)((float)Graphics.Current.ScreenHeight * 0.7f) / 16);
            this.PadWidth = (int)((float)Graphics.Current.ScreenWidth * 0.7f) / 16;
            base.Refresh();
            this.InitButtons();
        }
        public override bool HandleLeftClick(int x, int y)
        {
            Button clicked = this.CheckButtonCollisions(x, y);
            if (clicked != null)
            {
                Debugger.Current.AddTempString($"You clicked on the {clicked.ButtonCode} button!");

                if (this.ProfileButtonCodes.ContainsKey(clicked.ButtonCode))
                {
                    SaveManager.Current.SaveProfile(ProfileManager.Current.Loaded);
                    Profile profile = this.ProfileButtonCodes[clicked.ButtonCode];
                    ProfileManager.Current.Loaded = profile;
                    MenuManager.Current.CloseMenuType<ProfileLoadMenu>();
                    MenuManager.Current.Top.Refresh();
                    return true;
                }

                switch (clicked.ButtonCode)
                {
                    case "Back":
                        MenuManager.Current.CloseMenuType<ProfileLoadMenu>();
                        MenuManager.Current.Refresh();
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
