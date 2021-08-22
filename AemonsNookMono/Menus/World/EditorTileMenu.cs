using AemonsNookMono.Admin;
using AemonsNookMono.Levels.Creator;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
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
                null,
                string.Empty)
        {
            this.InitButtons();
            this.worldMenu = MenuManager.Current.RetrieveMenu("WorldMenu") as WorldMenu;
            this.CurSelection = null;
        }

        #region Public Properties
        public TileSelection CurSelection { get; set; }
        #endregion

        #region Interface
        public override void Update()
        {
            if (this.CurSelection != null) { this.CurSelection.Update(); }
            if (InputManager.Current.CheckMouseDownInterval(InputManager.MouseButton.Left, 2, 2))
            {
                if (this.CurSelection != null && this.CurSelection.TileUnderMouse != null)
                {
                    this.CurSelection.Paint();
                }
            }
            if (InputManager.Current.CheckMouseReleased(InputManager.MouseButton.Left))
            {
                if (this.CurSelection != null)
                {
                    GameWorld.World.Current.RefreshTiles(true);
                }
            }
            base.Update();
        }
        public override void Draw(bool isTop)
        {
            if (this.CurSelection != null) { this.CurSelection.Draw(); }
            base.Draw(isTop);
        }
        public override void InitButtons()
        {
            this.Spans.Clear();

            Span levelButtons = new Span(this.CenterX, this.CenterY, this.Width, this.Height, this.PadWidth, this.PadHeight, Span.SpanType.Vertical);
            levelButtons.AddColorButton("Grass", "Grass", Color.DarkOliveGreen);
            levelButtons.AddColorButton("Dirt", "Dirt", ProfileManager.Current.ColorPrimary);
            levelButtons.AddColorButton("Tree", "Tree", Color.Green);
            levelButtons.AddColorButton("Rock",  "Rock", Color.LightSlateGray);
            levelButtons.AddColorButton("Water",  "Water", Color.Blue);
            this.Spans.Add(levelButtons);
        }
        public override bool HandleLeftClick(int x, int y)
        {


            foreach (Button buttons in this.Spans[0].Cells.Where(cell => cell is Button))
            {
                buttons.Selected = false;
            }

            Button clicked = this.CheckButtonCollisions(x, y);
            if (clicked != null)
            {
                clicked.Selected = true;
                Debugger.Current.AddTempString($"You clicked on the {clicked.ButtonCode} button!");
                switch (clicked.ButtonCode)
                {
                    case "Grass":
                        this.CurSelection = new TileSelection(GameWorld.Tile.TileType.Grass);
                        return true;

                    case "Dirt":
                        this.CurSelection = new TileSelection(GameWorld.Tile.TileType.Dirt);
                        return true;

                    case "Tree":
                        this.CurSelection = new TileSelection(GameWorld.Tile.TileType.Tree);
                        return true;

                    case "Rock":
                        this.CurSelection = new TileSelection(GameWorld.Tile.TileType.Stone);
                        return true;

                    case "Water":
                        this.CurSelection = new TileSelection(GameWorld.Tile.TileType.Water);
                        return true;

                    default:
                        return false;
                }
            }

            if (this.worldMenu != null)
            {
                return this.worldMenu.HandleLeftClick(x, y);
            }

            return false;
        }
        #endregion

        #region Internal
        protected WorldMenu worldMenu { get; set; }
        #endregion
    }
}
