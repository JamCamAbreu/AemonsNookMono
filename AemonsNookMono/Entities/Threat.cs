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
    public class Threat : Humanoid
    {
        #region Public Properties
        #endregion

        #region Constructor
        public Threat()
        {
            this.TileOn = null;
            this.Tasks = new Stack<Task>();
            this.TotalTaskCapacity = 8; // make this configurable later (how much can they remember? Interrupts can cause things to stack up.)

            if (World.Current.SpawnTiles == null || World.Current.SpawnTiles.Count <= 0) { throw new Exception("No where to spawn! Oh my!"); }
            if (World.Current.RoadTiles == null || World.Current.RoadTiles.Count <= 0) { throw new Exception("No where to go! Oh my!"); }

            this.Ran = new Random();
            this.TileOn = World.Current.SpawnTiles[Ran.Next(0, World.Current.SpawnTiles.Count - 1)];
            this.CenterX = World.Current.StartDrawX + this.TileOn.RelativeX;
            this.CenterY = World.Current.StartDrawY + this.TileOn.RelativeY;

            this.EntranceTile = this.TileOn;
            this.ExitTile = World.Current.RetrieveRandomExit(this.EntranceTile);
            this.ReadyToExit = false;

            Task seekOutHero = new SeekAndDestroy(this, World.Current.hero, 30);
            this.Tasks.Push(seekOutHero);
        }
        #endregion


        #region Interface
        public override void Update()
        {
            bool interrupt = this.UpdatePosition();
            if (interrupt)
            {
                return;
            }

            if (this.CurrentTask == null)
            {
                if (Tasks.Count > 0)
                {
                    CurrentTask = Tasks.Pop();
                }
                else
                {
                    if (this.WanderEndlessly)
                    {
                        Tile target = World.Current.RoadTiles[Ran.Next(0, World.Current.RoadTiles.Count - 1)];
                        Task task = new WalkTask(this, DEFAULT_WALK_SPEED, target);
                        this.Tasks.Push(task);
                    }
                    else
                    {
                        this.ReadyToExit = true;
                    }
                }
            }

            if (this.CurrentTask != null)
            {
                this.CurrentTask.Update();
                if (this.CurrentTask.Finished) { this.CurrentTask = null; }
            }

            base.Update();
        }
        public void Draw()
        {
            Graphics.Current.SpriteB.Draw(Graphics.Current.SpritesByName["threats-head"], new Vector2(this.CenterX, this.CenterY), Color.White);
            if (this.CurrentTask != null)
            {
                this.CurrentTask.Draw();
            }
        }
        #endregion
    }
}
