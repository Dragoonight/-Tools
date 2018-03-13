using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Tools_starfield
{
    class CollisionsManager
    {
        private PlayerManager playerManager;
        private EnemyManager enemyManager;
        //private ExplosionManager explosionManager;
        private Vector2 offScreen = new Vector2(-500, -500);

        public CollisionsManager(PlayerManager playerSprite, EnemyManager enemyManager)
        {
            this.playerManager = playerSprite;
            //this.explosionManager = explosionManager;
            this.enemyManager = enemyManager;
        }

        private void checkShotToEnemyCollosions()
        {
            foreach (Sprite shot in playerManager.PlayerShotManager.shots)
            {
                foreach (Enemy enemy in enemyManager.Enemies)
                {
                    if (shot.IsCircleColliding(enemy.EnemySprite.Center, enemy.EnemySprite.CollisionRadius))
                    {
                        shot.Position = offScreen;
                        enemy.Destroyed = true;

                        //explosionManager.AddExplosion(enemy.EnemySprite.Center, enemy.EnemySprite.Velocity/ 10);
                    }
                }
            }
        }
        private void checkShotToPlayerCollisions ()
        {
            foreach (Sprite shot in enemyManager.EnemyShotManager.shots)
            {
                if (shot.IsCircleColliding(playerManager.Center, playerManager.CollisionRadius))
                {
                    shot.Position = offScreen;
                    playerManager.Destroyed = true;
                    
                    //explosionManager.AddExplosion(playerManager.Center,Vector2.Zero;        
                }
            }
        }

        private void checkEnemyToPlayerCollisions()
        {
            foreach (Enemy enemy in enemyManager.Enemies)
            {
                if (enemy.EnemySprite.IsCircleColliding(playerManager.Position, playerManager.CollisionRadius))
                {
                    enemy.Destroyed = true;

                    //explosionManager.AddExplosion(enemy.EnemySprite.Center, enemy.EnemySprite.Velocity/ 10;

                    playerManager.Destroyed = true;

                    //explosionManager.AddExplosion(playerManager.Position, Vector2.zero);
                }
            }
        }

        public void CheckCollisions()
        {
            checkShotToEnemyCollosions();

            if (!playerManager.Destroyed)
            {
                checkShotToPlayerCollisions();
                checkEnemyToPlayerCollisions();
            }
        }
    }
}
