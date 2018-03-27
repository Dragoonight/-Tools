using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tools_starfield
{
    class ShootManager
    {
        public List<Sprite> shots = new List<Sprite>();
        private Rectangle screenBounds;

        private static Texture2D Texture;
        private static Rectangle InitialFrame;
        private static int FrameCount;
        private float ShotsSpeed;
        private static int CollisionRadius;

        public ShootManager (Texture2D texture,Rectangle initialFrame, int frameCount, int collisionRadius, float shotsSpeed, Rectangle screenBounds)
        {
            Texture = texture;
            InitialFrame = initialFrame;
            FrameCount = frameCount;
            CollisionRadius = collisionRadius;
            this.ShotsSpeed = shotsSpeed;
            this.screenBounds = screenBounds;
        }

        public void FireShot(Vector2 position, Vector2 velocity, bool playerField)
        {
            Sprite thisShot = new Sprite(position, Texture, InitialFrame, velocity);

            thisShot.Velocity *= ShotsSpeed;

            for (int x = 1; x < FrameCount; x++)
            {
                thisShot.AddFrame(new Rectangle(InitialFrame.X + (InitialFrame.Width * x), InitialFrame.Y, InitialFrame.Width, InitialFrame.Height));
            }

            thisShot.CollisionRadius = CollisionRadius;
            shots.Add(thisShot);
        }

        public void Update (GameTime gameTime)
        {
            for (int x = shots.Count - 1; x >= 0; x--)
            {
                shots[x].Update(gameTime);
                if (!screenBounds.Intersects(shots[x].Destination))
                {
                    shots.RemoveAt(x);
                }
            }
        }

        public void Draw (SpriteBatch spriteBatch)
        {
            foreach (Sprite shot in shots)
            {
                shot.Draw(spriteBatch);
            }
        }

    }
}
