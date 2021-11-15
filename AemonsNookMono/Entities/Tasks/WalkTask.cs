using AemonsNookMono.Admin;
using AemonsNookMono.GameWorld;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Entities.Tasks
{
    public class WalkTask : Task
    {
        #region Constructor
        public WalkTask(Entity entity, Tile target) : base(entity)
        {
            this.WalkPath = new Path(entity.TileOn, target);
        }
        #endregion

        #region Public Properties
        public Path WalkPath { get; set; }
        public Tile NextTile { get; set; }
        public Vector2 Direction { get; set; }
        #endregion

        #region Interface
        public override void Draw()
        {
            #region DEBUG: DRAW PATH
            //foreach (Tile tile in this.Path)
            //{
            //    if (tile == this.NextTile)
            //    {
            //        Graphics.Current.SpriteB.Draw(Graphics.Current.SpritesByName["debug-tile-orange"], new Vector2(World.Current.StartDrawX + tile.RelativeX, World.Current.StartDrawY + tile.RelativeY), Color.White);
            //    }
            //    else if (tile == this.TargetTile)
            //    {
            //        Graphics.Current.SpriteB.Draw(Graphics.Current.SpritesByName["debug-tile-red"], new Vector2(World.Current.StartDrawX + tile.RelativeX, World.Current.StartDrawY + tile.RelativeY), Color.White);
            //    }
            //    else
            //    {
            //        Graphics.Current.SpriteB.Draw(Graphics.Current.SpritesByName["debug-tile-green"], new Vector2(World.Current.StartDrawX + tile.RelativeX, World.Current.StartDrawY + tile.RelativeY), Color.White);
            //    }
            //}
            #endregion
            base.Draw();
        }
        public override void Update()
        {
            this.Timer--;
            if (this.Timer <= 0)
            {
                this.StepPath();
                this.Timer = this.TimerLength;
            }
            if (this.NextTile != null)
            {
                float speed = 1.0f / this.Timer;
                Vector2 cur = new Vector2(this.Entity.CenterX, this.Entity.CenterY);
                Vector2 targ = new Vector2(World.Current.StartDrawX + this.NextTile.RelativeX, World.Current.StartDrawY + this.NextTile.RelativeY);
                this.Direction = targ - cur;
                int distance = Global.ApproxDist(cur, targ);
                if (distance >= 1)
                {
                    Vector2 updated = cur + (this.Direction * speed);
                    this.Entity.CenterX = (int)updated.X;
                    this.Entity.CenterY = (int)updated.Y;
                }
                if (this.Timer == 1 || distance < 1)
                {
                    this.Entity.CenterX = (int)targ.X;
                    this.Entity.CenterY = (int)targ.Y;
                    this.Entity.TileOn = this.NextTile;
                    this.NextTile = null;
                }
            }
        }
        #endregion

        #region Internal
        protected void StepPath()
        {
            if (this.WalkPath.TileStack != null)
            {
                if (this.WalkPath.TileStack.Count > 0)
                {
                    this.NextTile = this.WalkPath.NextTile;
                    Vector2 cur = new Vector2(this.Entity.CenterX, this.Entity.CenterY);
                    Vector2 targ = new Vector2(World.Current.StartDrawX + this.NextTile.RelativeX, World.Current.StartDrawY + this.NextTile.RelativeY);
                    this.Direction = targ - cur;
                }
                else if (this.WalkPath.TileStack.Count == 0)
                {
                    this.Finished = true;
                    this.NextTile = null;
                }
            }
        }
        #endregion
    }
}
