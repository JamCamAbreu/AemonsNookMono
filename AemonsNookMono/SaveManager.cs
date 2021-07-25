using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono
{
    public class SaveManager
    {
        #region Singleton Implementation
        private static SaveManager instance;
        private static object _lock = new object();
        private SaveManager() { }
        public static SaveManager Current
        {
            get
            {
                if (instance == null)
                {
                    lock (_lock)
                    {
                        if (instance == null)
                        {
                            instance = new SaveManager();
                        }
                    }
                }
                return instance;
            }
        }
        #endregion

        public void SaveFile(string fileNameAndLocation, string content, bool overwrite)
        {

        }

        public void AppendFile(string fileNameAndLocation, string content)
        {

        }

        public string[] LoadFile(string fileNameAndLocation)
        {
            string[] content = new string[0]; // delete me

            return content;
        }
    }
}
