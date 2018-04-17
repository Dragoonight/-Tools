using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace VTP18
{
    class Starfield
    {
        //This class makes new a List for the sprite 
        private List<Sprite> stars = new List<Sprite>();
        //ScreenWidth in int
        private int screenWidth = 500;
        //ScreenHeight in int
        private int ScreenHeight = 300;
        //A class for randomness
        private Random rand = new Random();
        //A class for all the colors that will be in use
        private Color[] colors = {Color.White, Color.WhiteSmoke, Color.SlateGray};

        //The StarField class will have all the mentioned things assigned to it
        public Starfield(int screenWidth, int ScreenHeight, int starCount, Vector2 starVelocity, Texture2D texture, Rectangle frameRectangle)
        {
            //ScreenWidth that is private is getting the corresponding values from the other screenWidth
            this.screenWidth = screenWidth;
            //ScreenHeight that is private is getting the corresponding values from the other screenHeight
            this.ScreenHeight = ScreenHeight;
            //Need help here
            for (int x = 0; x < starCount; x++)
            {
                stars.Add(new Sprite(
                    new Vector2(rand.Next(0, screenWidth),
                    rand.Next(0, ScreenHeight)),
                    texture,
                    frameRectangle,
                    starVelocity));
                Color starColor = colors[rand.Next(0, colors.Count())];
                starColor *= (float)(rand.Next(30, 80) / 100f); stars[stars.Count() - 1].TintColor = starColor;
            }
        }
        //This class udates the position of all the stars
        public void Update(GameTime gameTime)
        {
            //Updates that when the stars  Y axel is higher than screen height it will randomly spawn, i cannot add not remove things here
            foreach (Sprite star in stars)
            {
                star.Update(gameTime);
                if (star.Position.Y > ScreenHeight)
                {
                    star.Position = new Vector2(rand.Next(0, screenWidth), 0);
                }
            }
        }
        //This class draws the spriteBatch we need
        public void Draw(SpriteBatch spriteBatch)
        {
           //Draws the sprite star but i cannot add or remove things here 
            foreach (Sprite star in stars)
            {
                star.Draw(spriteBatch);
            }
        }



    }
}
