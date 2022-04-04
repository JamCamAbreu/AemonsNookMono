using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Menus
{
    /// <summary>
    /// The idea behind this is that it works like tabs.
    /// </summary>
    public class MenuChoice : Menu
    {
        public MenuChoice(List<Menu> choices, string menuname, int width, int height, int x, int y, int padWidth, int padHeight, Color? colorOverride, string sprite) : base(menuname, width, height, x, y, padWidth, padHeight, colorOverride, sprite)
        {
            this.Menus = choices != null ? choices : new List<Menu>();
            this.Selected = this.Menus.Count > 0 ? this.Menus[0] : null;
        }
        public Menu Selected { get; set; }
        public List<Menu> Menus { get; set; }
        public override void Refresh()
        {
            foreach (Menu menu in this.Menus)
            {
                menu.Refresh();
            }
        }
        public void SelectMenu<type>()
        {
            foreach (Menu menu in this.Menus)
            {
                if (menu is type)
                {
                    Selected = menu;
                }
            }
        }

        public override void Draw()
        {
            if (this.Selected != null)
            {
                this.Selected.Draw();
            }
        }
        public override void Update()
        {
            if (this.Selected != null)
            {
                this.Selected.Update();
            }
        }

        public override bool HandleEscape()
        {
            if (this.Selected != null)
            {
                return this.Selected.HandleEscape();
            }
            return false;
        }

        public override bool HandleLeftClick(int x, int y)
        {
            if (this.Selected != null)
            {
                return this.Selected.HandleLeftClick(x, y);
            }
            return false;
        }
    }
}
