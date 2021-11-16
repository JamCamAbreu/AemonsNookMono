using AemonsNookMono.GameWorld;
using AemonsNookMono.GameWorld.Effects;
using AemonsNookMono.Levels;
using AemonsNookMono.Menus;
using AemonsNookMono.Player;
using AemonsNookMono.Structures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Admin
{
    public class StateManager
    {
        #region Enums
        public enum State
        {
            MainMenu,
            Overworld,
            World,
            BuildSelection,
            Pause,
            LevelEditor,
            Exit
        }
        #endregion

        #region Singleton Implementation
        private static StateManager instance;
        private static object _lock = new object();
        private StateManager()
        {
            this.CurrentState = State.World;
        }
        public static StateManager Current
        {
            get
            {
                if (instance == null)
                {
                    lock (_lock)
                    {
                        if (instance == null)
                        {
                            instance = new StateManager();
                        }
                    }
                }
                return instance;
            }
        }
        #endregion

        #region Public Properties
        public State CurrentState { get; set; }
        #endregion

        #region Temp
        public Level level { get; set; }
        #endregion

        #region Interface
        public void Init()
        {
            Debugger.Current.Init();
            Cursor.Current.Init();
            ProfileManager.Current.Init();

            this.level = new SmallMeadow();
            //this.level = new Level2();

            Buildings.Current.Init();
            World.Current.Init(this.level);

            
        }
        public void Update(GameTime gameTime)
        {
            if (this.CurrentState != State.Pause)
            {
                #region TEMP
                if (Keyboard.GetState().IsKeyDown(Keys.R))
                {
                    World.Current.Init(this.level);
                }               
                #endregion
                
                World.Current.Update(gameTime);
                Buildings.Current.Update();
                EffectsGenerator.Current.Update();
                Cursor.Current.Update(gameTime);
            }
            MenuManager.Current.Update();
            Debugger.Current.Update(gameTime);
            InputManager.Current.Update();
            Camera.Current.Update();
        }
        public void Draw(GameTime gameTime)
        {
            // Use camera transform:
            World.Current.Draw();
            Buildings.Current.Draw();
            EffectsGenerator.Current.Draw();
            Cursor.Current.Draw();

            // Does not use camera
            MenuManager.Current.Draw();
            Debugger.Current.Draw(gameTime);
        }
        #endregion


    }
}
