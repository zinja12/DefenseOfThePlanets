using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DOTP
{
    public class Laser
    {
        private Vector2 position;
        private Vector2 targetPosition;
        private float rotation;
        private bool remove;
        private Vector2 origin;
        private Rectangle collision_Rect;

        public Laser(Vector2 startPosition, Vector2 targetPosition) 
        {
            this.position = startPosition;
            this.targetPosition = targetPosition;
            origin = new Vector2(7, 5);
            collision_Rect = new Rectangle((int)startPosition.X, (int)startPosition.Y, Constant.laser_size, Constant.laser_size);
            rotation = 0;
            remove = false;
        }

        public void update(GameTime gameTime) 
        {
            collision_Rect = new Rectangle((int)position.X, (int)position.Y, Constant.laser_size, Constant.laser_size);

            moveToPosition(targetPosition, 3f);
            handleRotation(targetPosition);

            if (Vector2.Distance(position, targetPosition) < 1.5f) 
            {
                remove = true;
            }
        }

        public Rectangle getCollisionRect() 
        {
            return collision_Rect;
        }

        private void moveToPosition(Vector2 targetPosition, float speed) 
        {
            Vector2 direction = targetPosition - position;
            direction.Normalize();

            position += direction * speed;
        }

        private void handleRotation(Vector2 targetPosition) 
        {
            Vector2 distance = new Vector2(position.X - targetPosition.X, position.Y - targetPosition.Y);
            rotation = (float)Math.Atan2((double)distance.Y, (double)distance.X);
            //rotation -= (float)(Math.PI * 0.5f);
        }

        public bool getToRemove() 
        {
            return remove;
        }

        public void draw(SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(Constant.laser_Tex, position, null, Color.White, rotation, origin, 1.0f, SpriteEffects.None, 0);
        }
    }
}
