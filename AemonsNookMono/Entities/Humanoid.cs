using AemonsNookMono.Admin;
using AemonsNookMono.Entities.Tasks;
using AemonsNookMono.GameWorld;
using AemonsNookMono.GameWorld.Effects;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Entities
{
    public class Humanoid : Entity
    {
        public const int DEFAULT_WALK_SPEED = 40;
        public Humanoid()
        {
            this.AttackReach = 25; // make this configurable later
            this.AttackDelay = 100; // make this configurable later
            this.attacktimer = 0;
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
        public bool WanderEndlessly { get; set; }
        public Random Ran { get; set; }

        protected Stack<Task> Tasks { get; set; }
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

            if (this.attacktimer > 0) { this.attacktimer--; }
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
