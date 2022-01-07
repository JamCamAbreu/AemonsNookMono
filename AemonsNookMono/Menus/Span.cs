using AemonsNookMono.Admin;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AemonsNookMono.Menus
{
    public class Span : Cell
    {
        #region Enums
        public enum SpanType
        {
            Horizontal,
            Vertical
        }
        #endregion

        #region Constructor
        public Span (SpanType type)
        {
            this.Type = type;
            this.CenterX = 0;
            this.CenterY = 0;
            this.Width = 1;
            this.Height = 1;
            this.PadWidth = 0;
            this.PadHeight = 0;
            this.Cells = new List<Cell>();
            this.InnerPadScale = 0.5f;
        }
        public Span(int padWidth, int padHeight, SpanType type)
        {
            this.Type = type;
            this.CenterX = 0;
            this.CenterY = 0;
            this.Width = padWidth * 2;
            this.Height = padHeight * 2;
            this.PadWidth = padWidth;
            this.PadHeight = padHeight;
            this.Cells = new List<Cell>();
            this.InnerPadScale = 0.5f;
        }
        public Span(int x, int y, int width, int height, int padWidth, int padHeight, SpanType type)
        {
            this.Type = type;
            this.CenterX = x;
            this.CenterY = y;
            this.Width = width;
            this.Height = height;
            this.PadWidth = padWidth;
            this.PadHeight = padHeight;
            this.Cells = new List<Cell>();
            this.InnerPadScale = 0.5f;
        }
        #endregion

        #region Public Properties
        public SpanType Type { get; set; }
        public int CenterX { get; set; }
        public int CenterY { get; set; }
        public int PadWidth { get; set; }
        public int PadHeight { get; set; }
        public List<Cell> Cells { get; set; }
        public int TopY { get { return this.CenterY - (this.Height / 2); } }
        public int LeftX { get { return this.CenterX - (this.Width / 2); } }
        public float InnerPadScale { get; set; }
        #endregion

        #region Interface
        public void AddDynamicCell(Cell cell)
        {
            int numCells = this.Cells.Count + 1;
            int cellHeight = this.Height - (this.PadHeight * 2); // default
            int cellWidth = this.Width - (this.PadWidth * 2); // default
            int cellPadWidth = 0;
            int cellPadHeight = 0;

            if (this.Type == SpanType.Vertical)
            {
                int innerHeight = this.Height - (this.PadHeight * 2);
                cellPadHeight = (int)((innerHeight / ((numCells * 2) - 1)) * InnerPadScale);
                cellHeight = (innerHeight - (cellPadHeight * (numCells - 1))) / numCells;
            }
            else if (this.Type == SpanType.Horizontal)
            {
                int innerWidth = this.Width - (this.PadWidth * 2);
                cellPadWidth = (int)((innerWidth / ((numCells * 2) - 1)) * InnerPadScale);
                cellWidth = (innerWidth - (cellPadWidth * (numCells - 1))) / numCells;
            }

            #region Update Prior Cells
            int cellNum = 0;
            foreach (Cell priorCell in this.Cells)
            {
                CellDimension dim = this.CalculateDimensions(cellWidth, cellPadWidth, cellHeight, cellPadHeight, cellNum);
                priorCell.Refresh(dim.Width, dim.Height, dim.CenterX, dim.CenterY);
                cellNum++;
            }
            #endregion

            #region New Cell
            if (cell != null)
            {
                CellDimension dim = this.CalculateDimensions(cellWidth, cellPadWidth, cellHeight, cellPadHeight, cellNum);
                cell.Refresh(dim.Width, dim.Height, dim.CenterX, dim.CenterY);                
                this.Cells.Add(cell);
            }
            #endregion
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
            Button created = new Button(code, title, 0, 0, this.PadWidth * 2, this.PadHeight * 2, sprites, null, shape, active && !string.IsNullOrEmpty(code));
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
            //this.ChildSpans.Add(span);
        }
        public override void Draw()
        {
            foreach (Cell cell in this.Cells)
            {
                cell.Draw();
            }
            //foreach (Span span in this.ChildSpans)
            //{
            //    span.Draw();
            //}
        }
        public override void Update()
        {
            foreach (Cell cell in this.Cells)
            {
                cell.Update();
            }
            //foreach (Span span in this.ChildSpans)
            //{
            //    span.Update();
            //}
        }
        public override void Refresh(int width, int height, int screenx, int screeny)
        {
            this.Width = width;
            this.Height = height;
            this.CenterX = screenx;
            this.CenterY = screeny;

            int numCells = this.Cells.Count;
            int cellHeight = this.Height - (this.PadHeight * 2); // default
            int cellWidth = this.Width - this.PadWidth * 2; // default
            int cellPadWidth = 0;
            int cellPadHeight = 0;

            if (this.Type == SpanType.Vertical)
            {
                int innerHeight = this.Height - (this.PadHeight * 2);
                cellPadHeight = (int)((innerHeight / ((numCells * 2) - 1)) * InnerPadScale);
                cellHeight = (innerHeight - (cellPadHeight * (numCells - 1))) / numCells;
            }
            else if (this.Type == SpanType.Horizontal)
            {
                int innerWidth = this.Width - (this.PadWidth * 2);
                cellPadWidth = (int)((innerWidth / ((numCells * 2) - 1)) * InnerPadScale);
                cellWidth = (innerWidth - (cellPadWidth * (numCells - 1))) / numCells;
            }

            int cellNum = 0;
            foreach (Cell cell in this.Cells)
            {
                CellDimension dim = this.CalculateDimensions(cellWidth, cellPadWidth, cellHeight, cellPadHeight, cellNum);
                cell.Refresh(dim.Width, dim.Height, dim.CenterX, dim.CenterY);
                cellNum++;
            }
        }
        public void Refresh()
        {
            this.Refresh(this.Width, this.Height, this.CenterX, this.CenterY);
        }
        public Button GetButton(string name)
        {
            foreach (Button b in this.Cells.Where(cell => cell is Button))
            {
                if (b.ButtonCode == name) { return b; }
            }
            foreach (Span span in this.Cells.Where(cell => cell is Span))
            {
                if (span.ContainsButton(name)) { return span.GetButton(name); }
            }
            return null;
        }
        public List<Button> AllButtons()
        {
            List<Button> buttons = new List<Button>();
            foreach (Button button in this.Cells.Where(cell => cell is Button))
            {
                buttons.Add(button);
            }
            //foreach (Span span in this.ChildSpans)
            //{
            //    buttons.AddRange(span.AllButtons());
            //}
            return buttons;
        }
        public bool ContainsButton(string name)
        {
            foreach (Button b in this.Cells.Where(cell => cell is Button))
            {
                if (b.ButtonCode == name) { return true; }
            }
            foreach (Span span in this.Cells.Where(cell => cell is Span))
            {
                if (span.ContainsButton(name)) { return true; }
            }
            return false;
        }
        public Button CheckButtonCollisions(int x, int y)
        {
            foreach (Button b in this.Cells.Where(cell => cell is Button))
            {
                if (b.Active && b.MyCollision != null && b.MyCollision.IsCollision(x, y))
                {
                    return b;
                }
            }
            Button button;
            foreach (Span span in this.Cells.Where(cell => cell is Span))
            {
                button = span.CheckButtonCollisions(x, y);
                if (button != null) { return button; }
            }
            return null;
        }
        #endregion

        #region Internal
        protected CellDimension CalculateDimensions(int cellWidth, int cellPadWidth, int cellHeight, int cellPadHeight, int index)
        {
            int calcX = this.CenterX;
            int calcY = this.CenterY;
            if (this.Type == SpanType.Vertical)
            {
                calcY = this.TopY + this.PadHeight + (cellHeight / 2) + (index * cellHeight) + (cellPadHeight * index);
            }
            else if (this.Type == SpanType.Horizontal)
            {
                calcX = this.LeftX + this.PadWidth + (cellWidth / 2) + (index * cellWidth) + (cellPadWidth * index);
            }
            return new CellDimension(cellWidth, cellHeight, calcX, calcY);
        }
        #endregion
    }
}
