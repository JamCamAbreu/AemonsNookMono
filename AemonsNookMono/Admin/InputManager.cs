using AemonsNookMono.Entities;
using AemonsNookMono.Entities.Tasks;
using AemonsNookMono.GameWorld;
using AemonsNookMono.Menus;
using AemonsNookMono.Menus.General;
using AemonsNookMono.Menus.LevelEditor;
using AemonsNookMono.Menus.World;
using AemonsNookMono.Player;
using AemonsNookMono.Resources;
using AemonsNookMono.Structures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AemonsNookMono.Admin
{
    public class InputManager
    {
        #region Constants
        public const int BUTTON_DOWN_INITIAL = 22;
        public const int BUTTON_DOWN_SPEED = 5;
        #endregion

        #region Enums
        public enum MouseButton
        {
            Left,
            Right
        }
        #endregion

        #region Singleton Implementation
        private static InputManager instance;
        private static object _lock = new object();
        private InputManager()
        {
            this.PrevKeyboardState = Keyboard.GetState();
            this.CurKeyboardState = Keyboard.GetState();
            this.PrevMouseState = Mouse.GetState();
            this.CurMouseState = Mouse.GetState();
            this.KeyboardAlarms = new Dictionary<Keys, int>();
            this.MouseAlarms = new Dictionary<MouseButton, int>();
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

        #region Public Properties
        public MouseState CurMouseState { get; set; }
        public MouseState PrevMouseState { get; set; }
        public KeyboardState CurKeyboardState { get; set; }
        public KeyboardState PrevKeyboardState { get; set; }
        #endregion

        #region Interface
        public bool CheckKeyboardReleased(Keys key)
        {
            if (this.CurKeyboardState.IsKeyUp(key) && this.PrevKeyboardState.IsKeyDown(key)) { return true; }
            return false;
        }
        public bool CheckKeyboardPressed(Keys key)
        {
            if (this.CurKeyboardState.IsKeyDown(key) && this.PrevKeyboardState.IsKeyUp(key)) 
            { 
                return true; 
            }
            return false;
        }
        public bool CheckKeyboardDown(Keys key)
        {
            if (this.CurKeyboardState.IsKeyDown(key)) { return true; }
            return false;
        }
        public bool CheckKeyboardDownInterval(Keys key, int initialFrameSpeed, int frameSpeed)
        {
            if (CheckKeyboardDown(key))
            {
                if (this.CheckKeyboardPressed(key) == true)
                {
                    if (this.KeyboardAlarms.ContainsKey(key))
                    {
                        this.KeyboardAlarms[key] = initialFrameSpeed;
                    }
                    else
                    {
                        this.KeyboardAlarms.Add(key, initialFrameSpeed);
                    }
                    return true;
                }
                else
                {
                    if (this.KeyboardAlarms.ContainsKey(key))
                    {
                        if (this.KeyboardAlarms[key] <= 0)
                        {
                            KeyboardAlarms[key] = frameSpeed;
                            return true;
                        }
                    }
                    else
                    {
                        this.KeyboardAlarms.Add(key, initialFrameSpeed);
                    }
                }
            }
            return false;
        }
        public bool CheckMousePressed(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    if (this.CurMouseState.LeftButton == ButtonState.Pressed && this.PrevMouseState.LeftButton == ButtonState.Released) { return true; }
                    break;

                case MouseButton.Right:
                    if (this.CurMouseState.RightButton == ButtonState.Pressed && this.PrevMouseState.RightButton == ButtonState.Released) { return true; }
                    break;
            }
            return false;
        }
        public bool CheckMouseReleased(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    if (this.CurMouseState.LeftButton == ButtonState.Released && this.PrevMouseState.LeftButton == ButtonState.Pressed) { return true; }
                    break;

                case MouseButton.Right:
                    if (this.CurMouseState.RightButton == ButtonState.Released && this.PrevMouseState.RightButton == ButtonState.Pressed) { return true; }
                    break;
            }
            return false;
        }
        public bool CheckMouseDown(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    if (this.CurMouseState.LeftButton == ButtonState.Pressed) { return true; }
                    break;

                case MouseButton.Right:
                    if (this.CurMouseState.RightButton == ButtonState.Pressed) { return true; }
                    break;
            }
            return false;
        }
        public bool CheckMouseDownInterval(MouseButton button, int initialFrameSpeed, int frameSpeed)
        {
            if (CheckMouseDown(button))
            {
                if (this.CheckMousePressed(button) == true)
                {
                    if (this.MouseAlarms.ContainsKey(button))
                    {
                        this.MouseAlarms[button] = initialFrameSpeed;
                    }
                    else
                    {
                        this.MouseAlarms.Add(button, initialFrameSpeed);
                    }
                    return true;
                }
                else
                {
                    if (this.MouseAlarms.ContainsKey(button))
                    {
                        if (this.MouseAlarms[button] <= 0)
                        {
                            MouseAlarms[button] = frameSpeed;
                            return true;
                        }
                    }
                    else
                    {
                        this.MouseAlarms.Add(button, initialFrameSpeed);
                    }
                }
            }
            return false;
        }
        public void Update()
        {
            PrevKeyboardState = CurKeyboardState;
            PrevMouseState = CurMouseState;

            CurKeyboardState = Keyboard.GetState();
            CurMouseState = Mouse.GetState();

            if (this.CheckMouseReleased(MouseButton.Left))
            {
                this.HandleLeftClick(CurMouseState.X, CurMouseState.Y);
            }
            if (this.CheckKeyboardPressed(Keys.Enter))
            {
                this.HandleEnter();
            }
            if (this.CheckKeyboardPressed(Keys.Escape))
            {
                this.HandleEscape();
            }
            if (this.CheckKeyboardPressed(Keys.D0))
            {
                this.HandleZero();
            }
            if (this.CheckKeyboardPressed(Keys.D9))
            {
                this.HandleNine();
            }
            if (this.CheckKeyboardPressed(Keys.D8))
            {
                Graphics.Current.GraphicsPresetIterate();
            }

            #region TEMP
            if (StateManager.Current.CurrentState == StateManager.State.World && Keyboard.GetState().IsKeyDown(Keys.R))
            {

                World.Current.Init(StateManager.Current.CurrentLevel);
            }
            #region DEBUG PURPOSES
            if (Admin.StateManager.Current.CurrentState == StateManager.State.World)
            {
                if (BuildingManager.Current.Selection == null && Keyboard.GetState().IsKeyDown(Keys.D1))
                {
                    BuildingManager.Current.Selection = new BuildingSelection(BuildingInfo.Type.STOCKPILE);
                    StateManager.Current.CurrentState = StateManager.State.BuildSelection;
                }
                if (BuildingManager.Current.Selection == null && Keyboard.GetState().IsKeyDown(Keys.D2))
                {
                    BuildingManager.Current.Selection = new BuildingSelection(BuildingInfo.Type.TOWER);
                    StateManager.Current.CurrentState = StateManager.State.BuildSelection;
                }
                if (BuildingManager.Current.Selection == null && Keyboard.GetState().IsKeyDown(Keys.D3))
                {
                    BuildingManager.Current.Selection = new BuildingSelection(BuildingInfo.Type.BOOTH_GEMS);
                    StateManager.Current.CurrentState = StateManager.State.BuildSelection;
                }
                if (BuildingManager.Current.Selection == null && Keyboard.GetState().IsKeyDown(Keys.D4))
                {
                    BuildingManager.Current.Selection = new BuildingSelection(BuildingInfo.Type.WOODCAMP);
                    StateManager.Current.CurrentState = StateManager.State.BuildSelection;
                }
            }
            #endregion
            #endregion

            #region Keyboard and Mouse Button Press Speed
            List<Keys> update = new List<Keys>();
            List<Keys> released = new List<Keys>();
            foreach (var alarm in this.KeyboardAlarms)
            {
                if (this.CurKeyboardState.IsKeyUp(alarm.Key))
                {
                    released.Add(alarm.Key);
                }
                else
                {
                    update.Add(alarm.Key);
                }
            }
            foreach (Keys key in update) { this.KeyboardAlarms[key]--; }
            foreach (Keys key in released) { this.KeyboardAlarms.Remove(key); }
            if (this.MouseAlarms.ContainsKey(MouseButton.Left))
            {
                if (this.CurMouseState.LeftButton == ButtonState.Released)
                {
                    this.MouseAlarms.Remove(MouseButton.Left);
                }
                else
                {
                    this.MouseAlarms[MouseButton.Left]--;
                }
            }
            if (this.MouseAlarms.ContainsKey(MouseButton.Right))
            {
                if (this.CurMouseState.RightButton == ButtonState.Released)
                {
                    this.MouseAlarms.Remove(MouseButton.Right);
                }
                else
                {
                    this.MouseAlarms[MouseButton.Right]--;
                }
            }
            #endregion
        }
        #endregion

        #region Internal
        private void HandleLeftClick(int x, int y)
        {
            StateManager.State curState = StateManager.Current.CurrentState;
            Vector2 worldPos = Camera.Current.ScreenToWorld(new Vector2(x, y));
            int worldX = (int)worldPos.X;
            int worldY = (int)worldPos.Y;

            MessagePopupMenu popupMenu = MenuManager.Current.TopScreenContainsType<MessagePopupMenu>() as MessagePopupMenu;
            if (popupMenu != null)
            {
                if (popupMenu.HandleLeftClick(x, y) == true) { return; }
            }

            switch (curState)
            {
                case StateManager.State.MainMenu:
                    {
                        // Check for buttons
                        break;
                    }

                case StateManager.State.Overworld:
                    {
                        // Check for buttons
                        // Check for Gui things
                        // Check for interactibles
                        break;
                    }

                case StateManager.State.BuildSelection:
                    {
                        if (BuildingManager.Current.Selection != null)
                        {
                            BuildingManager.Current.Selection.Build();
                            BuildingManager.Current.Selection = null;
                            StateManager.Current.CurrentState = StateManager.State.World;
                        }
                        break;
                    }


                case StateManager.State.World:
                    {
                        // todo: Help system / popups

                        // buttons:
                        WorldMenu worldMenu = MenuManager.Current.TopScreenContainsType<WorldMenu>() as WorldMenu;
                        if (worldMenu != null)
                        {
                            if (worldMenu.HandleLeftClick(x, y) == true) { return; }
                        }

                        // Resources:
                        foreach (Resource r in World.Current.Resources.Sorted.Values)
                        {
                            if (r.IsCollision(worldX, worldY))
                            {
                                r.HandleLeftClick();
                                return;
                            }
                        }


                        if (World.Current.InsideBounds(worldX, worldY))
                        {
                            // Buildings
                            foreach (Building building in BuildingManager.Current.AllBuildings)
                            {
                                if (building.IsCollision(worldX, worldY))
                                {
                                    building.HandleLeftClick();
                                    return;
                                }
                            }

                            // Tiles
                            Tile curTile = World.Current.TileAtPixel(worldX, worldY);
                            if (curTile != null)
                            {
                                curTile.HandleLeftClick();
                                return;
                            }
                        }
                        break;
                    }


                case StateManager.State.Pause:
                    {
                        foreach (Menu menu in MenuManager.Current.Top.MenuStack)
                        {
                            if (menu.HandleLeftClick(x, y) == true) { return; }
                        }
                        break;
                    }


                case StateManager.State.LevelEditor:
                    {
                        WorldMenu worldMenu = MenuManager.Current.TopScreenContainsType<WorldMenu>() as WorldMenu;
                        if (worldMenu != null)
                        {
                            if (worldMenu.HandleLeftClick(x, y) == true) { return; }
                        }

                        EditorTileMenu editorTileMenu = MenuManager.Current.TopScreenContainsType<EditorTileMenu>() as EditorTileMenu;
                        if (editorTileMenu != null)
                        {
                            if (editorTileMenu.HandleLeftClick(x, y) == true) { return; }
                        }

                        EditorSaveLevel editorSaveLevel = MenuManager.Current.TopScreenContainsType<EditorSaveLevel>() as EditorSaveLevel;
                        if (editorSaveLevel != null)
                        {
                            if (editorSaveLevel.HandleLeftClick(x, y) == true) { return; }
                        }
                        break;
                    }
            }

            Debugger.Current.AddTempString("[Clicked Empty]");
        }
        private void HandleEnter()
        {
            StateManager.State state = StateManager.Current.CurrentState;
            if (state == StateManager.State.World || state == StateManager.State.BuildSelection)
            {
                StateManager.Current.CurrentState = StateManager.State.Pause;
                MenuManager.Current.AddMenu(new PauseMenu(state), false, false);
            }
            if (state == StateManager.State.Pause)
            {
                PauseMenu pauseMenu = MenuManager.Current.TopScreenContainsType<PauseMenu>() as PauseMenu;
                if (pauseMenu != null)
                {
                    StateManager.Current.CurrentState = pauseMenu.OriginalState;
                    MenuManager.Current.CloseMenuType<PauseMenu>();
                }
            }
        }
        private void HandleEscape()
        {
            StateManager.State state = StateManager.Current.CurrentState;
            if (state == StateManager.State.World || state == StateManager.State.BuildSelection)
            {
                StateManager.Current.CurrentState = StateManager.State.Pause;
                MenuManager.Current.AddMenu(new PauseMenu(state), false, false);
                return;
            }
            if (state == StateManager.State.Pause)
            {

                PauseMenu pauseMenu = MenuManager.Current.TopScreenContainsType<PauseMenu>() as PauseMenu;
                if (pauseMenu != null)
                {
                    StateManager.Current.CurrentState = pauseMenu.OriginalState;
                    MenuManager.Current.CloseMenuType<PauseMenu>();
                    return;
                }

                ProfileMenu profileMenu = MenuManager.Current.TopScreenContainsType<ProfileMenu>() as ProfileMenu;
                if (profileMenu != null)
                {
                    StateManager.Current.CurrentState = profileMenu.OriginalState;
                    MenuManager.Current.CloseMenuType<ProfileMenu>();
                    return;
                }

                LevelSelectMenu levelSelectMenu = MenuManager.Current.TopScreenContainsType<LevelSelectMenu>() as LevelSelectMenu;
                if (levelSelectMenu != null)
                {
                    StateManager.Current.CurrentState = levelSelectMenu.OriginalState;
                    MenuManager.Current.CloseMenuType<LevelSelectMenu>();
                    return;
                }

                TestMenu testMenu = MenuManager.Current.TopScreenContainsType<TestMenu>() as TestMenu;
                if (testMenu != null)
                {
                    StateManager.Current.CurrentState = testMenu.OriginalState;
                    MenuManager.Current.CloseMenuType<TestMenu>();
                    return;
                }
            }
            if (MenuManager.Current.TopMenu != null)
            {
                MenuManager.Current.TopMenu.HandleEscape();
                return;
            }
        }
        private void HandleZero()
        {
            for (int i = 0; i < 1; i++)
            {
                Peep p = new Peep();
                p.Tasks.Clear();
                p.Tasks.Push(new WoodCampWork(p, 3, 30));
                World.Current.Peeps.Add(p);
            }
        }

        private void HandleNine()
        {
            for (int i = 0; i < 1; i++)
            {
                Threat t = new Threat();
                World.Current.Threats.Add(t);
            }
        }


        private Dictionary<Keys, int> KeyboardAlarms { get; set; }
        private Dictionary<MouseButton, int> MouseAlarms { get; set; }
        #endregion

    }
}
