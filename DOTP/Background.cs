using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DOTP
{
    public class Background
    {
        public Vector2 position;
        public int count;

        public Background(Vector2 position) 
        {
            this.position = position;
            count = 0;
        }

        public void draw(SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(Constant.stars_Tex, position, Color.White);
        }
    }
}
