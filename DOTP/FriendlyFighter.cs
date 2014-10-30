using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DOTP
{
    public class FriendlyFighter
    {
        private Vector2 position;
        private Vector2 targetPosition;
        private Vector2 planetPosition;
        private Rectangle collision_rect;
        private float rotation;
        private Vector2 origin;

        public FriendlyFighter(Vector2 planetPosition) 
        {
            this.position = planetPosition;
            this.planetPosition = planetPosition;
            collision_rect = new Rectangle((int)position.X, (int)position.Y, Constant.fighterSize, Constant.fighterSize);
            rotation = 0;
            origin = new Vector2((float)(Constant.fighterSize / 2), (float)(Constant.fighterSize / 2));
        }

        public void update(GameTime gameTime) 
        {
            //Update collision rectangle and origin
            collision_rect = new Rectangle((int)position.X, (int)position.Y, Constant.fighterSize, Constant.fighterSize);
        }

        private void gainFocus(EnemyFighter enemyFighter) 
        {
            if (Vector2.Distance(position, enemyFighter.getPosition()) < 60f)
            {
                moveToTargetPosition(enemyFighter.getPosition(), 2f);
                handleRotation(enemyFighter.getOrigin());
            }
            else 
            {
                //Wait for player to update RTS style
            }
        }

        private void moveToTargetPosition(Vector2 targetPosition, float speed) 
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

        public void draw(SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(Constant.player_Tex, position, null, Color.Blue, rotation, origin, 1.0f, SpriteEffects.None, 0);
        }
    }
}
