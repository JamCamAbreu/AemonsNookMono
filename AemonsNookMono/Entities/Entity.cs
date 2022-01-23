using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Entities
{
    public class Entity
    {
        public Entity()
        {
            this.CenterX = 0;
            this.CenterY = 0;
            this.ImpactX = 0;
            this.ImpactY = 0;
        }
        public float CenterX { get; set; }
        public float CenterY { get; set; }
        public int Weight { get; set; }
        public void Impact(int xVector, int yVector)
        {
            this.ImpactX += xVector;
            this.ImpactY += yVector;
        }
        public bool UpdatePosition()
        {
            if (Math.Abs(this.ImpactX) > 0 || Math.Abs(this.ImpactY) > 0)
            {
                this.CenterX += this.ImpactX;
                this.CenterY += this.ImpactY;

                this.ImpactX = (int)(this.ImpactX * 0.8f); // use weight eventually
                if (this.ImpactX > 0 && this.ImpactX == this.lastImpactX) { this.ImpactX = 0; }
                this.lastImpactX = this.ImpactX;
                
                this.ImpactY = (int)(this.ImpactY * 0.8f);
                if (this.ImpactY > 0 && this.ImpactY == this.lastImpactY) { this.ImpactY = 0; }
                this.lastImpactY = this.ImpactY;

                return true;
            }

            return false;
        }

        protected virtual int ImpactX { get; set; }
        protected virtual int ImpactY { get; set; }
        protected virtual int lastImpactX { get; set; }
        protected virtual int lastImpactY { get; set; }
    }
}
