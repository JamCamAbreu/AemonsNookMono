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
        #region Constructors
        public AemonsNook()
        {
            Graphics.Current.GraphicsDM = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        #endregion

        #region Game Loop
        protected override void Initialize()
        {
            Graphics.Current.Init(this.GraphicsDevice, this.Window, false);
            StateManager.Current.Init();
            this.Window.AllowUserResizing = true;
            base.Initialize(); // do this last
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
        #endregion

        protected override void LoadContent()
        {
            Graphics.Current.SpriteB = new SpriteBatch(Graphics.Current.GraphicsDM.GraphicsDevice);

            Graphics.Current.SpritesByName.Add("grass-a", Content.Load<Texture2D>("World/Terrain/Grass-a"));

            // ------TEMP ------- //
            #region Temp
            Graphics.Current.SpritesByName.Add("building-temp1x1", Content.Load<Texture2D>("World/Buildings/TempBuilding1x1"));
            Graphics.Current.SpritesByName.Add("profile-portrait-temp", Content.Load<Texture2D>("Menus/Profile/profile"));
            #endregion
            // ------------------ //

            #region Buildings
            Graphics.Current.SpritesByName.Add("building-placement-green", Content.Load<Texture2D>("World/Buildings/TileGreen"));
            Graphics.Current.SpritesByName.Add("building-placement-orange", Content.Load<Texture2D>("World/Buildings/TileOrange"));
            Graphics.Current.SpritesByName.Add("building-placement-red", Content.Load<Texture2D>("World/Buildings/TileRed"));

            Graphics.Current.SpritesByName.Add("building-booth-fish", Content.Load<Texture2D>("World/Buildings/booth_fish"));
            Graphics.Current.SpritesByName.Add("building-booth-jewels", Content.Load<Texture2D>("World/Buildings/Booth_Jewels"));
            Graphics.Current.SpritesByName.Add("building-booth-produce", Content.Load<Texture2D>("World/Buildings/booth_produce"));
            Graphics.Current.SpritesByName.Add("building-booth-seeds", Content.Load<Texture2D>("World/Buildings/Booth_Seeds"));

            Graphics.Current.SpritesByName.Add("building-production-woodcamp", Content.Load<Texture2D>("World/Buildings/WoodCuttersCamp"));
            #endregion

            #region Cursor
            Graphics.Current.SpritesByName.Add("cursor-redx", Content.Load<Texture2D>("Cursor/RedX"));
            #endregion

            #region Debug
            Graphics.Current.SpritesByName.Add("debug-square", Content.Load<Texture2D>("Test/collisionSquare10by10"));
            Graphics.Current.SpritesByName.Add("debug-circle", Content.Load<Texture2D>("Test/collisionCircle10by10"));
            Graphics.Current.SpritesByName.Add("debug-tile-green", Content.Load<Texture2D>("Test/TileGreen"));
            Graphics.Current.SpritesByName.Add("debug-tile-orange", Content.Load<Texture2D>("Test/TileOrange"));
            Graphics.Current.SpritesByName.Add("debug-tile-red", Content.Load<Texture2D>("Test/TileRed"));
            Graphics.Current.SpritesByName.Add("debug-tower", Content.Load<Texture2D>("Test/TowerConcept"));
            #endregion

            #region Decoration
            Graphics.Current.SpritesByName.Add("decoration-flowers-1", Content.Load<Texture2D>("World/Terrain/Decoration/flowers-1"));
            Graphics.Current.SpritesByName.Add("decoration-flowers-2", Content.Load<Texture2D>("World/Terrain/Decoration/flowers-2"));
            Graphics.Current.SpritesByName.Add("decoration-flowers-3", Content.Load<Texture2D>("World/Terrain/Decoration/flowers-3"));
            Graphics.Current.SpritesByName.Add("decoration-flowers-4", Content.Load<Texture2D>("World/Terrain/Decoration/flowers-4"));
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

            #region Effects
            Graphics.Current.SpritesByName.Add("sparkle-1", Content.Load<Texture2D>("World/Effects/Sparkle/sparkle1"));
            Graphics.Current.SpritesByName.Add("sparkle-2", Content.Load<Texture2D>("World/Effects/Sparkle/sparkle2"));
            Graphics.Current.SpritesByName.Add("sparkle-3", Content.Load<Texture2D>("World/Effects/Sparkle/sparkle3"));
            Graphics.Current.SpritesByName.Add("sparkle-4", Content.Load<Texture2D>("World/Effects/Sparkle/sparkle4"));
            Graphics.Current.SpritesByName.Add("sparkle-5", Content.Load<Texture2D>("World/Effects/Sparkle/sparkle5"));

            Graphics.Current.SpritesByName.Add("SwordSwing-1", Content.Load<Texture2D>("World/Effects/AttackAnimations/swordswing-1"));
            Graphics.Current.SpritesByName.Add("SwordSwing-2", Content.Load<Texture2D>("World/Effects/AttackAnimations/swordswing-2"));
            Graphics.Current.SpritesByName.Add("SwordSwing-3", Content.Load<Texture2D>("World/Effects/AttackAnimations/swordswing-3"));
            Graphics.Current.SpritesByName.Add("SwordSwing-4", Content.Load<Texture2D>("World/Effects/AttackAnimations/swordswing-4"));
            Graphics.Current.SpritesByName.Add("SwordSwing-5", Content.Load<Texture2D>("World/Effects/AttackAnimations/swordswing-5"));
            #endregion

            #region Fonts
            Graphics.Current.Fonts.Add("debug", Content.Load<SpriteFont>("Fonts/Consolas"));
            Graphics.Current.Fonts.Add("arial", Content.Load<SpriteFont>("Fonts/Arial"));
            Graphics.Current.Fonts.Add("couriernew", Content.Load<SpriteFont>("Fonts/CourierNew"));
            #endregion

            #region Fun
            Graphics.Current.SpritesByName.Add("addy-ella", Content.Load<Texture2D>("Addy/Ella"));
            Graphics.Current.SpritesByName.Add("addy-cheeseburger", Content.Load<Texture2D>("Addy/cheeseburger"));
            #endregion

            #region Menus
            Graphics.Current.SpritesByName.Add("menu-bullet-selected", Content.Load<Texture2D>("Menus/BulletSelected"));
            Graphics.Current.SpritesByName.Add("menu-bullet-selected-hover", Content.Load<Texture2D>("Menus/BulletSelectedHover"));
            Graphics.Current.SpritesByName.Add("menu-bullet-unselected", Content.Load<Texture2D>("Menus/BulletUnselected"));
            Graphics.Current.SpritesByName.Add("menu-bullet-unselected-hover", Content.Load<Texture2D>("Menus/BulletUnselectedHover"));
            Graphics.Current.SpritesByName.Add("menu-bullet-unselected-click", Content.Load<Texture2D>("Menus/BulletUnselectedClick"));

            Graphics.Current.SpritesByName.Add("menu-world-check", Content.Load<Texture2D>("Menus/WorldMenu/buttonCheck"));
            Graphics.Current.SpritesByName.Add("menu-world-check-hover", Content.Load<Texture2D>("Menus/WorldMenu/buttonCheckHover"));
            Graphics.Current.SpritesByName.Add("menu-world-circle", Content.Load<Texture2D>("Menus/WorldMenu/buttonCircle"));
            Graphics.Current.SpritesByName.Add("menu-world-circle-hover", Content.Load<Texture2D>("Menus/WorldMenu/buttonCircleHover"));
            Graphics.Current.SpritesByName.Add("menu-world-diamond", Content.Load<Texture2D>("Menus/WorldMenu/buttonDiamond"));
            Graphics.Current.SpritesByName.Add("menu-world-diamond-hover", Content.Load<Texture2D>("Menus/WorldMenu/buttonDiamondHover"));
            Graphics.Current.SpritesByName.Add("menu-world-gear", Content.Load<Texture2D>("Menus/WorldMenu/buttonGear"));
            Graphics.Current.SpritesByName.Add("menu-world-gear-hover", Content.Load<Texture2D>("Menus/WorldMenu/buttonGearHover"));
            Graphics.Current.SpritesByName.Add("menu-world-pentagon", Content.Load<Texture2D>("Menus/WorldMenu/buttonPentagon"));
            Graphics.Current.SpritesByName.Add("menu-world-pentagon-hover", Content.Load<Texture2D>("Menus/WorldMenu/buttonPentagonHover"));
            Graphics.Current.SpritesByName.Add("menu-world-square", Content.Load<Texture2D>("Menus/WorldMenu/buttonSquare"));
            Graphics.Current.SpritesByName.Add("menu-world-square-hover", Content.Load<Texture2D>("Menus/WorldMenu/buttonSquareHover"));

            Graphics.Current.SpritesByName.Add("menu-arrow-right", Content.Load<Texture2D>("Menus/WorldMenu/Right"));
            Graphics.Current.SpritesByName.Add("menu-arrow-right-hover", Content.Load<Texture2D>("Menus/WorldMenu/RightHover"));
            Graphics.Current.SpritesByName.Add("menu-arrow-left", Content.Load<Texture2D>("Menus/WorldMenu/Left"));
            Graphics.Current.SpritesByName.Add("menu-arrow-left-hover", Content.Load<Texture2D>("Menus/WorldMenu/LeftHover"));
            #endregion

            #region Other
            Graphics.Current.SpritesByName.Add("tile-outline", Content.Load<Texture2D>("World/Terrain/TileOutline"));
            #endregion

            #region Peeps
            Graphics.Current.SpritesByName.Add("hero", Content.Load<Texture2D>("World/Peeps/hero"));
            Graphics.Current.SpritesByName.Add("char", Content.Load<Texture2D>("World/Peeps/$char"));
            Graphics.Current.SpritesByName.Add("peep-royal", Content.Load<Texture2D>("World/Peeps/Peep-royal"));
            #endregion

            #region Stockpiles
            Graphics.Current.SpritesByName.Add("building-stockpile", Content.Load<Texture2D>("World/Buildings/Stockpiles/Stockpile"));

            Graphics.Current.SpritesByName.Add("stockpile-wood-a", Content.Load<Texture2D>("World/Buildings/Stockpiles/WoodStockA"));
            Graphics.Current.SpritesByName.Add("stockpile-wood-b", Content.Load<Texture2D>("World/Buildings/Stockpiles/WoodStockB"));
            Graphics.Current.SpritesByName.Add("stockpile-wood-c", Content.Load<Texture2D>("World/Buildings/Stockpiles/WoodStockC"));
            Graphics.Current.SpritesByName.Add("stockpile-wood-d", Content.Load<Texture2D>("World/Buildings/Stockpiles/WoodStockD"));

            Graphics.Current.SpritesByName.Add("stockpile-stone-a", Content.Load<Texture2D>("World/Buildings/Stockpiles/StoneStockA"));
            Graphics.Current.SpritesByName.Add("stockpile-stone-b", Content.Load<Texture2D>("World/Buildings/Stockpiles/StoneStockB"));
            Graphics.Current.SpritesByName.Add("stockpile-stone-c", Content.Load<Texture2D>("World/Buildings/Stockpiles/StoneStockC"));
            Graphics.Current.SpritesByName.Add("stockpile-stone-d", Content.Load<Texture2D>("World/Buildings/Stockpiles/StoneStockD"));
            #endregion

            #region Stone
            Graphics.Current.SpritesByName.Add("stone-harvest", Content.Load<Texture2D>("World/Terrain/Stone/StoneHarvest"));
            Graphics.Current.SpritesByName.Add("stone-1", Content.Load<Texture2D>("World/Terrain/Stone/Stone1"));
            Graphics.Current.SpritesByName.Add("stone-2", Content.Load<Texture2D>("World/Terrain/Stone/Stone2"));
            Graphics.Current.SpritesByName.Add("stone-3", Content.Load<Texture2D>("World/Terrain/Stone/Stone3"));
            Graphics.Current.SpritesByName.Add("stone-4", Content.Load<Texture2D>("World/Terrain/Stone/Stone4"));
            Graphics.Current.SpritesByName.Add("stone-5", Content.Load<Texture2D>("World/Terrain/Stone/Stone5"));
            Graphics.Current.SpritesByName.Add("stone-6", Content.Load<Texture2D>("World/Terrain/Stone/Stone6"));
            #endregion

            #region Threats
            Graphics.Current.SpritesByName.Add("threats-head", Content.Load<Texture2D>("World/Threats/Head"));
            #endregion

            #region Trees
            Graphics.Current.SpritesByName.Add("tree-harvest", Content.Load<Texture2D>("World/Terrain/Trees/TreeHarvest"));
            Graphics.Current.SpritesByName.Add("tree-1", Content.Load<Texture2D>("World/Terrain/Trees/Tree1"));
            Graphics.Current.SpritesByName.Add("tree-2", Content.Load<Texture2D>("World/Terrain/Trees/Tree2"));
            Graphics.Current.SpritesByName.Add("tree-3", Content.Load<Texture2D>("World/Terrain/Trees/Tree3"));
            Graphics.Current.SpritesByName.Add("tree-4", Content.Load<Texture2D>("World/Terrain/Trees/Tree4"));
            Graphics.Current.SpritesByName.Add("tree-5", Content.Load<Texture2D>("World/Terrain/Trees/Tree5"));
            Graphics.Current.SpritesByName.Add("tree-6", Content.Load<Texture2D>("World/Terrain/Trees/Tree6"));
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
        }

    }

}
