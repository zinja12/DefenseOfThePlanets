using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DOTP
{
    public class Score
    {
        private float score;

        public Score() 
        {
            score = 0;
        }

        public void addFleetScore() 
        {
            score += 100f;
        }

        public void addFighterScore() 
        {
            score += 50f;
        }

        public void addHitScore() 
        {
            score += 25f;
        }

        public float getScore() 
        {
            return score;
        }

        public void draw(SpriteBatch spriteBatch) 
        {
            Renderer.drawText(spriteBatch, "Score:" + score, Vector2.Zero, Color.White);
        }
    }
}
