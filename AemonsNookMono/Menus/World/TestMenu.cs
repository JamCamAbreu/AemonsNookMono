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
                  null,
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
            this.StaticCells.Clear();

            Span rows = new Span(this.CenterX, this.CenterY, this.Width, this.Height, this.PadWidth, this.PadHeight, Span.SpanType.Vertical);
            rows.AddColorButton("blank", "", null, false);
            rows.AddText("Here is a test page to test out and show off the interface used to design menus. Menus can use both static (fixed) position objects as well as dynamic (calculated) positions. Dynamic objects are placed inside 'span' objects, that can either span vertically (rows) or horizontally (columns). They are also nestable, spans inside of spans, etc..). This page features all of those examples.");
            rows.AddColorButton("blank2", "", null, false);

            Span rowTwo = new Span(Span.SpanType.Horizontal);
            rowTwo.AddText("Simple Button:");
            rowTwo.AddColorButton("Button", "Button", Color.Red);
            rowTwo.AddText("Sprite:");
            rowTwo.AddSprite("building-booth-fish", 64, 32);
            rowTwo.AddText("Sprite Button:");
            ButtonSprite check = new ButtonSprite("menu-world-check", "menu-world-check-hover", "menu-world-check-hover", 32, 32);
            rowTwo.AddSpriteButton("Check Button", "", check, Collision.CollisionShape.Rectangle);
            rowTwo.AddText("Animated:");
            List<string> animation = new List<string>() { "menu-world-circle", "menu-world-diamond", "menu-world-pentagon", "menu-world-square" };
            rowTwo.AddAnimatedSprite(animation, 32, 32, 30);
            rows.AddSpan(rowTwo);

            Span rowThree = new Span(Span.SpanType.Horizontal);
            rowThree.AddColorButton("row3placeholder", "", null, false);
            rows.AddSpan(rowThree);

            rows.AddText("Here's an example of paging:");
            Span rowFive = new Span(Span.SpanType.Horizontal);
            if (bottomRowPage > 0)
            {
                ButtonSprite left = new ButtonSprite("menu-arrow-left", "menu-arrow-left-hover", "menu-arrow-left-hover", 32, 26);
                rowFive.AddSpriteButton("Left", "", left, Collision.CollisionShape.Rectangle);
            }
            else { rowFive.AddColorButton("", "", null, false); }
            for (int i = this.bottomRowPage * this.bottomRowNumButtons; i < (this.bottomRowPage + 1) * this.bottomRowNumButtons; i++)
            {
                if (i < this.bottomRowButtons.Count)
                {
                    rowFive.AddDynamicCell(this.bottomRowButtons[i]);
                    this.bottomRowButtons[i].Active = true;
                }
                else { rowFive.AddColorButton("", "", null, false); }
            }
            if ((this.bottomRowPage + 1) * this.bottomRowNumButtons < this.bottomRowButtons.Count)
            {
                ButtonSprite right = new ButtonSprite("menu-arrow-right", "menu-arrow-right-hover", "menu-arrow-right-hover", 32, 26);
                rowFive.AddSpriteButton("Right", "", right, Collision.CollisionShape.Rectangle);
            }
            else { rowFive.AddColorButton("", "", null, false); }
            rowFive.InnerPadScale = 0.0f;
            rows.AddSpan(rowFive);

            rows.AddColorButton("Back", "Back", Color.Black);

            this.Spans.Add(rows);
            foreach (Span span in this.Spans)
            {
                span.Refresh();
            }

            // Static Cells:
            int configWidth = 40;
            Span configurable = new Span(rowThree.LeftX + (configWidth/2), rowThree.CenterY, configWidth, rowThree.Height, 0, 0, Span.SpanType.Vertical);
            configurable.AddColorButton("configplus", "+", Color.DarkOliveGreen);
            configurable.AddColorButton("configminus", "-", Color.DarkOliveGreen);
            this.AddStaticSpan(configurable);

            int manyButtonsWidth = rowThree.Width - configWidth;
            Span manyButtons = new Span(rowThree.LeftX + configWidth + manyButtonsWidth / 2, rowThree.CenterY, manyButtonsWidth, rowThree.Height, 40, 0, Span.SpanType.Horizontal);
            for (int i = 0; i < this.configurableButtonSize; i++)
            {
                manyButtons.AddColorButton(((char)(65 + i)).ToString(), ((char)(65 + i)).ToString(), Color.Green);
            }
            this.AddStaticSpan(manyButtons);
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
                        SaveManager.Current.SaveProfile(ProfileManager.Current.Loaded);
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
                        return false;
                }
            }
            return false;
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
