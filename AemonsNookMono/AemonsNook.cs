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
        #region System
        private GraphicsDeviceManager GM;
        private SpriteBatch SB;
        public Dictionary<string, Texture2D> Sprites; // Todo: move this to singleton
        private Dictionary<string, SpriteFont> Fonts { get; set; } // Todo: move this to singleton
        #endregion

        #region Temp
        public Cursor mouse;
        int test;
        public World world { get; set; }
        public FrameCounter fps { get; set; }
        private int screenWidthPixels { get; set; }
        #endregion

        public AemonsNook()
        {
            GM = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Sprites = new Dictionary<string, Texture2D>(); // todo: move this to singleton
            Fonts = new Dictionary<string, SpriteFont>(); // todo: move this to singleton
            test = 0;
            fps = new FrameCounter();
        }



        protected override void Initialize()
        {
            this.mouse = new Cursor();
            this.screenWidthPixels = GraphicsDevice.Viewport.Width;


            base.Initialize(); // do this last
        }

        protected override void LoadContent()
        {
            SB = new SpriteBatch(GraphicsDevice);
            world = new World(10, 10, Sprites, SB); // Todo: update this to not pass in resource!
            Sprites.Add("grass-a", Content.Load<Texture2D>("World/Terrain/Grass-a")); // Todo: move this to singleton
            Fonts.Add("debug", Content.Load<SpriteFont>("Fonts/Consolas"));
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
            this.world.Draw(); // Todo: update this to use singleton!

            SB.Begin();

            // FPS
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            this.fps.Update(deltaTime);
            var average = string.Format("FPS: {0}", this.fps.AverageFramesPerSecond);
            SB.DrawString(Fonts["debug"], average, new Vector2(screenWidthPixels - 100, 4), Color.White);

            SB.End();


            base.Draw(gameTime);
        }
    }

    public class FrameCounter
    {
        public FrameCounter()
        {
        }

        public long TotalFrames { get; private set; }
        public float TotalSeconds { get; private set; }
        public float AverageFramesPerSecond { get; private set; }
        public float CurrentFramesPerSecond { get; private set; }

        public const int MAXIMUM_SAMPLES = 100;

        private Queue<float> _sampleBuffer = new Queue<float>();

        public bool Update(float deltaTime)
        {
            CurrentFramesPerSecond = 1.0f / deltaTime;

            _sampleBuffer.Enqueue(CurrentFramesPerSecond);

            if (_sampleBuffer.Count > MAXIMUM_SAMPLES)
            {
                _sampleBuffer.Dequeue();
                AverageFramesPerSecond = _sampleBuffer.Average(i => i);
            }
            else
            {
                AverageFramesPerSecond = CurrentFramesPerSecond;
            }

            TotalFrames++;
            TotalSeconds += deltaTime;
            return true;
        }
    }
}
