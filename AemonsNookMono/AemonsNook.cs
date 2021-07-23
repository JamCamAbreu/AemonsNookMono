using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using AemonsNookMono.GameWorld;
using System.Linq;
using AemonsNookMono.Levels;

namespace AemonsNookMono
{
    public class AemonsNook : Game
    {

        #region Temp
        public Level level { get; set; }
        #endregion

        public AemonsNook()
        {
            Graphics.Current.GraphicsDM = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Graphics.Current.Init(this.GraphicsDevice, false);
            Debugger.Current.Init();
            Cursor.Current.Init();

            this.level = new Level1();
            World.Current.InitWorld(this.level);

            base.Initialize(); // do this last
        }

        protected override void LoadContent()
        {
            Graphics.Current.SpriteB = new SpriteBatch(Graphics.Current.GraphicsDM.GraphicsDevice);
            
            Graphics.Current.SpritesByName.Add("grass-a", Content.Load<Texture2D>("World/Terrain/Grass-a"));

            #region Dirt
            Graphics.Current.SpritesByName.Add("dirt-corner-bottomleft", Content.Load<Texture2D>("World/Terrain/Dirt/Dirt-Corner-BottomLeft"));
            Graphics.Current.SpritesByName.Add("dirt-corner-bottomright", Content.Load<Texture2D>("World/Terrain/Dirt/Dirt-Corner-BottomRight"));
            Graphics.Current.SpritesByName.Add("dirt-corner-topleft", Content.Load<Texture2D>("World/Terrain/Dirt/Dirt-CornerTopLeft"));
            Graphics.Current.SpritesByName.Add("dirt-corner-topright", Content.Load<Texture2D>("World/Terrain/Dirt/Dirt-CornerTopRight"));

            Graphics.Current.SpritesByName.Add("dirt-horizontal", Content.Load<Texture2D>("World/Terrain/Dirt/Dirt-Horizontal"));
            Graphics.Current.SpritesByName.Add("dirt-vertical", Content.Load<Texture2D>("World/Terrain/Dirt/Dirt-Vertical"));
            Graphics.Current.SpritesByName.Add("dirt-intersection", Content.Load<Texture2D>("World/Terrain/Dirt/Dirt-Intersection"));

            Graphics.Current.SpritesByName.Add("dirt-ts-bottom", Content.Load<Texture2D>("World/Terrain/Dirt/Dirt-TS-Bottom"));
            Graphics.Current.SpritesByName.Add("dirt-ts-left", Content.Load<Texture2D>("World/Terrain/Dirt/Dirt-TS-Left"));
            Graphics.Current.SpritesByName.Add("dirt-ts-right", Content.Load<Texture2D>("World/Terrain/Dirt/Dirt-TS-Right"));
            Graphics.Current.SpritesByName.Add("dirt-ts-top", Content.Load<Texture2D>("World/Terrain/Dirt/Dirt-TS-Top"));
            #endregion

            #region Water
            Graphics.Current.SpritesByName.Add("water-corner-bottomleft", Content.Load<Texture2D>("World/Terrain/Water/Water-Corner-BottomLeft"));
            Graphics.Current.SpritesByName.Add("water-corner-bottomright", Content.Load<Texture2D>("World/Terrain/Water/Water-Corner-BottomRight"));
            Graphics.Current.SpritesByName.Add("water-corner-topleft", Content.Load<Texture2D>("World/Terrain/Water/Water-Corner-TopLeft"));
            Graphics.Current.SpritesByName.Add("water-corner-topright", Content.Load<Texture2D>("World/Terrain/Water/Water-Corner-TopRight"));

            Graphics.Current.SpritesByName.Add("water-horizontal", Content.Load<Texture2D>("World/Terrain/Water/Water-Horizontal"));
            Graphics.Current.SpritesByName.Add("water-vertical", Content.Load<Texture2D>("World/Terrain/Water/Water-Vertical"));
            Graphics.Current.SpritesByName.Add("water-intersection", Content.Load<Texture2D>("World/Terrain/Water/Water-Intersection"));

            Graphics.Current.SpritesByName.Add("water-ts-bottom", Content.Load<Texture2D>("World/Terrain/Water/Water-TS-Bottom"));
            Graphics.Current.SpritesByName.Add("water-ts-left", Content.Load<Texture2D>("World/Terrain/Water/Water-TS-Left"));
            Graphics.Current.SpritesByName.Add("water-ts-right", Content.Load<Texture2D>("World/Terrain/Water/Water-TS-Right"));
            Graphics.Current.SpritesByName.Add("water-ts-top", Content.Load<Texture2D>("World/Terrain/Water/Water-TS-Top"));
            #endregion

            #region Trees
            Graphics.Current.SpritesByName.Add("tree-1", Content.Load<Texture2D>("World/Terrain/Trees/Tree1"));
            Graphics.Current.SpritesByName.Add("tree-2", Content.Load<Texture2D>("World/Terrain/Trees/Tree2"));
            Graphics.Current.SpritesByName.Add("tree-3", Content.Load<Texture2D>("World/Terrain/Trees/Tree3"));
            Graphics.Current.SpritesByName.Add("tree-4", Content.Load<Texture2D>("World/Terrain/Trees/Tree4"));
            Graphics.Current.SpritesByName.Add("tree-5", Content.Load<Texture2D>("World/Terrain/Trees/Tree5"));
            Graphics.Current.SpritesByName.Add("tree-6", Content.Load<Texture2D>("World/Terrain/Trees/Tree6"));
            #endregion

            #region Stone
            Graphics.Current.SpritesByName.Add("stone-1", Content.Load<Texture2D>("World/Terrain/Stone/Stone1"));
            Graphics.Current.SpritesByName.Add("stone-2", Content.Load<Texture2D>("World/Terrain/Stone/Stone2"));
            Graphics.Current.SpritesByName.Add("stone-3", Content.Load<Texture2D>("World/Terrain/Stone/Stone3"));
            Graphics.Current.SpritesByName.Add("stone-4", Content.Load<Texture2D>("World/Terrain/Stone/Stone4"));
            Graphics.Current.SpritesByName.Add("stone-5", Content.Load<Texture2D>("World/Terrain/Stone/Stone5"));
            Graphics.Current.SpritesByName.Add("stone-6", Content.Load<Texture2D>("World/Terrain/Stone/Stone6"));
            #endregion

            Graphics.Current.Fonts.Add("debug", Content.Load<SpriteFont>("Fonts/Consolas"));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            #region TEST
            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                World.Current.InitWorld(this.level);
            }
            #endregion

            Cursor.Current.Update(gameTime);
            Debugger.Current.Update(gameTime);

            base.Update(gameTime); // Do last
        }

        protected override void Draw(GameTime gameTime)
        {
            World.Current.Draw();
            Debugger.Current.Draw(gameTime);
            Cursor.Current.Draw();
            base.Draw(gameTime);
        }
    }

}
