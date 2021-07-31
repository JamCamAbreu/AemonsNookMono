using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using AemonsNookMono.GameWorld;
using System.Linq;
using AemonsNookMono.Levels;
using AemonsNookMono.GameWorld.Effects;
using AemonsNookMono.Structures;
using AemonsNookMono.Admin;

namespace AemonsNookMono
{
    public class AemonsNook : Game
    {
        public AemonsNook()
        {
            Graphics.Current.GraphicsDM = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Graphics.Current.Init(this.GraphicsDevice, false);
            StateManager.Current.Init();

            base.Initialize(); // do this last
        }

        protected override void LoadContent()
        {
            Graphics.Current.SpriteB = new SpriteBatch(Graphics.Current.GraphicsDM.GraphicsDevice);
            
            Graphics.Current.SpritesByName.Add("grass-a", Content.Load<Texture2D>("World/Terrain/Grass-a"));

            #region Temp
            Graphics.Current.SpritesByName.Add("building-temp1x1", Content.Load<Texture2D>("World/Buildings/TempBuilding1x1"));
            #endregion

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

            #region Effects
            Graphics.Current.SpritesByName.Add("sparkle-1", Content.Load<Texture2D>("World/Effects/Sparkle/sparkle1"));
            Graphics.Current.SpritesByName.Add("sparkle-2", Content.Load<Texture2D>("World/Effects/Sparkle/sparkle2"));
            Graphics.Current.SpritesByName.Add("sparkle-3", Content.Load<Texture2D>("World/Effects/Sparkle/sparkle3"));
            Graphics.Current.SpritesByName.Add("sparkle-4", Content.Load<Texture2D>("World/Effects/Sparkle/sparkle4"));
            Graphics.Current.SpritesByName.Add("sparkle-5", Content.Load<Texture2D>("World/Effects/Sparkle/sparkle5"));
            #endregion

            #region Decoration
            Graphics.Current.SpritesByName.Add("decoration-flowers-1", Content.Load<Texture2D>("World/Terrain/Decoration/flowers-1"));
            Graphics.Current.SpritesByName.Add("decoration-flowers-2", Content.Load<Texture2D>("World/Terrain/Decoration/flowers-2"));
            Graphics.Current.SpritesByName.Add("decoration-flowers-3", Content.Load<Texture2D>("World/Terrain/Decoration/flowers-3"));
            Graphics.Current.SpritesByName.Add("decoration-flowers-4", Content.Load<Texture2D>("World/Terrain/Decoration/flowers-4"));
            #endregion

            #region Buildings
            Graphics.Current.SpritesByName.Add("building-placement-green", Content.Load<Texture2D>("World/Buildings/TileGreen"));
            Graphics.Current.SpritesByName.Add("building-placement-orange", Content.Load<Texture2D>("World/Buildings/TileOrange"));
            Graphics.Current.SpritesByName.Add("building-placement-red", Content.Load<Texture2D>("World/Buildings/TileRed"));

            Graphics.Current.SpritesByName.Add("building-booth-fish", Content.Load<Texture2D>("World/Buildings/booth_fish"));
            Graphics.Current.SpritesByName.Add("building-booth-jewels", Content.Load<Texture2D>("World/Buildings/Booth_Jewels"));
            Graphics.Current.SpritesByName.Add("building-booth-produce", Content.Load<Texture2D>("World/Buildings/booth_produce"));
            Graphics.Current.SpritesByName.Add("building-booth-seeds", Content.Load<Texture2D>("World/Buildings/Booth_Seeds"));
            #endregion

            Graphics.Current.Fonts.Add("debug", Content.Load<SpriteFont>("Fonts/Consolas"));
            Graphics.Current.Fonts.Add("arial", Content.Load<SpriteFont>("Fonts/Arial"));
            Graphics.Current.Fonts.Add("couriernew", Content.Load<SpriteFont>("Fonts/CourierNew"));
        }

        protected override void Update(GameTime gameTime)
        {
            StateManager.Current.Update(gameTime);

            if (StateManager.Current.CurrentState == StateManager.State.Exit)
            {
                Exit();
            }

            base.Update(gameTime); // Do last
        }

        protected override void Draw(GameTime gameTime)
        {
            StateManager.Current.Draw(gameTime);
        }
    }

}
