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
using System.Xml.Serialization;
using System.IO;

namespace Tools_File
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //File variable
        public readonly string Filename = "saveFile.dat";

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        
        //Delar som vi vill skall finnas i vår fil
        [Serializable]
        public struct SaveData
        {
            public string[] PlayerName;
            public int[] Score;

            public int Count;

            public SaveData (int count)
            {
                PlayerName = new string[count];
                Score = new int[count];

                Count = count;
            }

            


        }


        string playerName = "Olle";
        Vector2 playerpos = new Vector2(40, 20);
        Vector2 scorePosition = new Vector2(200, 200);
        Vector2 scorePosition1 = new Vector2(200, 250);
        Vector2 scorePosition2 = new Vector2(200, 300);



        SpriteFont Font;


        int playerScore = (0);
       
      

        public static SaveData LoadData (string Filename)
        {
            SaveData data;

            //Get the path of the save game
            string fullpath = Filename;

            //Open the file
            FileStream stream = File.Open(fullpath, FileMode.Open,
                FileAccess.Read);
            try
            {

                //Read the data from the file 
                XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
                data = (SaveData)serializer.Deserialize(stream);
            }
            finally
            {
                //Close the File 
                stream.Close();
            }

            return (data);
        }



        //Funktionen för att öppna filen och spara datan
        public static void DoSave(SaveData data, String filename)
        {

            //Öppna filen eller skapa den om den inte finns
            FileStream stream = File.Open(filename, FileMode.Create);
            try
            {
                //Gör om till Xml och försök öppna Steamen 
                XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
                serializer.Serialize(stream, data);
            }
            finally
            {
                //Stäng filen 
                stream.Close();
            }
        }
        
        private void SaveHighScore()
        {
            //Create the data top save
            SaveData data = LoadData(Filename);

            int scoreIndex = -1;

            for (int x = 0; x < data.Count; x++)
            {
                if (playerScore > data.Score[x])
                {
                    scoreIndex = x;
                    break;
                }

            }

            if (scoreIndex > -1)
            {
                //New high score found ... do swaps 
                for (int x = data.Count - 1; x > scoreIndex; x--)
                {
                    data.Score[x] = data.Score[x - 1];
                }

                data.Score[scoreIndex] = playerScore;
                data.PlayerName[scoreIndex] = playerName;
                DoSave(data, Filename);

            }



        }


        enum GameStates
        {
           Changescore,
           Highscore,

        };
        GameStates gameState = GameStates.Changescore;



        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            //Kolla om filen finns annars stoppa in info
            if (!File.Exists(Filename))
            {
                SaveData data = new SaveData(3);
                data.PlayerName[0] = "Olle";
                data.Score[0] = 0;

                data.PlayerName[1] = "Subaru";
                data.Score[1] = 0;

                
                data.PlayerName[2] = "Rem";
                data.Score[2] = 0;

                //Lägg in datan i själva filen
                DoSave(data, Filename);
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

            // TODO: use this.Content to load your game content here

            Font = Content.Load<SpriteFont>(@"Pericles1");
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
            //This is the line of code that uppdates that the keyboard is in use 
            GamePadState gamePad = GamePad.GetState(PlayerIndex.One);
            KeyboardState keyboard = Keyboard.GetState();

            //This is the line of code that will control that the escape button exits the game
            if (gamePad.Buttons.Back == ButtonState.Pressed ||
                keyboard.IsKeyDown(Keys.Escape)) this.Exit();

            // This is the line of code that will control that pressing F to toggle full-screen mode
            if (keyboard.IsKeyDown(Keys.F))
            {
                graphics.IsFullScreen = !graphics.IsFullScreen;
                graphics.ApplyChanges();
            }

                switch (gameState)
                {
                    case GameStates.Changescore:
                            
                        if (keyboard.IsKeyDown(Keys.Up))
                        {
                            playerScore++;
                        }

                        if (keyboard.IsKeyDown(Keys.Down))
                        {
                            playerScore--;
                        }
                        
                        if (keyboard.IsKeyDown(Keys.S))
                        {
                            SaveHighScore();
                        }

                        if (keyboard.IsKeyDown(Keys.H))
                        {
                            gameState = GameStates.Highscore;
                        }


                    if (Keyboard.GetState().IsKeyDown(Keys.O))
                    {

                        playerName = "Olle";
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.P))
                    {


                        playerName = "Subaru";
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.I))
                    {


                        playerName = "Rem";
                    }






                    break;

                    case GameStates.Highscore:

                        if (keyboard.IsKeyDown(Keys.M))
                        {
                            gameState = GameStates.Changescore;
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

            spriteBatch.Begin();

            switch (gameState)
            {
                case GameStates.Changescore:
                  

                    spriteBatch.DrawString(Font, playerScore.ToString(), scorePosition1,
                        Color.White);

                    spriteBatch.DrawString(Font, playerName.ToString(), playerpos,
                        Color.White);

                    // Draw the controls.
                    spriteBatch.DrawString(Font, "Use the up and down arrow keys to increase and decrease the score.".ToString(), new Vector2(35, 390), Color.White);
                    spriteBatch.DrawString(Font, "Press I=Olle O=Suabru P=Rem to change player.".ToString(), new Vector2(130, 420), Color.White);
                    spriteBatch.DrawString(Font, "Press H for highscore.".ToString(), new Vector2(280, 450), Color.White);
                    break;

                case GameStates.Highscore:

                     spriteBatch.DrawString(Font, LoadData(Filename).PlayerName[0].ToString() + " " +  LoadData(Filename).Score[0].ToString(), scorePosition, Color.White);

                    spriteBatch.DrawString(Font, LoadData(Filename).PlayerName[1].ToString() + " " + LoadData(Filename).Score[1].ToString(), scorePosition1, Color.White);

                    spriteBatch.DrawString(Font, LoadData(Filename).PlayerName[2].ToString() + " " + LoadData(Filename).Score[2].ToString(), scorePosition2, Color.White);
                    break;

            }

            spriteBatch.End();
                    base.Draw(gameTime);
        }
    }
}
