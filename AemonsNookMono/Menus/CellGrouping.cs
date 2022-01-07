using AemonsNookMono.Admin;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AemonsNookMono.Menus
{
    public interface CellGrouping
    {
        public void Draw();
        public void Refresh(int width, int height, int screenx, int screeny);
        public void Update();
        public void AddDynamicCell(Cell cell);
        public Button GetButton(string name);
        public List<Button> AllButtons();
        public bool ContainsButton(string name);
        public Button CheckButtonCollisions(int x, int y);
        public void AddBlank();
        public Button AddColorButton(string code, string title, Color? primaryColor, bool active = true);
        public Button AddSpriteButton(string code, string title, ButtonSprite sprites, Collision.CollisionShape shape, bool active = true);
        public Textbox AddText(string text, string font = "couriernew", Color? background = null, Textbox.HorizontalAlign horzAlign = Textbox.HorizontalAlign.Center, Textbox.VerticalAlign vertAlign = Textbox.VerticalAlign.Center, bool active = true);
        public SpriteSimple AddSprite(string sprite, int spritewidth, int spriteheight);
        public SpriteAnimated AddAnimatedSprite(List<string> sprites, int framesTillUpdate, int spritewidth, int spriteheight);
        public TextInput AddTextInput(string code, string initialtext, Color? primaryColor, bool active = true);
        public void AddSpan(Span span);
        public void AddPagingSpan(PagingSpan pagingSpan);
    }
}
