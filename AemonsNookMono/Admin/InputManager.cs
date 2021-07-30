using AemonsNookMono.GameWorld;
using AemonsNookMono.Resources;
using AemonsNookMono.Structures;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Admin
{
    public class InputManager
    {
        #region Enums
        public enum InputType
        {
            LeftMouse
        }
        #endregion

        #region Singleton Implementation
        private static InputManager instance;
        private static object _lock = new object();
        private InputManager()
        {
            this.LeftMouseWait = false;
            this.EnterWait = false;
        }
        public static InputManager Current
        {
            get
            {
                if (instance == null)
                {
                    lock (_lock)
                    {
                        if (instance == null)
                        {
                            instance = new InputManager();
                        }
                    }
                }
                return instance;
            }
        }
        #endregion

        #region Interface
        public void Update()
        {
            var mousestate = Mouse.GetState();
            if (!this.LeftMouseWait && mousestate.LeftButton == ButtonState.Pressed)
            {
                this.LeftMouseWait = true;
                this.HandleLeftClick(mousestate.X, mousestate.Y);
            }
            if (this.LeftMouseWait && mousestate.LeftButton == ButtonState.Released) { this.LeftMouseWait = false; }

            if (!this.EnterWait && Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                this.EnterWait = true;
                this.HandleEnter();
            }
            if (this.EnterWait && Keyboard.GetState().IsKeyUp(Keys.Enter)) { this.EnterWait = false; }
        }
        #endregion

        #region Helper Methods
        private void HandleLeftClick(int x, int y)
        {
            StateManager.State curState = StateManager.Current.CurrentState;
            switch (curState)
            {
                case StateManager.State.MainMenu:
                    // Check for buttons
                    break;

                case StateManager.State.Overworld:
                    // Check for buttons
                    // Check for Gui things
                    // Check for interactibles
                    break;

                case StateManager.State.BuildSelection:
                    if (Buildings.Current.Selection != null)
                    {
                        Buildings.Current.Selection.Build();
                        Buildings.Current.Selection = null;
                        StateManager.Current.CurrentState = StateManager.State.World;
                    }
                    break;

                case StateManager.State.World:
                    // todo: Help system / popups
                    // Todo: buttons
                    // Todo: Gui

                    // Resources:
                    foreach (Resource r in World.Current.Resources.Sorted.Values)
                    {
                        if (r.IsCollision(x, y))
                        {
                            r.HandleLeftClick();
                            return;
                        }
                    }

                    // Tiles
                    if (World.Current.InsideBounds(x, y))
                    {
                        Tile curTile = World.Current.TileAtPixel(x, y);
                        if (curTile != null)
                        {
                            curTile.HandleLeftClick();
                            return;
                        }
                    }
                    break;

                case StateManager.State.Pause:
                    // Check for buttons
                    break;
            }

            Debugger.Current.AddTempString("[Clicked Empty]");
        }
        private void HandleEnter()
        {
            StateManager.State state = StateManager.Current.CurrentState;
            if (state == StateManager.State.World || state == StateManager.State.BuildSelection)
            {
                StateManager.Current.CurrentPauseMenu = new Menus.PauseMenu(state);
                StateManager.Current.CurrentState = StateManager.State.Pause;
            }
            if (state == StateManager.State.Pause)
            {
                if (StateManager.Current.CurrentPauseMenu != null)
                {
                    StateManager.Current.CurrentState = StateManager.Current.CurrentPauseMenu.OriginalState;
                    StateManager.Current.CurrentPauseMenu = null;
                }
            }
        }
        #endregion

        #region Internal
        private bool LeftMouseWait { get; set; }
        private bool EnterWait { get; set; }
        #endregion
    }
}
