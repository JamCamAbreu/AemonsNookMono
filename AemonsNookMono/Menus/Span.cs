using AemonsNookMono.Admin;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AemonsNookMono.Menus
{
    public class Span
    {
        #region Enums
        public enum SpanType
        {
            Horizontal,
            Vertical
        }
        #endregion

        #region Constructor
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
        }
        #endregion

        #region Public Properties
        public SpanType Type { get; set; }
        public int CenterX { get; set; }
        public int CenterY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int PadWidth { get; set; }
        public int PadHeight { get; set; }
        public List<Cell> Cells { get; set; }
        public int TopY { get { return this.CenterY - (this.Height / 2); } }
        public int LeftX { get { return this.CenterX - (this.Width / 2); } }
        #endregion

        #region Interface
        public void AddDynamicCell(Cell cell)
        {
            int numCells = this.Cells.Count + 1;
            int cellHeight = this.Height - (this.PadHeight * 2); // default
            int cellWidth = this.Width - this.PadWidth * 2; // default
            if (this.Type == SpanType.Vertical)
            {
                cellHeight = (this.Height - this.PadHeight * 2) / ((numCells * 2) - 1);
            }
            else if (this.Type == SpanType.Horizontal)
            {
                cellWidth = (this.Width - this.PadWidth * 2) / ((numCells * 2) - 1);
            }

            #region Update Prior Cells
            int cellNum = 0;
            foreach (Cell priorCell in this.Cells)
            {
                CellDimension dim = this.CalculateDimensions(cellWidth, cellHeight, cellNum);
                priorCell.Refresh(dim.Width, dim.Height, dim.CenterX, dim.CenterY);
                cellNum += 2;
            }
            #endregion

            #region New Cell
            if (cell != null)
            {
                CellDimension dim = this.CalculateDimensions(cellWidth, cellHeight, cellNum);
                cell.Refresh(dim.Width, dim.Height, dim.CenterX, dim.CenterY);                
                this.Cells.Add(cell);
            }
            #endregion
        }
        public void AddButtonColor(string code, string title, Color? primaryColor, bool active = true)
        {
            Button created = new Button(code, title, 0, 0, 8, 8, null, primaryColor, Collision.CollisionShape.Rectangle, active && !string.IsNullOrEmpty(code));
            this.AddDynamicCell(created);
        }
        public void AddButtonSprite(string code, string title, ButtonSprite sprites, Collision.CollisionShape shape, bool active = true)
        {
            Button created = new Button(code, title, 0, 0, this.PadWidth * 2, this.PadHeight * 2, sprites, null, shape, active && !string.IsNullOrEmpty(code));
            this.AddDynamicCell(created);
        }
        public void AddText(string text, string font = "couriernew", Color? background = null, Textbox.HorizontalAlign horzAlign = Textbox.HorizontalAlign.Center, Textbox.VerticalAlign vertAlign = Textbox.VerticalAlign.Center, bool active = true)
        {
            Textbox created = new Textbox(text, 0, 0, 8, 8, font, background, horzAlign, vertAlign, active);
            this.AddDynamicCell(created);
        }
        public void Draw()
        {
            foreach (Cell b in this.Cells)
            {
                b.Draw();
            }
        }
        public Button GetButton(string name)
        {
            foreach (Button b in this.Cells.Where(cell => cell is Button))
            {
                if (b.ButtonCode == name) { return b; }
            }
            return null;
        }
        public bool ContainsButton(string name)
        {
            foreach (Button b in this.Cells.Where(cell => cell is Button))
            {
                if (b.ButtonCode == name) { return true; }
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
            return null;
        }
        #endregion

        #region Internal
        protected CellDimension CalculateDimensions(int cellWidth, int cellHeight, int index)
        {
            int calcX = this.CenterX;
            int calcY = this.CenterY;
            if (this.Type == SpanType.Vertical)
            {
                calcY = this.TopY + this.PadHeight + (cellHeight / 2) + (index * cellHeight);
            }
            else if (this.Type == SpanType.Horizontal)
            {
                calcX = this.LeftX + this.PadWidth + (cellWidth / 2) + (index * cellWidth);
            }
            return new CellDimension(cellWidth, cellHeight, calcX, calcY);
        }
        #endregion
    }
}
