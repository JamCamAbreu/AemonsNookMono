using AemonsNookMono.Admin;
using AemonsNookMono.Menus.World;
using AemonsNookMono.Player;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Menus.General
{
    public class MessagePopupMenu : Menu
    {
        #region Constructor
        public MessagePopupMenu(string title, string message, string confirmbuttonstring, Menu Above = null) :
            base(
                title,
                (int)((float)Graphics.Current.ScreenWidth * 0.3f),
                (int)((float)Graphics.Current.ScreenHeight * 0.3f),
                  Graphics.Current.ScreenMidX,
                  Graphics.Current.ScreenMidY,
                  ((int)((float)Graphics.Current.ScreenWidth * 0.3f) / 16),
                  (int)((float)Graphics.Current.ScreenHeight * 0.3f) / 16,
                null,
                string.Empty)
        {
            this.message = message;
            this.confirmButtonString = confirmbuttonstring;
            this.InitButtons();
            this.MenuAbove = Above;
        }
        public MessagePopupMenu(int width, int height, int centerx, int centery, string title, string message, string confirmbuttonstring, Color? color = null, string sprite = "") :
            base(
                title,
                width,
                height,
                centerx,
                centery,
                (width / 16),
                (height / 16),
                color,
                sprite)
        {
            this.message = message;
            this.confirmButtonString = confirmbuttonstring;
            this.InitButtons();
        }
        #endregion

        #region Public Properties
        public Menu MenuAbove { get; set; }
        #endregion

        #region Interface
        public override void Draw(bool isTop)
        {
            if (this.MenuAbove != null && !string.IsNullOrEmpty(this.MenuAbove.MenuName))
            {
                MenuManager.Current.RetrieveMenu(MenuAbove.MenuName).Draw(true);
            }
            
            base.Draw(isTop);
        }
        public override void InitButtons()
        {
            this.Spans.Clear();
            this.StaticCells.Clear();
            int bottomRowHeight = this.Height / 12;
            Span rows = new Span(this.CenterX, this.CenterY, this.Width, this.Height, this.PadWidth, this.PadHeight, Span.SpanType.Vertical);
            rows.InnerPadScale = 0.2f;
            rows.AddText(this.message);
            rows.AddColorButton(confirmButtonString, confirmButtonString, ProfileManager.Current.ColorPrimary);
            this.Spans.Add(rows);
        }
        public override void Refresh()
        {
            base.Refresh();
            this.InitButtons();
        }
        public override bool HandleLeftClick(int x, int y)
        {
            Button clicked = this.CheckButtonCollisions(x, y);
            if (clicked != null)
            {
                Debugger.Current.AddTempString($"You clicked on the {clicked.ButtonCode} button!");
                if (clicked.ButtonCode == this.confirmButtonString)
                {
                    MenuManager.Current.CloseTop();
                    return true;
                }

                switch (clicked.ButtonCode)
                {
                    default:
                        return false;
                }
            }
            return false;
        }
        #endregion

        #region Internal
        protected string message { get; set; }
        protected string confirmButtonString { get; set; }
        #endregion
    }
}
