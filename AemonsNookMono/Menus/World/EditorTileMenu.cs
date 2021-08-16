using AemonsNookMono.Admin;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Menus.World
{
    public class EditorTileMenu : Menu
    {
        public EditorTileMenu() :
            base("Tiles",
                128, // width 
                (int)((float)Graphics.Current.ScreenHeight * 0.65f), // height
                (int)((float)Graphics.Current.ScreenWidth - 128), // x
                (int)((float)Graphics.Current.ScreenHeight * 0.6f), // y
                16, // padwidth
                16, // padheight
                Color.DimGray,
                string.Empty)
        {
            this.InitButtons();
            this.worldMenu = MenuManager.Current.RetrieveMenu("WorldMenu") as WorldMenu;
        }

        #region Public Properties
        
        #endregion

        #region Interface
        public override void InitButtons()
        {
            this.Spans.Clear();

            Span levelButtons = new Span(this.CenterX, this.CenterY, this.Width, this.Height, this.PadWidth, this.PadHeight, Span.SpanType.Vertical);
            levelButtons.AddColorButton("Grass", "Grass", Color.DarkOliveGreen);
            levelButtons.AddColorButton("Dirt", "Dirt", Color.SaddleBrown);
            levelButtons.AddColorButton("Tree", "Tree", Color.Green);
            levelButtons.AddColorButton("Rock",  "Rock", Color.LightSlateGray);
            levelButtons.AddColorButton("Water",  "Water", Color.Blue);
            this.Spans.Add(levelButtons);
        }
        public override bool HandleLeftClick(int x, int y)
        {
            Button clicked = this.CheckButtonCollisions(x, y);
            if (clicked != null)
            {
                Debugger.Current.AddTempString($"You clicked on the {clicked.ButtonCode} button!");
                switch (clicked.ButtonCode)
                {
                    case "Grass":
                        return true;

                    case "Dirt":
                        return true;

                    case "Tree":
                        return true;

                    case "Rock":
                        return true;

                    case "Water":
                        return true;

                    default:
                        return base.HandleLeftClick(x, y);
                }
            }

            if (this.worldMenu != null)
            {
                return this.worldMenu.HandleLeftClick(x, y);
            }

            return base.HandleLeftClick(x, y);
        }
        #endregion

        #region Internal
        protected WorldMenu worldMenu { get; set; }
        #endregion
    }
}
