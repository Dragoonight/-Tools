using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace VTP18
{
    class Enemy
    {
        //Variables and classes
        public Sprite EnemySprite;
        public Vector2 gunOffset = new Vector2(25, 25);
        private Queue<Vector2> waypoints = new Queue<Vector2>();
        private Vector2 currentWaypoint = Vector2.Zero;
        private float speed = 120f;
        public bool Destroyed = false;
        private int enemyRadius = 15;
        private Vector2 previousPosition = Vector2.Zero;

        //The enemy Constructer and some rules for enemy movement, speed
        public Enemy (Texture2D texture, Vector2 Position, Rectangle initialFrame, int frameCount)
        {
            EnemySprite = new Sprite(Position, texture, initialFrame, Vector2.Zero);

            for (int t = 1; t < frameCount; t++)
            {
                EnemySprite.AddFrame(new Rectangle(initialFrame.X = (initialFrame.Width * t), initialFrame.Y, initialFrame.Width, initialFrame.Height));
            }

            previousPosition = Position;
            currentWaypoint = Position;
            EnemySprite.CollisionRadius = enemyRadius;
        }
        //A line of waypoints
        public void Addwaypoint(Vector2 waypoint)
        {
            waypoints.Enqueue(waypoint);
        }
        //When the waypoints has been reached the corresonding things will happen
        public bool WaypointReached()
        {
            if (Vector2.Distance(EnemySprite.Position, currentWaypoint) < (float) EnemySprite.Source.Width / 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //
        public bool IsActive()
        {
            if (Destroyed)
            {
                return false;
            }

            if  (waypoints.Count > 0)
            {
                return true;
            }

            if (WaypointReached())
            {
                return false;
            }

            return true;
        }


        public void Update (GameTime gameTime)
        {
            if  (IsActive())
                    {
                Vector2 heading = currentWaypoint - EnemySprite.Position;
                if (heading != Vector2.Zero)
                {
                    heading.Normalize();
                }
                heading *= speed;
                EnemySprite.Velocity = heading;
                previousPosition = EnemySprite.Position;
                EnemySprite.Update(gameTime);
                EnemySprite.Rotation = (float)Math.Atan2(EnemySprite.Position.Y - previousPosition.Y, EnemySprite.Position.X - previousPosition.X);
                
                if (WaypointReached())
                {
                    if (waypoints.Count > 0)
                    {
                        currentWaypoint = waypoints.Dequeue();
                    }
                }
                     
            }
        }

        public void Draw (SpriteBatch spriteBatch)
        {
            if (IsActive())
            {
                EnemySprite.Draw(spriteBatch);
            }
        }




    }
}
