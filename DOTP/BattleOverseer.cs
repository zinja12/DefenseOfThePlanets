using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DOTP
{
    public class BattleOverseer
    {
        MouseHandler mouseHandler;
        Player player;
        BackgroundManager backgroundManager;
        List<Fleet> fleets;
        List<Planet> planets;
        Score score;
        Map map;
        MouseHighlighter mouseHighlighter;
        int planetsCount;
        int sourceAddPlanetIndex;

        public BattleOverseer(Vector2 playerPosition, Score score, Map map) 
        {
            mouseHandler = new MouseHandler();
            player = new Player(playerPosition);
            backgroundManager = new BackgroundManager();
            fleets = new List<Fleet>();
            planets = new List<Planet>();
            planets.Add(new Planet(playerPosition + new Vector2(150, 120)));
            map.addPlanetToMap(planets[0]);
            addFleet(planets[0]);
            planetsCount = 1;

            this.score = score;

            this.map = map;
            sourceAddPlanetIndex = 0;

            mouseHighlighter = new MouseHighlighter();
        }

        public void update(GameTime gameTime) 
        {
            mouseHandler.update(gameTime);
            player.update(gameTime);
            backgroundManager.update(gameTime, player.getPosition());
            foreach (Planet planet in planets)
            {
                planet.update(gameTime);
            }
            foreach (Fleet fleet in fleets) 
            {
                fleet.update(gameTime, this.getPlayerPosition());
            }
            checkCollisions();
            handleMapTransition();
            mouseHighlighter.update(gameTime);
        }

        private void checkCollisions() 
        {
            //Check fighter collisions within the same fleet
            if (fleets.Count != 0)
            {
                foreach (Fleet fleet in fleets)
                {
                    for (int i = 0; i < fleet.fighters.Count; i++)
                    {
                        foreach (EnemyFighter fighter in fleet.fighters)
                        {
                            fleet.fighters[i].checkEnemyFighterCollision(fighter);
                        }

                        if (fleet.fighters[i].getToRemove())
                        {
                            fleet.fighters.RemoveAt(i);
                        }

                        for (int j = 0; j < player.lasers.Count; j++)
                        {
                            if (fleet.fighters[i].getCollisionRect().Intersects(player.lasers[j].getCollisionRect()))
                            {
                                fleet.fighters[i].subtractHit(1);
                                fleet.fighters[i].setFocus("player");
                                player.lasers.RemoveAt(j);
                                score.addFighterScore();
                                score.addHitScore();
                            }
                        }
                    }
                }

                for (int i = 0; i < fleets.Count; i++) 
                {
                    for (int j = 0; j < player.lasers.Count; j++) 
                    {
                        if (fleets[i].checkCollision(player.lasers[j])) 
                        {
                            fleets[i].subtractHealth(1);
                            player.lasers.RemoveAt(j);
                            score.addHitScore();
                        }
                    }

                    if (fleets[i].getToRemove()) 
                    {
                        if ((planetsCount % 5) == 0)
                        {
                            addRandomPlanet();
                        }
                        else
                        {
                            addPlanet(sourcePlanetToAddAt());
                        }
                        fleets.RemoveAt(i);
                        score.addFleetScore();
                    }
                }
            }
        }

        private Planet sourcePlanetToAddAt() 
        {
            if (planetsCount < 5) 
            {
                return planets[0];
            }
            else if (planetsCount >= 5 && planetsCount < 10) 
            {
                return planets[5];
            }
            else if (planetsCount >= 10 && planetsCount < 15) 
            {
                return planets[10];
            }
            else if (planetsCount >= 15 && planetsCount < 20) 
            {
                return planets[15];
            }
            else if (planetsCount >= 20 && planetsCount < 25) 
            {
                return planets[20];
            }
            else if (planetsCount >= 25 && planetsCount < 30) 
            {
                return planets[25];
            }
            else if (planetsCount >= 30 && planetsCount < 35) 
            {
                return planets[30];
            }
            else if (planetsCount >= 35 && planetsCount < 40)
            {
                return planets[35];
            }
            else if (planetsCount >= 40 && planetsCount < 45)
            {
                return planets[40];
            }
            else if (planetsCount >= 45 && planetsCount < 50)
            {
                return planets[45];
            }
            else if (planetsCount >= 50 && planetsCount < 55)
            {
                return planets[50];
            }

            return planets[0];
        }

        public Vector2 getPlayerPosition()
        {
            return player.getPosition();
        }

        private void addPlanet(Planet planet) 
        {
            planets.Add(new Planet(Planet.positionToAddPlanet(planet, planets, planetsCount)));
            //planets[planets.Count - 1].adjustPlanet(planets[0]);
            map.addPlanetToMap(planets[planets.Count - 1]);
            addFleet(planets[planets.Count - 1]);
            planetsCount++;
        }

        private void addRandomPlanet()
        {
            planets.Add(new Planet(Planet.randomPositionToAddPlanet(planetsCount)));
            map.addPlanetToMap(planets[planets.Count - 1]);
            addFleet(planets[planets.Count - 1]);
            planetsCount++;
        }

        private void addFleet(Planet planet) 
        {
            if (!planet.getDesignation().Equals("large"))
            {
                fleets.Add(new Fleet(new Vector2(planet.getOrigin().X - (Constant.fleetSize / 2), planet.getOrigin().Y - (Constant.fleetSize * 6)),
                    planet.getOrigin(), planet.getDesignation()));
            }
            else 
            {
                fleets.Add(new Fleet(new Vector2(planet.getOrigin().X - (Constant.fleetSize / 2), planet.getOrigin().Y - (Constant.fleetSize * 9)),
                    planet.getOrigin(), planet.getDesignation()));
            }
        }

        private Planet getPlanet(Fleet fleet) 
        {
            Point point = new Point((int)fleet.getTargetPlanetOrigin().X, (int)fleet.getTargetPlanetOrigin().Y);
            foreach (Planet planet in planets) 
            {
                if (planet.getCollisionRect().Contains(point)) 
                {
                    return planet;
                }
            }
            return null;
        }

        public void handleMapTransition() 
        {
            KeyboardState keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.M)) 
            {
                map.setPlayerMapPosition(getPlayerPosition());
                GameStates.currentState = GameStates.GameState.MAP;
            }
        }

        public void setPlayerPosition(Vector2 position) 
        {
            player.setPlayerPosition(position);
        }

        public void draw(SpriteBatch spriteBatch) 
        {
            backgroundManager.draw(spriteBatch);
            foreach (Planet planet in planets)
            {
                planet.draw(spriteBatch);
            }
            foreach (Fleet fleet in fleets)
            {
                fleet.draw(spriteBatch);
            }
            player.draw(spriteBatch);
            mouseHandler.draw(spriteBatch);
            mouseHighlighter.draw(spriteBatch);
        }
    }
}
