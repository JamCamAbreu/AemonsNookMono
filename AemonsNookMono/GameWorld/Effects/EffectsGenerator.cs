using AemonsNookMono.Admin;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
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
        }
        public void Update()
        {
            if (this.waterSparkles.Triggered) 
            {
                Tuple<int, int> coord = this.GetRandomTileCoords(World.Current.WaterTiles);
                this.waterSparkles.AddEffect(new WaterSparkle(coord.Item1, coord.Item2, 160, 30));
            }
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
            count += this.waterSparkles.Effects.Count;
            return count;
        }
        #endregion

        #region Helper Methods
        private Tuple<int, int> GetRandomTileCoords(List<Tile> tiles)
        {
            if (tiles != null && tiles.Count > 0)
            {
                int r = this.ran.Next(0, tiles.Count - 1);
                int pad = World.TILE_DIMENSION_PIXELS / 4;
                int tilex = this.ran.Next(pad, World.TILE_DIMENSION_PIXELS - pad);
                int tiley = this.ran.Next(pad, World.TILE_DIMENSION_PIXELS - pad);
                return new Tuple<int, int>(tiles[r].RelativeX + tilex, tiles[r].RelativeY + tiley);
            }
            return null;
        }
        #endregion

        #region Internal
        private Random ran { get; set; }
        private EffectsComponent waterSparkles { get; set; }
        #endregion
    }

    public class EffectsComponent
    {
        #region Constructor
        public EffectsComponent(int maxEffects, int resetBase)
        {
            this.Effects = new List<TempEffect>();
            this.ran = new Random();
            this.maxNumberEffects = maxEffects;
            this.effectTimerReset = resetBase;
            this.resetTimer();
        }
        #endregion

        #region public Properties
        public bool Triggered { get; set; }
        public List<TempEffect> Effects { get; set; }
        #endregion

        #region Interface
        public void Update()
        {
            if (!Triggered)
            {
                effectTimer--;
                if (effectTimer <= 0 && this.Effects.Count < this.maxNumberEffects)
                {
                    this.Triggered = true;
                }
            }

            foreach (TempEffect effect in this.Effects)
            {
                effect.Update();
            }

            TempEffect[] destroyeffects = this.Effects.Where(e => e.Dead == true).ToArray();
            foreach (TempEffect effect in destroyeffects)
            {
                this.Effects.Remove(effect);
            }
        }
        public void AddEffect(TempEffect effect)
        {
            this.Effects.Add(effect);
            this.Triggered = false;
            this.resetTimer();
        }
        public void Draw()
        {
            foreach (TempEffect effect in this.Effects)
            {
                effect.Draw();
            }
        }
        #endregion

        #region Helper Methods
        private void resetTimer()
        {
            int random = ran.Next(-(effectTimerReset / 4), effectTimerReset / 4);
            this.effectTimer = effectTimerReset + random;
        }
        #endregion

        #region Internal
        private Random ran { get; set; }
        private int maxNumberEffects { get; set; }
        private int effectTimer { get; set; }
        private int effectTimerReset { get; set; }
        #endregion
    }
}
