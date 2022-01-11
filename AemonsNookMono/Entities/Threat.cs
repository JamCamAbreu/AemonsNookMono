using AemonsNookMono.Admin;
using AemonsNookMono.Entities.Tasks;
using AemonsNookMono.GameWorld;
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
        public int InterruptIntervalRandom { get; set; }
        public int InterruptTimer { get; set; }
        public int AttackReach { get; set; }
        public int AttackDelay { get; set; }
        #endregion

        #region Internal
        protected int attacktimer { get; set; }
        #endregion

        #region Constructor
        public Threat()
        {
            this.TileOn = null;
            this.Tasks = new Queue<Task>();

            this.InterruptInterval = 120;
            this.InterruptIntervalRandom = (int)((float)this.InterruptInterval * 0.4f);
            this.InterruptTimer = this.InterruptInterval;

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

            Task walkTowardsHero = new WalkTask(this, 30, TargetTile, true);
            this.Tasks.Enqueue(walkTowardsHero);
        }
        #endregion


        #region Interface
        public void Update()
        {
            if (this.CurrentTask == null)
            {
                if (Tasks.Count > 0)
                {
                    CurrentTask = Tasks.Dequeue();
                }
                // No more tasks:
                else
                {
                    //this.ReadyToExit = true;
                    this.TargetTile = World.Current.hero.TileOn;
                    Task walkTowardsHero = new WalkTask(this, 30, TargetTile, true);
                    this.Tasks.Enqueue(walkTowardsHero);
                }
            }

            if (this.CurrentTask != null)
            {
                this.InterruptTimer--;
                if (this.InterruptTimer <= 0)
                {
                    this.InterruptTimer = this.InterruptInterval + this.Ran.Next(-InterruptIntervalRandom, InterruptIntervalRandom);
                    if (this.TargetTile != World.Current.hero.TileOn)
                    {
                        this.CurrentTask = null;
                        this.TargetTile = World.Current.hero.TileOn;
                        Task walkTowardsHero = new WalkTask(this, 30, TargetTile, true);
                        this.Tasks.Enqueue(walkTowardsHero);
                    }
                }

                else
                {
                    this.CurrentTask.Update();
                    if (this.CurrentTask.Finished) { this.CurrentTask = null; }
                }
            }

            this.AttemptAttack();
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
                Global.ApproxDist(this.TileOn.RelativeX, this.TileOn.RelativeY, this.TargetTile.RelativeX, this.TargetTile.RelativeY) <= this.AttackReach)
            {
                if (this.attacktimer <= 0)
                {
                    Debugger.Current.AddTempString("Wham!");
                    this.attacktimer = this.AttackDelay;
                }
            }
            if (this.attacktimer > 0) { this.attacktimer--; }

        }
        #endregion
    }
}
