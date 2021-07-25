using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.GameWorld.Effects
{
    public class WaterSparkle : TempEffect
    {
        public WaterSparkle(int x, int y, int life, int speed) : base(x, y, life, speed)
        {
        }

        protected override string spriteBaseName { get { return "sparkle"; } }
        protected override int spriteNumFrames { get { return 5; } }
    }
}
