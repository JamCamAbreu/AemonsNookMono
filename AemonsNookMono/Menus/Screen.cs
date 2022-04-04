using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AemonsNookMono.Menus
{
    public class Screen
    {
        public Screen(bool drawPreviousScreen)
        {
            this.Menus = new List<Menu>();
            DrawPreviousScreen = drawPreviousScreen;
        }
        public List<Menu> Menus { get; set; }

        #region Interface
        public List<Menu> MenuStack
        {
            get
            {
                if (this.Menus != null)
                {
                    return this.Menus.AsEnumerable().Reverse().ToList();
                }
                return null;
            }
        }
        public void Refresh()
        {
            foreach (Menu m in this.Menus)
            {
                m.Refresh();
            }
        }
        public void AddMenu(Menu menu)
        {
            this.Menus.Add(menu);
        }
        public void CloseTop()
        {
            this.Menus.RemoveAt(this.Menus.Count - 1);
        }
        public void ClearAllMenus()
        {
            this.Menus.Clear();
        }
        public Menu Top
        {
            get
            {
                if (this.Menus.Count > 0)
                {
                    return this.Menus[0];
                }
                else return null;
            }
        }
        public int Count
        {
            get
            {
                return this.Menus.Count;
            }
        }
        public bool DrawPreviousScreen { get; set; }
        public Menu ContainsType<type>()
        {
            foreach (Menu menu in this.Menus)
            {
                if (menu is type)
                {
                    return menu;
                }
            }
            return null;
        }
        public Menu RetrieveMenu(string name)
        {
            foreach (Menu menu in this.Menus)
            {
                if (menu.MenuName == name)
                {
                    return menu;
                }
            }
            return null;
        }
        public virtual void Update()
        {
            foreach (Menu menu in this.Menus)
            {
                menu.Update();
            }
        }
        public virtual void Draw()
        {
            foreach (Menu menu in this.Menus)
            {
                menu.Draw();
            }
        }
        #endregion
    }
}
