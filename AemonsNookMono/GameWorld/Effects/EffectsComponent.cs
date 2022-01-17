using System;
using System.Collections.Generic;
using System.Linq;

namespace AemonsNookMono.GameWorld.Effects
{
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
        public List<TempEffect> Effects { get; set; }
        #endregion

        #region Interface
        public virtual void GenerateEffect()
        {

        }
        public void Update()
        {
            this.effectTimer--;
            if (effectTimer <= 0 && this.Effects.Count < this.maxNumberEffects)
            {
                this.GenerateEffect();
                this.resetTimer();
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
