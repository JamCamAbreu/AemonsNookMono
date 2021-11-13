﻿using AemonsNookMono.GameWorld.Effects;
using AemonsNookMono.Levels;
using AemonsNookMono.Resources;
using AemonsNookMono.Structures;
using AemonsNookMono.Admin;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AemonsNookMono.Menus;
using AemonsNookMono.Menus.World;
using AemonsNookMono.Peeps;
using AemonsNookMono.Player;

namespace AemonsNookMono.GameWorld
{
    public class World
    {
        #region Singleton Implementation
        private static World instance;
        private static object _lock = new object();
        private World() { }
        public static World Current
        {
            get
            {
                if (instance == null)
                {
                    lock (_lock)
                    {
                        if (instance == null)
                        {
                            instance = new World();
                        }
                    }
                }
                return instance;
            }
        }
        #endregion

        #region Public Properties
        public int StartDrawX = 0;
        public int StartDrawY = 0;
        public Vector2 StartDraw { get { return new Vector2(StartDrawX, StartDrawY); } }
        public const int TILE_DIMENSION_PIXELS = 32;
        public Tile[,] Tiles;
        public int Width { get; set; }
        public int Height { get; set; }
        public List<Tile> RoadTiles { get; set; }
        public List<Tile> SpawnTiles { get; set; }
        public List<Tile> TreeTiles { get; set; }
        public List<Tile> StoneTiles { get; set; }
        public List<Tile> WaterTiles { get; set; }
        public List<List<Tile>> TileLists { get; set; }
        public SortedResourceList Resources { get; set; }
        public List<Peep> Peeps { get; set; }
        public Random ran { get; set; }
        public Hero hero { get; set; }
        #endregion

        #region Constructors
        public void Init(Level level)
        {
            this.ran = new Random();

            this.TileLists = new List<List<Tile>>();
            this.RoadTiles = new List<Tile>();
            this.TileLists.Add(this.RoadTiles);

            this.SpawnTiles = new List<Tile>();
            this.TileLists.Add(this.SpawnTiles);

            this.TreeTiles = new List<Tile>();
            this.TileLists.Add(this.TreeTiles);

            this.StoneTiles = new List<Tile>();
            this.TileLists.Add(this.StoneTiles);

            this.WaterTiles = new List<Tile>();
            this.TileLists.Add(this.WaterTiles);

            this.Resources = new SortedResourceList();
            this.Peeps = new List<Peep>();
            this.LoadLevel(level);
            this.sizeX = level.WIDTH * TILE_DIMENSION_PIXELS;
            this.sizeY = level.HEIGHT * TILE_DIMENSION_PIXELS;
            this.StartDrawX = (Graphics.Current.Device.Viewport.Width / 2) - (this.sizeX / 2);
            this.StartDrawY = (Graphics.Current.Device.Viewport.Height / 2) - (this.sizeY / 2);
            Debugger.Current.CurrentWorld = this;
            EffectsGenerator.Current.Init();

            MenuManager.Current.ClearAllMenus();
            MenuManager.Current.AddMenu(new WorldMenu());

            this.hero = new Hero();
        }
        #endregion

