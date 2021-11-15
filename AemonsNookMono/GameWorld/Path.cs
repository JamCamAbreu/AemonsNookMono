using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.GameWorld
{
    public class Path
    {
        public int Count
        {
            get
            {
                if (this.TileStack == null) { return 0; }
                else { return this.TileStack.Count; }
            }
        }

        #region Find Path Implementation
        public Stack<Tile> TileStack { get; set; }
        public Tile TargetTile { get; set; }
        public Tile StartTile { get; set; }
        public Tile NextTile
        {
            get
            {
                if (this.TileStack == null || this.TileStack.Count == 0) { return null; }
                else
                {
                    return this.TileStack.Pop();
                }
            }
        }
        #endregion

        #region Constructor
        public Path(Tile starttile, Tile targettile, bool offroad = false)
        {
            this.StartTile = starttile;
            this.TargetTile = targettile;
            this.TileStack = new Stack<Tile>();
            
            Dictionary<Tile, Tile> Visited = new Dictionary<Tile, Tile>(); // To, From
            Queue<Tuple<Tile, Tile>> Neighbors = new Queue<Tuple<Tile, Tile>>(); // From, To
            Neighbors.Enqueue(new Tuple<Tile, Tile>(null, StartTile));
            Tuple<Tile, Tile> curPair;
            Tile cur;
            //Tile from;
            while (Neighbors.Count > 0)
            {
                curPair = Neighbors.Dequeue();
                //from = curPair.Item1;
                cur = curPair.Item2;
                if (cur == targettile)
                {
                    while (cur != StartTile)
                    {
                        TileStack.Push(cur);
                        cur = Visited[cur]; // prev
                    }
                    return;
                }

                if (cur.TileAbove != null && (cur.TileAbove.IsPath || offroad) && !Visited.ContainsKey(cur.TileAbove)) { Neighbors.Enqueue(new Tuple<Tile, Tile>(cur, cur.TileAbove)); Visited.Add(cur.TileAbove, cur); }
                if (cur.TileRight != null && (cur.TileRight.IsPath || offroad) && !Visited.ContainsKey(cur.TileRight)) { Neighbors.Enqueue(new Tuple<Tile, Tile>(cur, cur.TileRight)); Visited.Add(cur.TileRight, cur); }
                if (cur.TileBelow != null && (cur.TileBelow.IsPath || offroad) && !Visited.ContainsKey(cur.TileBelow)) { Neighbors.Enqueue(new Tuple<Tile, Tile>(cur, cur.TileBelow)); Visited.Add(cur.TileBelow, cur); }
                if (cur.TileLeft != null  && (cur.TileLeft.IsPath  || offroad) && !Visited.ContainsKey(cur.TileLeft))  { Neighbors.Enqueue(new Tuple<Tile, Tile>(cur, cur.TileLeft));  Visited.Add(cur.TileLeft,  cur); }
            }
        }
        #endregion
    }
}
