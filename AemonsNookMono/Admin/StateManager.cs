using AemonsNookMono.GameWorld;
using AemonsNookMono.GameWorld.Effects;
using AemonsNookMono.Levels;
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
            Pause,
            Exit
        }
        #endregion

        #region Singleton Implementation
        private static StateManager instance;
        private static object _lock = new object();
        private StateManager()
        {
            this.CurrentState = State.MainMenu;
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

            this.level = new Level1();
            //this.level = new Level2();

            Buildings.Current.Init();
            World.Current.Init(this.level);
        }
        public void Update(GameTime gameTime)
        {
            #region TEMP
            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                World.Current.Init(this.level);
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.CurrentState = State.Exit;
            }
            #endregion

            Cursor.Current.Update(gameTime);
            Debugger.Current.Update(gameTime);
            World.Current.Update(gameTime);
            Buildings.Current.Update();
            EffectsGenerator.Current.Update();
        }
        public void Draw(GameTime gameTime)
        {
            World.Current.Draw();
            Buildings.Current.Draw();
            EffectsGenerator.Current.Draw();
            Debugger.Current.Draw(gameTime);
            Cursor.Current.Draw();
        }
        #endregion


    }
}
