using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;



namespace VTP18
{
    class BackgoundManager2
    {
        Texture2D BackgroundImage2;

        //Variaberectanglel for the  
        Rectangle sourceRect;

        Vector2 position2 = new Vector2 (0, -490);


        public Texture2D texture2
        {
            get { return BackgroundImage2; }
            set { BackgroundImage2 = value; }
        }

        public Vector2 Position2
        {
            get { return position2; }
            set { position2 = value; }
        }

        //The same but with the Rectangle 
        public Rectangle SourceRect
        {
            get { return sourceRect; }
            set { sourceRect = value; }
        }

        //When mentioning the class playermanager it will all of the things under it related
        public BackgoundManager2 (Texture2D texture, int currentFrame, int spriteWidth, int spriteHeight, Rectangle screenBounds)
        {
            this.BackgroundImage2 = texture;
        }


        public void Update(GameTime gameTime)
        {
            position2.Y += 10;

            if (position2.Y > 500)
            {
                position2.Y = -495;
            }

        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BackgroundImage2, position2, Color.White);
        }
    }
}
