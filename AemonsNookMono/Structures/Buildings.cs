using AemonsNookMono.Admin;
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
        public BuildingSelection Selection { get; set; }
        #endregion

        #region Interface
        public void Init()
        {
            this.AllBuildings = new List<Building>();
        }
        public void AddBuilding(int x, int y, BuildingInfo.Type t)
        {
            Building b = new Building(x, y, t);
            this.AllBuildings.Add(b);
        }
        public void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D1))
            {
                this.Selection = new BuildingSelection(BuildingInfo.Type.STOCKPILE);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D2))
            {
                this.Selection = new BuildingSelection(BuildingInfo.Type.BOOTH_FISH);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D3))
            {
                this.Selection = new BuildingSelection(BuildingInfo.Type.BOOTH_GEMS);
            }

            if (this.Selection != null)
            {
                this.Selection.Update();
                if (Mouse.GetState().LeftButton == ButtonState.Pressed) // Todo Move this to input manager
                {
                    this.Selection.Build();
                    this.Selection = null;
                }
            }

            foreach (Building b in this.AllBuildings)
            {
                b.Update();
            }
        }
        public void Draw()
        {
            Graphics.Current.SpriteB.Begin();
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
    }
}
