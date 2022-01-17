using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.GameWorld.Effects
{
    public class WaterSparkle : EffectsComponent
    {
        public WaterSparkle(int maxEffects, int resetBase) : base(maxEffects, resetBase)
        {
        }

        public override void GenerateEffect()
        {
            Tuple<int, int> coord = World.Current.RetrieveRandomTileCoords(World.Current.WaterTiles);
            this.AddEffect(new TempEffect(coord.Item1, coord.Item2, 160, 30, "sparkle", 5));
            base.GenerateEffect();
        }
    }
}
