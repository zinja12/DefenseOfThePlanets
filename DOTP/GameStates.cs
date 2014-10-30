using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DOTP
{
    public class GameStates
    {
        public enum GameState 
        {
            GAMEPLAY,
            MAP,
        }

        public static GameState currentState;

        BattleOverseer battle_overseer;
        Score score;
        Map map;

        public GameStates(Vector2 playerPosition) 
        {
            currentState = GameState.GAMEPLAY;
            score = new Score();
            map = new Map();
            battle_overseer = new BattleOverseer(playerPosition, score, map);
        }

        public void update(GameTime gameTime) 
        {
            if (currentState == GameState.GAMEPLAY) 
            {
                battle_overseer.update(gameTime);

                Game1.camera.Update(battle_overseer.getPlayerPosition());
            }
            else if (currentState == GameState.MAP) 
            {
                map.update(gameTime);

                if (map.getClicked()) 
                {
                    battle_overseer.setPlayerPosition(map.getMovePosition());
                    currentState = GameStates.GameState.GAMEPLAY;
                }
            }
        }

        public void draw(SpriteBatch spriteBatch) 
        {
            if (currentState == GameState.GAMEPLAY)
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Game1.camera.Transform);

                battle_overseer.draw(spriteBatch);

                spriteBatch.End();

                spriteBatch.Begin();
                score.draw(spriteBatch);
                spriteBatch.End();
            }
            else if (currentState == GameState.MAP)
            {
                spriteBatch.Begin();

                spriteBatch.Draw(Constant.stars_Tex, Vector2.Zero, Color.White);
                map.draw(spriteBatch);

                spriteBatch.End();
            }
        }
    }
}
