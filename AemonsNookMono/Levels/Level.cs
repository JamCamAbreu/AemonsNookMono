using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Levels
{
    public abstract class Level
    {
        public abstract int WIDTH { get; }
        public abstract int HEIGHT { get; }
        protected abstract string ascii { get; }
        public string RetrieveLevelCode()
        {
            return System.Text.RegularExpressions.Regex.Replace(ascii, @"\s+", "");
        }
    }
}
