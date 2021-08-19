using AemonsNookMono.Player;
using Microsoft.Xna.Framework;
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

        #region Public Properties
        public Color ColorPrimary { get; set; }
        public Color ColorSecondary { get; set; }
        #endregion

        #region Interface
        public void Init()
        {
            if (this.Loaded == null) { throw new Exception("Could not load profile."); }
        }
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
                    this.ProfileInit();
                }
                return loaded;
            }
            set 
            { 
                this.loaded = value;
                this.ProfileInit();
            }
        }
        public Color GetPrimaryColor(Profile.ProfileTheme theme)
        {
            switch (theme)
            {
                case Profile.ProfileTheme.Aemon:
                    return Color.Blue;

                case Profile.ProfileTheme.Aletha:
                    return Color.Purple;

                case Profile.ProfileTheme.Jose:
                    return Color.White;

                case Profile.ProfileTheme.Helga:
                    return Color.Black;

                case Profile.ProfileTheme.Bruno:
                    return Color.Red;

                case Profile.ProfileTheme.Jade:
                    return Color.Green;

                default:
                    throw new NotImplementedException();
            }
        }
        public Color GetSecondaryColor(Profile.ProfileTheme theme)
        {
            switch (theme)
            {
                case Profile.ProfileTheme.Aemon:
                    return Color.Silver;

                case Profile.ProfileTheme.Aletha:
                    return Color.Black;

                case Profile.ProfileTheme.Jose:
                    return Color.Orange;

                case Profile.ProfileTheme.Helga:
                    return Color.Orange;

                case Profile.ProfileTheme.Bruno:
                    return Color.SaddleBrown;

                case Profile.ProfileTheme.Jade:
                    return Color.Aqua;

                default:
                    throw new NotImplementedException();
            }
        }
        public string GetTotalPlaytimeString(Profile profile = null)
        {
            if (profile == null) { profile = this.loaded; }
            TimeSpan time = new TimeSpan(0, 0, profile.TotalTimePlayedSeconds);
            return $"{time.Hours} Hours, {time.Minutes} Minutes, {time.Seconds} Seconds";
        }
        #endregion

        #region Internal
        protected void ProfileInit()
        {
            this.ColorPrimary = this.GetPrimaryColor(loaded.Theme);
            this.ColorSecondary = this.GetSecondaryColor(loaded.Theme);
        }
        #endregion
    }
}
