using AemonsNookMono.Admin;
using AemonsNookMono.Levels;
using AemonsNookMono.Menus.LevelEditor;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
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
          null,
          string.Empty)
        {
            this.InitButtons();
            this.OriginalState = originalState;
            this.customLevelWidth = 12;
            this.customLevelHeight = 12;
        }

        #region Public Properties
        public StateManager.State OriginalState { get; set; }
        #endregion

        #region Interface
        public override void InitButtons()
        {
            this.Spans.Clear();

            Span rows = new Span(this.CenterX, this.CenterY, this.Width, this.Height, this.PadWidth, this.PadHeight, Span.SpanType.Vertical);
            rows.AddColorButton("", "", null);
            rows.AddColorButton("", "", null);
            rows.AddColorButton("Back", "Back", Color.Black);
            this.Spans.Add(rows);

            Span levelButtons = new Span(this.CenterX, rows.Cells[0].ScreenY, this.Width, rows.Cells[0].Height, this.PadWidth, 0, Span.SpanType.Horizontal);
            levelButtons.AddColorButton("Small Meadow", "Small Meadow", ProfileManager.Current.ColorPrimary);
            levelButtons.AddColorButton("Cedric's Pass", "Cedric's Pass", ProfileManager.Current.ColorPrimary);
            this.Spans.Add(levelButtons);

            Span editorButtons = new Span(
                this.CenterX + (this.Width)/4, rows.Cells[1].ScreenY, 
                this.Width / 2, (int)((float)rows.Cells[1].Height * 2), 
                this.PadWidth, this.PadHeight, Span.SpanType.Horizontal);
            editorButtons.AddColorButton("Width", "Width", null, false);
            editorButtons.AddColorButton("Height",  "Height", null, false);
            editorButtons.AddColorButton("Create",  "Create", ProfileManager.Current.ColorPrimary);
            this.Spans.Add(editorButtons);

            int colNum = 0;
            Span WidthButtons = new Span(editorButtons.Cells[colNum].ScreenX, editorButtons.Cells[colNum].ScreenY, editorButtons.Cells[colNum].Width, editorButtons.Cells[colNum].Height, 4, 4, Span.SpanType.Vertical);
            WidthButtons.AddColorButton("WidthTitle", string.Empty, null, false);
            WidthButtons.AddColorButton("widthplus", "+", ProfileManager.Current.ColorPrimary);
            WidthButtons.AddColorButton("widthminus", "-", ProfileManager.Current.ColorPrimary);
            this.Spans.Add(WidthButtons);

            colNum = 1;
            Span HeightButtons = new Span(editorButtons.Cells[colNum].ScreenX, editorButtons.Cells[colNum].ScreenY, editorButtons.Cells[colNum].Width, editorButtons.Cells[colNum].Height, 4, 4, Span.SpanType.Vertical);
            HeightButtons.AddColorButton("HeightTitle", string.Empty, null, false);
            HeightButtons.AddColorButton("heightplus", "+", ProfileManager.Current.ColorPrimary);
            HeightButtons.AddColorButton("heightminus", "-", ProfileManager.Current.ColorPrimary);
            this.Spans.Add(HeightButtons);
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
                Debugger.Current.AddTempString($"You clicked on the {clicked.ButtonCode} button!");
                switch (clicked.ButtonCode)
                {
                    case "Create":
                        SaveManager.Current.SaveProfile(ProfileManager.Current.Loaded);
                        StateManager.Current.CurrentState = StateManager.State.LevelEditor;
                        Level created = this.GenerateBlankLevel(this.customLevelWidth, this.customLevelHeight);
                        GameWorld.World.Current.Init(created);
                        MenuManager.Current.AddMenu(new EditorTileMenu());
                        return true;

                    case "Small Meadow":
                        SaveManager.Current.SaveProfile(ProfileManager.Current.Loaded);
                        StateManager.Current.CurrentState = StateManager.State.World;
                        GameWorld.World.Current.Init(new Levels.SmallMeadow());
                        return true;

                    case "Cedric's Pass":
                        SaveManager.Current.SaveProfile(ProfileManager.Current.Loaded);
                        StateManager.Current.CurrentState = StateManager.State.World;
                        GameWorld.World.Current.Init(new Levels.CedricsPass());
                        return true;

                    case "Back":
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
        public override void Update()
        {
            if (InputManager.Current.CheckMouseDownInterval(InputManager.MouseButton.Left, InputManager.BUTTON_DOWN_INITIAL, InputManager.BUTTON_DOWN_SPEED))
            {
                MouseState state = InputManager.Current.CurMouseState;
                Button clicked = this.CheckButtonCollisions(state.X, state.Y);
                if (clicked != null)
                {
                    switch (clicked.ButtonCode)
                    {
                        case "widthplus":
                            if (this.customLevelWidth < 30) { this.customLevelWidth += 1; }
                            break;

                        case "widthminus":
                            if (this.customLevelWidth > 10) { this.customLevelWidth -= 1; }
                            break;

                        case "heightplus":
                            if (this.customLevelHeight < 25) { this.customLevelHeight += 1; }
                            break;

                        case "heightminus":
                            if (this.customLevelHeight > 10) { this.customLevelHeight -= 1; }
                            break;
                    }
                }
            }

            base.Update();
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
