using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Levels
{
    public class Level
    {
        public virtual string Name { get; set; }
        public virtual int WIDTH { get; set; }
        public virtual int HEIGHT { get; set; }
        public virtual string ascii { get; set; }
        public virtual string RetrieveLevelCode()
        {
            return System.Text.RegularExpressions.Regex.Replace(ascii, @"\s+", "");
        }
    }
}
