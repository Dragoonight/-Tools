using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;
using System.Xml.Serialization;

namespace Tools_File
{
    /// <summary> 
    /// This is the main type for your game 
    /// </summary> 
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        // Font 
        private SpriteFont font;

        // Keyboard states. 
        private KeyboardState keyboard;

        private KeyboardState previousKeyboard;

        // Current Score. 
        private int score;

        // Score file name 
        private readonly string scoreFile = "Scores.dat";

        // The different gamestates. 
        public enum GameState
        {
            TitleScreen, HighScore
        };

        // Makes game start at TitleScreen. 
        public static GameState gameState = GameState.TitleScreen;

        // The different Players. 
        public static string[] Players = new string[] { "Olle", "Subaru", "Rem" };

        // Current player in the array above. 
        public int Player = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        // Structure of the save data. 
        [Serializable]
        public struct SaveData
        {
            public string[] PlayerName;
            public int[] Score;

            public int Count;

            // Function to determine array lenghts while createing new data. 
            public SaveData(int count)
            {
                PlayerName = new string[count];
                Score = new int[count];

                Count = count;
            }
        }

        // Load data from file. 
        public static SaveData LoadData(string Filename)
        {
            SaveData data;

            // Get the path of the save game. 
            string fullpath = Filename;

            // Open the file. 
            FileStream stream = File.Open(fullpath, FileMode.Open, FileAccess.Read);
            try
            {
                // Read the data from the file. 
                XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
                data = (SaveData)serializer.Deserialize(stream);
            }
            finally
            {
                // Close the file. 
                stream.Close();
            }

            return (data);
        }

        // Save data to file. 
        public static void DoSave(SaveData data, string Filename)
        {
            // Open the file or create it if it doesn't exist. 
            FileStream stream = File.Open(Filename, FileMode.Create);
            try
            {
                // Convert to XML and try to open the stream. 
                XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
                serializer.Serialize(stream, data);
            }
            finally
            {
                // Close the file. 
                stream.Close();
            }
        }

        // Sort the highscore. 
        public void SaveAndSortHighScore()
        {
            // Load the stored data. 
            SaveData data = LoadData(scoreFile);

            // Sorting algorithm. 
            int scoreIndex = -1;
            for (int i = 0; i < data.Count; i++)
            {
                if (score > data.Score[i])
                {
                    scoreIndex = i;
                    break;
                }
            }

            if (scoreIndex > -1)
            {
                // New high score found ... do swaps. 
                for (int i = data.Count - 1; i > scoreIndex; i--)
                {
                    data.Score[i] = data.Score[i - 1];
                    data.PlayerName[i] = data.PlayerName[i - 1];
                }

                data.Score[scoreIndex] = score;
                data.PlayerName[scoreIndex] = Players[Player];

                DoSave(data, scoreFile);
            }
        }

        /// <summary> 
        /// Allows the game to perform any initialization it needs to before starting to run. 
        /// This is where it can query for any required services and load any non-graphic 
        /// related content.  Calling base.Initialize will enumerate through any components 
        /// and initialize them as well. 
        /// </summary> 
        protected override void Initialize()
        {
            // Check if file exists otherwise create it with blank scores. 
            if (!File.Exists(scoreFile))
            {
                SaveData data = new SaveData(3);
                data.PlayerName[0] = "Blank";
                data.PlayerName[1] = "Blank";
                data.PlayerName[2] = "Blank";
                // Save the data to the file. 
                DoSave(data, scoreFile);
            }
            base.Initialize();
        }

