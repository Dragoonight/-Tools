using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace VTP18
{
    class CreditsManager2
    {
        //Variables
        Texture2D CreditsBG2;

        Vector2 position = new Vector2(0, 1220);

        //Get set
        public Texture2D texture2
        {
            get { return CreditsBG2; }
            set { CreditsBG2 = value; }
        }
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        //The Constuctorn
        public CreditsManager2(Texture2D texture)
        {
            this.CreditsBG2 = texture;
           
        }

        //The Update
        public void Update(GameTime gameTime)
        {
            position.Y -= 3;

            if (position.Y < 0)
            {
                position.Y  *= 0;
            }

        }


        //The Draw
        public void draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(CreditsBG2, position, Color.White);

        }
    }
}
