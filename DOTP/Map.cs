using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DOTP
{
    public class Map
    {
        private Vector2 movePosition;
        private bool clicked;
        private Vector2 playerMapPosition;

        List<MapPlanet> mapPlanets;

        public Map() 
        {
            mapPlanets = new List<MapPlanet>();
            movePosition = Vector2.Zero;
            playerMapPosition = Vector2.Zero;
            clicked = false;
        }

        public void update(GameTime gameTime) 
        {
            MouseState mouse = Mouse.GetState();

            if (mouse.LeftButton == ButtonState.Pressed)
            {
                clicked = true;
                movePosition = new Vector2((float)(mouse.X * Constant.mapReduction_Constant), (float)(mouse.Y * Constant.mapReduction_Constant));
            }
            else 
            {
                clicked = false;
            }

            /*for (int i = 0; i < mapPlanets.Count; i++) 
            {
                mapPlanets[i].update(gameTime);

                if (mouse.LeftButton == ButtonState.Pressed && mapPlanets[i].getCollisionRect().Contains(new Point(mouse.X, mouse.Y)))
                {
                    clicked = true;
                    movePosition = mapPlanets[i].getOriginalPlanetPosition();
                }
                else 
                {
                    clicked = false;
                }
            }*/

            handleGameplayTransition();
        }

        public void addPlanetToMap(Planet planet) 
        {
            mapPlanets.Add(new MapPlanet(planet));
        }

        public Vector2 getMovePosition() 
        {
            return movePosition;
        }

        public bool getClicked() 
        {
            return clicked;
        }

        private void handleGameplayTransition() 
        {
            KeyboardState keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.N)) 
            {
                GameStates.currentState = GameStates.GameState.GAMEPLAY;
            }
        }

        public Vector2 getPlayerMapPosition() 
        {
            return playerMapPosition;
        }

        public void setPlayerMapPosition(Vector2 playerPosition) 
        {
            this.playerMapPosition = new Vector2(playerPosition.X / Constant.mapReduction_Constant, playerPosition.Y / Constant.mapReduction_Constant);
        }

        public void draw(SpriteBatch spriteBatch) 
        {
            foreach (MapPlanet mapPlanet in mapPlanets) 
            {
                mapPlanet.draw(spriteBatch);
            }

            spriteBatch.Draw(Constant.player_Tex, playerMapPosition, Color.White);
        }
    }
}
