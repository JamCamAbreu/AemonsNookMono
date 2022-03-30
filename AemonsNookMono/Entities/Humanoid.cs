using AemonsNookMono.Admin;
using AemonsNookMono.Entities.Tasks;
using AemonsNookMono.GameWorld;
using AemonsNookMono.GameWorld.Effects;
using AemonsNookMono.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Entities
{
    public class Humanoid : Entity
    {
        public enum InterruptStateReset
        {
            CannotInterrupt = -1,
            EveryStep = 1,
            ExtremelyAlarmed = 4,
            Alarmed = 8,
            Watchful = 16,
            Normal = 32,
            SlightlyDistracted = 64,
            Distracted = 128,
            Drunk = 256,
            HardToInterract = 512,
        }

        public const int DEFAULT_WALK_SPEED = 40;
        public Humanoid()
        {
            this.AttackReach = 25; // make this configurable later
            this.AttackDelay = 100; // make this configurable later
            this.attacktimer = 0;
            this.InterruptTimer = (int)InterruptStateReset.Normal;
        }

        #region Internal
        protected int attacktimer { get; set; }
        #endregion

        #region Public Properties
        public Tile TileOn { get; set; }
        public Tile EntranceTile { get; set; }
        public Tile ExitTile { get; set; }
        public bool ReadyToExit { get; set; }
        public int Health { get; set; } 
        public int TotalTaskCapacity { get; set; } // how much can they remember?
        public int AttackReach { get; set; }
        public int AttackDelay { get; set; }
        public int InterruptTimer { get; set; }
        public bool WanderEndlessly { get; set; }
        public Random Ran { get; set; }

        public Stack<Task> Tasks { get; set; }
        protected Task CurrentTask { get; set; }
        #endregion

        public void Attack(Entity target)
        {
            Debugger.Current.AddTempString("Wham!");
            EffectsGenerator.Current.AddSingleEffect(new TempEffect(this.TileOn.RelativeX + 8, this.TileOn.RelativeY, 10, 2, "SwordSwing", 5));

            int strength = 4;
            int xImpact = this.CenterX < target.CenterX ? strength : -strength;
            int yImpact = this.CenterY < target.CenterY ? strength : -strength;

            target.Impact(xImpact, yImpact);
            this.Impact(-xImpact/2, -yImpact/2);

            ResetAttackTimer();
        }
        public void Attack(Resource target)
        {
            Debugger.Current.AddTempString("Chop!");
            EffectsGenerator.Current.AddSingleEffect(new TempEffect(this.TileOn.RelativeX + 8, this.TileOn.RelativeY, 10, 2, "SwordSwing", 5));

            target.AttackResource();

            ResetAttackTimer();
        }
        public virtual void Update()
        {
            this.attacktimer--;
        }

        public void ResetAttackTimer(int additionalFramesDelay = 0)
        {
            this.attacktimer = this.AttackDelay + additionalFramesDelay;
        }
        public bool CanAttack()
        {
            return this.attacktimer <= 0;
        }
    }
}
