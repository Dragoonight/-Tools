using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
//Class name
namespace VTP18
{
    
    public class Game1 : Microsoft.Xna.Framework.Game
    #region Variables
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Refering to playermanager class
        PlayerManager playerSprite;
        //Refering to Starfield class
        Starfield starField;
        //Refering to BackgroundManager class
        BackgroundManager backgroundImage;
        //Refering to BackgroundManager class
        BackgroundManager2 backgroundImage2;
        //Refering to EnemyManager class
        EnemyManager enemyManager;
        //Refering to CololisionManager class
        CollisionsManager collisionManager;
        //Refering to ExplosionManager class
        ExplosionManager explosionManager;
        //Variable Texture
        Texture2D mixedSprites;       
        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        #region Initialize
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            

            base.Initialize();
        }
        #endregion
        #region Loadcontent
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Loads Mixed sprites
            mixedSprites = Content.Load<Texture2D>(@"Images/Mixed");
            //Loads Starfiedls star texture
            starField = new Starfield(this.Window.ClientBounds.Width, this.Window.ClientBounds.Height, 200, new Vector2(0, 500f), mixedSprites, new Rectangle(0, 450, 2, 2));

            Rectangle screenBounds = new Rectangle(0, 0, this.Window.ClientBounds.Width, this.Window.ClientBounds.Height);
            playerSprite = new PlayerManager(Content.Load<Texture2D>(@"Images/mixed"), 1, 32, 48, screenBounds);

            //Loads and locates Enemt sprite from Mixed sprites
            enemyManager = new EnemyManager(mixedSprites, new Rectangle(0, 200, 50, 50), 6, playerSprite, screenBounds);

            //Loads Explosion sprite from mixed sprites
            explosionManager = new ExplosionManager(mixedSprites, new Rectangle(0, 100, 50, 50), 3, new Rectangle(0, 450, 2, 2));
            //Loads Collision properties
            collisionManager = new CollisionsManager(playerSprite, explosionManager, enemyManager);

            //Loads Background Image from images 
            backgroundImage = new BackgroundManager(Content.Load<Texture2D>(@"Images/HWB2"), 0, 0, 0, screenBounds);
            //Loads Background2 Image from images 
            backgroundImage2 = new BackgroundManager2 (Content.Load<Texture2D>(@"Images/HWB"), 0, 0, 0, screenBounds);
            
             
            
        }
        #endregion
        #region Unloadcontent
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        #endregion       
        #region Update
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //Allows game to exit 
            GamePadState gamePad = GamePad.GetState(PlayerIndex.One);
            KeyboardState keyboard = Keyboard.GetState();
            //Back or Escape exits the game
            if (gamePad.Buttons.Back == ButtonState.Pressed ||
                keyboard.IsKeyDown(Keys.Escape)) this.Exit();
           
            // Press F to toggle full-screen mode
            if (Keyboard.GetState().IsKeyDown(Keys.F))
            {
                graphics.IsFullScreen = !graphics.IsFullScreen;
                graphics.ApplyChanges();
            }

           //Updates that Starfield corresponds with time
           starField.Update(gameTime);

            //Updates spritemovement in playersprite class 
            playerSprite.HandleSpriteMovement(gameTime);
            //Updates playerSprite class
            playerSprite.Update(gameTime);
            //Updates Backgroundimage class
            backgroundImage.Update(gameTime);
            //Updates BackgroundImage2 class
            backgroundImage2.Update(gameTime);
            //Updates collisionManager class
            collisionManager.CheckCollisions();
            //Updates enemymanager class
            enemyManager.Update(gameTime);
            //Updates explosionManager class
            explosionManager.Update(gameTime);
            //Updates and gives the respective value
            collisionManager = new CollisionsManager(playerSprite, explosionManager, enemyManager);

            base.Update(gameTime);


           
        }
        #endregion
        #region Draw
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            //Draws all the different classes
            spriteBatch.Begin();
            backgroundImage2.draw(spriteBatch);
            backgroundImage.draw(spriteBatch);

            explosionManager.Draw(spriteBatch);
            starField.Draw(spriteBatch);
            playerSprite.draw(spriteBatch);

            enemyManager.Draw(spriteBatch);
            spriteBatch.Draw(playerSprite.Texture, playerSprite.Position, playerSprite.SourceRect, Color.White);
           
            spriteBatch.End();

            base.Draw(gameTime);
        }
        #endregion
    }
}
