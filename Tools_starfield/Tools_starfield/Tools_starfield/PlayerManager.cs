using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tools_starfield
{
    class PlayerManager
    {
        


        //variabel for player sprite
        Texture2D playerSprite;
        //variabel for the timer
        float timer = 0f;
        //variabel for the interval
        float interval = 200f;
        //variabel for the current frame
        int currentFrame = 0;
        //variabel for the sprite height and width
        int spriteWidth = 32;
        int spriteHeight = 48;
        //Variabel for the sprite speed
        int spriteSpeed = 2;
        //Variabel for the rectangle 
        Rectangle sourceRect;
        //Variabel for the rectangle position
        Vector2 position;
        //Variabel Velocity for the 
        Vector2 velocity;
        

        public int CollisionRadius = 0;
        public bool Destroyed = false;
        public int LivesRemaining = 3;
        private int playerRadius = 15;

        private Vector2 gunOffset = new Vector2(25, 10);
        private float shotTimer = 0.0f;
        private float minShotTimer = 0.2f;
        public ShootManager PlayerShotManager;
        Rectangle screenBounds;

        public int CollisionRadius = 0;
        public bool Destroyed = false;
        public int LivesRemaining = 3;
        private int playerRadius = 15;

        //Communicates so that the class vector Position returns and it values will return
        public Vector2 Position
        {
            get { return position;  }
            set { position = value; }
        }

        //The same but with the velocity
        public Vector2 Velocity
        {
            get { return velocity;  }
            set { velocity = value; }
        }

        //the same but with the Texture
        public Texture2D Texture
        {
            get { return playerSprite; }
            set { playerSprite = value; }
        }

        //The same but with the Rectangle 
        public Rectangle SourceRect
        {
            get { return sourceRect; }
            set { sourceRect = value; }
        }

        public Vector2 Center
        {
            get
            {
                return position + new Vector2(spriteWidth / 2, spriteHeight / 2);
            }
        }

        //When mentioning the class playermanager it will all of the things under it related
        public PlayerManager (Texture2D texture, int currentFrame,int spriteWidth , int spriteHeight, Rectangle screenBounds)
        {
            this.playerSprite = texture;
            this.currentFrame = currentFrame;
            this.spriteWidth = spriteWidth;
            this.spriteHeight = spriteHeight;

            this.screenBounds = screenBounds;

            CollisionRadius = playerRadius;

            PlayerShotManager = new ShootManager(texture, new Rectangle(0, 300, 5, 5), 4, 2, 250f, screenBounds);

            CollisionRadius = playerRadius;
        }

        //keyboardcontroll variabel for current
        KeyboardState currentKBState;
        //Keyboardcurrent variabel for previous
        KeyboardState prevoiusKBState;

        //The class handlespritemovement will have the values that are mention under it
        public void HandleSpriteMovement(GameTime gameTime)
        {
            //The previousKBState will get the value of the CurrentKBState
            prevoiusKBState = currentKBState;
            //The current KBState will get the values of the Keyboard State
            currentKBState = Keyboard.GetState();

            
            
            //The sourceRect will get new values that are mentioned under i
            sourceRect = new Rectangle(currentFrame * spriteWidth, 0, spriteWidth, spriteHeight);


            //If the current KBstate gets pressed the corresponding things will happen under it
            if (currentKBState.GetPressedKeys().Length == 0)
            { 

            //The frame will go from 0 to 3 (always under 4) and reset to 0 again
            if (currentFrame > 0 && currentFrame < 4)
            {
                currentFrame = 0;
            }

            //the frame will go from 4 and to 7 and reset to 4 again
            if (currentFrame > 4 && currentFrame < 8)
                {
                    currentFrame = 4;
                }

            //The frame will go from 8 to 11 and reset to 8 again
            if (currentFrame > 8 && currentFrame < 12)
                {
                    currentFrame = 8;
                }

            //The frame will go from 12 to 15 and reset to 12 again
            if (currentFrame > 12 && currentFrame < 16)
                {
                    currentFrame = 12;
                }

            
            }

            //When right keys is pressed the correspodning things will happen mentioned under
            if (currentKBState.IsKeyDown(Keys.Right) == true)
            {
                
                //Everyhthing will be related to AnimateRight
                AnimateRight (gameTime);
                //As long as the player position is under position.X 780 the Spritespeed goes +
                if (position.X < 780)
                {
                    position.X += spriteSpeed;
                }
            }
           

            //When left key is pressed the corresponding things will happen mentioned under
            if (currentKBState.IsKeyDown(Keys.Left) == true)
            {
                //Everything will be related to AnimateLeft
                AnimateLeft(gameTime);
                //As long as the player position is above position.X 20 the spritespeed goes -
                if (position.X > 20)
                {
                    position.X -= spriteSpeed;
                }       
            }

            //When Down key is pressed the corresponding things will happen mentioned under
            if (currentKBState.IsKeyDown(Keys.Down) == true)
            {
                //Everything will be related to AnimateDown
                AnimateDown(gameTime);
                //As long as the player position is under position.Y 400 the spritespeed goes +
                if (position.Y < 400)
                {
                    position.Y += spriteSpeed;
                }
            }


            //When Up Key is pressed the corresponding things will happen mentioned under
            if (currentKBState.IsKeyDown(Keys.Up) == true)
            {
                //As long as the player position is above position.Y 25 the spritespeed goes -
                AnimateUp(gameTime);
                if (position.Y > 25)
                {
                    position.Y -= spriteSpeed;
                }
            }


            if (currentKBState.IsKeyDown(Keys.Space))
            {
                FireShot();
            }


             //Velocity gets a new vector also the Sourcerect.Width and sourceRect.Height is halfed for some reason
            Velocity = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);
        }

        //The class AnimateRight have some rules mentioned under
        public void AnimateRight (GameTime gameTime)
        {
            //if CurrentKbstate and prevousKbstate are not the same the currentframe goes to 9 
            if (currentKBState != prevoiusKBState)
            {
                currentFrame = 9;
            }

            //The timer gets higher and higher with the time in milliseconds
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            //If the timer is higher than the interval the currentframe goes + and the resets
            if (timer > interval)
            {
                currentFrame++;
                timer = 0f;

                //Not mentioned is that if the currentframe is higher than 11 then it goes back to 8 again
                if (currentFrame > 11)
                {
                    currentFrame = 8;
                }            
                
            } 
        }

        //The class AnimateUp have some rules mentioned under
        public void AnimateUp(GameTime gameTime)
        {
            
           
            //if CurrentKbstate and prevousKbstate are not the same then the currentframe goes to 13
            if (currentKBState != prevoiusKBState)
            {
                currentFrame = 13;
            }

            //The timer goes higher and higher with the time in milliseconds
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            //If the timer is higher than the interval the currentFrame goes +, but has a max to under 15 and it will reset 12
            if (timer > interval)
            {
                currentFrame++;

                if (currentFrame > 15)
                {
                    currentFrame = 12;
                }
                //The timer also resets to 0f
                timer = 0f;
            }
        }

        //The class AnimateDown have some rules mentioned under
        public void AnimateDown(GameTime gameTime)
        {
            //if CurrentKbstate and prevousKbstate are not the same then the currentframe goes to 1
            if (currentKBState != prevoiusKBState)
            {
                currentFrame = 1;
            }

            //The timer goes higher and higher with the time in milliseconds
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            //if the timer is higher than the interval the currentframe goes +, but has a max to under 3 and it will reset to 0
            if (timer > interval)
            {
                currentFrame++;

                if (currentFrame > 3)
                {
                    currentFrame = 0;
                }
                //The timer also resets to 0f
                timer = 0f;
            }
        }

        //The class AnimateLeft have some rules mentioned under
        public void AnimateLeft(GameTime gameTime)
        {
            //if CurrentKbstate and prevousKbstate are not the same then the currentframe goes to 5
            if (currentKBState != prevoiusKBState)
            {
                currentFrame = 5;
            }

            //The timer goes higher and higher with the time in milliseconds
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            ////if the timer is higher than the interval the currentframe goes +, but has a max to under 7 and it will reset to 4
            if (timer > interval)
            {
                currentFrame++;

                if (currentFrame > 7)
                {
                    currentFrame = 4;
                }
                //The timer also resets to 0f
                timer = 0f;
            }

        }
       
        private void FireShot()
        {
            if (shotTimer >= minShotTimer)
            {
                PlayerShotManager.FireShot(position + gunOffset, new Vector2(0, -1), true);
                shotTimer = 0.0f;            
            }
        }

        public void Update (GameTime gameTime)
        {
            PlayerShotManager.Update(gameTime);
            shotTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        
        public void draw (SpriteBatch spriteBatch)
        {
            PlayerShotManager.Draw(spriteBatch);
        }
        
            }

}
