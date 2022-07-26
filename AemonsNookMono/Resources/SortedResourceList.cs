using AemonsNookMono.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AemonsNookMono.Resources
{
    public class SortedResourceList
    {
        #region Public Properties
        public SortedList<int, Resource> Sorted { get; set; }
        public List<Resource> ResourcesToRemove { get; set; } = new List<Resource>();
        public List<Tree> AllTrees { get; set; } = new List<Tree>();
        public List<Stone> AllStones { get; set; } = new List<Stone>();
        public Random ran { get; set; }
        #endregion

        #region Constructor
        public SortedResourceList()
        {
            Sorted = new SortedList<int, Resource>();
            this.AllStones = new List<Stone>();
            this.AllTrees = new List<Tree>();
            ResourcesToRemove = new List<Resource>();
            ran = new Random();
        }
        #endregion

        #region Interface
        public void Clear()
        {
            this.Sorted.Clear();
        }
        public void Add(Resource r)
        {
            if (r != null)
            {
                int xfactor = (int)r.Position.X * 100;
                int yfactor = (int)r.Position.Y * 10000;
                int collision = 0;
                bool added = false;
                switch (r.Type)
                {
                    case Resource.ResourceType.Tree:
                        this.AllTrees.Add(r as Tree);
                        break;

                    case Resource.ResourceType.Stone:
                        this.AllStones.Add(r as Stone);
                        break;
                }
                while (!added)
                {
                    try
                    {
                        int key = xfactor + yfactor + collision;
                        this.Sorted.Add(key, r);
                        added = true;
                    }
                    catch
                    {
                        collision++;
                        Debugger.Current.NumCollisionsDetected++; // Keep an eye on this, if needed bump up the yfactor to 100000 to avoid collisions
                    }
                }
            }
        }
        #endregion

        #region Game Loop
        public void Update()
        {
            foreach (Resource r in this.Sorted.Values)
            {
                r.Update();
            }

            foreach (Resource resource in this.ResourcesToRemove)
            {
                int key = this.Sorted.IndexOfValue(resource);
                if (key != -1)
                {
                    this.Sorted.RemoveAt(key);
                }
                switch (resource.Type)
                {
                    case Resource.ResourceType.Tree:
                        this.AllTrees.Remove(resource as Tree);
                        break;

                    case Resource.ResourceType.Stone:
                        this.AllStones.Remove(resource as Stone);
                        break;
                }
            }
            this.ResourcesToRemove.Clear();
        }
        public void Draw()
        {
            foreach (Resource r in this.Sorted.Values)
            {
                r.Draw();
            }
        }
        #endregion
    }
}
