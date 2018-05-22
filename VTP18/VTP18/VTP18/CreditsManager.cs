using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace VTP18
{
    class CreditsManager
    {
        Texture2D CreditsBG;
        //Variabel for the starting position
        Vector2 position = new Vector2(0, 0);
        

        //Get set
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public Texture2D Texture
        {
            get { return CreditsBG; }
            set { CreditsBG = value; }
        }
        //The Constructor
        public CreditsManager(Texture2D texture)
        {
            this.CreditsBG = texture;
            
        }


        //The speed 
        public void Update(GameTime gameTime)
        {
            position.Y -= 3;
        }

        //The Draw
        public void draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(CreditsBG, position, Color.White);

        }
    }
}
