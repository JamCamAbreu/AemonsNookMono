using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AemonsNookMono.Menus
{
    public class ButtonSelection
    {
        #region Constructor
        public ButtonSelection()
        {
            this.Options = new List<Button>();
            this.SelectedButton = null;
        }
        public ButtonSelection(List<Button> options, Button selected = null)
        {
            if (options == null || options.Count == 0) { throw new Exception("Why you do that? What good is a button selection with no buttons? Use the other constructor."); }

            this.Options = options;
            this.SelectedButton = selected;
        }
        #endregion

        #region Public Properties
        public List<Button> Options { get; set; }
        public Button SelectedButton { get; set; }
        #endregion

        #region Interface
        public void Add(Button button)
        {
            if (button == null || string.IsNullOrEmpty(button.ButtonCode)) { throw new Exception("This is going to cause trouble!"); }
            this.Options.Add(button);
        }
        public void CheckSelect(string buttonCode)
        {
            Button button = this.Options.Where(button => button.ButtonCode == buttonCode).FirstOrDefault();
            if (button != null)
            {
                this.unselectAll();
                this.SelectedButton = button;
                this.SelectedButton.Selected = true;
            }
        }
        public void Select(Button button)
        {
            if (this.Options.Contains(button))
            {
                this.unselectAll();
                this.SelectedButton = button;
                this.SelectedButton.Selected = true;
            }
        }
        #endregion

        #region Internal
        protected void unselectAll()
        {
            foreach (Button button in this.Options)
            {
                button.Selected = false;
            }
        }
        #endregion
    }
}
