using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace AemonsNookMono
{
    public class AemonsNook : Game
    {
        private GraphicsDeviceManager GM;
        private SpriteBatch SB;

        public Dictionary<string, Texture2D> Sprites;
        public Cursor mouse;
        int test;


        public AemonsNook()
        {
            GM = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Sprites = new Dictionary<string, Texture2D>();

            test = 0;
        }



        protected override void Initialize()
        {
            this.mouse = new Cursor();

            base.Initialize(); // do this last
        }

        protected override void LoadContent()
        {
            SB = new SpriteBatch(GraphicsDevice);
            Sprites.Add("grass-a", Content.Load<Texture2D>("World/Terrain/Grass-a"));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            this.test = this.mouse.Update(gameTime);

            base.Update(gameTime); // Do last
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            SB.Begin();
            SB.Draw(Sprites["grass-a"], new Vector2(50, 50), Color.White);

            SB.End();

            base.Draw(gameTime);
        }
    }
}
