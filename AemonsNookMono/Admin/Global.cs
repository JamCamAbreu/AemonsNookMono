using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Admin
{
    public static class Global
    {
        public static int IntSqrt(int num)
        {
            if (0 == num) { return 0; }  // Avoid zero divide  
            int n = (num / 2) + 1;       // Initial estimate, never low  
            int n1 = (n + (num / n)) / 2;
            while (n1 < n)
            {
                n = n1;
                n1 = (n + (num / n)) / 2;
            } // end while  
            return n;
        }


        public static int ApproxDist(int x1, int y1, int x2, int y2)
        {
            return IntSqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1)); // Pythagorean brah.
        }
        public static int ApproxDist(Vector2 from, Vector2 to)
        {
            return IntSqrt((int) (((to.X - from.X)*(to.X - from.X)) + ((to.Y - from.Y) * (to.Y - from.Y)))); // Pythagorean brah.
        }

        public static float Ease(float curVal, float targetVal, float speed)
        {
            return curVal + (targetVal - curVal) * speed;
        }

        public static int Ease(int curVal, int targetVal, float speed)
        {
            return (int)(curVal + (targetVal - curVal) * speed);
        }

        public static double Ease(double curVal, double targetVal, float speed)
        {
            return (double)(curVal + (targetVal - curVal) * speed);
        }

        public static Vector2 Ease(Vector2 curVal, Vector2 targetVal, float speed)
        {
            return (Vector2)(curVal + (targetVal - curVal) * speed);
        }

        public static Vector3 Ease(Vector3 curVal, Vector3 targetVal, float speed)
        {
            return (Vector3)(curVal + (targetVal - curVal) * speed);
        }
    }
}
