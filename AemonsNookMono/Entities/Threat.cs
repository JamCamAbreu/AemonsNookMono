using AemonsNookMono.Admin;
using AemonsNookMono.Entities.Tasks;
using AemonsNookMono.GameWorld;
using AemonsNookMono.GameWorld.Effects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Entities
{
    public class Threat : Entity
    {
        #region Public Properties
        public Tile TargetTile { get; set; }
        public int InterruptInterval { get; set; }
        public int AttackReach { get; set; }
        public int AttackDelay { get; set; }
        #endregion

        #region Internal
        protected int attacktimer { get; set; }
        protected int interrupttimer { get; set; }
        protected int interruptintervalrandom { get; set; }
        #endregion

        #region Constructor
        public Threat()
        {
            this.TileOn = null;
            this.Tasks = new Stack<Task>();
            this.TotalTaskCapacity = 8; // make this configurable later (how much can they remember? Interrupts can cause things to stack up.)

            this.InterruptInterval = 80; // make this configurable later
            this.interruptintervalrandom = (int)((float)this.InterruptInterval * 0.25f);
            this.interrupttimer = this.InterruptInterval;

            this.AttackReach = 20; // make this configurable later
            this.AttackDelay = 100; // make this configurable later
            this.attacktimer = 0;

            if (World.Current.SpawnTiles == null || World.Current.SpawnTiles.Count <= 0) { throw new Exception("No where to spawn! Oh my!"); }
            if (World.Current.RoadTiles == null || World.Current.RoadTiles.Count <= 0) { throw new Exception("No where to go! Oh my!"); }

            this.Ran = new Random();
            this.TileOn = World.Current.SpawnTiles[Ran.Next(0, World.Current.SpawnTiles.Count - 1)];
            this.CenterX = World.Current.StartDrawX + this.TileOn.RelativeX;
            this.CenterY = World.Current.StartDrawY + this.TileOn.RelativeY;

            this.EntranceTile = this.TileOn;
            this.ExitTile = World.Current.RetrieveRandomExit(this.EntranceTile);
            this.ReadyToExit = false;

            this.TargetTile = World.Current.hero.TileOn;

            Task walkTowardsHero = new WalkTask(this, 30, TargetTile, true); // replace this with Seek and Destroy
            this.Tasks.Push(walkTowardsHero);
        }
        #endregion


        #region Interface
        public void AddTask(Task task)
        {

        }
        public void Update()
        {
            this.interrupttimer--;
            if (this.CurrentTask == null)
            {
                if (Tasks.Count > 0)
                {
                    CurrentTask = Tasks.Pop();
                }
                // No more tasks:
                else
                {
                    if (this.interrupttimer <= 0)
                    {
                        //this.ReadyToExit = true;
                        this.TargetTile = World.Current.hero.TileOn;
                        Task walkTowardsHero = new WalkTask(this, 30, TargetTile, true);
                        this.Tasks.Push(walkTowardsHero);
                    }
                }
            }

            if (this.CurrentTask != null)
            {
                if (this.interrupttimer <= 0)
                {
                    ResetInterruptTimer();
                    if (this.TargetTile != World.Current.hero.TileOn)
                    {
                        this.CurrentTask = null;
                        this.TargetTile = World.Current.hero.TileOn;
                        Task walkTowardsHero = new WalkTask(this, 30, TargetTile, true);
                        this.Tasks.Push(walkTowardsHero);
                    }
                }

                else
                {
                    this.CurrentTask.Update();
                    if (this.CurrentTask.Finished) { this.CurrentTask = null; }
                }
            }

            this.AttemptAttack();

            if (this.TargetTile != null)
            {
                Debugger.Current.Debugger1 = $"{this.TargetTile.Column}, {this.TargetTile.Row}";
            }
            Debugger.Current.Debugger2 = $"{this.interrupttimer}";
        }
        public void Draw()
        {
            Graphics.Current.SpriteB.Draw(Graphics.Current.SpritesByName["threats-head"], new Vector2(this.CenterX, this.CenterY), Color.White);
            if (this.CurrentTask != null)
            {
                this.CurrentTask.Draw();
            }
        }
        public void AttemptAttack()
        {
            if (this.TileOn != null && this.TargetTile != null &&
                this.attacktimer <= 0 &&
                this.TargetTile == World.Current.hero.TileOn &&
                Global.ApproxDist(this.TileOn.RelativeX, this.TileOn.RelativeY, this.TargetTile.RelativeX, this.TargetTile.RelativeY) <= this.AttackReach)
            {
                Debugger.Current.AddTempString("Wham!");
                EffectsGenerator.Current.AddSingleEffect(new TempEffect(this.TileOn.RelativeX + 8, this.TileOn.RelativeY, 10, 2, "SwordSwing", 5));
                ResetInterruptTimer(40);
                ResetAttackTimer();
            }
            if (this.attacktimer > 0) { this.attacktimer--; }
        }

        public void ResetInterruptTimer(int additionalFramesDelay = 0)
        {
            this.interrupttimer = this.InterruptInterval + this.Ran.Next(-interruptintervalrandom, interruptintervalrandom) + additionalFramesDelay;
        }
        public void ResetAttackTimer(int additionalFramesDelay = 0)
        {
            this.attacktimer = this.AttackDelay + additionalFramesDelay;
        }
        #endregion
    }
}
