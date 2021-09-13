using AemonsNookMono.Admin;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Menus.LevelEditor
{
    public class EditorSaveLevel : Menu
    {
        public EditorSaveLevel() :
            base("Save Level",
                256, // width 
                (int) ((float) Graphics.Current.ScreenHeight* 0.65f), // height
                (int) ((float) Graphics.Current.ScreenWidth - 256), // x
                (int) ((float) Graphics.Current.ScreenHeight* 0.6f), // y
                16, // padwidth
                16, // padheight
                null,
                string.Empty)
        {
            this.InitButtons();
            //this.worldMenu = MenuManager.Current.RetrieveMenu("WorldMenu") as WorldMenu;
        }

        public override void InitButtons()
        {
            this.Spans.Clear();

            Span rows = new Span(this.CenterX, this.CenterY, this.Width, this.Height, this.PadWidth, this.PadHeight, Span.SpanType.Vertical);
            rows.AddText("Please enter a name for your level:");
            rows.AddTextInput("Level Name", "New Level", Color.White);
            rows.AddBlank();
            rows.AddBlank();

            Span bottomrow = new Span(Span.SpanType.Horizontal);
            bottomrow.AddColorButton("Save", "Save", Color.Black);
            bottomrow.AddColorButton("Cancel", "Cancel", Color.Black);
            rows.AddSpan(bottomrow);

            this.Spans.Add(rows);
        }

        public override bool HandleLeftClick(int x, int y)
        {
            Button clicked = this.CheckButtonCollisions(x, y);
            if (clicked != null)
            {
                Debugger.Current.AddTempString($"You clicked on the {clicked.ButtonCode} button!");
                switch (clicked.ButtonCode)
                {
                    case "Cancel":
                        MenuManager.Current.CloseTop();
                        return true;

                    default:
                        return false;
                }
            }

            //if (this.worldMenu != null)
            //{
            //    return this.worldMenu.HandleLeftClick(x, y);
            //}

            return false;
        }
    }
}
