using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DOTP
{
    public class MouseHighlighter
    {
        private Vector2 startPosition;
        private Vector2 currentPosition;
        private bool startPosSet;
        private Rectangle collision_Rect;

        public MouseHighlighter() 
        {
            startPosition = Vector2.Zero;
            currentPosition = Vector2.Zero;
            startPosSet = false;
            collision_Rect = new Rectangle(0, 0, 0, 0);
        }

        public void update(GameTime gameTime) 
        {
            MouseState mouse = Mouse.GetState();

            currentPosition = Vector2.Transform(new Vector2((float)mouse.X, (float)mouse.Y), Matrix.Invert(Game1.camera.Transform));

            if (mouse.RightButton == ButtonState.Pressed) 
            {
                if (!startPosSet)
                {
                    startPosition = currentPosition;
                    startPosSet = true;
                }
                collision_Rect = new Rectangle((int)currentPosition.X, (int)startPosition.Y, (int)Math.Abs(currentPosition.X - startPosition.X), (int)Math.Abs(currentPosition.Y - startPosition.Y));
            }
            else 
            {
                startPosSet = false;
                startPosition = Vector2.Zero;
                collision_Rect = new Rectangle(0, 0, 0, 0);
            }
        }

        public Vector2 getStartPosition() 
        {
            return startPosition;
        }

        public Vector2 getCurrentPosition() 
        {
            return currentPosition;
        }

        public bool getStartPosSet() 
        {
            return startPosSet;
        }

        public Rectangle getCollisionRect() 
        {
            return collision_Rect;
        }

        public void draw(SpriteBatch spriteBatch) 
        {
            if (startPosSet)
            {
                Renderer.DrawALine(spriteBatch, Constant.pixel_Tex, 1, Color.LightGreen, startPosition, new Vector2(currentPosition.X, startPosition.Y));
                Renderer.DrawALine(spriteBatch, Constant.pixel_Tex, 1, Color.LightGreen, startPosition, new Vector2(startPosition.X, currentPosition.Y));
                Renderer.DrawALine(spriteBatch, Constant.pixel_Tex, 1, Color.LightGreen, new Vector2(startPosition.X, currentPosition.Y), currentPosition);
                Renderer.DrawALine(spriteBatch, Constant.pixel_Tex, 1, Color.LightGreen, new Vector2(currentPosition.X, startPosition.Y), currentPosition);
            }
        }
    }
}
