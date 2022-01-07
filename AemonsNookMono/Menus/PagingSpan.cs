using AemonsNookMono.Admin;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Menus
{
    public class PagingSpan : Cell
    {
        #region Constructor
        public PagingSpan(string uniquename, Span.SpanType type, int maxButtons = 5)
        {
            this.UniqueName = uniquename;
            this.InnerSpan = new Span(type);
            this.RawCells = new List<Cell>();
            this.MaxCells = maxButtons;
        }
        public PagingSpan(string uniquename, int padWidth, int padHeight, Span.SpanType type, int maxButtons = 5)
        {
            this.UniqueName = uniquename;
            this.InnerSpan = new Span(padWidth, padHeight, type);
            this.RawCells = new List<Cell>();
            this.MaxCells = maxButtons;
        }
        public PagingSpan(string uniquename, int x, int y, int width, int height, int padWidth, int padHeight, Span.SpanType type, int maxButtons = 5)
        {
            this.UniqueName = uniquename;
            this.InnerSpan = new Span(x, y, width, height, padWidth, padHeight, type);
            this.RawCells = new List<Cell>();
            this.MaxCells = maxButtons;
        }
        #endregion

        #region Public Properties
        public string UniqueName { get; set; }
        public Span InnerSpan { get; set; }
        public int PageNumber { get; private set; }

        #endregion

        #region Internal
        protected List<Cell> RawCells { get; set; }
        protected int MaxCells { get; set; }
        #endregion

        #region Interface
        public void AddDynamicCell(Cell cell)
        {
            this.RawCells.Add(cell);
            this.Init();
        }

        public void PageNext()
        {
            if ((this.PageNumber + 1) * this.MaxCells < this.RawCells.Count)
            {
                this.PageNumber++;
                this.Init();
            }
        }

        public void PagePrev()
        {
            if (this.PageNumber > 0)
            {
                this.PageNumber--;
                this.Init();
            }
        }

        public override void Draw()
        {
            this.InnerSpan?.Draw();
        }

        public override void Refresh(int width, int height, int screenx, int screeny)
        {
            this.InnerSpan?.Refresh(width, height, screenx, screeny);
        }

        public override void Update()
        {
            this.InnerSpan?.Update();
        }

        public Button GetButton(string name)
        {
            return this.InnerSpan?.GetButton(name) ?? null;
        }
        public List<Button> AllButtons()
        {
            return this.InnerSpan?.AllButtons() ?? new List<Button>();
        }

        public bool ContainsButton(string name)
        {
            if (this.InnerSpan == null) return false;
            return this.InnerSpan.ContainsButton(name);
        }

        /// <returns>This will return any button codes you added to the span, but also the special 'PagePrev' and 'PageNext' codes.</returns>
        public Button CheckButtonCollisions(int x, int y)
        {
            return this.InnerSpan?.CheckButtonCollisions(x, y) ?? null;
        }

        protected virtual void Init()
        {
            this.InnerSpan.Cells.Clear();
            if (this.PageNumber > 0)
            {
                // Todo: change left to 'up' depending on the span direction (vertical)
                ButtonSprite left = new ButtonSprite("menu-arrow-left", "menu-arrow-left-hover", "menu-arrow-left-hover", 32, 26);
                this.InnerSpan.AddSpriteButton($"{this.UniqueName}-PagePrev", "", left, Collision.CollisionShape.Rectangle);
            }
            else { this.InnerSpan.AddColorButton("", "", null, false); }
            for (int i = this.PageNumber * this.MaxCells; i < (this.PageNumber + 1) * this.MaxCells; i++)
            {
                if (i < this.RawCells.Count)
                {
                    this.InnerSpan.AddDynamicCell(this.RawCells[i]);
                    this.InnerSpan.AddBlank();
                    this.RawCells[i].Active = true;
                }
                else { this.InnerSpan.AddColorButton("", "", null, false); }
            }
            if ((this.PageNumber + 1) * this.MaxCells < this.RawCells.Count)
            {
                // Todo: change left to 'down' depending on the span direction (vertical)
                ButtonSprite right = new ButtonSprite("menu-arrow-right", "menu-arrow-right-hover", "menu-arrow-right-hover", 32, 26);
                this.InnerSpan.AddSpriteButton($"{this.UniqueName}-PageNext", "", right, Collision.CollisionShape.Rectangle);
            }
            else { this.InnerSpan.AddColorButton("", "", null, false); }
            this.InnerSpan.InnerPadScale = 0.0f;
        }

        public void AddBlank()
        {
            this.AddColorButton("", "", null, false);
        }
        public Button AddColorButton(string code, string title, Color? primaryColor, bool active = true)
        {
            Button created = new Button(code, title, 0, 0, 20, 20, null, primaryColor, Collision.CollisionShape.Rectangle, active && !string.IsNullOrEmpty(code));
            this.AddDynamicCell(created);
            return created;
        }
        public Button AddSpriteButton(string code, string title, ButtonSprite sprites, Collision.CollisionShape shape, bool active = true)
        {
            Button created = new Button(code, title, 0, 0, this.InnerSpan.PadWidth * 2, this.InnerSpan.PadHeight * 2, sprites, null, shape, active && !string.IsNullOrEmpty(code));
            this.AddDynamicCell(created);
            return created;
        }
        public Textbox AddText(string text, string font = "couriernew", Color? background = null, Textbox.HorizontalAlign horzAlign = Textbox.HorizontalAlign.Center, Textbox.VerticalAlign vertAlign = Textbox.VerticalAlign.Center, bool active = true)
        {
            Textbox created = new Textbox(text, 0, 0, 8, 8, font, background, horzAlign, vertAlign, active);
            this.AddDynamicCell(created);
            return created;
        }
        public SpriteSimple AddSprite(string sprite, int spritewidth, int spriteheight)
        {
            SpriteSimple simple = new SpriteSimple(sprite, spritewidth, spriteheight, 0, 0, 1, 1);
            this.AddDynamicCell(simple);
            return simple;
        }
        public SpriteAnimated AddAnimatedSprite(List<string> sprites, int framesTillUpdate, int spritewidth, int spriteheight)
        {
            SpriteAnimated animated = new SpriteAnimated(sprites, spritewidth, spriteheight, framesTillUpdate, 0, 0, 1, 1);
            this.AddDynamicCell(animated);
            return animated;
        }
        public TextInput AddTextInput(string code, string initialtext, Color? primaryColor, bool active = true)
        {
            TextInput created = new TextInput(code, initialtext, 20, 0, 0, 20, 20, null, primaryColor, active && !string.IsNullOrEmpty(code));
            this.AddDynamicCell(created);
            return created;
        }
        public void AddSpan(Span span)
        {
            this.AddDynamicCell(span);
        }
        #endregion
    }
}
