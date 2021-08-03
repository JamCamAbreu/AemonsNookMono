using AemonsNookMono.Menus;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Admin
{
    public class MenuManager
    {
        #region Singleton Implementation
        private static MenuManager instance;
        private static object _lock = new object();
        private MenuManager()
        {
            this.menuStack = new Stack<Menu>();
        }
        public static MenuManager Current
        {
            get
            {
                if (instance == null)
                {
                    lock (_lock)
                    {
                        if (instance == null)
                        {
                            instance = new MenuManager();
                        }
                    }
                }
                return instance;
            }
        }
        #endregion

        #region Interface
        public void Refresh()
        {
            foreach (Menu m in this.menuStack)
            {
                m.Refresh();
            }
        }
        public void AddMenu(Menu menu)
        {
            this.menuStack.Push(menu);
        }
        public void CloseTop()
        {
            this.menuStack.Pop();
        }
        public Menu Top
        {
            get
            {
                if (this.menuStack.Count > 0)
                {
                    return this.menuStack.Peek();
                }
                else return null;
            }
        }
        public void Update()
        {
            if (this.Top != null)
            {
                this.Top.Update();
            }
        }
        public void Draw()
        {
            foreach (Menu menu in this.menuStack)
            {
                if (menu == this.Top)
                {
                    menu.Draw(true);
                }
                else
                {
                    menu.Draw(false);
                }
            }
        }
        #endregion

        #region internal
        private Stack<Menu> menuStack { get; set; }
        #endregion
    }
}
