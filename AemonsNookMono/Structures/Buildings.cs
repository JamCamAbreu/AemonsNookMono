using AemonsNookMono.Admin;
using AemonsNookMono.GameWorld;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Structures
{
    public class Buildings
    {
        #region Singleton Implementation
        private static Buildings instance;
        private static object _lock = new object();
        private Buildings() { }
        public static Buildings Current
        {
            get
            {
                if (instance == null)
                {
                    lock (_lock)
                    {
                        if (instance == null)
                        {
                            instance = new Buildings();
                        }
                    }
                }
                return instance;
            }
        }
        #endregion

        #region Public Properties
        public List<Building> AllBuildings { get; set; }
        public List<Stockpile> AllStockpiles { get; set; }
        public BuildingSelection Selection { get; set; }
        #endregion

        #region Interface
        public void Init()
        {
            this.AllBuildings = new List<Building>();
            this.AllStockpiles = new List<Stockpile>();
        }
        public void AddBuilding(int x, int y, BuildingInfo.Type t)
        {
            if (t == BuildingInfo.Type.STOCKPILE)
            {
                Stockpile st = new Stockpile(x, y, t);
                this.AllStockpiles.Add(st);
                this.AllBuildings.Add(st);
            }
            else
            {
                Building b = new Building(x, y, t);
                this.AllBuildings.Add(b);
            }

        }
        public void Update()
        {
            if (this.Selection == null && Keyboard.GetState().IsKeyDown(Keys.D1))
            {
                this.Selection = new BuildingSelection(BuildingInfo.Type.STOCKPILE);
                StateManager.Current.CurrentState = StateManager.State.BuildSelection;
            }
            if (this.Selection == null && Keyboard.GetState().IsKeyDown(Keys.D2))
            {
                this.Selection = new BuildingSelection(BuildingInfo.Type.TOWER);
                StateManager.Current.CurrentState = StateManager.State.BuildSelection;
            }
            if (this.Selection == null && Keyboard.GetState().IsKeyDown(Keys.D3))
            {
                this.Selection = new BuildingSelection(BuildingInfo.Type.BOOTH_GEMS);
                StateManager.Current.CurrentState = StateManager.State.BuildSelection;
            }
            if (this.Selection != null)
            {
                this.Selection.Update();
            }
            foreach (Building b in this.AllBuildings)
            {
                b.Update();
            }
        }
        public void Draw()
        {
            Graphics.Current.SpriteB.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend,
    null, null, null, null, Camera.Current.TranslationMatrix);
            if (this.Selection != null)
            {
                this.Selection.Draw();
            }
            foreach (Building b in this.AllBuildings)
            {
                b.Draw();
            }
            Graphics.Current.SpriteB.End();
        }
        public Stockpile GetClosestStockpile(Tile fromtile)
        {
            if (this.AllStockpiles == null || this.AllStockpiles.Count == 0)
            {
                return null;
            }

            int dist = int.MaxValue;
            Path path;
            Stockpile closest = null;
            foreach (Stockpile pile in this.AllStockpiles)
            {
                path = new Path(fromtile, pile.TilesUnderneath[0], true);
                if (path.Count < dist)
                {
                    closest = pile;
                    dist = path.Count;
                }
            }
            return closest;
        }
        #endregion
    }
}
