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

        private Profile loadedProfile { get; set; }
        public Profile Loaded
        {
            get
            {
                if (this.loadedProfile != null)
                {
                    return loadedProfile;
                }
                else
                {
                    throw new Exception("Attempt to retrieve null profile. Is it safe to do so?");
                }
            }
            set
            {
                this.loadedProfile = value;
            }
        }
    }
}
