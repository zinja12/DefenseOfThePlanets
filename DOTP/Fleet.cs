using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DOTP
{
    public class Fleet
    {
        private Vector2 position;
        public List<EnemyFighter> fighters;
        private Vector2 planetPosition;
        private String targetPlanetDesignation;
        private double time;
        private int health;
        private bool remove;
        private Rectangle collision_Rect;

        public Fleet(Vector2 position, Vector2 planetPosition, String targetPlanetDesignation) 
        {
            this.position = position;
            this.planetPosition = planetPosition;
            this.targetPlanetDesignation = targetPlanetDesignation;
            collision_Rect = new Rectangle((int)position.X, (int)position.Y, Constant.fleetSize, Constant.fleetSize);
            health = 10;
            remove = false;
            time = 0;
            fighters = new List<EnemyFighter>();
            if (fighters.Count == 0) 
            {
                fighters.Add(new EnemyFighter(position, planetPosition, targetPlanetDesignation));
            }
        }

        public void update(GameTime gameTime, Vector2 playerPosition) 
        {
            collision_Rect = new Rectangle((int)position.X, (int)position.Y, Constant.fleetSize, Constant.fleetSize);

            foreach (EnemyFighter fighter in fighters) 
            {
                fighter.update(gameTime, playerPosition);
            }

            time += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (time >= 3000) 
            {
                addFighter();
                time = 0;
            }

            if (health <= 0) 
            {
                remove = true;
            }
        }

        public Rectangle getCollisionRect() 
        {
            return collision_Rect;
        }

        public int getHealth() 
        {
            return health;
        }

        public bool checkCollision(Laser laser) 
        {
            if (collision_Rect.Intersects(laser.getCollisionRect()))
            {
                return true;
            }
            return false;
        }

        private void addFighter() 
        {
            if (fighters.Count < 10)
            {
                fighters.Add(new EnemyFighter(position, planetPosition, targetPlanetDesignation));
            }
        }

        public void subtractHealth(int subtraction) 
        {
            health -= subtraction;
        }

        public bool getToRemove() 
        {
            return remove;
        }

        public Vector2 getTargetPlanetOrigin() 
        {
            return planetPosition;
        }

        public void draw(SpriteBatch spriteBatch) 
        {
            foreach (EnemyFighter fighter in fighters)
            {
                fighter.draw(spriteBatch);
            }
            spriteBatch.Draw(Constant.fleet_Tex, position, Color.Red);
        }
    }
}
