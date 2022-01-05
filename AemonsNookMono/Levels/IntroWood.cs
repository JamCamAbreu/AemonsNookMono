using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Levels
{
    public class IntroWood : Level
    {
        public override string Name { get { return "Intro Wood"; } }
        public override int WIDTH
        {
            get { return 8; }
        }

        public override int HEIGHT
        {
            get { return 8; }
        }

        public override string ascii
        {
            get
            {
                return @"
TTTTTTTT
TTTTTTTT
---TTTTT
--DDDDTT
1DDTTTDT
-TTTDTTD
-TTTDDDT
TTTTTTTT";
            }
        }
    }
}
