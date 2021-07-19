using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using AemonsNookMono.GameWorld;
using System.Linq;

namespace AemonsNookMono
{
    public class AemonsNook : Game
    {

        #region Temp
        public World world { get; set; }
        #endregion

        public AemonsNook()
        {
            Graphics.Current().GraphicsDM = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Graphics.Current().Init(this.GraphicsDevice);
            Debugger.Current().Init();
            Cursor.Current().Init();
            this.world = new World(30, 20);


            base.Initialize(); // do this last
        }

        protected override void LoadContent()
        {
            Graphics.Current().SpriteB = new SpriteBatch(Graphics.Current().GraphicsDM.GraphicsDevice);
            
            Graphics.Current().Sprites.Add("grass-a", Content.Load<Texture2D>("World/Terrain/Grass-a"));
            Graphics.Current().Fonts.Add("debug", Content.Load<SpriteFont>("Fonts/Consolas"));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Cursor.Current().Update(gameTime);
            Debugger.Current().Update(gameTime);

            base.Update(gameTime); // Do last
        }

        protected override void Draw(GameTime gameTime)
        {
            this.world.Draw();
            Debugger.Current().Draw(gameTime);
            Cursor.Current().Draw();
            base.Draw(gameTime);
        }
    }

}
