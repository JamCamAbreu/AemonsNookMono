using AemonsNookMono.Admin;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Menus.World
{
    public class TestMenu : Menu
    {
        #region Constructor
        public TestMenu(StateManager.State originalState) :
            base("Test Menu",
                  (int)((float)Graphics.Current.ScreenWidth * 0.7f),
                  (int)((float)Graphics.Current.ScreenHeight * 0.7f),
                  Graphics.Current.ScreenMidX,
                  Graphics.Current.ScreenMidY,
                  ((int)((float)Graphics.Current.ScreenWidth * 0.7f) / 16),
                  (int)((float)Graphics.Current.ScreenHeight * 0.7f) / 16,
                  Color.SaddleBrown,
                  string.Empty)
        {
            this.configurableButtonSize = 4;

            this.bottomRowButtons = new List<Cell>();
            this.bottomRowPage = 0;
            this.bottomRowNumButtons = 3;
            for (int i = 0; i < 33; i++)
            {
                Button test = new Button($"Item {i}", $"Item {i}", 0, 0, 1, 1, null, Color.Purple, Collision.CollisionShape.Rectangle, false);
                this.bottomRowButtons.Add(test);
            }

            this.InitButtons();
            this.OriginalState = originalState;
        }
        #endregion

        #region Public Properties
        public StateManager.State OriginalState { get; set; }
        #endregion

        #region Interface
        public override void InitButtons()
        {
            this.Spans.Clear();
            Span rows = new Span(this.CenterX, this.CenterY, this.Width, this.Height, this.PadWidth, this.PadHeight, Span.SpanType.Vertical);
            rows.AddText("Here are some rows");

            Span rowTwo = new Span(Span.SpanType.Horizontal);
            rowTwo.AddText("Static Button:");
            rowTwo.AddButtonColor("Button", "Button", Color.Red);
            rowTwo.AddText("Sprite:");
            rowTwo.AddSprite("building-booth-fish", 64, 32);
            rowTwo.AddText("Sprite Button:");
            ButtonSprite check = new ButtonSprite("menu-world-check", "menu-world-check-hover", "menu-world-check-hover", 32, 32);
            rowTwo.AddButtonSprite("Check Button", "", check, Collision.CollisionShape.Rectangle);
            rowTwo.AddText("Animated:");
            List<string> animation = new List<string>() { "menu-world-circle", "menu-world-diamond", "menu-world-pentagon", "menu-world-square" };
            rowTwo.AddAnimatedSprite(animation, 32, 32, 30);
            rows.AddSpan(rowTwo);


            Span rowThree = new Span(Span.SpanType.Horizontal);
            Span configurable = new Span(Span.SpanType.Vertical);
            configurable.AddButtonColor("configplus", "+", Color.DarkOliveGreen);
            configurable.AddButtonColor("configminus", "-", Color.DarkOliveGreen);
            rowThree.AddSpan(configurable);
            for (int i = 0; i < this.configurableButtonSize; i++)
            {
                rowThree.AddButtonColor(((char)(65 + i)).ToString(), ((char)(65 + i)).ToString(), Color.Green);
            }
            rows.AddSpan(rowThree);

            rows.AddText("Here's an example of paging:");
            Span rowFive = new Span(Span.SpanType.Horizontal);
            if (bottomRowPage > 0)
            {
                ButtonSprite left = new ButtonSprite("menu-arrow-left", "menu-arrow-left-hover", "menu-arrow-left-hover", 32, 26);
                rowFive.AddButtonSprite("Left", "", left, Collision.CollisionShape.Rectangle);
            }
            else { rowFive.AddButtonColor("", "", null, false); }
            for (int i = this.bottomRowPage * this.bottomRowNumButtons; i < (this.bottomRowPage + 1) * this.bottomRowNumButtons; i++)
            {
                if (i < this.bottomRowButtons.Count)
                {
                    rowFive.AddDynamicCell(this.bottomRowButtons[i]);
                    this.bottomRowButtons[i].Active = true;
                }
                else { rowFive.AddButtonColor("", "", null, false); }
            }
            if ((this.bottomRowPage + 1) * this.bottomRowNumButtons < this.bottomRowButtons.Count)
            {
                ButtonSprite right = new ButtonSprite("menu-arrow-right", "menu-arrow-right-hover", "menu-arrow-right-hover", 32, 26);
                rowFive.AddButtonSprite("Right", "", right, Collision.CollisionShape.Rectangle);
            }
            else { rowFive.AddButtonColor("", "", null, false); }
            rows.AddSpan(rowFive);

            rows.AddButtonColor("Back", "Back", Color.Black);

            this.Spans.Add(rows);
            foreach (Span span in this.Spans)
            {
                span.Refresh();
            }
        }
        public override void Refresh()
        {
            this.Height = (int)((float)Graphics.Current.ScreenHeight * 0.7f); // Todo: match constructor
            this.Width = (int)((float)Graphics.Current.ScreenWidth * 0.7f);
            this.CenterX = Graphics.Current.ScreenMidX;
            this.CenterY = Graphics.Current.ScreenMidY;
            this.PadHeight = ((int)((float)Graphics.Current.ScreenHeight * 0.7f) / 16);
            this.PadWidth = (int)((float)Graphics.Current.ScreenWidth * 0.7f) / 16;
            base.Refresh();
            this.InitButtons();
        }
        public override bool HandleLeftClick(int x, int y)
        {
            Button clicked = this.CheckButtonCollisions(x, y);
            if (clicked != null)
            {
                Debugger.Current.AddTempString($"You clicked on the {clicked.ButtonCode} button!");
                switch (clicked.ButtonCode)
                {
                    case "Back":
                        StateManager.Current.CurrentState = this.OriginalState;
                        MenuManager.Current.CloseTop();
                        return true;

                    case "Left":
                        bottomRowPage--;
                        foreach (Button b in this.bottomRowButtons) { b.Active = false; }
                        this.InitButtons();
                        return true;

                    case "Right":
                        bottomRowPage++;
                        foreach (Button b in this.bottomRowButtons) { b.Active = false; }
                        this.InitButtons();
                        return true;

                    case "configplus":
                        if (this.configurableButtonSize < 20)
                        {
                            this.configurableButtonSize++;
                            this.InitButtons();
                        }
                        return true;

                    case "configminus":
                        if (this.configurableButtonSize > 0)
                        {
                            this.configurableButtonSize--;
                            this.InitButtons();
                        }
                        return true;

                    default:
                        return base.HandleLeftClick(x, y);
                }
            }
            return base.HandleLeftClick(x, y);
        }
        #endregion

        #region Internal
        protected List<Cell> bottomRowButtons { get; set; }
        protected int bottomRowPage { get; set; }
        protected int bottomRowNumButtons { get; set; }
        protected int configurableButtonSize { get; set; }
        #endregion
    }
}
