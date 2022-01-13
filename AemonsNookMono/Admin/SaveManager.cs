using AemonsNookMono.Levels;
using AemonsNookMono.Player;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AemonsNookMono.Admin
{
    public class NookFile
    {
        #region Constructor
        public NookFile(string folder, string filename)
        {
            this.properties = new Dictionary<string, string>();
            this.Folder = folder;
            this.Filename = filename;
        }
        #endregion

        #region Interface
        public string Folder { get; set; }
        public string Filename { get; set; }
        public string Filepath
        {
            get
            {
                NookFile.EnsureFolderLocation(this.Folder);
                return NookFile.RetrieveFilePath(Folder, Filename, "nook");
            }
        }
        public void SetValue(string property, string value)
        {
            if (this.properties.ContainsKey(property))
            {
                this.properties[property] = value;
            }
            else
            {
                this.properties.Add(property, value);
            }
        }
        public string GetValue(string property)
        {
            if (this.properties.ContainsKey(property))
            {
                return this.properties[property];
            }
            else
            {
                throw new Exception("Error: Corrupt save file.");
            }
        }
        public bool CheckFileExists()
        {
            if (File.Exists(this.Filepath)) { return true; }
            return false;
        }
        public static void EnsureFolderLocation(string foldername)
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), $"AemonsNook{Path.DirectorySeparatorChar}{foldername}");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        public void LoadProperties()
        {
            string[] lines = System.IO.File.ReadAllLines(this.Filepath);
            for (int i = 0; i < lines.Length; i += 2)
            {
                string prop = lines[i];
                string value = lines[i + 1];
                
                if (this.properties.ContainsKey(prop))
                {
                    this.properties[prop] = value;
                }
                else
                {
                    this.properties.Add(prop, value);
                }
            }
        }
        public void Save()
        {
            using (StreamWriter outputFile = new StreamWriter(this.Filepath, false))
            {
                foreach (string prop in this.properties.Keys)
                {
                    outputFile.WriteLine(prop);
                    outputFile.WriteLine(this.properties[prop]);
                }
            }
        }
        public static string RetrieveFilePath(string foldername, string filename, string ext)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), $"AemonsNook{Path.DirectorySeparatorChar}{foldername}{Path.DirectorySeparatorChar}{filename}.{ext}");
        }
        public static string RetrieveFolderPath(string foldername)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), $"AemonsNook{Path.DirectorySeparatorChar}{foldername}{Path.DirectorySeparatorChar}");
        }
        #endregion

        #region Internal
        private Dictionary<string, string> properties { get; set; }
        #endregion
    }
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
        public List<Profile> RetrieveAllSavedProfiles()
        {
            List<Profile> all = new List<Profile>();
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), $"AemonsNook{Path.DirectorySeparatorChar}Profile");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            DirectoryInfo di = new DirectoryInfo(path);
            FileInfo[] files = di.GetFiles("*.nook");
            foreach (FileInfo file in files)
            {
                string filename = file.Name.Substring(0, file.Name.LastIndexOf('.'));
                Profile cur = this.LoadProfile(filename);
                all.Add(cur);
            }
            return all;
        }
        public void SaveProfile(Profile profile)
        {
            if (profile != null)
            {
                TimeSpan t = DateTime.Now - profile.LastPlayed;
                profile.TotalTimePlayedSeconds += (int)t.TotalSeconds;
                profile.LastPlayed = DateTime.Now;

                NookFile savefile = new NookFile("Profile", profile.Name);
                foreach (var prop in typeof(Profile).GetProperties())
                {
                    savefile.SetValue(prop.Name, prop.GetValue(profile).ToString());
                }
                savefile.Save();
            }
        }
        public void SaveLevel(Level level)
        {
            if (level != null)
            {
                NookFile savefile = new NookFile("Level", level.Name);
                foreach (var prop in typeof(Level).GetProperties())
                {
                    savefile.SetValue(prop.Name, prop.GetValue(level).ToString());
                }
                savefile.Save();
            }
        }
        public bool CheckProfileExists(string profileName)
        {
            NookFile file = new NookFile("Profile", profileName);
            return file.CheckFileExists();
        }
        public bool CheckLevelExists(string levelName)
        {
            NookFile file = new NookFile("Level", levelName);
            return file.CheckFileExists();
        }
        public string[] RetrieveLevelNames()
        {
            string levelfolderpath = NookFile.RetrieveFolderPath("Level");
            NookFile.EnsureFolderLocation("Level");
            string[] filenames = System.IO.Directory.GetFiles(levelfolderpath, "*.nook");
            List<string> fileonlynames = new List<string>();
            foreach (string filename in filenames)
            {
                string name = Path.GetFileName(filename).Replace(".nook", "");
                fileonlynames.Add(name);
            }
            return fileonlynames.ToArray();
        }

        public Profile LoadProfile(string profileName)
        {
            Profile profile = null;
            NookFile file = new NookFile("Profile", profileName);
            if (file.CheckFileExists())
            {
                profile = new Profile();
                file.LoadProperties();
                foreach (var prop in typeof(Profile).GetProperties())
                {
                    string valueString = file.GetValue(prop.Name);

                    if (prop.PropertyType == typeof(int))
                    {
                        prop.SetValue(profile, int.Parse(valueString));
                    }
                    else if (prop.PropertyType == typeof(double))
                    {
                        prop.SetValue(profile, double.Parse(valueString));
                    }
                    else if (prop.PropertyType == typeof(string))
                    {
                        prop.SetValue(profile, valueString);
                    }
                    else if (prop.PropertyType == typeof(Profile.ProfileTheme))
                    {
                        Profile.ProfileTheme theme = (Profile.ProfileTheme)Enum.Parse(typeof(Profile.ProfileTheme), valueString);
                        prop.SetValue(profile, theme);
                    }
                    else if (prop.PropertyType == typeof(DateTime))
                    {
                        if (prop.Name == "LastPlayed")
                        {
                            prop.SetValue(profile, DateTime.Now);
                        }
                        else
                        {
                            prop.SetValue(profile, DateTime.Parse(valueString));
                        }
                        
                    }
                    else
                    {
                        throw new Exception("Whoops! This data type is not supported in Save()/Load() yet!");
                    }
                }
            }

            return profile;
        }
        public Level LoadLevel(string levelName)
        {
            Level level = null;
            NookFile file = new NookFile("Level", levelName);
            if (file.CheckFileExists())
            {
                level = new Level();
                file.LoadProperties();
                foreach (var prop in typeof(Level).GetProperties())
                {
                    string valueString = file.GetValue(prop.Name);

                    if (prop.PropertyType == typeof(int))
                    {
                        prop.SetValue(level, int.Parse(valueString));
                    }
                    else if (prop.PropertyType == typeof(double))
                    {
                        prop.SetValue(level, double.Parse(valueString));
                    }
                    else if (prop.PropertyType == typeof(string))
                    {
                        prop.SetValue(level, valueString);
                    }
                    else
                    {
                        throw new Exception("Whoops! This data type is not supported in Save()/Load() yet!");
                    }
                }
            }

            return level;
        }
        #endregion

        #region Internal
        private static void StoreLastSavedProfile(string filepath)
        {
            NookFile system = new NookFile("System", "SaveData");

        }

        #endregion

    }
}
