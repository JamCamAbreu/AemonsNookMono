using AemonsNookMono.Player;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AemonsNookMono.Admin
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

        #region Interface
        public static void EnsureFolderLocation(string foldername)
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), $"AemonsNook{Path.DirectorySeparatorChar}{foldername}");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        public static string RetrieveFilePath(string foldername, string filename, string ext)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), $"AemonsNook{Path.DirectorySeparatorChar}{foldername}{Path.DirectorySeparatorChar}{filename}.{ext}");
        }

        #endregion

        public void SaveProfile(Profile profile)
        {
            if (ProfileManager.Current.Loaded != null)
            {
                SaveManager.EnsureFolderLocation("Profile");
                string path = SaveManager.RetrieveFilePath("Profile", profile.CharacterName, "prof");

                using (StreamWriter outputFile = new StreamWriter(path, false))
                {
                    outputFile.WriteLine(profile.CharacterName);
                    outputFile.WriteLine(profile.Theme);
                    outputFile.WriteLine(profile.TotalTimePlayed);
                    outputFile.WriteLine(profile.TotalWoodCollected);
                    outputFile.WriteLine(profile.TotalStoneCollected);
                }
            }
        }

        public Profile LoadProfile(string profileName)
        {
            Profile profile = null;

            return profile;
        }
    }
}
