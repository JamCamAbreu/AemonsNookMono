using AemonsNookMono.GameWorld;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Entities.Tasks
{
    public class SeekAndDestroy : Task
    {
        #region Constructor
        public SeekAndDestroy(Humanoid entity, Humanoid targetEntity, int updateinterval) : base(entity, updateinterval)
        {
            this.TargetEntity = targetEntity;
            this.ChildrenTasks = new Stack<Task>();
            this.ResetInterruptTimer(40);
        }
        #endregion

        #region Public Properties
        public Humanoid TargetEntity { get; set; }
        public Tile TargetTile { get; set; }
        public int InterruptInterval { get; set; }
        #endregion

        protected int interrupttimer { get; set; }
        protected int interruptintervalrandom { get; set; }

        #region Interface
        public override void Update()
        {
            if (this.CurrentChildTask == null && this.ChildrenTasks.Count <= 0)
            {
                if (this.TargetEntity != null)
                {
                    if (Admin.Global.ApproxDist(
                        new Vector2(this.Entity.CenterX, this.Entity.CenterY), 
                        new Vector2(this.TargetEntity.CenterX, this.TargetEntity.CenterY)
                        ) <= this.Entity.AttackReach)
                    {
                        if (this.Entity.CanAttack())
                        {
                            this.Entity.Attack(this.TargetEntity);
                            this.ResetInterruptTimer(40);
                        }
                    }
                    else
                    {
                        if (this.TargetTile != World.Current.hero.TileOn)
                        {
                            this.TargetTile = World.Current.hero.TileOn;
                            Task walkTowardsHero = new WalkTask(this.Entity, 30, this.TargetTile, true);
                            this.ChildrenTasks.Push(walkTowardsHero);
                        }
                    }
                }
            }

            this.interrupttimer--;
            if (this.CurrentChildTask != null)
            {
                if (this.interrupttimer <= 0)
                {
                    this.ResetInterruptTimer();
                    if (this.TargetTile != World.Current.hero.TileOn)
                    {
                        this.CurrentChildTask = null;
                        this.TargetTile = World.Current.hero.TileOn;
                        Task walkTowardsHero = new WalkTask(this.Entity, 30, TargetTile, true);
                        this.ChildrenTasks.Push(walkTowardsHero);
                    }
                }

                else
                {
                    this.CurrentChildTask.Update();
                    if (this.CurrentChildTask.Finished) { this.CurrentChildTask = null; }
                }
            }
            this.UpdateChildrenTasks();
        }
        #endregion

        public void ResetInterruptTimer(int additionalFramesDelay = 0)
        {
            this.interrupttimer = this.InterruptInterval + this.Entity.Ran.Next(-interruptintervalrandom, interruptintervalrandom) + additionalFramesDelay;
        }

    }
}
