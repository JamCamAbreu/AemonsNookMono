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
                  (int)((float)Graphics.Current.ScreenHeight * 0.7f),
                  Graphics.Current.ScreenMidX,
                  Graphics.Current.ScreenMidY,
                  ((int)((float)Graphics.Current.ScreenWidth * 0.75f) / 16),
                  (int)((float)Graphics.Current.ScreenHeight * 0.7f) / 16,
                  null,
                  string.Empty)
        {
            SaveManager.Current.SaveProfile(ProfileManager.Current.Loaded);
            this.InitButtons();
            this.OriginalState = originalState;
        }

        #region Public Properties
        public StateManager.State OriginalState { get; set; }
        #endregion


        #region Interface
        public override void InitButtons()
        {
            this.Spans.Clear();
            this.StaticCells.Clear();

            int bottomRowHeight = this.Height / 12;
            Span columns = new Span(this.CenterX, this.CenterY - (bottomRowHeight / 2), this.Width, this.Height - bottomRowHeight, this.PadWidth, this.PadHeight, Span.SpanType.Horizontal);
            columns.InnerPadScale = 0.1f;

            Span leftside = new Span(Span.SpanType.Vertical);
            //leftside.AddColorButton("Left", "Left", Color.Red, true);
            leftside.AddText($"{ProfileManager.Current.Loaded.Name}");
            leftside.AddColorButton("", "", null, false);
            leftside.AddColorButton("", "", null, false);
            leftside.AddSprite("profile-portrait-temp", 256, 256);
            leftside.AddColorButton("", "", null, false);
            leftside.AddColorButton("", "", null, false);
            leftside.AddColorButton("", "", null, false);
            leftside.AddColorButton("", "", null, false);
            leftside.AddColorButton("", "", null, false);
            leftside.AddColorButton("Load", "Load", ProfileManager.Current.ColorPrimary, true);
            columns.AddSpan(leftside);

            Span middle = new Span(Span.SpanType.Vertical);
            //middle.AddColorButton("Middle", "Middle", Color.Red, true);
            middle.AddColorButton("", "", null, false);
            middle.AddText($"Total Time Played: {ProfileManager.Current.GetTotalPlaytimeString()}", "couriernew", Color.Bisque, Textbox.HorizontalAlign.Left, Textbox.VerticalAlign.Center);
            middle.AddText($"Stone Collected: {ProfileManager.Current.Loaded.TotalStoneCollected} stone", "couriernew", Color.Bisque, Textbox.HorizontalAlign.Left, Textbox.VerticalAlign.Center);
            middle.AddText($"Wood Collected: {ProfileManager.Current.Loaded.TotalWoodCollected} wood", "couriernew", Color.Bisque, Textbox.HorizontalAlign.Left, Textbox.VerticalAlign.Center);
            middle.AddColorButton("", "", null, false);
            middle.AddColorButton("", "", null, false);
            middle.AddColorButton("", "", null, false);
            middle.AddColorButton("", "", null, false);
            middle.AddColorButton("", "", null, false);
            middle.AddColorButton("Create", "Create", ProfileManager.Current.ColorPrimary, true);
            columns.AddSpan(middle);

            //Span rightside = new Span(Span.SpanType.Vertical);
            ////rightside.AddColorButton("Right", "Right", Color.Red, true);
            //rightside.AddColorButton("", "", null, false);
            //rightside.AddText("Test:", "couriernew", Color.Bisque, Textbox.HorizontalAlign.Left, Textbox.VerticalAlign.Center);
            //rightside.AddText("Test 2:", "couriernew", Color.Bisque, Textbox.HorizontalAlign.Left, Textbox.VerticalAlign.Center);
            //rightside.AddText("Test 3:", "couriernew", Color.Bisque, Textbox.HorizontalAlign.Left, Textbox.VerticalAlign.Center);
            //rightside.AddColorButton("", "", null, false);
            //rightside.AddColorButton("", "", null, false);
            //rightside.AddColorButton("", "", null, false);
            //rightside.AddColorButton("", "", null, false);
            //rightside.AddColorButton("", "", null, false);
            //columns.AddSpan(rightside);

            this.Spans.Add(columns);
            foreach (Span span in this.Spans)
            {
                span.Refresh();
            }

            Button BackButton = new Button(
                "Back", "Back",
                this.CenterX,                                       // x
                this.TopY + (int)(this.Height - (bottomRowHeight)),      // y
                this.Width - (this.PadWidth * 2),                   // width
                bottomRowHeight,                          // height
                null,                                               // sprite
                Color.Black,                                        // color
                Collision.CollisionShape.Rectangle,
                true);
            this.StaticCells.Add(BackButton);
        }
        public override void Refresh()
        {
            this.Width = (int)((float)Graphics.Current.ScreenWidth * 0.75f);
            this.Height = (int)((float)Graphics.Current.ScreenHeight * 0.7f);
            this.CenterX = Graphics.Current.ScreenMidX;
            this.CenterY = Graphics.Current.ScreenMidY;
            this.PadWidth = (int)((float)Graphics.Current.ScreenWidth * 0.75f) / 16;
            this.PadHeight = ((int)((float)Graphics.Current.ScreenHeight * 0.7f) / 16);
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
                        SaveManager.Current.SaveProfile(ProfileManager.Current.Loaded);
                        StateManager.Current.CurrentState = this.OriginalState;
                        MenuManager.Current.CloseTop();
                        return true;

                    case "Create":
                        ProfileCreateMenu createmenu = new ProfileCreateMenu();
                        MenuManager.Current.AddMenu(createmenu);
                        return true;

                    case "Load":
                        ProfileLoadMenu loadmenu = new ProfileLoadMenu();
                        MenuManager.Current.AddMenu(loadmenu);
                        return true;

                    case "Save":
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
