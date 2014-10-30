using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DOTP
{
    public class Player
    {
        private Vector2 position;
        private Vector2 velocity;
        private Vector2 origin;
        private float rotation;

        public List<Laser> lasers;

        private readonly TimeSpan intervalBetweenAttack = TimeSpan.FromMilliseconds(750);
        TimeSpan lastTimeAttack;

        public Player(Vector2 position)
        {
            this.position = position;
            velocity = Vector2.Zero;
            origin = new Vector2((float)(Constant.fighterSize / 2), (float)(Constant.fighterSize / 2));
            lasers = new List<Laser>();
        }

        public void update(GameTime gameTime)
        {
            handleInput();
            position += velocity;

            attack(gameTime);
            updateLasers(gameTime);
        }

        private void handleInput()
        {
            KeyboardState keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.W))
            {
                velocity.Y = -2f;
            }
            else if (keyboard.IsKeyDown(Keys.S))
            {
                velocity.Y = 2f;
            }
            else
            {
                velocity.Y = 0f;
            }

            if (keyboard.IsKeyDown(Keys.A))
            {
                velocity.X = -2f;
            }
            else if (keyboard.IsKeyDown(Keys.D))
            {
                velocity.X = 2f;
            }
            else
            {
                velocity.X = 0f;
            }

            handleRotation();
        }

        private void handleRotation()
        {
            MouseState mouse = Mouse.GetState();

            Vector2 rotationTarget = Vector2.Transform(new Vector2((float)mouse.X, (float)mouse.Y), Matrix.Invert(Game1.camera.Transform));

            Vector2 distance = new Vector2(position.X - rotationTarget.X, position.Y - rotationTarget.Y);
            rotation = (float)Math.Atan2((double)distance.Y, (double)distance.X);
            rotation -= (float)(Math.PI * 0.5f);
        }

        private void attack(GameTime gameTime) 
        {
            MouseState mouse = Mouse.GetState();
            Vector2 mousePosition = Vector2.Transform(new Vector2((float)mouse.X, (float)mouse.Y), Matrix.Invert(Game1.camera.Transform));

            if ((lastTimeAttack + intervalBetweenAttack) < gameTime.TotalGameTime && mouse.LeftButton == ButtonState.Pressed)
            {
                lasers.Add(new Laser(getOrigin(), mousePosition));
                lastTimeAttack = gameTime.TotalGameTime;
            }
        }

        private void updateLasers(GameTime gameTime) 
        {
            if (lasers.Count > 0) 
            {
                for (int i = 0; i < lasers.Count; i++)
                {
                    lasers[i].update(gameTime);

                    if (lasers[i].getToRemove())
                    {
                        lasers.RemoveAt(i);
                    }
                }
            }
        }

        public Vector2 getPosition()
        {
            return position;
        }

        public Vector2 getOrigin() 
        {
            return position + new Vector2(Constant.fighterSize / 2, Constant.fighterSize / 2);
        }

        public void setPlayerPosition(Vector2 position)
        {
            this.position = position;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Constant.player_Tex, position, null, Color.White, rotation, origin, 1.0f, SpriteEffects.None, 0);
            foreach (Laser laser in lasers) 
            {
                laser.draw(spriteBatch);
            }
        }
    }
}
