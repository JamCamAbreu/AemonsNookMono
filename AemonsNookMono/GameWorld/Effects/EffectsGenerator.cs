using AemonsNookMono.Admin;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.GameWorld.Effects
{
    public class EffectsGenerator
    {
        #region Singleton Implementation
        private static EffectsGenerator instance;
        private static object _lock = new object();
        private EffectsGenerator() { }
        public static EffectsGenerator Current
        {
            get
            {
                if (instance == null)
                {
                    lock (_lock)
                    {
                        if (instance == null)
                        {
                            instance = new EffectsGenerator();
                        }
                    }
                }
                return instance;
            }
        }
        #endregion

        #region Public Properties
        public List<EffectsComponent> Components { get; set; }
        public List<TempEffect> SingleEffects { get; set; }
        #endregion

        #region Interface
        public void Init()
        {
            ran = new Random();
            this.Components = new List<EffectsComponent>();
            this.SingleEffects = new List<TempEffect>();

            int maxWaterEffects = World.Current.WaterTiles.Count * 4;
<<<<<<< Updated upstream
            this.waterSparkles = new EffectsComponent(maxWaterEffects, 16);
            this.AddEffectsComponent(this.waterSparkles);
        }
        public void AddEffectsComponent(EffectsComponent component)
        {
            this.Components.Add(component);
        }
        public void AddSingleEffect(TempEffect effect)
        {
            this.SingleEffects.Add(effect);
=======
            if (maxWaterEffects > 0)
            {
                var waterSparkles = new WaterSparkle(maxWaterEffects, 16);
                this.Components.Add(waterSparkles);
            }

            // Rain effect would be fun!
            // Wind
            // Birds
>>>>>>> Stashed changes
        }
        public void Update()
        {
            foreach (EffectsComponent component in Components)
            {
                component.Update();
            }
<<<<<<< Updated upstream
            this.waterSparkles.Update();

            foreach (TempEffect effect in this.SingleEffects)
            {
                effect.Update();
            }

            TempEffect[] destroyeffects = this.SingleEffects.Where(e => e.Dead == true).ToArray();
            foreach (TempEffect effect in destroyeffects)
            {
                this.SingleEffects.Remove(effect);
            }
=======
>>>>>>> Stashed changes
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

            if (this.Components != null && this.Components.Count > 0)
            {
                foreach (EffectsComponent component in this.Components)
                {
                    component.Draw();
                }
            }

            foreach (TempEffect effect in this.SingleEffects)
            {
                effect.Draw();
            }

            Graphics.Current.SpriteB.End();
        }
        public int CountTotalEffects()
        {
            int count = 0;
            foreach (EffectsComponent component in Components)
            {
                count += component.Effects.Count;
            }
            return count;
        }
        #endregion

        #region Internal
        private Random ran { get; set; }
        #endregion
    }
}
