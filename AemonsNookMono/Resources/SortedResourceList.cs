using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Resources
{
    public class SortedResourceList
    {
        public SortedList<int, Resource> Sorted;
        public Random ran { get; set; }
        public SortedResourceList()
        {
            Sorted = new SortedList<int, Resource>();
            ran = new Random();
        }
        public void Add(Resource r)
        {
            if (r != null)
            {
                int xfactor = r.PosX * 100;
                int yfactor = r.PosY * 1000;
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
                    }
                }
            }
        }
        public void Draw(int startDrawX, int startDrawY)
        {
            foreach (Resource r in this.Sorted.Values)
            {
                r.Draw(startDrawX, startDrawY);
            }
        }
    }
}
