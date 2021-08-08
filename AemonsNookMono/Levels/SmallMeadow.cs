using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Levels
{
    public class SmallMeadow : Level
    {
        public override int WIDTH
        {
            get { return 19; }
        }

        public override int HEIGHT
        {
            get { return 11; }
        }

        public override string ascii
        {
            get
            {
                return @"
TT------------S-SSS
T------------TSSSSS
----DDDDDD-----SSS-
--DDDWWWWDD----TS--
1DDWWWWWWWDDD------
----WWWWW--DDDDDDD2
-------------------
-W--------------T--
----TTTTTTTTTTT----
-----TTTTTTTTTWW---
---TTTTTTTTTTTTTT--";
            }
        }
    }
}
