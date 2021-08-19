using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Player
{
    public class Profile
    {
        #region Stats
        public int TotalTimePlayedSeconds { get; set; }
        public int TotalWoodCollected { get; set; }
        public int TotalStoneCollected { get; set; }
        public int TimesClicked { get; set; }
        public DateTime LastPlayed { get; set; }
        #endregion

        #region Constructors
        public Profile() { }
        public Profile(ProfileTheme theme, string name)
        {
            this.Theme = theme;
            this.Name = name;

            TotalTimePlayedSeconds = 0;
            this.LastPlayed = DateTime.Now;

            #region Zero Stats
            
            TotalWoodCollected = 0;
            TotalStoneCollected = 0;
            #endregion
        }
        #endregion

        public enum ProfileTheme
        {
            // -----------------------------------------------------------
            //  NAME        SKIN        HAIR        PRIMARY     SECONDARY
            // -----------------------------------------------------------
            Aemon,  //  white       brown       blue        silver
            Aletha, //  asian       brown       purple      black
            Jose,   //  hispanic    brown       white       orange
            Helga,  //  white       red         black       orange
            Bruno,  //  black       brown       Red         brown
            Jade    //  brown       green       green       aqua
        }
        public ProfileTheme Theme { get; set; }
        public string Name { get; set; }
    }
}
