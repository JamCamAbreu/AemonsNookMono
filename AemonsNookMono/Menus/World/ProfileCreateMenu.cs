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
            this.CellGroupings.Clear();
            this.StaticCells.Clear();
            int bottomRowHeight = this.Height / 6;
            Span columns = new Span(this.CenterX, this.CenterY - (bottomRowHeight / 2), this.Width, this.Height - bottomRowHeight, this.PadWidth, this.PadHeight, Span.SpanType.Horizontal);

            profileOptions = new ButtonSelection();

            Span leftColumn = new Span(Span.SpanType.Vertical);
            leftColumn.AddText("Please click here and enter a profile name:");
            leftColumn.AddTextInput("NameInput", "Aemon", Color.Black);

            leftColumn.AddBlank();
            leftColumn.AddText("Please select your character theme:");
            profileOptions.Add(leftColumn.AddColorButton("Aemon", "Aemon", ProfileManager.Current.GetPrimaryColor(Player.Profile.ProfileTheme.Aemon)));
            profileOptions.Add(leftColumn.AddColorButton("Aletha", "Aletha", ProfileManager.Current.GetPrimaryColor(Player.Profile.ProfileTheme.Aletha)));
            profileOptions.Add(leftColumn.AddColorButton("Jose", "Jose", ProfileManager.Current.GetPrimaryColor(Player.Profile.ProfileTheme.Jose)));
            leftColumn.GetButton("Jose").TitleColor = Color.Black;
            leftColumn.AddBlank();
            columns.AddSpan(leftColumn);

            Span rightColumn = new Span(Span.SpanType.Vertical);
            rightColumn.AddBlank();
            rightColumn.AddBlank();
            rightColumn.AddBlank();
            rightColumn.AddBlank();
            profileOptions.Add(rightColumn.AddColorButton("Helga", "Helga", ProfileManager.Current.GetPrimaryColor(Player.Profile.ProfileTheme.Helga)));
            profileOptions.Add(rightColumn.AddColorButton("Bruno", "Bruno", ProfileManager.Current.GetPrimaryColor(Player.Profile.ProfileTheme.Bruno)));
            profileOptions.Add(rightColumn.AddColorButton("Jade", "Jade", ProfileManager.Current.GetPrimaryColor(Player.Profile.ProfileTheme.Jade)));
            rightColumn.AddBlank();
            columns.AddSpan(rightColumn);

            Span bottomButtons = new Span(this.CenterX, this.TopY + (int)(this.Height - (bottomRowHeight)), this.Width, bottomRowHeight, this.PadWidth, 8, Span.SpanType.Vertical);
            bottomButtons.AddColorButton("Create", "Create", ProfileManager.Current.ColorPrimary);
            bottomButtons.AddColorButton("Back", "Back", Color.Black);
            this.CellGroupings.Add(bottomButtons);

            this.CellGroupings.Add(columns);
            foreach (Span span in this.CellGroupings)
            {
                span.Refresh();
            }
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
            this.GetButton("NameInput").Selected = false;
            Button clicked = this.CheckButtonCollisions(x, y);
            if (clicked != null)
            {
                Debugger.Current.AddTempString($"You clicked on the {clicked.ButtonCode} button!");
                this.profileOptions.CheckSelect(clicked.ButtonCode);
                switch (clicked.ButtonCode)
                {
                    case "NameInput":
                        clicked.Selected = true;
                        return true;

                    case "Back":
                        MenuManager.Current.CloseMenuType<ProfileCreateMenu>();
                        return true;

                    case "Create":
                        if (this.profileOptions.SelectedButton != null && !string.IsNullOrEmpty(this.profileOptions.SelectedButton.ButtonCode))
                        {
                            if (!string.IsNullOrEmpty(this.GetButton("NameInput").Title))
                            {
                                if (SaveManager.Current.CheckProfileExists(this.GetButton("NameInput").Title) == false)
                                {
                                    Profile profile = this.createProfileFromSelection(this.profileOptions.SelectedButton.ButtonCode, this.GetButton("NameInput").Title);
                                    if (profile != null)
                                    {
                                        SaveManager.Current.SaveProfile(profile);
                                        ProfileManager.Current.Loaded = profile;
                                        MenuManager.Current.CloseMenuType<ProfileCreateMenu>();
                                        MenuManager.Current.Top.Refresh();
                                        MenuManager.Current.AddMenu(new MessagePopupMenu("Profile Created", "Your new profile was created successfully.", "Okay"), false, false);
                                    }
                                }
                                else
                                {
                                    MenuManager.Current.AddMenu(new MessagePopupMenu("", "A profile with this name already exists, please choose a different name.", "Okay"), false, false);
                                }
                            }
                            else
                            {
                                MenuManager.Current.AddMenu(new MessagePopupMenu("", "Please enter a name for your profile", "Okay"), false, false);
                            }
                        }
                        else
                        {
                            MenuManager.Current.AddMenu(new MessagePopupMenu("", "Please select one of the profile options.", "Okay"), false, false);
                        }
                        return true;

                    default:
                        return false;
                }
            }
            return false;
        }
        #endregion

        #region Internal
        ButtonSelection profileOptions { get; set; }
        protected Profile createProfileFromSelection(string buttonCode, string profilename)
        {
            
            Profile prof = null;
            switch (buttonCode)
            {
                case "Aemon":
                    {
                        prof = new Profile(Profile.ProfileTheme.Aemon, profilename);
                        break;
                    }

                case "Aletha":
                    {
                        prof = new Profile(Profile.ProfileTheme.Aletha, profilename);
                        break;
                    }

                case "Jose":
                    {
                        prof = new Profile(Profile.ProfileTheme.Jose, profilename);
                        break;
                    }

                case "Helga":
                    {
                        prof = new Profile(Profile.ProfileTheme.Helga, profilename);
                        break;
                    }

                case "Bruno":
                    {
                        prof = new Profile(Profile.ProfileTheme.Bruno, profilename);
                        break;
                    }

                case "Jade":
                    {
                        prof = new Profile(Profile.ProfileTheme.Jade, profilename);
                        break;
                    }
            }
            return prof;
        }
        #endregion
    }
}
