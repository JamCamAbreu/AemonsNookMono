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
            this.ButtonSpans.Clear();

            ButtonSpan levelButtons = new ButtonSpan(this.CenterX, this.CenterY, this.Width, this.Height, this.PadWidth, this.PadHeight, ButtonSpan.SpanType.Vertical);
            levelButtons.AddButton("Grass", Color.DarkOliveGreen);
            levelButtons.AddButton("Dirt", Color.SaddleBrown);
            levelButtons.AddButton("Tree", Color.Green);
            levelButtons.AddButton("Rock", Color.LightSlateGray);
            levelButtons.AddButton("Water", Color.Blue);
            this.ButtonSpans.Add(levelButtons);
        }
        public override bool HandleLeftClick(int x, int y)
        {
            Button clicked = this.CheckButtonCollisions(x, y);
            if (clicked != null)
            {
                Debugger.Current.AddTempString($"You clicked on the {clicked.Name} button!");
                switch (clicked.Name)
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
