using AemonsNookMono.Admin;
using AemonsNookMono.GameWorld;
using AemonsNookMono.Levels;
using AemonsNookMono.Menus.General;
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
            this.CellGroupings.Clear();

            Span rows = new Span(this.CenterX, this.CenterY, this.Width, this.Height, this.PadWidth, this.PadHeight, Span.SpanType.Vertical);
            rows.AddText("Please enter a name for your level:");
            rows.AddTextInput("Level Name", "New Level", Color.White).Selected = true;
            rows.AddBlank();
            rows.AddBlank();

            Span bottomrow = new Span(Span.SpanType.Horizontal);
            bottomrow.AddColorButton("Save", "Save", Color.Black);
            bottomrow.AddColorButton("Cancel", "Cancel", Color.Black);
            rows.AddSpan(bottomrow);

            this.CellGroupings.Add(rows);
        }

        public override bool HandleLeftClick(int x, int y)
        {
            Button clicked = this.CheckButtonCollisions(x, y);
            if (clicked != null)
            {
                Debugger.Current.AddTempString($"You clicked on the {clicked.ButtonCode} button!");
                switch (clicked.ButtonCode)
                {
                    case "Level Name":
                        clicked.Selected = true;
                        return true;

                    case "Save":
                        if (!string.IsNullOrEmpty(this.GetButton("Level Name").Title))
                        {
                            if (SaveManager.Current.CheckLevelExists(this.GetButton("Level Name").Title) == false)
                            {
                                string validationResult = this.ValidateLevel();
                                if (validationResult == "okay")
                                {
                                    Level level = this.CreateLevelFromEditor(this.GetButton("Level Name").Title);
                                    if (level != null)
                                    {
                                        SaveManager.Current.SaveLevel(level);
                                        MenuManager.Current.CloseTop();
                                        MenuManager.Current.Top.Refresh();
                                        MenuManager.Current.AddMenu(new MessagePopupMenu("Level Saved", "Your level was saved successfully.", "Okay", MenuManager.Current.Top));
                                    }
                                }
                                else
                                {
                                    MenuManager.Current.AddMenu(new MessagePopupMenu("", validationResult, "Okay", this));
                                }

                            }
                            else
                            {
                                MenuManager.Current.AddMenu(new MessagePopupMenu("", "A level with this name already exists, please choose a different name.", "Okay", this));
                            }
                        }
                        else
                        {
                            MenuManager.Current.AddMenu(new MessagePopupMenu("", "Please enter a name for your level", "Okay", this));
                        }
                        return true;

                    case "Cancel":
                        MenuManager.Current.CloseTop();
                        return true;

                    default:
                        return false;
                }
            }

            return false;
        }

        public string ValidateLevel()
        {
            int edgePathTiles = 0; // Test: Count edge paths:
            Tile[,] worldtiles = AemonsNookMono.GameWorld.World.Current.Tiles;
            Tile curTile;
            for (int row = 0; row < AemonsNookMono.GameWorld.World.Current.Height; row++)
            {
                for (int col = 0; col < AemonsNookMono.GameWorld.World.Current.Width; col++)
                {
                    curTile = worldtiles[row, col];
                    if (curTile != null)
                    {
                        switch (curTile.Type)
                        {
                            case Tile.TileType.Grass:
                                break;

                            case Tile.TileType.Tree:
                                break;

                            case Tile.TileType.Stone:
                                break;

                            case Tile.TileType.Water:
                                break;

                            case Tile.TileType.Dirt:
                                if (curTile.IsMapEdge) { edgePathTiles++; }
                                break;
                        }
                    }
                }
            }

            if (edgePathTiles > 10)
            {
                return "The maximum amount of entrances (dirt paths) on the edge of the level is 10. Please remove paths from the edge.";
            }

            return "okay";
        }
        public Level CreateLevelFromEditor(string levelname)
        {
            Level newlevel = new Level();
            newlevel.Name = levelname;

            Tile[,] worldtiles = AemonsNookMono.GameWorld.World.Current.Tiles;
            newlevel.WIDTH = AemonsNookMono.GameWorld.World.Current.Width;
            newlevel.HEIGHT = AemonsNookMono.GameWorld.World.Current.Height;

            StringBuilder levelstring = new StringBuilder();
            int entranceNumber = 0;
            Tile curTile;
            for (int row = 0; row < newlevel.HEIGHT; row++)
            {
                for (int col = 0; col < newlevel.WIDTH; col++)
                {
                    curTile = worldtiles[row, col];
                    if (curTile != null)
                    {
                        switch (curTile.Type)
                        {
                            case Tile.TileType.Grass:
                                levelstring.Append('-');
                                break;

                            case Tile.TileType.Tree:
                                levelstring.Append('T');
                                break;

                            case Tile.TileType.Stone:
                                levelstring.Append('S');
                                break;

                            case Tile.TileType.Water:
                                levelstring.Append('W');
                                break;

                            case Tile.TileType.Dirt:
                                if (curTile.IsMapEdge)
                                {
                                    levelstring.Append(entranceNumber.ToString());
                                    entranceNumber++;
                                }
                                else
                                {
                                    levelstring.Append('D');
                                }
                                break;
                        }
                    }
                }
                levelstring.Append(' ');
            }

            newlevel.ascii = levelstring.ToString();
            return newlevel;
        }
    }
}
