using AemonsNookMono.GameWorld;
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
            this.TimerLength = 20;
            this.Timer = 20;
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
        #endregion

        #region Interface
        public void CreateWalkTask(Tile target)
        {
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
        public void Update()
        {
            this.Timer--;
            if (this.Timer <= 0)
            {
                this.StepPath();
                this.Timer = this.TimerLength;
            }
        }
        public void StepPath()
        {
            if (this.Path != null) 
            {
                if (this.Path.Count > 0)
                {
                    Tile updateTile = this.Path.Pop();
                    this.MyPeep.TileOn = updateTile;
                    this.MyPeep.CenterX = World.Current.StartDrawX + updateTile.RelativeX;
                    this.MyPeep.CenterY = World.Current.StartDrawY + updateTile.RelativeY;
                }

                if (this.Path.Count == 0)
                {
                    this.Finished = true;
                }
            }
        }
        #endregion
    }
}
