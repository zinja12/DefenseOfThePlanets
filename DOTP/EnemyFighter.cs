using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DOTP
{
    public class EnemyFighter
    {
        private Vector2 position;
        private Vector2 targetPosition;
        private Vector2 origin;
        private float rotation;
        private bool attacking;
        private String targetDesignation;
        private Rectangle collision_Rect;
        private String focus;
        private Vector2 playerPosition;
        private int health;
        private bool remove;

        public EnemyFighter(Vector2 fleetPosition, Vector2 targetPosition, String targetDesignation) 
        {
            this.position = fleetPosition;
            this.targetPosition = targetPosition;
            origin = new Vector2((float)(Constant.fighterSize / 2), (float)(Constant.fighterSize / 2));
            rotation = 0;
            attacking = false;
            this.targetDesignation = targetDesignation;
            collision_Rect = new Rectangle((int)position.X, (int)position.Y, Constant.fighterSize, Constant.fighterSize);
            focus = "target";
            health = 2;
            remove = false;
        }

        public void update(GameTime gameTime, Vector2 playerPosition) 
        {
            //Update the position of the rectangle
            collision_Rect = new Rectangle((int)position.X, (int)position.Y, Constant.fighterSize, Constant.fighterSize);
            this.playerPosition = playerPosition;

            if (focus.Equals("target"))
            {
                handleRotation(targetPosition);
                if (targetDesignation.Equals("small"))
                {
                    if (Vector2.Distance(position, targetPosition) > (Constant.planet_small_Size / 2) + 15)
                    {
                        moveToTarget(targetPosition, 2f);
                    }
                }
                else if (targetDesignation.Equals("large"))
                {
                    if (Vector2.Distance(position, targetPosition) > (Constant.planet_large_Size / 2) + 15)
                    {
                        moveToTarget(targetPosition, 2f);
                    }
                }
                else
                {
                    if (Vector2.Distance(position, targetPosition) > (Constant.planet_normal_Size / 2) + 15)
                    {
                        moveToTarget(targetPosition, 2f);
                    }
                }

                attack(targetDesignation);
            }
            else if (focus.Equals("player")) 
            {
                handleRotation(playerPosition);
                moveToTarget(playerPosition, 2f);
                attack(targetDesignation);
            }

            if (health <= 0) 
            {
                remove = true;
            }
        }

        public void setFocus(String focus) 
        {
            this.focus = focus;
        }

        public int getHealth() 
        {
            return health;
        }

        public void subtractHit(int subtraction) 
        {
            this.health -= subtraction;
        }

        public bool getToRemove() 
        {
            return remove;
        }

        private void moveToTarget(Vector2 targetPosition, float speed) 
        {
            Vector2 direction = targetPosition - position;
            direction.Normalize();

            position += direction * speed;
        }

        private void handleRotation(Vector2 target) 
        {
            Vector2 rotationTarget = Vector2.Transform(new Vector2(target.X, target.Y), Matrix.Invert(Game1.camera.Transform));

            Vector2 distance = position - rotationTarget;
            rotation = (float)Math.Atan2((double)distance.Y, (double)distance.X);
            rotation -= (float)(Math.PI * 0.5f);
        }

        private void attack(String targetDesignation) 
        {
            if (focus.Equals("target"))
            {
                if (targetDesignation.Equals("small"))
                {
                    if (Vector2.Distance(position, targetPosition) <= ((Constant.planet_small_Size / 2) + 15))
                    {
                        attacking = true;
                    }
                    else
                    {
                        attacking = false;
                    }
                }
                else if (targetDesignation.Equals("large"))
                {
                    if (Vector2.Distance(position, targetPosition) <= ((Constant.planet_large_Size / 2) + 15))
                    {
                        attacking = true;
                    }
                    else
                    {
                        attacking = false;
                    }
                }
                else
                {
                    if (Vector2.Distance(position, targetPosition) <= ((Constant.planet_normal_Size / 2) + 15))
                    {
                        attacking = true;
                    }
                    else
                    {
                        attacking = false;
                    }
                }
            }
            else if (focus.Equals("player")) 
            {
                if (Vector2.Distance(position, playerPosition) <= (Constant.fighterSize * 2))
                {
                    attacking = true;
                }
                else 
                {
                    attacking = false;
                }
            }
        }

        public bool getAttacking() 
        {
            return attacking;
        }

        public Vector2 getPosition() 
        {
            return position;
        }

        public Rectangle getCollisionRect()
        {
            return collision_Rect;
        }

        public Vector2 getOrigin() 
        {
            return origin + position;
        }

        public void checkEnemyFighterCollision(EnemyFighter fighter) 
        {
            if (fighter.getCollisionRect().Contains(new Point((int)position.X, (int)(position.Y + (Constant.fighterSize / 2))))) 
            {
                position.X += 1f;
            }
            else if (fighter.getCollisionRect().Contains(new Point((int)position.X + Constant.fighterSize, (int)(position.Y + (Constant.fighterSize / 2))))) 
            {
                position.X -= 1f;
            }
            else if (fighter.getCollisionRect().Contains(new Point((int)position.X + (Constant.fighterSize / 2), (int)(position.Y + Constant.fighterSize))))
            {
                position.Y -= 1f;
            }
            else if (fighter.getCollisionRect().Contains(new Point((int)position.X + (Constant.fighterSize / 2), (int)(position.Y + (Constant.fighterSize / 2)))))
            {
                position.Y += 1f;
            }
            else if (fighter.getCollisionRect().Contains(new Point((int)position.X, (int)position.Y)))
            {
                position.X -= 1f;
                position.Y -= 1f;
            }
            else if (fighter.getCollisionRect().Contains(new Point((int)position.X + Constant.fighterSize, (int)position.Y)))
            {
                position.X += 1f;
                position.Y -= 1f;
            }
            else if (fighter.getCollisionRect().Contains(new Point((int)position.X + Constant.fighterSize, (int)position.Y + Constant.fighterSize)))
            {
                position.X += 1f;
                position.Y += 1f;
            }
            else if (fighter.getCollisionRect().Contains(new Point((int)position.X, (int)position.Y + Constant.fighterSize)))
            {
                position.X -= 1f;
                position.Y += 1f;
            }
        }

        public void draw(SpriteBatch spriteBatch) 
        {
            if (attacking) 
            {
                if (focus.Equals("target")) 
                {
                    Renderer.DrawALine(spriteBatch, Constant.pixel_Tex, 7, Color.Cyan, position, targetPosition);
                }
                else if (focus.Equals("player")) 
                {
                    Renderer.DrawALine(spriteBatch, Constant.pixel_Tex, 7, Color.Cyan, position, playerPosition);
                }
            }
            spriteBatch.Draw(Constant.player_Tex, position, null, Color.Red, rotation, origin, 1.0f, SpriteEffects.None, 0);
        }
    }
}
