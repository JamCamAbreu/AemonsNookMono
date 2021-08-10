using AemonsNookMono.Admin;
using AemonsNookMono.Levels;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AemonsNookMono.Menus.World
{
    public class LevelSelectMenu : Menu
    {

        public LevelSelectMenu(StateManager.State originalState) :
            base("Level Select",
          (int)((float)Graphics.Current.ScreenWidth * 0.75f),
          (int)((float)Graphics.Current.ScreenHeight * 0.6f),
          Graphics.Current.ScreenMidX,
          Graphics.Current.ScreenMidY,
          32,
          32,
          Color.SaddleBrown,
          string.Empty)
        {
            this.InitButtons();
            this.OriginalState = originalState;
            this.customLevelWidth = 20;
            this.customLevelHeight = 20;
        }

        #region Public Properties
        public StateManager.State OriginalState { get; set; }
        #endregion


        #region Interface
        public override void InitButtons()
        {
            this.ButtonSpans.Clear();

            ButtonSpan rows = new ButtonSpan(this.CenterX, this.CenterY, this.Width, this.Height, this.PadWidth, this.PadHeight, ButtonSpan.SpanType.Vertical);
            rows.AddButton("", null);
            rows.AddButton("", null);
            rows.AddButton("Back", Color.Black);
            this.ButtonSpans.Add(rows);

            ButtonSpan levelButtons = new ButtonSpan(this.CenterX, rows.Buttons[0].ScreenY, this.Width, rows.Buttons[0].Height, this.PadWidth, 0, ButtonSpan.SpanType.Horizontal);
            levelButtons.AddButton("Small Meadow", Color.DarkGreen);
            levelButtons.AddButton("Cedric's Pass", Color.DarkGreen);
            this.ButtonSpans.Add(levelButtons);

            ButtonSpan editorButtons = new ButtonSpan(
                this.CenterX + (this.Width)/4, rows.Buttons[1].ScreenY, 
                this.Width / 2, (int)((float)rows.Buttons[1].Height * 2), 
                this.PadWidth, this.PadHeight, ButtonSpan.SpanType.Horizontal);
            editorButtons.AddButton("Width", null, false);
            editorButtons.AddButton("Height", null, false);
            editorButtons.AddButton("Create", Color.DarkOliveGreen);
            this.ButtonSpans.Add(editorButtons);

            int colNum = 0;
            ButtonSpan WidthButtons = new ButtonSpan(editorButtons.Buttons[colNum].ScreenX, editorButtons.Buttons[colNum].ScreenY, editorButtons.Buttons[colNum].Width, editorButtons.Buttons[colNum].Height, 4, 4, ButtonSpan.SpanType.Vertical);
            WidthButtons.AddButton("WidthTitle", null, false);
            WidthButtons.AddButton("widthplus", Color.DarkOliveGreen);
            WidthButtons.AddButton("widthminus", Color.DarkOliveGreen);
            WidthButtons.Buttons[1].Title = "+";
            WidthButtons.Buttons[2].Title = "-";
            this.ButtonSpans.Add(WidthButtons);

            colNum = 1;
            ButtonSpan HeightButtons = new ButtonSpan(editorButtons.Buttons[colNum].ScreenX, editorButtons.Buttons[colNum].ScreenY, editorButtons.Buttons[colNum].Width, editorButtons.Buttons[colNum].Height, 4, 4, ButtonSpan.SpanType.Vertical);
            HeightButtons.AddButton("HeightTitle", null, false);
            HeightButtons.AddButton("heightplus", Color.DarkOliveGreen);
            HeightButtons.AddButton("heightminus", Color.DarkOliveGreen);
            HeightButtons.Buttons[1].Title = "+";
            HeightButtons.Buttons[2].Title = "-";
            this.ButtonSpans.Add(HeightButtons);
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

                Graphics.Current.SpriteB.Begin();
                Button widthButton = this.GetButton("WidthTitle");
                if (widthButton != null) 
                {
                    string wstring = $"Width: {this.customLevelWidth.ToString()}";
                    Vector2 size = Graphics.Current.Fonts["couriernew"].MeasureString(wstring);
                    int x = widthButton.ScreenX - ((int)size.X / 2);
                    int y = widthButton.ScreenY - ((int)size.Y / 2);
                    Graphics.Current.SpriteB.DrawString(Graphics.Current.Fonts["couriernew"], wstring, new Vector2(x, y), Color.White);
                }

                Button heightButton = this.GetButton("HeightTitle");
                if (heightButton != null)
                {
                    string hstring = $"Height: {this.customLevelHeight.ToString()}";
                    Vector2 size = Graphics.Current.Fonts["couriernew"].MeasureString(hstring);
                    int x = heightButton.ScreenX - ((int)size.X / 2);
                    int y = heightButton.ScreenY - ((int)size.Y / 2);
                    Graphics.Current.SpriteB.DrawString(Graphics.Current.Fonts["couriernew"], hstring, new Vector2(x, y), Color.White);
                }

                Graphics.Current.SpriteB.End();
            }
        }

        public override bool HandleLeftClick(int x, int y)
        {
            Button clicked = this.CheckButtonCollisions(x, y);
            if (clicked != null)
            {
                Debugger.Current.AddTempString($"You clicked on the {clicked.Name} button!");
                switch (clicked.Name)
                {
                    case "Create":
                        StateManager.Current.CurrentState = StateManager.State.LevelEditor;
                        Level created = this.GenerateBlankLevel(this.customLevelWidth, this.customLevelHeight);
                        GameWorld.World.Current.Init(created);
                        MenuManager.Current.AddMenu(new EditorTileMenu());
                        return true;

                    case "Small Meadow":
                        StateManager.Current.CurrentState = StateManager.State.World;
                        GameWorld.World.Current.Init(new Levels.SmallMeadow());
                        return true;

                    case "Cedric's Pass":
                        StateManager.Current.CurrentState = StateManager.State.World;
                        GameWorld.World.Current.Init(new Levels.CedricsPass());
                        return true;

                    case "Back":
                        StateManager.Current.CurrentState = this.OriginalState;
                        MenuManager.Current.CloseTop();
                        return true;

                    case "widthplus":
                        if (this.customLevelWidth < 30) { this.customLevelWidth += 1; }
                        return true;

                    case "widthminus":
                        if (this.customLevelWidth > 10) { this.customLevelWidth -= 1; }
                        return true;

                    case "heightplus":
                        if (this.customLevelHeight < 25) { this.customLevelHeight += 1; }
                        return true;

                    case "heightminus":
                        if (this.customLevelHeight > 10) { this.customLevelHeight -= 1; }
                        return true;

                    default:
                        return base.HandleLeftClick(x, y);
                }
            }
            return base.HandleLeftClick(x, y);
        }
        #endregion


        #region Internal
        protected int customLevelHeight { get; set; }
        protected int customLevelWidth { get; set; }
        protected Level GenerateBlankLevel(int width, int height)
        {
            Level lev = new Level();
            StringBuilder ascii = new StringBuilder();
            for (int w = 0; w < width; w++)
            {
                for (int h = 0; h < height; h++)
                {
                    ascii.Append("-");
                }
            }
            lev.ascii = ascii.ToString();
            lev.WIDTH = width;
            lev.HEIGHT = height;
            return lev;
        }
        #endregion
    }
}
