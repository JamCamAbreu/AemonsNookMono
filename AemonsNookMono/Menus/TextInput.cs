using AemonsNookMono.Admin;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Menus
{
    public class TextInput : Button
    {
        #region Constructor
        public TextInput(string code, string initialText, int maxChars, int x, int y, int width, int height, ButtonSprite sprites, Color? color, bool active = true) : base(code, initialText, x, y, width, height, sprites, color, Collision.CollisionShape.Rectangle, active)
        {
            this.MaxCharacters = maxChars;
        }
        #endregion

        #region Public Properties
        public int MaxCharacters { get; set; }
        #endregion

        #region Interface
        public override void Draw()
        {
            base.Draw();
        }
        public override void Refresh(int width, int height, int screenx, int screeny)
        {
            base.Refresh(width, height, screenx, screeny);
        }
        public override void Update()
        {
            if (this.Selected)
            {
                this.CheckInput();
            }

            base.Update();
        }
        #endregion

        #region Internal
        protected void CheckInput()
        {
            InputManager input = InputManager.Current;
            if (this.Title.Length < this.MaxCharacters)
            {
                if (input.CheckKeyboardDown(Keys.LeftShift) || input.CheckKeyboardDown(Keys.RightShift))
                {
                    if (input.CheckKeyboardPressed(Keys.Q)) { this.Title += 'Q'; }
                    if (input.CheckKeyboardPressed(Keys.W)) { this.Title += 'W'; }
                    if (input.CheckKeyboardPressed(Keys.E)) { this.Title += 'E'; }
                    if (input.CheckKeyboardPressed(Keys.R)) { this.Title += 'R'; }
                    if (input.CheckKeyboardPressed(Keys.T)) { this.Title += 'T'; }
                    if (input.CheckKeyboardPressed(Keys.Y)) { this.Title += 'Y'; }
                    if (input.CheckKeyboardPressed(Keys.U)) { this.Title += 'U'; }
                    if (input.CheckKeyboardPressed(Keys.I)) { this.Title += 'I'; }
                    if (input.CheckKeyboardPressed(Keys.O)) { this.Title += 'O'; }
                    if (input.CheckKeyboardPressed(Keys.P)) { this.Title += 'P'; }

                    if (input.CheckKeyboardPressed(Keys.A)) { this.Title += 'A'; }
                    if (input.CheckKeyboardPressed(Keys.S)) { this.Title += 'S'; }
                    if (input.CheckKeyboardPressed(Keys.D)) { this.Title += 'D'; }
                    if (input.CheckKeyboardPressed(Keys.F)) { this.Title += 'F'; }
                    if (input.CheckKeyboardPressed(Keys.G)) { this.Title += 'G'; }
                    if (input.CheckKeyboardPressed(Keys.H)) { this.Title += 'H'; }
                    if (input.CheckKeyboardPressed(Keys.J)) { this.Title += 'J'; }
                    if (input.CheckKeyboardPressed(Keys.K)) { this.Title += 'K'; }
                    if (input.CheckKeyboardPressed(Keys.L)) { this.Title += 'L'; }

                    if (input.CheckKeyboardPressed(Keys.Z)) { this.Title += 'Z'; }
                    if (input.CheckKeyboardPressed(Keys.X)) { this.Title += 'X'; }
                    if (input.CheckKeyboardPressed(Keys.C)) { this.Title += 'C'; }
                    if (input.CheckKeyboardPressed(Keys.V)) { this.Title += 'V'; }
                    if (input.CheckKeyboardPressed(Keys.B)) { this.Title += 'B'; }
                    if (input.CheckKeyboardPressed(Keys.N)) { this.Title += 'N'; }
                    if (input.CheckKeyboardPressed(Keys.M)) { this.Title += 'M'; }
                }
                else
                {
                    if (input.CheckKeyboardPressed(Keys.Q)) { this.Title += 'q'; }
                    if (input.CheckKeyboardPressed(Keys.W)) { this.Title += 'w'; }
                    if (input.CheckKeyboardPressed(Keys.E)) { this.Title += 'e'; }
                    if (input.CheckKeyboardPressed(Keys.R)) { this.Title += 'r'; }
                    if (input.CheckKeyboardPressed(Keys.T)) { this.Title += 't'; }
                    if (input.CheckKeyboardPressed(Keys.Y)) { this.Title += 'y'; }
                    if (input.CheckKeyboardPressed(Keys.U)) { this.Title += 'u'; }
                    if (input.CheckKeyboardPressed(Keys.I)) { this.Title += 'i'; }
                    if (input.CheckKeyboardPressed(Keys.O)) { this.Title += 'o'; }
                    if (input.CheckKeyboardPressed(Keys.P)) { this.Title += 'p'; }

                    if (input.CheckKeyboardPressed(Keys.A)) { this.Title += 'a'; }
                    if (input.CheckKeyboardPressed(Keys.S)) { this.Title += 's'; }
                    if (input.CheckKeyboardPressed(Keys.D)) { this.Title += 'd'; }
                    if (input.CheckKeyboardPressed(Keys.F)) { this.Title += 'f'; }
                    if (input.CheckKeyboardPressed(Keys.G)) { this.Title += 'g'; }
                    if (input.CheckKeyboardPressed(Keys.H)) { this.Title += 'h'; }
                    if (input.CheckKeyboardPressed(Keys.J)) { this.Title += 'j'; }
                    if (input.CheckKeyboardPressed(Keys.K)) { this.Title += 'k'; }
                    if (input.CheckKeyboardPressed(Keys.L)) { this.Title += 'l'; }

                    if (input.CheckKeyboardPressed(Keys.Z)) { this.Title += 'z'; }
                    if (input.CheckKeyboardPressed(Keys.X)) { this.Title += 'x'; }
                    if (input.CheckKeyboardPressed(Keys.C)) { this.Title += 'c'; }
                    if (input.CheckKeyboardPressed(Keys.V)) { this.Title += 'v'; }
                    if (input.CheckKeyboardPressed(Keys.B)) { this.Title += 'b'; }
                    if (input.CheckKeyboardPressed(Keys.N)) { this.Title += 'n'; }
                    if (input.CheckKeyboardPressed(Keys.M)) { this.Title += 'm'; }
                }

                if (input.CheckKeyboardPressed(Keys.D1)) { this.Title += '1'; }
                if (input.CheckKeyboardPressed(Keys.D2)) { this.Title += '2'; }
                if (input.CheckKeyboardPressed(Keys.D3)) { this.Title += '3'; }
                if (input.CheckKeyboardPressed(Keys.D4)) { this.Title += '4'; }
                if (input.CheckKeyboardPressed(Keys.D5)) { this.Title += '5'; }
                if (input.CheckKeyboardPressed(Keys.D6)) { this.Title += '6'; }
                if (input.CheckKeyboardPressed(Keys.D7)) { this.Title += '7'; }
                if (input.CheckKeyboardPressed(Keys.D8)) { this.Title += '8'; }
                if (input.CheckKeyboardPressed(Keys.D9)) { this.Title += '9'; }
                if (input.CheckKeyboardPressed(Keys.D0)) { this.Title += '0'; }

                if (input.CheckKeyboardPressed(Keys.Space)) { this.Title += ' '; }
            }

            if (input.CheckKeyboardPressed(Keys.Back)) 
            { 
                if (!string.IsNullOrEmpty(this.Title))
                {
                    this.Title = this.Title.Remove(this.Title.Length - 1, 1);
                }
            }
            if (input.CheckKeyboardPressed(Keys.Enter))
            {
                this.Selected = false;
            }
        }
        #endregion
    }
}
