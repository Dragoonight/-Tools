using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace VTP18
{
    class BackgroundManager
    {
        //variabel for player sprite
        Texture2D BackgroundImage;
        //variabel for the timer
        float timer = 0f;
        //variabel for the interval
        float interval = 200f;
        //variabel for the current frame
        int currentFrame = 0;
        //variabel for the sprite height and width
        int spriteWidth = 800;
        int spriteHeight = 490;
        //Variabel for the sprite speed
        int spriteSpeed = 2;
        //Variabel for the rectangle 
        Rectangle sourceRect;
        //Variabel for the rectangle position
        Vector2 position;
        //Variabel Velocity for the 
        Vector2 velocity;

        //Communicates so that the class vector Position returns and it values will return
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        //The same but with the velocity
        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        //the same but with the Texture
        public Texture2D Texture
        {
            get { return BackgroundImage; }
            set { BackgroundImage = value; }
        }

        //The same but with the Rectangle 
        public Rectangle SourceRect
        {
            get { return sourceRect; }
            set { sourceRect = value; }
        }

        //When mentioning the class playermanager it will all of the things under it related
        public BackgroundManager (Texture2D texture, int currentFrame, int spriteWidth, int spriteHeight, Rectangle screenBounds)
        {
            this.BackgroundImage = texture;
            this.currentFrame = currentFrame;
            this.spriteWidth = spriteWidth;
            this.spriteHeight = spriteHeight;
        }

       

        public void Update(GameTime gameTime)
        {

       
        

            //The timer gets higher and higher with the time in milliseconds
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            //If the timer is higher than the interval the currentframe goes + and the resets
            if (timer > interval)
            {
                currentFrame++;
                timer = 0f;

                //Not mentioned is that if the currentframe is higher than 11 then it goes back to 8 again
                if (currentFrame > 0)
                {
                    currentFrame = 4;
                }

            }
        



    }
        
           
        

        public void draw(SpriteBatch spriteBatch)
        {




        }

    }
}