        #region Interface
        public void RefreshTiles(bool fixedResources)
        {
            this.SetTileShapes();
            this.ReloadTiles();
            if (fixedResources)
            {
                this.SpawnTrees(1, 1, false);
                this.SpawnStones(1, 1, false);
            }
            else
            {
                this.SpawnTrees(0, 4);
                this.SpawnStones(0, 3);
            }

        }
        public void RefreshDisplay()
        {
            this.StartDrawX = (Graphics.Current.Device.Viewport.Width / 2) - (this.sizeX / 2);
            this.StartDrawY = (Graphics.Current.Device.Viewport.Height / 2) - (this.sizeY / 2);
            if (this.Resources != null && this.Resources.Sorted != null && this.Resources.Sorted.Count > 0)
            {
                foreach (Resource r in this.Resources.Sorted.Values)
                {
                    r.PosX = this.StartDrawX + r.TileOn.RelativeX + r.TileRelativeX;
                    r.PosY = this.StartDrawY + r.TileOn.RelativeY + r.TileRelativeY;
                    if (r is Tree) { (r as Tree).SetCollisions(); }
                    else if (r is Stone) { (r as Stone).SetCollisions(); }
                    else { throw new NotImplementedException(); }
                }
            }
        }
        public bool InsideBounds(int pixelX, int pixelY)
        {
            if (pixelX < this.StartDrawX || pixelY < this.StartDrawY || pixelX >= this.StartDrawX + this.sizeX || pixelY >= this.StartDrawY + this.sizeY) { return false; }
            return true;
        }
        public Tile TileAt(int x, int y)
        {
            if (x < 0 || y < 0 || x > this.Width || y > this.Height) { throw new Exception("Attempt to retrieve Tile out of bounds!"); }
            return this.Tiles[y, x];
        }
        public Tile TileAtPixel(int pixelX, int pixelY)
        {
            if (!this.InsideBounds(pixelX, pixelY)) { return null; }
            int relativeX = pixelX - this.StartDrawX;
            int relativeY = pixelY - this.StartDrawY;
            int tileX = relativeX / TILE_DIMENSION_PIXELS;
            int tileY = relativeY / TILE_DIMENSION_PIXELS;
            return TileAt(tileX, tileY);
        }
        public Tile RetrieveRandomTile()
        {
            int x = this.ran.Next(0, this.Width - 1);
            int y = this.ran.Next(0, this.Height - 1);
            return TileAt(x, y);
        }
        public void Draw()
        {
            Graphics.Current.SpriteB.Begin();
            Graphics.Current.GraphicsDM.GraphicsDevice.Clear(Color.Black);

            for (int row = 0; row < this.Height; row++)
            {
                for (int col = 0; col < this.Width; col++)
                {
                    TileAt(col, row).Draw();
                }
            }
            this.Resources.Draw();

            foreach (Peep peep in this.Peeps)
            {
                peep.Draw();
            }

            this.hero.Draw();

            Graphics.Current.SpriteB.End();
        }
        public void Update(GameTime gameTime)
        {
            foreach (Tile t in this.Tiles)
            {
                t.Update();
            }
            this.Resources.Update();
            List<Peep> exitPeeps = new List<Peep>();
            foreach (Peep peep in this.Peeps)
            {
                peep.Update();
                if (peep.ReadyToExit) { exitPeeps.Add(peep); }
            }
            foreach (Peep exitpeep in exitPeeps)
            {
                this.Peeps.Remove(exitpeep);
            }

            this.hero.Update();
        }
        public Tile RetrieveRandomExit(Tile excluding)
        {
            if (this.SpawnTiles == null || this.SpawnTiles.Count == 0)
            {
                throw new Exception("Attempted to retrieve an exit but couldn't find one.");
            }
            else if (excluding != null && this.SpawnTiles.Count == 1)
            {
                throw new Exception("There is only one entrance, so could not find an exit.");
            }
            else
            {
                List<Tile> toChooseFrom = new List<Tile>(this.SpawnTiles);
                if (excluding != null)
                {
                    toChooseFrom.Remove(excluding);
                }
                return toChooseFrom[this.ran.Next(0, toChooseFrom.Count - 1)];
            }
        }
        #endregion

        #region Private Properties
        private const int MAX_WORLD_WIDTH = 40;
        private const int MAX_WORLD_HEIGHT = 40;
        private int sizeX = 0;
        private int sizeY = 0;
        #endregion

