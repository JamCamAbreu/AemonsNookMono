using AemonsNookMono.Admin;
using AemonsNookMono.GameWorld;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Player
{
    public class Hero
    {
        public enum FacingDirection
        {
            Up,
            Right,
            Left,
            Down
        }

        public Hero()
        {
            if (World.Current.SpawnTiles == null || World.Current.SpawnTiles.Count <= 0) 
            {
                //throw new Exception("No where to spawn! Oh my!"); 
                Debugger.Current.AddTempString("Could not spawn hero. No entrance!");
                this.Spawned = false;
                return;
            }

            this.Ran = new Random();
            this.TileOn = World.Current.SpawnTiles[Ran.Next(0, World.Current.SpawnTiles.Count - 1)];
            this.ScreenX = World.Current.StartDrawX + this.TileOn.RelativeX;
            this.ScreenY = World.Current.StartDrawY + this.TileOn.RelativeY;
            this.Speed = 1;

            this.Direction = FacingDirection.Right;

            DownAnimations = new Rectangle[]
            {
                new Rectangle(0, 0, 16, 16),
                new Rectangle(16, 0, 16, 16),
                new Rectangle(32, 0, 16, 16)
            };

            LeftAnimations = new Rectangle[]
            {
                new Rectangle(0, 16, 16, 16),
                new Rectangle(16, 16, 16, 16),
                new Rectangle(32, 16, 16, 16)
            };

            RightAnimations = new Rectangle[]
            {
                new Rectangle(0, 32, 16, 16),
                new Rectangle(16, 32, 16, 16),
                new Rectangle(32, 32, 16, 16)
            };

            UpAnimations = new Rectangle[]
            {
                new Rectangle(0, 48, 16, 16),
                new Rectangle(16, 48, 16, 16),
                new Rectangle(32, 48, 16, 16)
            };

            this.AnimationTimer = 0;
            this.AnimationTimerReset = 10;
            this.Spawned = true;
        }
        public bool Spawned { get; set; }

        public Tile TileOn { get; set; }
        public int ScreenX { get; set; }
        public int ScreenY { get; set; }

        public int Speed { get; set; }
        public int VerticalSpeed { get; set; }
        public int HorizontalSpeed { get; set; }
        public Random Ran { get; set; }
        public FacingDirection Direction { get; set; }
        
        Rectangle[] DownAnimations { get; set; }
        Rectangle[] LeftAnimations { get; set; }
        Rectangle[] RightAnimations { get; set; }
        Rectangle[] UpAnimations { get; set; }
        protected int AnimationTimer { get; set; }
        protected int AnimationTimerReset { get; set; }
        protected int SpriteIndex { get; set; }

        public void Update()
        {
            if (!Spawned) { return; }

            this.SetMovementVector();
            this.Move();
        }

        public void Draw()
        {
            if (!Spawned) { return; }

            Rectangle spriteBox = RightAnimations[1];
            switch (this.Direction)
            {
                case FacingDirection.Up:
                    spriteBox = UpAnimations[this.SpriteIndex];
                    break;
                case FacingDirection.Right:
                    spriteBox = RightAnimations[this.SpriteIndex];
                    break;
                case FacingDirection.Left:
                    spriteBox = LeftAnimations[this.SpriteIndex];
                    break;
                case FacingDirection.Down:
                    spriteBox = DownAnimations[this.SpriteIndex];
                    break;
            }

            Graphics.Current.SpriteB.Draw(Graphics.Current.SpritesByName["char"], new Vector2(this.ScreenX, this.ScreenY), spriteBox, Color.White);
        }

        protected void SetMovementVector()
        {
            if (InputManager.Current.CheckKeyboardDown(Microsoft.Xna.Framework.Input.Keys.W))
            {
                this.VerticalSpeed = -Speed;
                this.Direction = FacingDirection.Up;
            }
            if (InputManager.Current.CheckKeyboardDown(Microsoft.Xna.Framework.Input.Keys.S))
            {
                this.VerticalSpeed = Speed;
                this.Direction = FacingDirection.Down;
            }
            if (InputManager.Current.CheckKeyboardDown(Microsoft.Xna.Framework.Input.Keys.A))
            {
                this.HorizontalSpeed = -Speed;
                this.Direction = FacingDirection.Left;
            }
            if (InputManager.Current.CheckKeyboardDown(Microsoft.Xna.Framework.Input.Keys.D))
            {
                this.HorizontalSpeed = Speed;
                this.Direction = FacingDirection.Right;
            }

            if (!InputManager.Current.CheckKeyboardDown(Microsoft.Xna.Framework.Input.Keys.W) && !InputManager.Current.CheckKeyboardDown(Microsoft.Xna.Framework.Input.Keys.S))
            {
                this.VerticalSpeed = 0;
            }
            if (!InputManager.Current.CheckKeyboardDown(Microsoft.Xna.Framework.Input.Keys.A) && !InputManager.Current.CheckKeyboardDown(Microsoft.Xna.Framework.Input.Keys.D))
            {
                this.HorizontalSpeed = 0;
            }

            if (!InputManager.Current.CheckKeyboardDown(Microsoft.Xna.Framework.Input.Keys.W) &&
                !InputManager.Current.CheckKeyboardDown(Microsoft.Xna.Framework.Input.Keys.S) &&
                !InputManager.Current.CheckKeyboardDown(Microsoft.Xna.Framework.Input.Keys.A) &&
                !InputManager.Current.CheckKeyboardDown(Microsoft.Xna.Framework.Input.Keys.D))
            {
                this.AnimationTimer = 0;
                this.SpriteIndex = 0;
            }
            else
            {
                this.AnimationTimer++;
            }
            if (this.AnimationTimer >= this.AnimationTimerReset)
            {
                this.AnimationTimer = 0;
                this.SpriteIndex++;

                if (this.SpriteIndex > 2)
                {
                    this.SpriteIndex = 0;
                }
            }
        }
        protected void Move()
        {
            this.ScreenX += HorizontalSpeed;
            this.ScreenY += VerticalSpeed;
        }
    }
}
