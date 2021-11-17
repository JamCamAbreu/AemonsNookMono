using AemonsNookMono.Admin;
using AemonsNookMono.GameWorld;
using AemonsNookMono.Resources;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Structures
{
    public class Stockpile : Building
    {
        #region Constructor
        public Stockpile(int x, int y, BuildingInfo.Type t) : base(x, y, t)
        {
        }
        #endregion

        #region Public Properties
        const int RESOURCE_DIMENSION = 16;
        // Todo: Maybe create "slots" for resources, then draw them accordingly instead of just static values like this
        // Then create a function for this building to try to store a resource. If it can't it returns a null or error
        public int NumWood { get; set; } = 0;
        public int NumStone { get; set; } = 0;
        #endregion

        #region Interface
        public override void HandleLeftClick()
        {
            if (World.Current.hero != null && 
                World.Current.hero.HeldResources != null &&
                World.Current.hero.HeldResources.Count > 0)
            {
                Debugger.Current.AddTempString($"Deposited {World.Current.hero.HeldResources.Count} resources.");
                foreach (Resource res in World.Current.hero.HeldResources)
                {
                    if (res is Tree)
                    {
                        this.NumWood++;
                    }
                    else if (res is Stone)
                    {
                        this.NumStone++;
                    }
                    res.Destroy();
                }
                World.Current.hero.HeldResources.Clear();
            }
            else
            {
                Debugger.Current.AddTempString("You have no resources to deposit.");
            }
        }
        public override void Update()
        {

        }
        public override void Draw()
        {
            Graphics.Current.SpriteB.Draw(Graphics.Current.SpritesByName[this.Sprite], new Vector2(World.Current.StartDrawX + this.OriginX, World.Current.StartDrawY + this.OriginY), Color.White);

            string woodimg = this.RetrieveResourceSprite(this.NumWood);
            if (!string.IsNullOrWhiteSpace(woodimg))
            {
                // Top Left
                Graphics.Current.SpriteB.Draw(Graphics.Current.SpritesByName[$"stockpile-wood-{woodimg}"], new Vector2(World.Current.StartDrawX + this.OriginX, World.Current.StartDrawY + this.OriginY), Color.White);
            }
            string stoneimg = this.RetrieveResourceSprite(this.NumStone);
            if (!string.IsNullOrWhiteSpace(stoneimg))
            {
                // Top Right
                Graphics.Current.SpriteB.Draw(Graphics.Current.SpritesByName[$"stockpile-stone-{stoneimg}"], new Vector2(World.Current.StartDrawX + this.OriginX + RESOURCE_DIMENSION, World.Current.StartDrawY + this.OriginY), Color.White);
            }
        }
        #endregion

        #region Helper Methods
        protected string RetrieveResourceSprite(int resourceCount)
        {
            int threshOneMax = 5;
            int threshTwoMax = 12;
            int threshThreeMax = 20;

            if (resourceCount <= 0) { return ""; }
            else if (resourceCount > 0 && resourceCount <= threshOneMax) { return "a"; }
            else if (resourceCount > threshOneMax && resourceCount <= threshTwoMax) { return "b"; }
            else if (resourceCount > threshTwoMax && resourceCount <= threshThreeMax) { return "c"; }
            else { return "d"; }
        }
        #endregion
    }
}
