using AemonsNookMono.Admin;
using AemonsNookMono.GameWorld;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Peeps
{
    public class Task
    {
        #region Constructor
        public Task(Peep peep)
        {
            Random ran = new Random();
            this.TimerLength = ran.Next(20, 100);
            this.Timer = this.TimerLength;
            this.Finished = false;
            this.MyPeep = peep;
            this.Path = new Stack<Tile>();
        }
        #endregion

        #region Public Properties
        public Peep MyPeep { get; set; }
        public int TimerLength { get; set; }
        public int Timer { get; set; }
        public bool Finished { get; set; }
        #endregion

        #region Find Path Implementation
        public Stack<Tile> Path { get; set; }
        public Tile NextTile { get; set; }
        public Tile TargetTile { get; set; }
        public Vector2 Direction { get; set; }
        #endregion

        #region Interface
        // TODO: Move this to the Tile class such that it takes in a start and end tile, and outputs the stack of tiles (with configuration for tile type specifity) Call it FindPath or something. 
        public void CreateWalkTask(Tile target)
        {
            this.TargetTile = target;
            if (this.MyPeep == null || this.MyPeep.TileOn == null) { throw new Exception("Can't calculate path without a starting location!"); }
            Tile startTile = this.MyPeep.TileOn;
            Dictionary<Tile, Tile> Visited = new Dictionary<Tile, Tile>(); // To, From
            Queue<Tuple<Tile, Tile>> Neighbors = new Queue<Tuple<Tile, Tile>>(); // From, To
            Neighbors.Enqueue(new Tuple<Tile, Tile>(null, startTile));
            Tuple<Tile, Tile> curPair;
            Tile cur;
            Tile from = null;
            while (Neighbors.Count > 0)
            {
                curPair = Neighbors.Dequeue();
                from = curPair.Item1;
                cur = curPair.Item2;
                if (cur == target)
                {
                    while (cur != startTile)
                    {
                        Path.Push(cur);
                        cur = Visited[cur]; // prev
                    }
                    return;
                }
                
                if (cur.TileAbove != null && cur.TileAbove.IsPath && !Visited.ContainsKey(cur.TileAbove)) { Neighbors.Enqueue(new Tuple<Tile,Tile>(cur, cur.TileAbove)); Visited.Add(cur.TileAbove, cur); }
                if (cur.TileRight != null && cur.TileRight.IsPath && !Visited.ContainsKey(cur.TileRight)) { Neighbors.Enqueue(new Tuple<Tile, Tile>(cur, cur.TileRight)); Visited.Add(cur.TileRight, cur); }
                if (cur.TileBelow != null && cur.TileBelow.IsPath && !Visited.ContainsKey(cur.TileBelow)) { Neighbors.Enqueue(new Tuple<Tile, Tile>(cur, cur.TileBelow)); Visited.Add(cur.TileBelow, cur); }
                if (cur.TileLeft != null && cur.TileLeft.IsPath && !Visited.ContainsKey(cur.TileLeft)) { Neighbors.Enqueue(new Tuple<Tile, Tile>(cur, cur.TileLeft)); Visited.Add(cur.TileLeft, cur); }
            }
        }
        public void Draw()
        {
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
        }
        public void Update()
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
                Vector2 cur = new Vector2(this.MyPeep.CenterX, this.MyPeep.CenterY);
                Vector2 targ = new Vector2(World.Current.StartDrawX + this.NextTile.RelativeX, World.Current.StartDrawY + this.NextTile.RelativeY);
                this.Direction = targ - cur;
                int distance = Global.ApproxDist(cur, targ);
                if (distance >= 1)
                {
                    Vector2 updated = cur + (this.Direction * speed);
                    this.MyPeep.CenterX = (int)updated.X;
                    this.MyPeep.CenterY = (int)updated.Y;
                }
                if (this.Timer == 1 || distance < 1)
                {
                    this.MyPeep.CenterX = (int)targ.X;
                    this.MyPeep.CenterY = (int)targ.Y;
                    this.MyPeep.TileOn = this.NextTile;
                    this.NextTile = null;
                }
            }
        }
        public void StepPath()
        {
            if (this.Path != null) 
            {
                if (this.Path.Count > 0)
                {
                    this.NextTile = this.Path.Pop();
                    Vector2 cur = new Vector2(this.MyPeep.CenterX, this.MyPeep.CenterY);
                    Vector2 targ = new Vector2(World.Current.StartDrawX + this.NextTile.RelativeX, World.Current.StartDrawY + this.NextTile.RelativeY);
                    this.Direction = targ - cur;
                }
                else if (this.Path.Count == 0)
                {
                    this.Finished = true;
                    this.NextTile = null;
                }
            }
        }
        #endregion
    }
}
