using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DOTP
{
    public class MouseHandler
    {
        MouseState mouse;

        private Vector2 mousePosition;

        public MouseHandler()
        {
            mousePosition = Vector2.Zero;
        }

        public void update(GameTime gameTime)
        {
            mouse = Mouse.GetState();

            mousePosition = new Vector2((float)mouse.X, (float)mouse.Y);
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Constant.cursor_Tex, Vector2.Transform(mousePosition, Matrix.Invert(Game1.camera.Transform)), Color.White);
        }
    }
}
