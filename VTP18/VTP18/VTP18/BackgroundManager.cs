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

        //Get set
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public Texture2D Texture
        {
            get { return BackgroundImage; }
            set { BackgroundImage = value; }
        }
        public Rectangle SourceRect
        {
            get { return sourceRect; }
            set { sourceRect = value; }
        }

        //The Constructor
        public BackgroundManager (Texture2D texture, int currentFrame, int spriteWidth, int spriteHeight, Rectangle screenBounds)
        {
            this.BackgroundImage = texture;
        }

       
        //The Updates and rules
        public void Update(GameTime gameTime)
        {
            position.Y += 10;

            if (position.Y > 500)
            {
                position.Y = -495;
            }
    }
        //The Draw
        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BackgroundImage,position,Color.White );
            
        }

    }
}

