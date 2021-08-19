using AemonsNookMono.Admin;
using AemonsNookMono.Menus.General;
using AemonsNookMono.Player;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Menus.World
{
    public class ProfileCreateMenu : Menu
    {
        #region Constructor
        public ProfileCreateMenu() :
            base("Create Profile",
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

        #region Interface
        public override void InitButtons()
        {
            this.Spans.Clear();
            this.StaticCells.Clear();
            int bottomRowHeight = this.Height / 12;
            Span columns = new Span(this.CenterX, this.CenterY - (bottomRowHeight / 2), this.Width, this.Height - bottomRowHeight, this.PadWidth, this.PadHeight, Span.SpanType.Horizontal);
            
            Span leftColumn = new Span(Span.SpanType.Vertical);
            leftColumn.AddBlank();
            leftColumn.AddColorButton("Aemon", "Aemon", ProfileManager.Current.GetPrimaryColor(Player.Profile.ProfileTheme.Aemon));
            leftColumn.AddColorButton("Aletha", "Aletha", ProfileManager.Current.GetPrimaryColor(Player.Profile.ProfileTheme.Aletha));
            leftColumn.AddColorButton("Jose", "Jose", ProfileManager.Current.GetPrimaryColor(Player.Profile.ProfileTheme.Jose));
            leftColumn.GetButton("Jose").TitleColor = Color.Black;
            leftColumn.AddBlank();
            columns.AddSpan(leftColumn);

            Span rightColumn = new Span(Span.SpanType.Vertical);
            rightColumn.AddBlank();
            rightColumn.AddColorButton("Helga", "Helga", ProfileManager.Current.GetPrimaryColor(Player.Profile.ProfileTheme.Helga));
            rightColumn.AddColorButton("Bruno", "Bruno", ProfileManager.Current.GetPrimaryColor(Player.Profile.ProfileTheme.Bruno));
            rightColumn.AddColorButton("Jade", "Jade", ProfileManager.Current.GetPrimaryColor(Player.Profile.ProfileTheme.Jade));
            rightColumn.AddBlank();
            columns.AddSpan(rightColumn);

            this.Spans.Add(columns);
            foreach (Span span in this.Spans)
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
                switch (clicked.ButtonCode)
                {
                    case "Back":
                        MenuManager.Current.CloseTop();
                        return true;

                    case "Aemon":
                        {
                            Profile prof = new Profile(Profile.ProfileTheme.Aemon, "Aemon");
                            SaveManager.Current.SaveProfile(prof);
                            ProfileManager.Current.Loaded = prof;
                            MenuManager.Current.CloseTop();
                            MenuManager.Current.Top.Refresh();
                            MenuManager.Current.AddMenu(new MessagePopupMenu("Profile Created", "Your new profile was created successfully.", "Okay"));
                            return true;
                        }

                    case "Aletha":
                        {
                            Profile prof = new Profile(Profile.ProfileTheme.Aletha, "Aletha");
                            SaveManager.Current.SaveProfile(prof);
                            ProfileManager.Current.Loaded = prof;
                            MenuManager.Current.CloseTop();
                            MenuManager.Current.Top.Refresh();
                            MenuManager.Current.AddMenu(new MessagePopupMenu("Profile Created", "Your new profile was created successfully.", "Okay"));
                            return true;
                        }

                    case "Jose":
                        {
                            Profile prof = new Profile(Profile.ProfileTheme.Jose, "Jose");
                            SaveManager.Current.SaveProfile(prof);
                            ProfileManager.Current.Loaded = prof;
                            MenuManager.Current.CloseTop();
                            MenuManager.Current.Top.Refresh();
                            MenuManager.Current.AddMenu(new MessagePopupMenu("Profile Created", "Your new profile was created successfully.", "Okay"));
                            return true;
                        }

                    case "Helga":
                        {
                            Profile prof = new Profile(Profile.ProfileTheme.Helga, "Helga");
                            SaveManager.Current.SaveProfile(prof);
                            ProfileManager.Current.Loaded = prof;
                            MenuManager.Current.CloseTop();
                            MenuManager.Current.Top.Refresh();
                            MenuManager.Current.AddMenu(new MessagePopupMenu("Profile Created", "Your new profile was created successfully.", "Okay"));
                            return true;
                        }

                    case "Bruno":
                        {
                            Profile prof = new Profile(Profile.ProfileTheme.Bruno, "Bruno");
                            SaveManager.Current.SaveProfile(prof);
                            ProfileManager.Current.Loaded = prof;
                            MenuManager.Current.CloseTop();
                            MenuManager.Current.Top.Refresh();
                            MenuManager.Current.AddMenu(new MessagePopupMenu("Profile Created", "Your new profile was created successfully.", "Okay"));
                            return true;
                        }

                    case "Jade":
                        {
                            Profile prof = new Profile(Profile.ProfileTheme.Jade, "Jade");
                            SaveManager.Current.SaveProfile(prof);
                            ProfileManager.Current.Loaded = prof;
                            MenuManager.Current.CloseTop();
                            MenuManager.Current.Top.Refresh();
                            MenuManager.Current.AddMenu(new MessagePopupMenu("Profile Created", "Your new profile was created successfully.", "Okay"));
                            return true;
                        }

                    default:
                        return false;
                }
            }
            return false;
        }
        #endregion
    }
}
