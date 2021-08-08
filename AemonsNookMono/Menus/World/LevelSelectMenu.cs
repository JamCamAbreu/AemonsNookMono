using AemonsNookMono.Admin;
using AemonsNookMono.Levels;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Menus.World
{
    public class LevelSelectMenu : Menu
    {

        public LevelSelectMenu(StateManager.State originalState) :
            base("Level Select",
          (int)((float)Graphics.Current.ScreenWidth * 0.75f),
          (int)((float)Graphics.Current.ScreenHeight * 0.4f),
          Graphics.Current.ScreenMidX,
          Graphics.Current.ScreenMidY,
          ((int)((float)Graphics.Current.ScreenWidth * 0.75f) / 16),
          (int)((float)Graphics.Current.ScreenHeight * 0.4f) / 16,
          Color.LightGreen,
          string.Empty)
        {
            this.InitButtons();
            this.OriginalState = originalState;
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
            levelButtons.AddButton("Cedric's Pass", Color.Black);
            this.ButtonSpans.Add(levelButtons);

            ButtonSpan editorButtons = new ButtonSpan(this.CenterX, rows.Buttons[1].ScreenY, this.Width, rows.Buttons[1].Height, this.PadWidth, 0, ButtonSpan.SpanType.Horizontal);
            editorButtons.AddButton("Width", Color.DarkOliveGreen);
            editorButtons.AddButton("", null);
            editorButtons.AddButton("Height", Color.DarkOliveGreen);
            editorButtons.AddButton("", null);
            editorButtons.AddButton("", null); // Editor Left
            editorButtons.AddButton("", null); // Editor Right
            this.ButtonSpans.Add(editorButtons);

            this.AddStaticButton("Editor",
                editorButtons.Buttons[0].Width * 2,
                editorButtons.Buttons[0].Height,
                (editorButtons.Buttons[4].ScreenX + editorButtons.Buttons[5].ScreenX) / 2,
                editorButtons.Buttons[0].ScreenY,
                null,
                Color.Goldenrod,
                Collision.CollisionShape.Rectangle);
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
                    case "Editor":
                        StateManager.Current.CurrentState = this.OriginalState;
                        Level created = this.GenerateBlankLevel(20, 20); // todo, pull values from some kind of integer controls
                        GameWorld.World.Current.Init(created);
                        return true;

                    case "Small Meadow":
                        StateManager.Current.CurrentState = this.OriginalState;
                        GameWorld.World.Current.Init(new Levels.SmallMeadow());
                        return true;

                    case "Cedric's Pass":
                        StateManager.Current.CurrentState = this.OriginalState;
                        GameWorld.World.Current.Init(new Levels.CedricsPass());
                        return true;

                    case "Back":
                        StateManager.Current.CurrentState = this.OriginalState;
                        MenuManager.Current.CloseTop();
                        return true;

                    default:
                        return base.HandleLeftClick(x, y);
                }
            }
            return base.HandleLeftClick(x, y);
        }
        #endregion


        #region Internal
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
