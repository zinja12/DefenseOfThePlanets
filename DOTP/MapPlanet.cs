using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DOTP
{
    public class MapPlanet
    {
        private Vector2 position;
        private Vector2 originalPlanetPosition;
        private Vector2 origin;
        private Rectangle collision_Rect;
        private Color color;

        public MapPlanet(Vector2 position, Color color) 
        {
            this.position = position;
            originalPlanetPosition = position;
            origin = new Vector2((Constant.planet_small_Size / 2), (Constant.planet_small_Size / 2));
            collision_Rect = new Rectangle((int)position.X, (int)position.Y, Constant.mapPlanet_Size, Constant.mapPlanet_Size);
            this.color = color;
        }

        public MapPlanet(Planet planet) 
        {
            originalPlanetPosition = planet.getPosition();
            this.position = new Vector2(planet.getPosition().X / 40, planet.getPosition().Y / 40);
            origin = new Vector2((Constant.planet_small_Size / 2), (Constant.planet_small_Size / 2));
            collision_Rect = new Rectangle((int)position.X, (int)position.Y, Constant.mapPlanet_Size , Constant.mapPlanet_Size);
            this.color = planet.getColor();
        }

        public void update(GameTime gameTime) 
        {
            //Update collision rect
            collision_Rect = new Rectangle((int)position.X, (int)position.Y, Constant.mapPlanet_Size, Constant.mapPlanet_Size);
        }

        public Rectangle getCollisionRect() 
        {
            return collision_Rect;
        }

        public Vector2 getPosition() 
        {
            return position;
        }

        public Vector2 getOrigin() 
        {
            return position + origin;
        }

        public Color getColor() 
        {
            return color;
        }

        public void setColor(Color color) 
        {
            this.color = color;
        }

        public Vector2 getOriginalPlanetPosition() 
        {
            return originalPlanetPosition;
        }

        public void draw(SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(Constant.planet_small_Tex, position, null, color, 0.0f, origin, 0.2f, SpriteEffects.None, 0f);
        }
    }
}
