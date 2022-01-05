using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Levels
{
    public class CedricsPass : Level
    {
        public override string Name { get { return "Cedric's Pass"; } }
        public override int WIDTH
        {
            get { return 39; }
        }

        public override int HEIGHT
        {
            get { return 22; }
        }

        public override string ascii
        {
            get
            {
                return @"
-------SS---W-----------DD----SSS--TT--
--------------SS-------DD---SS-SS------
---------------------DDD---------------
---------DDDDDDDDDDDDDDDDDDDDDDDDDDDDDD
------DDDD---SS------------------------
-----DD--------SS-SSS------------------
---DDD---------------------------------
1DDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD2
1DDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD2
---DDDDD------------------WWW----------
-------DD---------------WWWW-----------
--------DD-----------WWWWWWWW----------
--------DD------------WWWWWW-----------
--------DD-----------------------------
---------DDD--DDDDDDDDDDDD-------------
----------DDDDD----------DDD-----------
--------DDDD---------------DDD---------
-------DD-------------------DD--------T
----DDDD---------TTTTTTT-----DD-----TTT
--DDD----------TTTTTTTTT-----DD--TTTTTT
DDD----------TTTT-----------DD----TTTTT
D------------TTTTTTT------DDD------TTTT
";
            }
        }
    }
}
