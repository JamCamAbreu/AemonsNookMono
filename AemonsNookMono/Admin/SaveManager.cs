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
        #endregion

        #region Internal
        private Dictionary<string, string> properties { get; set; }
        private static void EnsureFolderLocation(string foldername)
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), $"AemonsNook{Path.DirectorySeparatorChar}{foldername}");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        private static string RetrieveFilePath(string foldername, string filename, string ext)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), $"AemonsNook{Path.DirectorySeparatorChar}{foldername}{Path.DirectorySeparatorChar}{filename}.{ext}");
        }
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
            return all;
        }
        public void SaveProfile(Profile profile)
        {
            if (ProfileManager.Current.Loaded != null)
            {
                NookFile savefile = new NookFile("Profile", profile.CharacterName);
                foreach (var prop in typeof(Profile).GetProperties())
                {
                    savefile.SetValue(prop.Name, prop.GetValue(profile).ToString());
                }
                savefile.Save();
            }
        }
        public Profile LoadProfile(string profileName)
        {
            Profile profile = null;

            NookFile file = new NookFile("Profile", profileName);
            if (file.CheckFileExists())
            {
                profile = new Profile(Profile.ProfileTheme.Aemon);
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
                    else
                    {
                        throw new Exception("Whoops! This data type is not supported in Save()/Load() yet!");
                    }
                }
            }

            return profile;
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
