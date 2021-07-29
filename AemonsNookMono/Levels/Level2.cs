using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Levels
{
    public class Level2 : Level
    {
        public override int WIDTH
        {
            get { return 50; }
        }

        public override int HEIGHT
        {
            get { return 35; }
        }

        protected override string ascii
        {
            get
            {
                return @"
TTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT-----333-----TT
TTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT----DDDDD------TT
TTTTTTTTT-----TTTTTTTTTTTT--------DDDD--------TTTT
TTTTT-------------TTTT----------DDD----------TTTTT
TT-TTT------SS-------------T---DDD------TTTTTTTTTT
TTTTTTTTTT-------------T------DDD-------TTTTTTTTTT
TTTTTTTT--SS-----W--TT----SS-DDD---T---T---TTTTTTT
TTTTTT----------WWW-----SS---DD---------------TTTT
TTTTT-------SS---W-----------DD----SSS--TT------TT
-------------------SS-------DD---SS-SS--------TT--
--------------------------DDD---------------------
--------------DDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD-----
-----------DDDD---SS------------------------------
----------DD--------SS-SSS------------------------
--------DDD---------------------------------------
1DDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD2
1DDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD2
--------DDDDD------------------WWW----------------
------------DD---------------WWWW-----------------
-------------DD-----------WWWWWWWW----------------
-------------DD------------WWWWWW-----------------
-------------DD-----------------------------------
--------------DDD--DDDDDDDDDDDD-----------------TT
---------------DDDDD----------DDD--------------TTT
-------------DDDD---------------DDD----------TTTTT
------------DD-------------------DD--------TTTTTTT
---------DDDD---------TTTTTTT-----DD-----TTTTTTTTT
-------DDD----------TTTTTTTTT-----DD--TTTTTTTTTTTT
W---DDDD----------TTTT-----------DD----TTTTTTTTTTT
5DDDDD------------TTTTTTT------DDD------TTTT------
W---------------TTTTTTT-------DD-------TTTT-------
-TTTT---------TTTTTTT---------D--------TTT-------W
TTTTTWWWWWWWTTTTTTTTTT--------DD-------------DDDD4
TTTTTTTTWWWWTWWTTTTTTTTTT------DDDD-------DDDDD--W
TTTTTTTWWWWWWWWTTTTTTTTTTT-------DDDDDDDDDDD------
TTTTTTTTWWTTTTTTTTTTTTTTTTTT----------------------";
            }
        }
    }
}
