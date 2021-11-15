using AemonsNookMono.Entities.Tasks;
using AemonsNookMono.GameWorld;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Entities
{
    public class Entity
    {
        #region Public Properties
        public int CenterX { get; set; }
        public int CenterY { get; set; }
        public Tile TileOn { get; set; }
        public Tile EntranceTile { get; set; }
        public Tile ExitTile { get; set; }
        public bool ReadyToExit { get; set; }
        public int Health { get; set; } 

        protected Queue<Task> Tasks { get; set; }
        protected Task CurrentTask { get; set; }
        protected Random Ran { get; set; }
        #endregion
    }
}
