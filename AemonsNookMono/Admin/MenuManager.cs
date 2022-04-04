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
            this.screenStack = new Stack<Screen>();
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
            foreach (Screen screen in this.screenStack)
            {
                screen.Refresh();
            }
        }
        public void AddMenu(Menu menu, bool newMenu, bool drawPrevScreen)
        {
            if (this.Top == null || newMenu)
            {
                Screen screen = new Screen(drawPrevScreen);
                screen.AddMenu(menu);
                this.screenStack.Push(screen);
            }
            else
            {
                this.Top.AddMenu(menu);
            }
        }
        public void CloseMenuType<type>()
        {
            if (this.Top != null)
            {
                Menu menu = this.Top.ContainsType<type>();
                while (menu != null)
                {
                    this.Top.Menus.Remove(menu);
                    menu = this.Top.ContainsType<type>();
                }
                if (this.Top.Menus.Count <= 0)
                {
                    this.CloseTopScreen();
                }
            }
            return;
        }
        public void CloseTopMenu()
        {
            if (this.Top != null)
            {
                this.Top.CloseTop();
                if (this.Top.Menus.Count <= 0)
                {
                    this.CloseTopScreen();
                }
            }
        }
        public void CloseTopScreen()
        {
            this.screenStack.Pop();
        }
        public void ClearAllMenus()
        {
            foreach (Screen screen in this.screenStack)
            {
                screen.ClearAllMenus();
            }
            this.screenStack.Clear();
        }
        public Screen Top
        {
            get
            {
                if (this.screenStack.Count > 0)
                {
                    return this.screenStack.Peek();
                }
                else return null;
            }
        }
        public Menu TopMenu
        {
            get
            {
                if (this.screenStack != null && this.screenStack.Count > 0)
                {
                    return this.Top.Top;
                }
                return null;
            }
        }
        public int Count
        {
            get
            {
                return this.screenStack.Count;
            }
        }
        public Menu TopScreenContainsType<type>()
        {
            if (this.Top != null)
            {
                return this.Top.ContainsType<type>();
            }
            return null;
        }
        public Menu RetrieveMenu(string name)
        {
            Menu menu = null;
            foreach (Screen screen in this.screenStack)
            {
                menu = screen.RetrieveMenu(name);
                if (menu != null)
                {
                    return menu;
                }
            }
            return null;
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
            Stack<Screen> drawScreens = new Stack<Screen>();
            foreach (Screen screen in this.screenStack)
            {
                drawScreens.Push(screen);
                if (!screen.DrawPreviousScreen) { break; }
            }
            foreach (Screen screen in drawScreens)
            {
                screen.Draw();
            }
        }
        #endregion

        #region internal
        private Stack<Screen> screenStack { get; set; }
        #endregion
    }
}
