using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DOTP
{
    public class Renderer
    {
        public static float lineAngle;
        public static float lineLength;

        public static void DrawALine(SpriteBatch batch, Texture2D blank,
              float width, Color color, Vector2 point1, Vector2 point2)
        {
            float angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
            float length = Vector2.Distance(point1, point2);
            lineAngle = angle;
            lineLength = length;

            batch.Draw(blank, point1, null, color,
                       angle, Vector2.Zero, new Vector2(length, width),
                       SpriteEffects.None, 0);
        }

        public static void drawText(SpriteBatch spriteBatch, String text, Vector2 position, Color color) 
        {
            for (int i = 0; i < text.Length; i++) 
            {
                for (int j = 0; j < Constant.characters.Length; j++) 
                {
                    if (text[i] == Constant.characters[j])
                    {
                        spriteBatch.Draw(Constant.font_Tex, new Vector2(position.X + (i * Constant.letter_size), position.Y),
                            new Rectangle((j * Constant.letter_size), 0, Constant.letter_size, Constant.letter_size), color);
                    }
                }
            }
        }
    }
}
