using AemonsNookMono.Admin;
using AemonsNookMono.Entities.Tasks;
using AemonsNookMono.GameWorld;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Entities
{
    public class Peep : Humanoid
    {

        #region Constructor
        public Peep()
        {
            this.TileOn = null;
            this.Tasks = new Stack<Task>();

            if (World.Current.SpawnTiles == null || World.Current.SpawnTiles.Count <= 0) { throw new Exception("No where to spawn! Oh my!"); }
            if (World.Current.RoadTiles == null || World.Current.RoadTiles.Count <= 0) { throw new Exception("No where to go! Oh my!"); }

            this.Ran = new Random();
            this.TileOn = World.Current.SpawnTiles[Ran.Next(0, World.Current.SpawnTiles.Count - 1)];
            this.CenterX = World.Current.StartDrawX + this.TileOn.RelativeX;
            this.CenterY = World.Current.StartDrawY + this.TileOn.RelativeY;

            this.EntranceTile = this.TileOn;
            this.ExitTile = World.Current.RetrieveRandomExit(this.EntranceTile);
            this.ReadyToExit = false;

            // WANDER:
            //this.WanderEndlessly = true;
            //Tile target = World.Current.RoadTiles[Ran.Next(0, World.Current.RoadTiles.Count - 1)];

            // Go straight to the other exit tile, if it exists:
            this.WanderEndlessly = true;
            Tile target = this.ExitTile;

            //Task walktask = new WalkTask(this, DEFAULT_WALK_SPEED, target);
            Task walktask = new WalkTask(this, Ran.Next(DEFAULT_WALK_SPEED - 15, DEFAULT_WALK_SPEED + 15), target);
            this.Tasks.Push(walktask);
        }
        #endregion


        #region Interface
        public override void Update()
        {
            base.Update();
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
                        Task task = new WalkTask(this, DEFAULT_WALK_SPEED, target, !this.TileOn.IsRoad);
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
        }
        public void Draw()
        {
            Graphics.Current.SpriteB.Draw(Graphics.Current.SpritesByName["peep-royal"], new Vector2(this.CenterX, this.CenterY), Color.White);
            if (this.CurrentTask != null)
            {
                this.CurrentTask.Draw();
            }
        }
        #endregion
    }
}
