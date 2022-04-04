using AemonsNookMono.Admin;
using AemonsNookMono.Levels.Creator;
using AemonsNookMono.Menus.World;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AemonsNookMono.Menus.LevelEditor
{
    public class EditorTileMenu : Menu
    {
        public EditorTileMenu() :
            base("Tiles",
                256, // width 
                (int)((float)Graphics.Current.ScreenHeight * 0.65f), // height
                (int)((float)Graphics.Current.ScreenWidth - 256), // x
                (int)((float)Graphics.Current.ScreenHeight * 0.6f), // y
                16, // padwidth
                16, // padheight
                null,
                string.Empty)
        {
            this.InitButtons();
            this.worldMenu = MenuManager.Current.RetrieveMenu("WorldMenu") as WorldMenu;

            this.SelectedBrush.Select(this.CellGroupings[0].GetButton("1X1"));
            this.SelectedTileType.Select(this.CellGroupings[0].GetButton("Tree"));
            this.CurSelection = new TileSelection(GameWorld.Tile.TileType.Tree);
        }

        #region Public Properties
        public TileSelection CurSelection { get; set; }
        public ButtonSelection SelectedBrush { get; set; }
        public ButtonSelection SelectedTileType { get; set; }
        #endregion

        #region Interface
        public override void Update()
        {
            if (StateManager.Current.CurrentState == StateManager.State.LevelEditor)
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
            }

            base.Update();
        }
        public override void Draw()
        {
            if (StateManager.Current.CurrentState == StateManager.State.LevelEditor && this.CurSelection != null) { this.CurSelection.Draw(); }
            base.Draw();
        }
        public override void InitButtons()
        {
            this.CellGroupings.Clear();

            Span columns = new Span(this.CenterX, this.CenterY, this.Width, this.Height, this.PadWidth, this.PadHeight, Span.SpanType.Horizontal);

            this.SelectedBrush = new ButtonSelection();
            Span Other = new Span(Span.SpanType.Vertical);
            this.SelectedBrush.Add(Other.AddColorButton("1X1", "1X1", Color.SaddleBrown));
            this.SelectedBrush.Add(Other.AddColorButton("2X2", "2X2", Color.SaddleBrown));
            this.SelectedBrush.Add(Other.AddColorButton("Cross", "Cross", Color.SaddleBrown));
            Other.AddBlank();
            this.SelectedBrush.Add(Other.AddColorButton("Save", "Save", Color.Black));
            columns.AddSpan(Other);

            this.SelectedTileType = new ButtonSelection();
            Span TileTypes = new Span(Span.SpanType.Vertical);
            this.SelectedTileType.Add(TileTypes.AddColorButton("Grass", "Grass", Color.DarkOliveGreen));
            this.SelectedTileType.Add(TileTypes.AddColorButton("Dirt", "Dirt", ProfileManager.Current.ColorPrimary));
            this.SelectedTileType.Add(TileTypes.AddColorButton("Tree", "Tree", Color.Green));
            this.SelectedTileType.Add(TileTypes.AddColorButton("Rock",  "Rock", Color.LightSlateGray));
            this.SelectedTileType.Add(TileTypes.AddColorButton("Water",  "Water", Color.Blue));
            columns.AddSpan(TileTypes);

            foreach (Span span in this.CellGroupings)
            {
                span.Refresh();
            }

            this.CellGroupings.Add(columns);
        }
        public override bool HandleLeftClick(int x, int y)
        {
            Button clicked = this.CheckButtonCollisions(x, y);
            if (clicked != null)
            {
                this.SelectedBrush.CheckSelect(clicked.ButtonCode);
                this.SelectedTileType.CheckSelect(clicked.ButtonCode);
                GameWorld.Tile.TileType curType = this.CurSelection?.Type ?? GameWorld.Tile.TileType.Grass;
                TileSelection.BrushShape curShape = this.CurSelection?.Shape ?? TileSelection.BrushShape.OneByOne;
                Debugger.Current.AddTempString($"You clicked on the {clicked.ButtonCode} button!");
                switch (clicked.ButtonCode)
                {
                    case "Grass":
                        this.CurSelection = new TileSelection(GameWorld.Tile.TileType.Grass, curShape);
                        return true;

                    case "Dirt":
                        this.CurSelection = new TileSelection(GameWorld.Tile.TileType.Dirt, curShape);
                        return true;

                    case "Tree":
                        this.CurSelection = new TileSelection(GameWorld.Tile.TileType.Tree, curShape);
                        return true;

                    case "Rock":
                        this.CurSelection = new TileSelection(GameWorld.Tile.TileType.Stone, curShape);
                        return true;

                    case "Water":
                        this.CurSelection = new TileSelection(GameWorld.Tile.TileType.Water, curShape);
                        return true;

                    case "1X1":
                        
                        this.CurSelection = new TileSelection(curType, TileSelection.BrushShape.OneByOne);
                        return true;

                    case "2X2":
                        this.CurSelection = new TileSelection(curType, TileSelection.BrushShape.TwoByTwo);
                        return true;

                    case "Cross":
                        this.CurSelection = new TileSelection(curType, TileSelection.BrushShape.Cross);
                        return true;

                    case "Save":
                        MenuManager.Current.AddMenu(new EditorSaveLevel(), false, true);
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
