using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;



namespace VTP18
{
    class BackgroundManager2
    {
        //Variables
        Texture2D BackgroundImage2;      
    
        Vector2 position2 = new Vector2 (0, -490);

        //Get set
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
        //The Constuctorn
        public BackgroundManager2 (Texture2D texture)
            
        {
            this.BackgroundImage2 = texture;
        }

        //The Update
        public void Update(GameTime gameTime)
        {
            position2.Y += 9;

            if (position2.Y > 500)
            {
                position2.Y = -495;
            }

        }

        
        //The Draw
        public void draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(BackgroundImage2, position2, Color.White);
           
        }    
    }
}
