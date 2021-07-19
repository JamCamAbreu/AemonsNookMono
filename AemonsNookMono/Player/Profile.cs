using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Player
{
    public class Profile
    {
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
        public string CharacterName { get; set; }

        #region Stats
        public double TotalTimePlayed { get; set; }
        public int TotalWoodCollected { get; set; }
        #endregion

        public Profile(ProfileTheme theme)
        {
            this.Theme = theme;

            #region Zero Stats
            TotalTimePlayed = 0;
            TotalWoodCollected = 0;
            #endregion

            switch (theme)
            {

                case ProfileTheme.Aemon:
                    this.CharacterName = "Aemon";
                    break;

                case ProfileTheme.Aletha:
                    this.CharacterName = "Aletha";
                    break;

                case ProfileTheme.Jose:
                    this.CharacterName = "Jose";
                    break;

                case ProfileTheme.Helga:
                    this.CharacterName = "Helga";
                    break;

                case ProfileTheme.Bruno:
                    this.CharacterName = "Bruno";
                    break;

                case ProfileTheme.Jade:
                    this.CharacterName = "Jade";
                    break;

                default:
                    throw new Exception("Whoops! Theme does not exist.");
            }
        }
    }
}
