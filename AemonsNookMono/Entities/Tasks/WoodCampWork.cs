using AemonsNookMono.Admin;
using AemonsNookMono.GameWorld;
using AemonsNookMono.Resources;
using AemonsNookMono.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AemonsNookMono.Entities.Tasks
{
    public enum State
    {
        FindTree,
        WalkToTree,
        ChopTree,
        ScanWood,
        ReturnToCamp
    }
    public enum TreeScanType
    {
        FromTargetCamp,
        FromMe
    }
    public class WoodCampWork : Task
    {
        public List<Resource> Inventory { get; set; }
        public int LoadSize { get; set; }
        public Tree TargetTree { get; set; }
        public Building TargetCamp { get; set; }

        public WoodCampWork(Humanoid entity, int loadSize, int updateinterval) : base(entity, updateinterval)
        {
            Admin.Debugger.Current.AddTempString("New WoodCampWork Task");

            this.Inventory = new List<Resource>();
            this.LoadSize = loadSize;
            this.TargetCamp = this.RetrieveClosestCamp();
            this.TargetTree = this.RetrieveClosestTree(TreeScanType.FromTargetCamp);
            // append a walk child task to walk to that tree
            // append a tree chopping task with that tree (takes in tree for it's constructor)
            // 
        }

        public override void Update()
        {
            base.Update();

            //string posX = this.TargetTree?.TargetPosition.X.ToString() ?? "";
            //string posY = this.TargetTree?.TargetPosition.Y.ToString() ?? "";
            //Admin.Debugger.Current.Debugger1 = $"Target Tree Coords: {posX}, {posY}";

            if (this.Active)
            {
                if (this.Inventory.Count < this.LoadSize)
                {
                    if (this.TargetTree == null)
                    {
                        if (this.Inventory.Count == 0)
                        {
                            this.TargetCamp = this.RetrieveClosestCamp();
                            this.TargetTree = this.RetrieveClosestTree(TreeScanType.FromTargetCamp);
                        }
                        else
                        {
                            this.TargetTree = this.RetrieveClosestTree(TreeScanType.FromMe);
                        }
                    }

                    if (this.TargetTree != null && this.TargetTree.State != Resource.ResourceState.Raw)
                    {
                        this.TargetTree = null;
                        this.TargetTree.CurrentHarvesters.Remove(this.Entity);
                    }

                    if (this.TargetTree != null)
                    {
                        int dist = Global.ApproxDist((int)this.Entity.CenterX, (int)this.Entity.CenterY, (int)this.TargetTree.TargetPosition.X, (int)this.TargetTree.TargetPosition.Y);
                        if (dist > this.Entity.AttackReach && this.Entity.TileOn != this.TargetTree.TileOn)
                        {
                            this.ChildrenTasks.Push(new WalkTask(this.Entity, this.UpdateInterval, this.TargetTree.TileOn, true));
                        }
                        else
                        {
                            if (this.TargetTree.State != Resource.ResourceState.Harvestable)
                            {
                                if (this.Entity.CanAttack())
                                {
                                    this.Entity.Attack(this.TargetTree);
                                }
                            }
                            if (this.TargetTree.State == Resource.ResourceState.Harvestable)
                            {
                                this.Inventory.Add(this.TargetTree);
                                this.TargetTree.CurrentHarvesters.Remove(this.Entity);
                                this.TargetTree.Destroy();
                                this.TargetTree = null;
                            }
                        }
                    }
                }
                else
                {
                    this.TargetCamp = this.RetrieveClosestCamp();

                    // Todo: Update to use building entrance tileon instead of index 0? 
                    if (this.Entity.TileOn != this.TargetCamp.TilesUnderneath[0])
                    {
                        Admin.Debugger.Current.AddTempString("Inventory full!");
                        Admin.Debugger.Current.AddTempString("Walking to camp.");
                        this.ChildrenTasks.Push(new WalkTask(this.Entity, this.UpdateInterval, this.TargetCamp.TilesUnderneath[0], true));
                    }
                    else
                    {
                        
                        Admin.Debugger.Current.AddTempString("Arrived at camp.");
                        Admin.Debugger.Current.AddTempString("Clearing inventory for debugging...");
                        this.Inventory.Clear();
                    }
                }

                // Check if inventory is full,
                // if so append going to homecamp as task
                // if not, see if there's another tree close to the peep and append walk to tree task and tree chop task

                // start over, do items in constructor
            }
        }

        public Building RetrieveClosestCamp()
        {
            List<Building> camps = BuildingManager.Current.AllBuildings.Where(b => b.Type == BuildingInfo.Type.WOODCAMP).ToList();
            if (camps.Count <= 0)
            {
                Debugger.Current.AddTempString("Error! No Wood Camp!");
                return null;
            }

            int closestDist = int.MaxValue;
            Building closestCamp = null;
            foreach (Building camp in camps)
            {
                int dist = int.MaxValue;
                dist = Global.ApproxDist(World.Current.StartDrawX + camp.OriginX, World.Current.StartDrawY + camp.OriginY, (int)this.Entity.CenterX, (int)this.Entity.CenterY);

                if (dist < closestDist)
                {
                    closestCamp = camp;
                    closestDist = dist;
                }
            }
            return closestCamp;
        }

        public Tree RetrieveClosestTree(TreeScanType scanFrom)
        {


            int closestDist = int.MaxValue;
            Tree closestTree = null;
            foreach (Tree tree in World.Current.Resources.AllTrees.Where(t => t.State == Resource.ResourceState.Raw))
            {
                if (tree.CurrentHarvesters.Count > 0)
                {
                    continue;
                }

                int dist = int.MaxValue;
                if (scanFrom == TreeScanType.FromMe)
                {
                    dist = Global.ApproxDist((int)tree.TargetPosition.X, (int)tree.TargetPosition.Y, (int)this.Entity.CenterX, (int)this.Entity.CenterY);
                }
                else if (scanFrom == TreeScanType.FromTargetCamp && this.TargetCamp != null)
                {
                    dist = Global.ApproxDist((int)tree.TargetPosition.X, (int)tree.TargetPosition.Y, (int)this.TargetCamp.OriginX, (int)this.TargetCamp.OriginY);
                }
                
                if (dist < closestDist)
                {
                    closestTree = tree;
                    closestDist = dist;
                }
            }

            if (closestTree != null)
            {
                closestTree.CurrentHarvesters.Add(this.Entity);
            }
            else
            {
                Debugger.Current.AddTempString("No more trees to collect!");
                this.Finished = true;
                return null;
            }

            return closestTree;
        }

    }
}
