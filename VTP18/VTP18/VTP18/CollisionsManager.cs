using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace VTP18
{
    class CollisionsManager
    {
        //Variables
        private PlayerManager playerManager;
        private EnemyManager enemyManager;
        private ExplosionManager explosionManager;
        public int playerLife = 5;
        public int enemyLife = 1;
        public int playerScore = 0;


        // Get/Set
        public int PlayerLife
        {
            get { return playerLife; }
            set { playerLife = value; }
        }
        public int ScoreBoard
        {
            get { return enemyLife; }
            set { enemyLife = value; }
        }



        //Offscreen deletes things on the screen that goes out from ScreenBounds
        private Vector2 offScreen = new Vector2(-500, -500);

        //The Cunstructor
        public CollisionsManager (PlayerManager playerSprite, ExplosionManager explosionManager, EnemyManager enemyManager)//int playerLife
        {
            this.playerManager = playerSprite;
            this.explosionManager = explosionManager;
            this.enemyManager = enemyManager;
            //this.playerLife = playerLife;
        } 
         //Checks the enemy collision
        private void checkShotToEnemyCollisions()
        {
            foreach (Sprite shot in playerManager.PlayerShotManager.shots)
            {
                foreach (Enemy enemy in enemyManager.Enemies)
                {
                    if (shot.IsCircleColliding(enemy.EnemySprite.Center, enemy.EnemySprite.CollisionRadius))
                    {
                        enemyLife--;
                        playerScore++;
                        shot.Position = offScreen;
                        enemy.Destroyed = true;
                        

                        explosionManager.AddExplosion(enemy.EnemySprite.Center, enemy.EnemySprite.Velocity / 10);
                        
                    }
                }
            }
        }
        //Checks player Collsion
        private void checkShotToPlayerCollisions()
        {
            foreach (Sprite shot in enemyManager.EnemyShotManager.shots)
                if (shot.IsCircleColliding(
                       playerManager.Center,
                       playerManager.CollisionRadius))
                {
                    shot.Position = offScreen;
                    playerLife--;
                    if (playerLife < 1)
                    {
                        playerManager.Destroyed = true;
                        explosionManager.AddExplosion(
                        playerManager.Position,
                        Vector2.Zero);
                    }
                }
        }
        //Checks the collsion between enemy and player and the rules
        private void checkEnemyToPlayerCollisions()
        {
            foreach (Enemy enemy in enemyManager.Enemies)
                if (enemy.EnemySprite.IsCircleColliding(
                        playerManager.Position,
                        playerManager.CollisionRadius))
                {
                    enemy.Destroyed = true;
                    explosionManager.AddExplosion(
                    enemy.EnemySprite.Center,
                    enemy.EnemySprite.Velocity / 10);
                    playerScore++;
                    playerLife--;
                    if (playerLife < 1)
                    {
                        playerManager.Destroyed = true;
                        explosionManager.AddExplosion(
                        playerManager.Position,
                        Vector2.Zero);
                    }
                }
        }
        //Checks 
        public void CheckCollisions(GameTime gameTime) 
        {
            checkShotToEnemyCollisions();
            if (!playerManager.Destroyed)
            {
                checkShotToPlayerCollisions();
                checkEnemyToPlayerCollisions();
               
            }
        }


    }
}
