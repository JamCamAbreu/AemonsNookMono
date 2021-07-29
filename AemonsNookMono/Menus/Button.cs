using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Menus
{
    public class Button
    {
        #region Constructor
        public Button()
        {

        }
        #endregion

        #region Interface
        public void Update()
        {

        }
        public void Draw()
        {

        }
        #endregion

        #region Internal
        private string spriteNormal { get; set; }
        private string spriteHover { get; set; }
        private string spriteClick { get; set; }
        #endregion
    }
}
