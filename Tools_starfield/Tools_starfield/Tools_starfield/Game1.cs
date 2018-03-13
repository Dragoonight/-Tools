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
namespace Tools_starfield
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
        //
        EnemyManager enemyManager;
        //Refering to Starfield class
        Texture2D mixedSprites;
        #endregion

        CollisionsManager collisionManager;
    
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
            // TODO: Add your initialization logic here

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
            //Loads Mixeds prite
            mixedSprites = Content.Load<Texture2D>(@"Images/Mixed");
            //Loads new starfield to this StarField
            starField = new Starfield(this.Window.ClientBounds.Width, this.Window.ClientBounds.Height, 200, new Vector2(0, 30f), mixedSprites, new Rectangle(0, 450, 2, 2));

            Rectangle screenBounds = new Rectangle(0, 0, this.Window.ClientBounds.Width, this.Window.ClientBounds.Height);
            playerSprite = new PlayerManager(Content.Load<Texture2D>(@"Images/SpriteSheet"), 1, 32, 48, screenBounds);


            //_________________________________
            playerSprite.Position = new Vector2(400, 300);

            //
            enemyManager = new EnemyManager(mixedSprites, new Rectangle(0, 200, 50, 50), 6, playerSprite, screenBounds);
            
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            collisionManager = new CollisionsManager(playerSprite, enemyManager);

           //Updates that Starfield corresponds with time
           starField.Update(gameTime);

            //Player Movement
            playerSprite.HandleSpriteMovement(gameTime);

            playerSprite.Update(gameTime);

            enemyManager.Update(gameTime);

            collisionManager.CheckCollisions();
            
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

            spriteBatch.Begin();
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
