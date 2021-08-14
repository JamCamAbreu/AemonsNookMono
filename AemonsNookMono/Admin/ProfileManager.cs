using AemonsNookMono.Player;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Admin
{
    public class ProfileManager
    {
        #region Singleton Implementation
        private static ProfileManager instance;
        private static object _lock = new object();
        private ProfileManager() { }
        public static ProfileManager Current
        {
            get
            {
                if (instance == null)
                {
                    lock (_lock)
                    {
                        if (instance == null)
                        {
                            instance = new ProfileManager();
                        }
                    }
                }
                return instance;
            }
        }
        #endregion

        private Profile loaded { get; set; }
        public Profile Loaded
        {
            get
            {
                if (loaded == null)
                {
                    Profile Default = SaveManager.Current.LoadProfile("Default");
                    if (Default == null)
                    {
                        Default = new Profile(Profile.ProfileTheme.Bruno, "Default");
                        SaveManager.Current.SaveProfile(Default);
                    }
                    loaded = Default;
                }
                return loaded;
            }
            set { this.loaded = value; }
        }
    }
}
