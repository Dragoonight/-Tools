using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace VTP18
{
    class ShootManager
    {
        //Variables in shoot manager
        public List<Sprite> shots = new List<Sprite>();
        private Rectangle screenBounds;

        private static Texture2D Texture;
        private static Rectangle InitialFrame;
        private static int FrameCount;
        private float ShotsSpeed;
        private static int CollisionRadius;

        //The constructor
        public ShootManager (Texture2D texture,Rectangle initialFrame, int frameCount, int collisionRadius, float shotsSpeed, Rectangle screenBounds)
        {
            Texture = texture;
            InitialFrame = initialFrame;
            FrameCount = frameCount;
            CollisionRadius = collisionRadius;
            this.ShotsSpeed = shotsSpeed;
            this.screenBounds = screenBounds;
        }
        //Fireshot function 
        public void FireShot(Vector2 position, Vector2 velocity, bool playerField)
        {
            Sprite thisShot = new Sprite(position, Texture, InitialFrame, velocity);

            thisShot.Velocity *= ShotsSpeed;

            for (int t = 1; t < FrameCount; t++)
            {
                thisShot.AddFrame(new Rectangle(InitialFrame.X + (InitialFrame.Width * t), InitialFrame.Y, InitialFrame.Width, InitialFrame.Height));
            }

            thisShot.CollisionRadius = CollisionRadius;
            shots.Add(thisShot);
        }
        //Update 
        public void Update (GameTime gameTime)
        {
            for (int t = shots.Count - 1; t >= 0; t--)
            {
                shots[t].Update(gameTime);
                if (!screenBounds.Intersects(shots[t].Destination))
                {
                    shots.RemoveAt(t);
                }
            }
        }
        //Draw the corresponding shots visually 
        public void Draw (SpriteBatch spriteBatch)
        {
            foreach (Sprite shot in shots)
            {
                shot.Draw(spriteBatch);
            }
        }

    }
}
