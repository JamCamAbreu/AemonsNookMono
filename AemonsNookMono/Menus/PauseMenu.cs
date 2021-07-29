using AemonsNookMono.Admin;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Menus
{
    public class PauseMenu
    {
        #region Constructors
        public PauseMenu(StateManager.State originalState)
        {
            this.Buttons = new List<Button>();
            this.OriginalState = originalState;
        }
        #endregion

        #region Public Properties
        public List<Button> Buttons { get; set; }
        public StateManager.State OriginalState { get; set; }
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
