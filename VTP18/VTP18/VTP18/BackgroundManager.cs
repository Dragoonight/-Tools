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
        //variabel for player sprite
    {
        Texture2D BackgroundImage;

       

        //Variaberectanglel for the  
        Rectangle sourceRect;
        //Variabel for the rectangle position
        Vector2 position = new Vector2 (0, 0);

       

        //Communicates so that the class vector Position returns and it values will return
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
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
        }

       

        public void Update(GameTime gameTime)
        {


            position.Y += 10;

            if (position.Y > 500)
            {
                position.Y = -495;
            }

          

            //    //The timer gets higher and higher with the time in milliseconds
            //    timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            ////If the timer is higher than the interval the currentframe goes + and the resets
            //if (timer > interval)
            //{
            //    currentFrame++;
            //    timer = 0f;

            //    //Not mentioned is that if the currentframe is higher than 11 then it goes back to 8 again
            //    if (currentFrame > 0)
            //    {
            //        currentFrame = 4;
            //    }

            //}
        



    }
        
        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BackgroundImage,position,Color.White );
            
        }

    }
}

