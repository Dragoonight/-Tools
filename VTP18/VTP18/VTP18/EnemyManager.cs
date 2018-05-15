using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace VTP18
{
    class EnemyManager
    {
        private Texture2D texture;
        private Rectangle initialFrame;
        private int frameCount;

        public List<Enemy> Enemies = new List<Enemy>();

        public ShootManager EnemyShotManager;
        private PlayerManager playerManager;

        public int MinShipsPerWave = 5;
        public int MaxShipsPerWave = 8;
        private float nextWaveTimer = 0.0f;
        private float nextWaveMinTimer = 8.0f;
        private float shipsSpawnTimer = 0.0f;
        private float shipsSpawnWaitTimer = 0.5f;

        private float shipsShotChance = 0.2f;

        private List<List<Vector2>> pathWaypoints = new List<List<Vector2>>();

        private Dictionary<int, int> waveSpawns = new Dictionary<int, int>();

        public bool Active = true;

        private Random rand = new Random();

        private void SetUpWaypoints()
        {
            List<Vector2> path0 = new List<Vector2>();
            path0.Add(new Vector2(300, -100));
            path0.Add(new Vector2(300, 550));
            pathWaypoints.Add(path0);
            waveSpawns[0] = 0;

            List<Vector2> path1 = new List<Vector2>();
            path1.Add(new Vector2(450, -100));
            path1.Add(new Vector2(450, 550));
            pathWaypoints.Add(path1);
            waveSpawns[1] = 0;


        }

        public EnemyManager(Texture2D texture, Rectangle initialFrame, int frameCount, PlayerManager playerSprite, Rectangle screenBounds)
        {
            this.texture = texture;
            this.initialFrame = initialFrame;
            this.frameCount = frameCount;
            this.playerManager = playerSprite;

            EnemyShotManager = new ShootManager(texture, new Rectangle(0, 300, 5, 5), 4, 2, 150f, screenBounds);

            SetUpWaypoints();
        }

        public void SpawnEnemy(int path)
        {
            Enemy thisEnemy = new Enemy(texture, pathWaypoints[path][0], initialFrame, frameCount);

            for (int x = 0; x < pathWaypoints[path].Count(); x++)
            {
                thisEnemy.Addwaypoint(pathWaypoints[path][x]);
            }
            Enemies.Add(thisEnemy);
        }

        public void SpawnWave(int waveType)
        {
            waveSpawns[waveType] += rand.Next(MinShipsPerWave, MaxShipsPerWave + 1);
        }

        private void updateWaveSpawns(GameTime gameTime)
        {
            shipsSpawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (shipsSpawnTimer > shipsSpawnWaitTimer)
            {
                for (int x = waveSpawns.Count - 1; x >= 0; x--)
                {
                    if (waveSpawns[x] > 0)
                    {
                        waveSpawns[x]--;
                        SpawnEnemy(x);
                    }
                }
                shipsSpawnTimer = 0f;
            }
            nextWaveTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (nextWaveTimer > nextWaveMinTimer)
            {
                SpawnWave(rand.Next(0, pathWaypoints.Count));
                nextWaveTimer = 0f;
            }
        }

        public void Update(GameTime gameTime)
        {
            EnemyShotManager.Update(gameTime);

            for (int x = Enemies.Count - 1; x >= 0; x--)
            {
                Enemies[x].Update(gameTime);
                if (Enemies[x].IsActive() == false)
                {
                    Enemies.RemoveAt(x);
                }
                else
                {
                    if ((float)rand.Next(0, 1000) / 10 <= shipsShotChance)
                    {
                        Vector2 fireLoc = Enemies[x].EnemySprite.Position;
                        fireLoc += Enemies[x].gunOffset;

                        Vector2 ShotDirection = playerManager.Position - fireLoc;

                        ShotDirection.Normalize();

                        EnemyShotManager.FireShot(fireLoc, ShotDirection, false);
                    }
                }

            }
            if (Active)
            {
                updateWaveSpawns(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            EnemyShotManager.Draw(spriteBatch);

            foreach (Enemy enemy in Enemies)
            {
                enemy.Draw(spriteBatch);
            }
        }

    }
}
