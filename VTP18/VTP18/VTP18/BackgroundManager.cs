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
        //Variabel for the starting position
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
        //The Constructor
        public BackgroundManager(Texture2D texture)
        {
            this.BackgroundImage = texture;
        }


        //The speed 
        public void Update (GameTime gameTime)
        {
            position.Y += 9;

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