        #region Helper Methods
        private void initTiles(int width, int height)
        {
            this.Tiles = new Tile[height,width];
            this.Width = width;
            this.Height = height;
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    this.Tiles[row, col] = new Tile(col, row, Tile.TileType.Grass, TILE_DIMENSION_PIXELS);
                }
            }
        }
        private void SetTileNeighbors()
        {
            Tile curTile;
            for (int row = 0; row < this.Height; row++)
            {
                for (int col = 0; col < this.Width; col++)
                {
                    curTile = TileAt(col, row);
                    if (row > 0)
                    {
                        curTile.TileBelow = TileAt(col, row - 1);
                    }
                    if (row < this.Height - 1)
                    {
                        curTile.TileAbove = TileAt(col, row + 1);
                    }
                    if (col > 0)
                    {
                        curTile.TileLeft = TileAt(col - 1, row);
                    }
                    if (col < this.Width - 1)
                    {
                        curTile.TileRight = TileAt(col + 1, row);
                    }
                }
            }
        }
        private void SetTileShapes()
        {
            Tile curTile;
            for (int row = 0; row < this.Height; row++)
            {
                for (int col = 0; col < this.Width; col++)
                {
                    int shape = 0;
                    curTile = TileAt(col, row);
                    if (curTile.TileAbove != null && curTile.TileAbove.Type == curTile.Type) { shape |= 4; }
                    if (curTile.TileRight != null && curTile.TileRight.Type == curTile.Type) { shape |= 1; }
                    if (curTile.TileBelow != null && curTile.TileBelow.Type == curTile.Type) { shape |= 2; }
                    if (curTile.TileLeft != null && curTile.TileLeft.Type == curTile.Type) { shape |= 8; }

                    curTile.Shape = shape;
                }
            }
        }
        private void LoadLevel(Level level)
        {
            this.initTiles(level.WIDTH, level.HEIGHT);
            this.sizeX = level.WIDTH * TILE_DIMENSION_PIXELS;
            this.sizeY = level.HEIGHT * TILE_DIMENSION_PIXELS;
            this.StartDrawX = (Graphics.Current.Device.Viewport.Width / 2) - (this.sizeX / 2);
            this.StartDrawY = (Graphics.Current.Device.Viewport.Height / 2) - (this.sizeY / 2);

            this.SetTileNeighbors();

            string levelcode = level.RetrieveLevelCode();
            int i = 0;
            char c;
            for (int row = 0; row <= this.Height - 1; row++)
            {
                for (int col = 0; col < this.Width; col++)
                {
                    c = levelcode[i];
                    LoadChar(row, col, c);
                    i++;
                }
            }

            this.SpawnTrees(0, 4);
            this.SpawnStones(0, 3);

            this.SetTileShapes();
        }
        private void LoadChar(int row, int col, char c)
        {
            Tile curTile = this.TileAt(col, row);
            switch (c)
            {
                case 'T':
                    curTile.Type = Tile.TileType.Tree;
                    this.TreeTiles.Add(curTile);
                    break;

                case 'S':
                    curTile.Type = Tile.TileType.Stone;
                    this.StoneTiles.Add(curTile);
                    break;

                case 'W':
                    curTile.Type = Tile.TileType.Water;
                    this.WaterTiles.Add(curTile);
                    break;

                case 'D':
                    curTile.Type = Tile.TileType.Dirt;
                    curTile.IsPath = true;
                    this.RoadTiles.Add(curTile);
                    break;

                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    curTile.Type = Tile.TileType.Dirt;
                    curTile.IsPath = true;
                    curTile.IsMapEdge = true;
                    curTile.MapEdgeId = int.Parse(c.ToString());
                    this.SpawnTiles.Add(curTile);
                    this.RoadTiles.Add(curTile);
                    break;

                default:
                    break;
            }
        }
        private void ReloadTiles()
        {
            this.Resources.Clear();
            foreach (List<Tile> list in this.TileLists)
            {
                list.Clear();
            }
            Tile curTile;
            int mapEdgeId = 0;
            for (int row = 0; row < this.Height; row++)
            {
                for (int col = 0; col < this.Width; col++)
                {
                    curTile = TileAt(col, row);
                    if (curTile != null)
                    {
                        curTile.Regenerate();
                        switch (curTile.Type)
                        {
                            case Tile.TileType.Grass:
                                break;

                            case Tile.TileType.Stone:
                                this.StoneTiles.Add(curTile);
                                break;

                            case Tile.TileType.Tree:
                                this.TreeTiles.Add(curTile);
                                break;

                            case Tile.TileType.Building:
                                break;

                            case Tile.TileType.Water:
                                this.WaterTiles.Add(curTile);
                                break;

                            case Tile.TileType.Dirt:
                                curTile.Type = Tile.TileType.Dirt;
                                curTile.IsPath = true;
                                this.RoadTiles.Add(curTile);
                                if (mapEdgeId < 10 && curTile.TileAbove == null || curTile.TileRight == null || curTile.TileBelow == null || curTile.TileLeft == null)
                                {
                                    curTile.IsMapEdge = true;
                                    curTile.MapEdgeId = mapEdgeId;
                                    mapEdgeId++;
                                    this.SpawnTiles.Add(curTile);
                                }
                                break;
                        }
                    }
                }
            }
        }
        private void ReloadTile(Tile tile)
        {

        }
        private void SpawnTrees(int min, int max, bool useRandomOffset = true)
        {
            int mid = 16;
            int pad = World.TILE_DIMENSION_PIXELS / 8;
            int offsetX;
            int offsetY;
            foreach (Tile tile in this.TreeTiles)
            {
                int num = this.ran.Next(min, max);
                for (int i = 0; i < num; i++)
                {
                    if (useRandomOffset)
                    {
                        offsetX = this.ran.Next(-mid + pad, mid - pad);
                        offsetY = this.ran.Next(-mid * 2 + pad, -pad);
                    }
                    else
                    {
                        offsetX = 0;
                        offsetY = 0;
                    }

                    Tree t = new Tree(this.StartDrawX + tile.RelativeX + offsetX, this.StartDrawY + tile.RelativeY + offsetY, tile);
                    t.TileRelativeX = offsetX;
                    t.TileRelativeY = offsetY;
                    this.Resources.Add(t);
                    tile.Resources.Add(t);
                }
            }
        }
        private void SpawnStones(int min, int max, bool useRandomOffset = true)
        {
            int mid = 8;
            int pad = World.TILE_DIMENSION_PIXELS / 8;
            int offsetX;
            int offsetY;
            foreach (Tile tile in this.StoneTiles)
            {
                int num = this.ran.Next(min, max);
                for (int i = 0; i < num; i++)
                {
                    if (useRandomOffset)
                    {
                        offsetX = this.ran.Next(-mid + pad, mid * 2 - pad);
                        offsetY = this.ran.Next(-mid + pad, mid * 2 - pad);
                    }
                    else
                    {
                        offsetX = 0;
                        offsetY = 0;
                    }

                    Stone s = new Stone(this.StartDrawX + tile.RelativeX + offsetX, this.StartDrawY + tile.RelativeY + offsetY, tile);
                    s.TileRelativeX = offsetX;
                    s.TileRelativeY = offsetY;
                    this.Resources.Add(s);
                    tile.Resources.Add(s);
                }
            }
        }
        #endregion
    }
}
