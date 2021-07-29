using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Menus
{
    public class PauseMenu
    {
        #region Constructors
        public PauseMenu()
        {
            this.Buttons = new List<Button>();
        }
        #endregion

        #region Public Properties
        List<Button> Buttons { get; set; }
        #endregion

        #region Interface
        public void Draw()
        {

        }
        public void Update()
        {

        }
        #endregion
    }
}
