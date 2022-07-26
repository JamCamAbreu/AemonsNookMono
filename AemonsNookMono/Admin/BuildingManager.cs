using AemonsNookMono.Admin;
using AemonsNookMono.GameWorld;
using AemonsNookMono.Structures;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Admin
{
    public class BuildingManager
    {
        #region Singleton Implementation
        private static BuildingManager instance;
        private static object _lock = new object();
        private BuildingManager() { }
        public static BuildingManager Current
        {
            get
            {
                if (instance == null)
                {
                    lock (_lock)
                    {
                        if (instance == null)
                        {
                            instance = new BuildingManager();
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

        #region Game Loop
        public void Init()
        {
            this.AllBuildings = new List<Building>();
            this.AllStockpiles = new List<Stockpile>();
        }
        public void Update()
        {
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
            if (World.Current.hero != null)
            {
                Graphics.Current.SpriteB.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend,
                    null, null, null, null, Camera.Current.TranslationMatrix);
            }
            else
            {
                Graphics.Current.SpriteB.Begin();
            }
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
        #endregion

        #region Interface
        public void ClearAllBuildings()
        {
            this.AllBuildings.Clear();
            this.AllStockpiles.Clear();
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
