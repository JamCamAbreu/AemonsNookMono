using AemonsNookMono.Admin;
using AemonsNookMono.GameWorld;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Peeps
{
    public class Peep
    {
        #region Constructor
        public Peep()
        {
            this.TileOn = null;
            this.Tasks = new Queue<Task>();

            if (World.Current.SpawnTiles == null || World.Current.SpawnTiles.Count <= 0) { throw new Exception("No where to spawn! Oh my!"); }
            if (World.Current.RoadTiles == null || World.Current.RoadTiles.Count <= 0) { throw new Exception("No where to go! Oh my!"); }

            this.Ran = new Random();
            this.TileOn = World.Current.SpawnTiles[Ran.Next(0, World.Current.SpawnTiles.Count - 1)];
            this.CenterX = World.Current.StartDrawX + this.TileOn.RelativeX;
            this.CenterY = World.Current.StartDrawY + this.TileOn.RelativeY;

            this.EntranceTile = this.TileOn;
            this.ExitTile = World.Current.RetrieveRandomExit(this.EntranceTile);
            this.ReadyToExit = false;

            // Random tile:
            //Tile target = World.Current.RoadTiles[Ran.Next(0, World.Current.RoadTiles.Count - 1)];

            // Go straight to the other exit tile, if it exists:
            Tile target = this.ExitTile;

            Task task = new Task(this);
            task.CreateWalkTask(target);
            this.Tasks.Enqueue(task);
        }
        #endregion

        #region Public Properties
        public int CenterX { get; set; }
        public int CenterY { get; set; }
        public Tile TileOn { get; set; }
        public Tile EntranceTile { get; set; }
        public Tile ExitTile { get; set; }
        public bool ReadyToExit { get; set; }
        Queue<Task> Tasks { get; set; }
        Task CurrentTask { get; set; }
        Random Ran { get; set; }
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
                else
                {
                    // Walk endlessly:
                    Tile target = World.Current.RoadTiles[Ran.Next(0, World.Current.RoadTiles.Count - 1)];
                    Task task = new Task(this);
                    task.CreateWalkTask(target);
                    this.Tasks.Enqueue(task);

                    // Leave:
                    this.ReadyToExit = true;
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
