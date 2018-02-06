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
        //_Need help
        Vector2 velocity;

        //Communicates with with caller  
        public Vector2 Position
        {
            get { return position;  }
            set { position = value; }
        }

        //Communicates with with caller  
        public Vector2 Velocity
        {
            get { return velocity;  }
            set { velocity = value; }
        }

        //Communicates with with caller  
        public Texture2D Texture
        {
            get { return playerSprite; }
            set { playerSprite = value; }
        }

        //
        public Rectangle SourceRect
        {
            get { return sourceRect; }
            set { sourceRect = value; }
        }

        //__________________________
        public PlayerManager (Texture2D texture, int currentFrame,int spriteWidth , int spriteHeight)
        {
            this.playerSprite = texture;
            this.currentFrame = currentFrame;
            this.spriteWidth = spriteWidth;
            this.spriteHeight = spriteHeight;
        }

        //keyboardcontroll variabel
        KeyboardState currentKBState;
        KeyboardState prevoiusKBState;

        //_______________________
        public void HandleSpriteMovement(GameTime gameTime)
        {
            //_____
            prevoiusKBState = currentKBState;
            currentKBState = Keyboard.GetState();

            //___
            sourceRect = new Rectangle(currentFrame * spriteWidth, 0, spriteWidth, spriteHeight);


            //__________
            if (currentKBState.GetPressedKeys().Length == 0)
            { 

            //______
            if (currentFrame > 0 && currentFrame < 4)
            {
                currentFrame = 0;
            }

            //_________
            if (currentFrame > 4 && currentFrame < 8)
                {
                    currentFrame = 4;
                }

            //_________
            if (currentFrame > 8 && currentFrame < 12)
                {
                    currentFrame = 8;
                }

            //________
            if (currentFrame > 12 && currentFrame < 16)
                {
                    currentFrame = 12;
                }

            }

            //____________________
            if (currentKBState.IsKeyDown(Keys.Right) == true)
            {
                AnimateRight(gameTime);
                if (position.X < 780)
                {
                    position.X += spriteSpeed;
                }
            }

            //___________________
            if (currentKBState.IsKeyDown(Keys.Left) == true)
            {
                AnimateLeft(gameTime);
                if (position.X > 20)
                {
                    position.X -= spriteSpeed;
                }
            }

            //__________________
            if (currentKBState.IsKeyDown(Keys.Down) == true)
            {
                AnimateDown(gameTime);
                if (position.Y < 575)
                {
                    position.Y += spriteSpeed;
                }
            }


            //__________________
            if (currentKBState.IsKeyDown(Keys.Up) == true)
            {
                AnimateUp(gameTime);
                if (position.Y > 25)
                {
                    position.Y -= spriteSpeed;
                }
            }
             //______________________________________
            Velocity = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);
        }

        //__________________
        public void AnimateRight (GameTime gameTime)
        {
            if (currentKBState != prevoiusKBState)
            {
                currentFrame = 9;
            }

            //________________
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            //_________________
            if (timer > interval)
            {
                currentFrame++;

                if (currentFrame > 11)
                {
                    currentFrame = 8; 
                }
                //_
                timer = 0f;
            } 
        }

        //__________________
        public void AnimateUp(GameTime gameTime)
        {
            if (currentKBState != prevoiusKBState)
            {
                currentFrame = 13;
            }

            //________________
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            //_________________
            if (timer > interval)
            {
                currentFrame++;

                if (currentFrame > 15)
                {
                    currentFrame = 12;
                }
                //_
                timer = 0f;
            }
        }

        //__________________
        public void AnimateDown(GameTime gameTime)
        {
            if (currentKBState != prevoiusKBState)
            {
                currentFrame = 1;
            }

            //________________
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            //_________________
            if (timer > interval)
            {
                currentFrame++;

                if (currentFrame > 3)
                {
                    currentFrame = 0;
                }
                //_
                timer = 0f;
            }
        }

        //__________________
        public void AnimateLeft(GameTime gameTime)
        {
            if (currentKBState != prevoiusKBState)
            {
                currentFrame = 5;
            }

            //________________
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            //_________________
            if (timer > interval)
            {
                currentFrame++;

                if (currentFrame > 7)
                {
                    currentFrame = 4;
                }
                //_
                timer = 0f;
            }
        }





            }

}
