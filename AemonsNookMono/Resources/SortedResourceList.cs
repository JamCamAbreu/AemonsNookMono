using AemonsNookMono.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AemonsNookMono.Resources
{
    public class SortedResourceList
    {
        public SortedList<int, Resource> Sorted { get; set; }
        public List<Resource> ResourcesToRemove { get; set; }
        public Random ran { get; set; }
        public SortedResourceList()
        {
            Sorted = new SortedList<int, Resource>();
            ResourcesToRemove = new List<Resource>();
            ran = new Random();
        }
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
    }
}