        /// <summary> 
        /// LoadContent will be called once per game and is the place to load 
        /// all of your content. 
        /// </summary> 
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures. 
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load font pericles size 36. 
            font = Content.Load<SpriteFont>(@"font");
        }

        /// <summary> 
        /// UnloadContent will be called once per game and is the place to unload 
        /// all content. 
        /// </summary> 
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here 
        }

        /// <summary> 
        /// Allows the game to run logic such as updating the world, 
        /// checking for collisions, gathering input, and playing audio. 
        /// </summary> 
        /// <param name="gameTime">Provides a snapshot of timing values.</param> 
        protected override void Update(GameTime gameTime)
        {
            // Allow the game to exit with "Back" or "ESC". 
            GamePadState gamePad = GamePad.GetState(PlayerIndex.One);
            // Update the keyboard state 
            previousKeyboard = keyboard;
            keyboard = Keyboard.GetState();

            if (gamePad.Buttons.Back == ButtonState.Pressed || keyboard.IsKeyDown(Keys.Escape))
                this.Exit();
            // Makes F toggle full-screen mode. 
            if (Keyboard.GetState().IsKeyDown(Keys.F))
            {
                graphics.IsFullScreen = !graphics.IsFullScreen;
                graphics.ApplyChanges();
            }

            // Create a switch for where code specific to a gamestate can be placed. 
            switch (gameState)
            {
                case GameState.TitleScreen:
                    // Change gamestate to Highscore when pressing H. 
                    if (keyboard.IsKeyDown(Keys.H))
                    {
                        gameState = GameState.HighScore;
                    }

                    // Adjust the score when tapping the up and down arrow keys. 
                    if (keyboard.IsKeyDown(Keys.Up) && previousKeyboard.IsKeyUp(Keys.Up))
                    {
                        score++;
                    }
                    else if (keyboard.IsKeyDown(Keys.Down) && previousKeyboard.IsKeyUp(Keys.Down) && score > 0)
                    {
                        score--;
                    }

                    // Save the score if S is pressed 
                    if (keyboard.IsKeyDown(Keys.S) && previousKeyboard.IsKeyUp(Keys.S))
                    {
                        SaveAndSortHighScore();
                    }

                    // Change the player using Left and Right while within the array. 
                    if (keyboard.IsKeyDown(Keys.Left) && previousKeyboard.IsKeyUp(Keys.Left) && Player > 0)
                    {
                        Player--;
                    }
                    if (keyboard.IsKeyDown(Keys.Right) && previousKeyboard.IsKeyUp(Keys.Right) && Player < Players.Length - 1)
                    {
                        Player++;
                    }

                    break;

                case GameState.HighScore:
                    // Change gamestate to the title screen when pressing M. 
                    if (keyboard.IsKeyDown(Keys.M))
                    {
                        gameState = GameState.TitleScreen;
                    }
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary> 
        /// This is called when the game should draw itself. 
        /// </summary> 
        /// <param name="gameTime">Provides a snapshot of timing values.</param> 
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // Start a new spriteBatch. 
            spriteBatch.Begin();

            // Create a switch for where code specific to a gamestate can be placed. 
            switch (gameState)
            {
                case GameState.TitleScreen:
                    // Draw the current score. 
                    spriteBatch.DrawString(font, "Currrent Score: " + score.ToString(), new Vector2(300, 160), Color.White);
                    // Draw the current player. 
                    spriteBatch.DrawString(font, "Currrent Player: " + Players[Player].ToString(), new Vector2(265, 200), Color.White);

                    // Draw the controls. 
                    spriteBatch.DrawString(font, "Use the up and down arrow keys to increase and decrease the score.".ToString(), new Vector2(35, 360), Color.White);
                    spriteBatch.DrawString(font, "Use the left and right arrow keys to change player.".ToString(), new Vector2(120, 390), Color.White);
                    spriteBatch.DrawString(font, "Press H for highscore.".ToString(), new Vector2(280, 420), Color.White);
                    spriteBatch.DrawString(font, "(Don't forget to press S to save score)".ToString(), new Vector2(200, 450), Color.White);
                    break;

                case GameState.HighScore:
                    // Draw the Highscore title. 
                    spriteBatch.DrawString(font, "Highscore".ToString(), new Vector2(343, 10), Color.White);
                    // Load the highscore data 
                    SaveData data = LoadData(scoreFile);
                    for (int i = 0; i <= data.Count - 1; i++)
                    {
                        // Draw the score position. 
                        spriteBatch.DrawString(font, (i + 1) + ".".ToString(), new Vector2(315, 40 * i + 55), Color.White);
                        // Draw the player name. 
                        spriteBatch.DrawString(font, data.PlayerName[i] + ":".ToString(), new Vector2(350, 40 * i + 55), Color.White);
                        // Draw the score. 
                        spriteBatch.DrawString(font, data.Score[i].ToString(), new Vector2(450, 40 * i + 55), Color.White);
                    }
                    // Draw the controls 
                    spriteBatch.DrawString(font, "Press M to return.".ToString(), new Vector2(300, 450), Color.White);
                    break;
            }
            // End the spriteBatch. 
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}