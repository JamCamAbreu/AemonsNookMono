using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Levels
{
    public class LevelTestTrees : Level
    {
        public override int WIDTH
        {
            get { return 19; }
        }

        public override int HEIGHT
        {
            get { return 11; }
        }

        //        protected override string ascii
        //        {
        //            get
        //            {
        //                return @"
        //T-T-T-T-T-T-T-T-T-T
        //-T-T-T-T-T-T-T-T-T-
        //T-T-T-T-T-T-T-T-T-T
        //-T-T-T-T-T-T-T-T-T-
        //T-T-T-T-T-T-T-T-T-T
        //-T-T-T-T-T-T-T-T-T-
        //T-T-T-T-T-T-T-T-T-T
        //-T-T-T-T-T-T-T-T-T-
        //T-T-T-T-T-T-T-T-T-T
        //-T-T-T-T-T-T-T-T-T-
        //T-T-T-T-T-T-T-T-T-T";
        //            }
        //        }

        protected override string ascii
        {
            get
            {
                return @"
        S------------------
        -------------------
        -------------------
        -------------------
        -------------------
        -------------------
        -------------------
        -------------------
        -------------------
        -------------------
        -------------------";
            }
        }

    }
}
