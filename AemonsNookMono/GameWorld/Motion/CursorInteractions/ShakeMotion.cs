using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.GameWorld.Motion.CursorInteractions
{
    public class ShakeMotion : Motion
    {
        public ShakeMotion(Vector2 startPos, bool repeat) : base(startPos, repeat) { }
        protected override void Init()
        {
            this._Positions = new Vector2[6]
            {
                new Vector2(0, 0),
                new Vector2(1, 1),
                new Vector2(2, 2),
                new Vector2(3, 3),
                new Vector2(4, 4),
                new Vector2(5, 5),
            };
        }
    }
}
