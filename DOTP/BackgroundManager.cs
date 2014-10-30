using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DOTP
{
    public class BackgroundManager
    {
        List<Background> backgrounds;

        public BackgroundManager() 
        {
            backgrounds = new List<Background>();
            /*backgrounds.Add(new Background(Vector2.Zero)); //0-Center
            backgrounds.Add(new Background(Vector2.Zero + new Vector2(-Constant.background_Width, 0))); //1-Left
            backgrounds.Add(new Background(Vector2.Zero + new Vector2(-Constant.background_Width, -Constant.background_Height))); //2-UpLeft
            backgrounds.Add(new Background(Vector2.Zero + new Vector2(0, -Constant.background_Height))); //3-Up
            backgrounds.Add(new Background(Vector2.Zero + new Vector2(Constant.background_Width, -Constant.background_Height))); //4-UpRight
            backgrounds.Add(new Background(Vector2.Zero + new Vector2(Constant.background_Width, 0))); //5-Right
            backgrounds.Add(new Background(Vector2.Zero + new Vector2(Constant.background_Width, Constant.background_Height))); //6-DownRight
            backgrounds.Add(new Background(Vector2.Zero + new Vector2(0, Constant.background_Height))); //7-Down
            backgrounds.Add(new Background(Vector2.Zero + new Vector2(-Constant.background_Width, Constant.background_Height))); //8-DownLeft*/

            for (int i = 0; i < 20; i++) 
            {
                for (int j = 0; j < 20; j++) 
                {
                    backgrounds.Add(new Background(Vector2.Zero + new Vector2(i * Constant.background_Width, j * Constant.background_Height)));
                }
            }
        }

        public void update(GameTime gameTime, Vector2 playerPosition) 
        {
            
        }

        public void draw(SpriteBatch spriteBatch) 
        {
            foreach (Background background in backgrounds) 
            {
                background.draw(spriteBatch);
            }
        }
    }
}
