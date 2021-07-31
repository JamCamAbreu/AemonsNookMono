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
        public PauseMenu(StateManager.State originalState) : base()
        {
            this.OriginalState = originalState;
            this.Height = (int)((float)Graphics.Current.ScreenHeight * 0.6f);
            this.Width = Graphics.Current.ScreenWidth / 3;
            this.backPanel = new Panel(this.Width, this.Height, Color.Black, Color.SaddleBrown, 0.9f);
            this.title = "Pause Menu";

            this.InnerPad = this.Height / 16;
            this.TopY = Graphics.Current.ScreenMidY - this.Height / 2;

            this.AddButton("Continue", null, Color.Red);
            this.AddButton("Profile", null, Color.Blue);
            this.AddButton("Options", null, Color.Purple);
            this.AddButton("Save / Exit Level", null, Color.Green);
        }
        #endregion

        #region Public Properties
        public StateManager.State OriginalState { get; set; }
        #endregion

        #region Interface

        public override void Draw(bool isTop)
        {
            Graphics.Current.SpriteB.Begin();
            int titlex = Graphics.Current.CenterStringX(Graphics.Current.ScreenMidX, this.title, "couriernew");
            int titley = this.TopY - 32;
            Graphics.Current.SpriteB.DrawString(Graphics.Current.Fonts["couriernew"], this.title, new Vector2(titlex, titley), Color.White);
            Graphics.Current.SpriteB.End();

            this.backPanel.Draw(Graphics.Current.ScreenMidX, Graphics.Current.ScreenMidY);

            foreach (Button b in this.Buttons)
            {
                b.Draw();
            }
        }
        #endregion

        #region Internal
        protected Panel backPanel { get; set; }
        protected string title { get; set; }
        #endregion
    }
}
