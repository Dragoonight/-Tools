using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
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
        EnemyManager2 enemyManager2;
        //Refering to CololisionManager class
        CollisionsManager collisionManager;
        //Refering to ExplosionManager class
        ExplosionManager explosionManager;
        
        //Credits Class
        CreditsManager CreditsBG;
        CreditsManager2 CreditsBG2;

        
        //Variable Texture
        Texture2D mixedSprites;    
        Texture2D GameoverBG;
        Texture2D TutorialBG;

        SpriteFont RobotoRegular36;
        SpriteFont RobotoBold36;
        Texture2D Logo;
        

        Vector2 lifePosition = new Vector2(24, 40);
        Vector2 scoreposition = new Vector2(725, 40);

        //The text i want to be able to show
        readonly string[] mainMenuStrArr = new string[] { "Campaign", "Credits", "Exit" };
        readonly string[] campaingStrArr = new string[] { "Start game", "Tutorial", "back" };
        readonly string[] TutorialStrArr = new string[] { "back" };
        readonly string[] Game_overStrArr = new string[] { "Restart", "Back" };
        readonly string[] Credits_overStrArr = new string [] { "(Press esc to Menu)" };

        bool firstRun = true; // for running a segment once 
        bool KeyIsUp;

        enum GameStates
        {
            MainMenu,
            Campaign,
            Tutorial,
            Level_1,
            Credits,                 
            Gameover,
            Exit
        };

        //starting gamestate
        GameStates gameState = GameStates.MainMenu;
       

        private int selected;
        private int selectionIndex; // number of options

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

        //Load content here 
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);


            //Loads Mixed sprites
            mixedSprites = Content.Load<Texture2D>(@"Images/Mixed");
            //Loads Starfiedls star texture
            starField = new Starfield
                (this.Window.ClientBounds.Width, this.Window.ClientBounds.Height, 200, new Vector2(0, 500f), mixedSprites, new Rectangle(0, 288, 2, 2));

            Rectangle screenBounds = new Rectangle(0, 0, this.Window.ClientBounds.Width, this.Window.ClientBounds.Height);

            //Loads and locates Player spite from Mixed sprites
            playerSprite = new PlayerManager(Content.Load<Texture2D>(@"Images/mixed"), 1, 32, 48, screenBounds);
            //Loads but also declares playerSprite position
            playerSprite.Position = new Vector2(400, 300);

            //Loads and locates Enemt sprite from Mixed sprites
            enemyManager = new EnemyManager(mixedSprites, new Rectangle(0, 55, 32, 50), 4, playerSprite, screenBounds);

            enemyManager2 = new EnemyManager2(mixedSprites, new Rectangle(0, 110, 32, 50), 4, playerSprite, screenBounds);

            //Loads Explosion sprite from mixed sprites
            explosionManager = new ExplosionManager(mixedSprites, new Rectangle(0, 100, 50, 50), 3, new Rectangle(0, 450, 2, 2));
            //Loads Collision properties
            collisionManager = new CollisionsManager(playerSprite, explosionManager, enemyManager);

            //Loads Background Image from images 
            backgroundImage = new BackgroundManager(Content.Load<Texture2D>(@"Images/HWB"));
            //Loads Background2 Image from images 
            backgroundImage2 = new BackgroundManager2 (Content.Load<Texture2D>(@"Images/HWB"));
            collisionManager = new CollisionsManager(playerSprite, explosionManager, enemyManager);
            //
            CreditsBG = new CreditsManager (Content.Load<Texture2D>(@"images/CreditsBG"));
            CreditsBG2 = new CreditsManager2 (Content.Load<Texture2D>(@"images/CreditsBG2"));

            #region Background
            // BG = BackGround you need to change here
            TutorialBG = Content.Load<Texture2D>(@"Images/TutorialBG");
            GameoverBG = Content.Load<Texture2D>(@"Images/Gameover");
            #endregion

            RobotoRegular36 = Content.Load<SpriteFont>(@"Font/Roboto/Normal");
            RobotoBold36 = Content.Load<SpriteFont>(@"Font/Roboto/Bold");

            Logo = Content.Load<Texture2D>(@"Images/Logo");

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
      
        protected override void Update(GameTime gameTime)
        {
            //Allows game to exit 
            GamePadState gamePad = GamePad.GetState(PlayerIndex.One);
            KeyboardState keyboard = Keyboard.GetState();

            #region going back to main menu
            // Esc or back to return to Mainmenu
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) || Keyboard.GetState().IsKeyDown(Keys.Back))
            {
                firstRun = true;
                gameState = GameStates.MainMenu;
            }
            #endregion

            #region Game Exit
            // Back or End exits the game
            if (gamePad.Buttons.Back == ButtonState.Pressed || keyboard.IsKeyDown(Keys.End)) { this.Exit(); }
            #endregion

            #region Fullscreen
            // F to toggle fullscreen
            if (Keyboard.GetState().IsKeyDown(Keys.F))
            {
                graphics.IsFullScreen = !graphics.IsFullScreen;
                graphics.ApplyChanges();
            }
            #endregion

            

           

            switch (gameState)
            {
                #region Mainmenu
                case GameStates.MainMenu:
                    //Resets credits positions
                    CreditsBG.Position = new Vector2(0, 0);
                    CreditsBG2.Position = new Vector2(0, 1220);

                    //Updates enemymanager class
                    enemyManager.Update(gameTime);

                    enemyManager2.Update(gameTime);

                    if (firstRun)
                    {
                        selected = 0;
                        selectionIndex = 2;
                        firstRun = false;
                    }
                    #region Menu Controls 
                    // detect if key is up
                    if (                        
                        Keyboard.GetState().IsKeyUp(Keys.Down) &&
                        Keyboard.GetState().IsKeyUp(Keys.Up) &&
                        Keyboard.GetState().IsKeyUp(Keys.Enter)
                        )
                    { KeyIsUp = true; }
                    // detect if key is down
                    if (KeyIsUp)
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.Down))
                        {
                            KeyIsUp = false;
                            if (selected < selectionIndex) { selected++; }
                        }

                        if ( Keyboard.GetState().IsKeyDown(Keys.Up))
                        {
                            KeyIsUp = false;
                            if (selected > 0) { selected--; }
                        }


                        // menu options
                        if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                        {
                            KeyIsUp = false;
                            switch (selected)
                            {
                                case 0:
                                    firstRun = true;
                                    gameState = GameStates.Campaign;
                                    break;
                                case 1:
                                    firstRun = true;
                                    gameState = GameStates.Credits;
                                    break;
                                case 2:
                                    firstRun = true;
                                    gameState = GameStates.Exit;
                                    break;
                                default:
                                    throw new InvalidOperationException("Unexpected value for 'selected' = " + selected);
                            }
                        }
                    }
                    #endregion

                    #region background scrolling
                    //Updates the auto scrolling                   
                    backgroundImage.Update(gameTime);
                    backgroundImage2.Update(gameTime);


                    #endregion
                    break;
                #endregion

                #region Campaign
                case GameStates.Campaign:
                    if (firstRun)
                    {
                        selected = 0;
                        selectionIndex = 2;
                        firstRun = false;
                    }
                    #region Menu Controls
                    // detect if key is up
                    if (                    
                        Keyboard.GetState().IsKeyUp(Keys.Down) &&                      
                        Keyboard.GetState().IsKeyUp(Keys.Up) &&
                        Keyboard.GetState().IsKeyUp(Keys.Enter)
                        )
                    { KeyIsUp = true; }

                    if (KeyIsUp)
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.Down)) //detect if key is down
                        {
                            KeyIsUp = false;
                            if (selected < selectionIndex) { selected++; }
                        }

                        if (Keyboard.GetState().IsKeyDown(Keys.Up))
                        {
                            KeyIsUp = false;
                            if (selected > 0) { selected--; }
                        }

                        if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                        {
                            switch (selected)
                            {
                                case 0: // Level_1
                                    firstRun = true;
                                    gameState = GameStates.Level_1;
                                    break;
                                case 1: // Tutorial
                                    firstRun = true;
                                    gameState = GameStates.Tutorial;
                                    break;
                                default:
                                    throw new InvalidOperationException("Unexpected value for 'selected' = " + selected);
                            }
                        }
                    }
                    #endregion

                    #region background scrolling
                    //Updates the auto scrolling                   
                    backgroundImage.Update(gameTime);
                    backgroundImage2.Update(gameTime);


                    #endregion
                    break;
                #endregion

                #region Level1
                case GameStates.Level_1:
                    //Updates all of the other classes
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
                    collisionManager.CheckCollisions(gameTime);
                    
                    //Updates enemymanager class
                    enemyManager.Update(gameTime);

                    enemyManager2.Update(gameTime);
                    //Updates explosionManager class
                    explosionManager.Update(gameTime);
                    //Updates and gives the respective value
                   

                    if (collisionManager.playerLife < 1)
                    {
                        gameState = GameStates.Gameover;
                    }

                   

                    break;
                #endregion

                #region Tutorial
                case GameStates.Tutorial:

                    if (firstRun)
                    {
                        selected = 0;
                        selectionIndex = 2;
                        firstRun = false;
                    }


                    break;
                #endregion 

                #region Credits
                case GameStates.Credits:
                    //
                    if (Keyboard.GetState().IsKeyDown(Keys.Insert))
                    {
                        firstRun = true;

                    }
                    #region Credits scrolling
                    //Updates the auto scrolling
                    CreditsBG.Update(gameTime);
                    CreditsBG2.Update(gameTime);
                    #endregion

                    break;
                #endregion                                   

                #region Gameover
                case GameStates.Gameover:

                    if (firstRun)
                    {
                        selected = 0;
                        selectionIndex = 2;
                        firstRun = false;
                    }
                    #region Menu Controls
                    // detect if key is up
                    if (
                        Keyboard.GetState().IsKeyUp(Keys.Down) &&
                        Keyboard.GetState().IsKeyUp(Keys.Up) &&
                        Keyboard.GetState().IsKeyUp(Keys.Enter)
                        )
                    { KeyIsUp = true; }

                    if (KeyIsUp)
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.Down)) //detect if key is down
                        {
                            KeyIsUp = false;
                            if (selected < selectionIndex)
                            {
                                selected++;
                            }
                        }

                        if (Keyboard.GetState().IsKeyDown(Keys.Up))
                        {
                            KeyIsUp = false;
                            if (selected > 0)
                            {
                                selected--;
                            }
                        }

                        if (Keyboard.GetState().IsKeyDown(Keys.Enter) && (selected == 0 || selected == 1))
                        {
                          
                            
                                KeyIsUp = false;
                                switch (selected)
                                {
                                    case 0:
                                        firstRun = true;
                                        gameState = GameStates.Level_1;
                                        break;
                                    case 1:
                                        firstRun = true;
                                        gameState = GameStates.MainMenu;
                                        break;
                                    default:
                                        throw new InvalidOperationException(
                                            "Unexpected value for 'selected' = " + selected);
                                }
                            
                        }
                    }

                                #endregion

                    collisionManager.playerLife = 5;
                    collisionManager.playerScore = 0;
                                break;
                #endregion

                #region Exit
                case GameStates.Exit:
                    this.Exit();
                    break;
                #endregion

            
            }

            base.Update(gameTime);




        }
        #endregion
        #region Draw
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            switch (gameState)
            {

                #region MainMenu
                case GameStates.MainMenu:


                    
                    backgroundImage.draw(spriteBatch);
                    backgroundImage2.draw(spriteBatch);
                    enemyManager2.Draw(spriteBatch);
                    enemyManager.Draw(spriteBatch);

                    spriteBatch.Draw(Logo, new Vector2 (0, 0), Color.White);

                    //campaign
                    Vector2 campaingOrigin = RobotoRegular36.MeasureString(mainMenuStrArr[0]) / 2;
                    Vector2 campaingPos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 100 * 55);
                    if (selected == 0)
                    {
                        spriteBatch.DrawString(RobotoBold36, mainMenuStrArr[0], campaingPos, Color.White,
                                0, campaingOrigin, 1.0f, SpriteEffects.None, 0.5f);
                    }
                    else
                    {
                        spriteBatch.DrawString(RobotoRegular36, mainMenuStrArr[0], campaingPos, Color.White,
                                0, campaingOrigin, 1.0f, SpriteEffects.None, 0.5f);
                    }

                    //Credits
                    Vector2 creditsOrigin = RobotoRegular36.MeasureString(mainMenuStrArr[1]) / 2;
                    Vector2 creditsPos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 100 * 49, graphics.GraphicsDevice.Viewport.Height / 100 * 70);
                    if (selected == 1)
                    {
                        spriteBatch.DrawString(RobotoBold36, mainMenuStrArr[1], creditsPos, Color.White,
                                0, creditsOrigin, 1.0f, SpriteEffects.None, 0.5f);
                    }
                    else
                    {
                        spriteBatch.DrawString(RobotoRegular36, mainMenuStrArr[1], creditsPos, Color.White,
                                0, creditsOrigin, 1.0f, SpriteEffects.None, 0.5f);
                    }

                    //Exit
                    Vector2 exitOrigin = RobotoRegular36.MeasureString(mainMenuStrArr[2]) / 2;
                    Vector2 exitPos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 100 * 48, graphics.GraphicsDevice.Viewport.Height / 100 * 85);
                    if (selected == 2)
                    {
                        spriteBatch.DrawString(RobotoBold36, mainMenuStrArr[2], exitPos, Color.White,
                                0, exitOrigin, 1.0f, SpriteEffects.None, 0.5f);
                    }
                    else
                    {
                        spriteBatch.DrawString(RobotoRegular36, mainMenuStrArr[2], exitPos, Color.White,
                                0, exitOrigin, 1.0f, SpriteEffects.None, 0.5f);
                    }
                    break;
                #endregion

                #region Campaign
                case GameStates.Campaign:

                    backgroundImage.draw(spriteBatch);
                    backgroundImage2.draw(spriteBatch);


                    //Start game 
                    Vector2 Level_1Origin = RobotoRegular36.MeasureString(campaingStrArr[0]) / 2;
                    Vector2 Level_1Pos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 100 * 52, graphics.GraphicsDevice.Viewport.Height / 100 * 15);
                    if (selected == 0)
                    {
                        spriteBatch.DrawString(RobotoBold36, campaingStrArr[0], Level_1Pos, Color.White,
                                0, Level_1Origin, 1.0f, SpriteEffects.None, 0.5f);
                    }
                    else
                    {
                        spriteBatch.DrawString(RobotoRegular36, campaingStrArr[0],
                            Level_1Pos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 100 * 49, graphics.GraphicsDevice.Viewport.Height / 100 * 15),
                            Color.White,
                               0, Level_1Origin, 1.0f, SpriteEffects.None, 0.5f);
                    }


                    //Tutorial
                    Vector2 TutorialOrigin = RobotoRegular36.MeasureString(campaingStrArr[1]) / 2;
                    Vector2 TutorialPos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 100 * 45);
                    if (selected == 1)
                    {
                        spriteBatch.DrawString(RobotoBold36, campaingStrArr[1], TutorialPos, Color.White,
                                0, TutorialOrigin, 1.0f, SpriteEffects.None, 0.5f);
                    }
                    else
                    {
                        spriteBatch.DrawString(RobotoRegular36, campaingStrArr[1], TutorialPos, Color.White,
                                0, TutorialOrigin, 1.0f, SpriteEffects.None, 0.5f);
                    }



                    spriteBatch.DrawString(RobotoBold36, Credits_overStrArr[0], new Vector2(0, 420), Color.White);



                    break;
                #endregion

                #region Credits
                case GameStates.Credits:

                    CreditsBG.draw(spriteBatch);
                    CreditsBG2.draw(spriteBatch);

                    spriteBatch.DrawString(RobotoBold36, Credits_overStrArr[0], new Vector2(0, 420), Color.White);

                    break;
                #endregion
                
                #region Level 1
                case GameStates.Level_1:                   
                    backgroundImage2.draw(spriteBatch);
                    backgroundImage.draw(spriteBatch);

                    explosionManager.Draw(spriteBatch);
                    starField.Draw(spriteBatch);
                    playerSprite.draw(spriteBatch);
                    

                    enemyManager.Draw(spriteBatch);
                    enemyManager2.Draw(spriteBatch);
                    spriteBatch.DrawString(RobotoBold36, collisionManager.playerLife.ToString(), lifePosition, Color.DarkRed);
                    spriteBatch.DrawString(RobotoBold36, collisionManager.playerScore.ToString(), scoreposition, Color.Gray);
                    spriteBatch.Draw(playerSprite.Texture, playerSprite.Position, playerSprite.SourceRect, Color.White);
                   
                    break;
                #endregion

                #region Tutorial
                case GameStates.Tutorial:

                    //Back 
                    Vector2 BackOrigin= RobotoRegular36.MeasureString(mainMenuStrArr[0]) / 2;
                    Vector2 Backpos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 100 * 65);
                    if (selected == 0)
                    {
                        spriteBatch.DrawString(RobotoBold36, mainMenuStrArr[2], Backpos , Color.White,
                                0, BackOrigin , 1.0f, SpriteEffects.None, 0.5f);
                    }

                    spriteBatch.Draw(TutorialBG, new Vector2 (0,0), Color.White);

                    spriteBatch.DrawString(RobotoBold36, Credits_overStrArr[0], new Vector2(0, 420), Color.White);

                    break;
                #endregion

                #region Gameover
                case GameStates.Gameover:


                    spriteBatch.Draw( //Background
                        GameoverBG,
                        new Rectangle(0, 0,
                        this.Window.ClientBounds.Width,
                        this.Window.ClientBounds.Height),
                        Color.White);

                    //Restart
                    Vector2 RestartOrigin = RobotoRegular36.MeasureString(mainMenuStrArr[1]) / 2;
                    Vector2 RestartPos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 100 * 49, graphics.GraphicsDevice.Viewport.Height / 100 * 70);
                    if (selected == 1)
                    {
                        spriteBatch.DrawString(RobotoBold36, Game_overStrArr[0], RestartPos, Color.White,
                                0, RestartOrigin, 1.0f, SpriteEffects.None, 0.5f);
                    }
                    else
                    {
                        spriteBatch.DrawString(RobotoRegular36, Game_overStrArr[0], RestartPos, Color.White,
                                0, RestartOrigin, 1.0f, SpriteEffects.None, 0.5f);
                    }

                    //Exit
                    Vector2 backMenuOrigin = RobotoRegular36.MeasureString(mainMenuStrArr[2]) / 2;
                    Vector2 backMenuPos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 100 * 48, graphics.GraphicsDevice.Viewport.Height / 100 * 85);
                    if (selected == 2)
                    {
                        spriteBatch.DrawString(RobotoBold36, Game_overStrArr[1], backMenuPos, Color.White,
                                0, backMenuOrigin, 1.0f, SpriteEffects.None, 0.5f);
                    }
                    else
                    {
                        spriteBatch.DrawString(RobotoRegular36, Game_overStrArr[1], backMenuPos, Color.White,
                                0, backMenuOrigin, 1.0f, SpriteEffects.None, 0.5f);
                    }

                    spriteBatch.DrawString(RobotoBold36, Credits_overStrArr[0], new Vector2(0, 420), Color.White);


                    break;
                #endregion

                #region  Exit
                case GameStates.Exit:
                    break;
                #endregion
                default:
                    throw new InvalidOperationException("Unexpected value for gameState = " + gameState);
            }



            spriteBatch.End();

            base.Draw(gameTime);
        }
        #endregion
    }
}
