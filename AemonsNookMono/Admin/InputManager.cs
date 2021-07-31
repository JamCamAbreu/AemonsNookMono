﻿using AemonsNookMono.GameWorld;
using AemonsNookMono.Menus;
using AemonsNookMono.Resources;
using AemonsNookMono.Structures;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
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

        #region Internal
        private bool LeftMouseWait { get; set; }
        private bool EnterWait { get; set; }
        private bool EscapeWait { get; set; }
        #endregion

        #region Singleton Implementation
        private static InputManager instance;
        private static object _lock = new object();
        private InputManager()
        {
            this.LeftMouseWait = false;
            this.EnterWait = false;
            this.EscapeWait = false;
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
            // Todo: use function pointers or reflection to refactor this?
            #region TODO NEEDS REFACTOR
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

            if (!this.EscapeWait && Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.EscapeWait = true;
                this.HandleEscape();
            }
            if (this.EscapeWait && Keyboard.GetState().IsKeyUp(Keys.Escape)) { this.EscapeWait = false; }
            #endregion
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
                    if (MenuManager.Current.Top != null && MenuManager.Current.Top is PauseMenu)
                    {
                        PauseMenu pmenu = MenuManager.Current.Top as PauseMenu; 
                        if (pmenu != null)
                        {
                            if (pmenu.Buttons[0].MyCollision.IsCollision(x, y))
                            {
                                StateManager.Current.CurrentState = pmenu.OriginalState;
                                MenuManager.Current.CloseTop();
                                return;
                            }
                            else if (pmenu.Buttons[1].MyCollision.IsCollision(x, y))
                            {
                                Debugger.Current.AddTempString($"You clicked on the {pmenu.Buttons[1].Name} button!");
                                return;
                            }
                            else if (pmenu.Buttons[2].MyCollision.IsCollision(x, y))
                            {
                                Debugger.Current.AddTempString($"You clicked on the {pmenu.Buttons[2].Name} button!");
                                return;
                            }
                            else if (pmenu.Buttons[3].MyCollision.IsCollision(x, y))
                            {
                                StateManager.Current.CurrentState = StateManager.State.Exit;
                                return;
                            }
                        }
                    }
                    break;
            }

            Debugger.Current.AddTempString("[Clicked Empty]");
        }
        private void HandleEnter()
        {
            StateManager.State state = StateManager.Current.CurrentState;
            if (state == StateManager.State.World || state == StateManager.State.BuildSelection)
            {
                StateManager.Current.CurrentState = StateManager.State.Pause;
                MenuManager.Current.AddMenu(new PauseMenu(state));

            }
            if (state == StateManager.State.Pause)
            {

                if (MenuManager.Current.Top != null && MenuManager.Current.Top is PauseMenu)
                {
                    StateManager.Current.CurrentState = (MenuManager.Current.Top as PauseMenu).OriginalState;
                    MenuManager.Current.CloseTop();
                }
            }
        }
        private void HandleEscape()
        {
            StateManager.State state = StateManager.Current.CurrentState;
            if (state == StateManager.State.World || state == StateManager.State.BuildSelection)
            {
                StateManager.Current.CurrentState = StateManager.State.Pause;
                MenuManager.Current.AddMenu(new PauseMenu(state));

            }
            if (state == StateManager.State.Pause)
            {

                if (MenuManager.Current.Top != null && MenuManager.Current.Top is PauseMenu)
                {
                    StateManager.Current.CurrentState = (MenuManager.Current.Top as PauseMenu).OriginalState;
                    MenuManager.Current.CloseTop();
                }
            }
        }
        #endregion


    }
}