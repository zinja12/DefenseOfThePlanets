using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DOTP
{
    public class Planet
    {
        public Vector2 position;
        private Color planetColor;
        private float health;
        private String designation;
        private float rotation;
        private Vector2 origin;
        private float scale;
        private Random rnd;
        private Rectangle collision_rect;

        public Planet(Vector2 position)
        {
            this.position = position;
            rnd = new Random();

            int randomDesignation = (rnd.Next(13) + 1);
            if (randomDesignation <= 4) 
            {
                designation = "small";
                health = 2500f;
            }
            else if (randomDesignation > 4 && randomDesignation <= 8)
            {
                designation = "normal";
                health = 5000f;
            }
            else 
            {
                designation = "large";
                health = 10000f;
            }

            if (designation.Equals("small"))
            {
                collision_rect = new Rectangle((int)position.X, (int)position.Y, 150, 150);
                scale = 0.5f;
                origin = new Vector2(75, 75);
            }
            else if (designation.Equals("large"))
            {
                collision_rect = new Rectangle((int)position.X, (int)position.Y, 600, 600);
                scale = 2.0f;
                origin = new Vector2(300, 300);
            }
            else
            {
                collision_rect = new Rectangle((int)position.X, (int)position.Y, 300, 300);
                scale = 1.0f;
                origin = new Vector2(150, 150);
            }
            rotation = (float)rnd.Next(360);
            planetColor = new Color((byte)rnd.Next(0, 255), (byte)rnd.Next(0, 255), (byte)rnd.Next(0, 255));
        }

        public void update(GameTime gameTime)
        {

        }

        public Rectangle getCollisionRect()
        {
            return collision_rect;
        }

        public Vector2 getOrigin() 
        {
            return origin + position;
        }

        public Vector2 getPosition() 
        {
            return position;
        }

        public String getDesignation() 
        {
            return designation;
        }

        public Point[] getCollisionBorderPoints() 
        {
            Point[] points = new Point[6];

            points[0] = new Point((int)(getOrigin().X - getSize()), (int)(getOrigin().Y));
            points[1] = new Point((int)(getOrigin().X - getSize()), (int)(getOrigin().Y - getSize()));
            points[2] = new Point((int)(getOrigin().X + getSize()), (int)(getOrigin().Y - getSize()));
            points[3] = new Point((int)(getOrigin().X + getSize()), (int)(getOrigin().Y));
            points[4] = new Point((int)(getOrigin().X + getSize()), (int)(getOrigin().Y + getSize()));
            points[5] = new Point((int)(getOrigin().X - getSize()), (int)(getOrigin().Y + getSize()));

            return points;
        }

        public void adjustPlanet(Planet sourcePlanet)
        {
            Point[] points = getCollisionBorderPoints();
            for (int i = 0; i < points.Length; i++)
            {
                if (sourcePlanet.getCollisionRect().Contains(points[i]))
                {
                    switch (i)
                    {
                        case 0:
                            position.X += ((sourcePlanet.getPosition().X + getSize()) - points[i].X);
                            break;
                        case 1:
                            position.X += ((sourcePlanet.getPosition().X + getSize()) - points[i].X);
                            position.Y += ((sourcePlanet.getPosition().Y + getSize()) - points[i].Y);
                            break;
                        case 2:
                            position.X -= (points[i].X - sourcePlanet.getPosition().X);
                            position.Y += ((sourcePlanet.getPosition().Y + getSize()) - points[i].Y);
                            break;
                        case 3:
                            position.X -= (points[i].X - sourcePlanet.getPosition().X);
                            break;
                        case 4:
                            position.X -= (points[i].X - sourcePlanet.getPosition().X);
                            position.Y -= (points[i].Y - (sourcePlanet.getPosition().Y));
                            break;
                        case 5:
                            position.X += ((sourcePlanet.getPosition().X + getSize()) - points[i].X);
                            position.Y -= (points[i].Y - (sourcePlanet.getPosition().Y));
                            break;
                        default:
                            Console.WriteLine("Could not adjust planet");
                            break;
                    }
                }
            }
        }

        public int getSize()
        {
            if (designation.Equals("small"))
            {
                return Constant.planet_small_Size;
            }
            else if (designation.Equals("large"))
            {
                return Constant.planet_large_Size;
            }
            else
            {
                return Constant.planet_normal_Size;
            }
        }

        public Color getColor() 
        {
            return planetColor;
        }

        public void draw(SpriteBatch spriteBatch) 
        {
            if (designation.Equals("small")) 
            {
                spriteBatch.Draw(Constant.planet_small_Tex, position, planetColor);
            }
            else if (designation.Equals("large"))
            {
                spriteBatch.Draw(Constant.planet_large_Tex, position, planetColor);
            }
            else 
            {
                spriteBatch.Draw(Constant.planet_normal_Tex, position, planetColor);
            }
        }

        public static Vector2 randomPositionToAddPlanet(int planetCount) 
        {
            int remainder = (planetCount % 5);
            if (remainder == 0)
            {
                Random rnd = new Random();
                return new Vector2(rnd.Next(1000, (Constant.background_Width * 20) - 1000), rnd.Next(1000, (Constant.background_Height * 20) - 1000));
            }

            return Vector2.Zero;
        }

        public static Vector2 positionToAddPlanet(Planet sourcePlanet, List<Planet> currentPlanets, int planetCount) 
        {
            /*Point[] points = new Point[6];

            points[0] = new Point((int)(sourcePlanet.getOrigin().X - Constant.planet_large_Size), (int)(sourcePlanet.getOrigin().Y));
            points[1] = new Point((int)(sourcePlanet.getOrigin().X - Constant.planet_large_Size), (int)(sourcePlanet.getOrigin().Y - Constant.planet_large_Size));
            points[2] = new Point((int)(sourcePlanet.getOrigin().X + Constant.planet_large_Size), (int)(sourcePlanet.getOrigin().Y - Constant.planet_large_Size));
            points[3] = new Point((int)(sourcePlanet.getOrigin().X + Constant.planet_large_Size), (int)(sourcePlanet.getOrigin().Y));
            points[4] = new Point((int)(sourcePlanet.getOrigin().X + Constant.planet_large_Size), (int)(sourcePlanet.getOrigin().Y + Constant.planet_large_Size));
            points[5] = new Point((int)(sourcePlanet.getOrigin().X - Constant.planet_large_Size), (int)(sourcePlanet.getOrigin().Y + Constant.planet_large_Size));
            */

            int remainder = (planetCount % 5);
            if (remainder == 0) 
            {
                Random rnd = new Random();
                return new Vector2(rnd.Next(1000, (Constant.background_Width * 20) - 1000), rnd.Next(1000, (Constant.background_Height * 20) - 1000));
            }

            switch (remainder) 
            {
                case 1:
                    return new Vector2(sourcePlanet.getOrigin().X - Constant.planet_large_Size, sourcePlanet.getOrigin().Y - Constant.planet_large_Size);
                case 2:
                    return new Vector2(sourcePlanet.getOrigin().X + Constant.planet_large_Size, sourcePlanet.getOrigin().Y - Constant.planet_large_Size);
                case 3:
                    return new Vector2(sourcePlanet.getOrigin().X + Constant.planet_large_Size, sourcePlanet.getOrigin().Y + Constant.planet_large_Size);
                case 4:
                    return new Vector2(sourcePlanet.getOrigin().X - Constant.planet_large_Size, sourcePlanet.getOrigin().Y + Constant.planet_large_Size);
                default:
                    Console.Write("Planet placed at Vector2.Zero");
                    return Vector2.Zero;
            }

            /*if (planetCount < 7)
            {
                for (int i = 0; i < points.Length; i++)
                {
                    for (int j = 0; j < currentPlanets.Count; j++)
                    {
                        if (!currentPlanets[j].getCollisionRect().Contains(points[i]))
                        {
                            return new Vector2((float)points[i].X, (float)points[i].Y);
                        }
                    }
                }
            }
            Console.WriteLine("Planet placed at Vector2.Zero");
            return Vector2.Zero;*/

            /*ndom rnd = new Random();
            float x = rnd.Next((int)(sourcePlanet.getPosition().X - Constant.planet_large_Size), (int)(sourcePlanet.getPosition().X + sourcePlanet.getSize() + Constant.planet_large_Size));
            float y = rnd.Next((int)(sourcePlanet.getPosition().Y - Constant.planet_large_Size), (int)(sourcePlanet.getPosition().Y + sourcePlanet.getSize() + Constant.planet_large_Size));
            float newX;
            float newY;
            while (x > sourcePlanet.getPosition().X && x < sourcePlanet.getPosition().X + sourcePlanet.getSize() &&
                y > sourcePlanet.getPosition().Y && y < sourcePlanet.getPosition().Y + sourcePlanet.getSize()) 
            {
                newX = rnd.Next((int)(sourcePlanet.getPosition().X - Constant.planet_large_Size), (int)(sourcePlanet.getPosition().X + sourcePlanet.getSize() + Constant.planet_large_Size));
                newY = rnd.Next((int)(sourcePlanet.getPosition().Y - Constant.planet_large_Size), (int)(sourcePlanet.getPosition().Y + sourcePlanet.getSize() + Constant.planet_large_Size));

                for (int i = 0; i < currentPlanets.Count; i++) 
                {
                    if (currentPlanets[i].getCollisionRect().Contains(new Point((int)newX, (int)newY)))
                    {
                        newX = rnd.Next((int)(sourcePlanet.getPosition().X - Constant.planet_large_Size), (int)(sourcePlanet.getPosition().X + sourcePlanet.getSize() + Constant.planet_large_Size));
                        newY = rnd.Next((int)(sourcePlanet.getPosition().Y - Constant.planet_large_Size), (int)(sourcePlanet.getPosition().Y + sourcePlanet.getSize() + Constant.planet_large_Size));
                    }
                }
                return new Vector2(newX, newY);
            }

            return new Vector2(x, y);*/
        }
    }
}
