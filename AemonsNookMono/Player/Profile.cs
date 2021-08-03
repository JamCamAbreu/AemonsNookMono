using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Player
{
    public class Profile
    {
        public Profile(ProfileTheme theme)
        {
            this.Theme = theme;

            #region Zero Stats
            TotalTimePlayed = 0;
            TotalWoodCollected = 0;
            TotalStoneCollected = 0;
            #endregion
        }
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
        public string CharacterName
        {
            get
            {
                return this.Theme.ToString();
            }
        }

        #region Stats
        public double TotalTimePlayed { get; set; }
        public int TotalWoodCollected { get; set; }
        public int TotalStoneCollected { get; set; }
        #endregion

    }
}
