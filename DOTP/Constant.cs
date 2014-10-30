using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DOTP
{
    public class Constant
    {
        public static Texture2D player_Tex;
        public static Texture2D cursor_Tex;
        public static Texture2D stars_Tex;
        public static Texture2D planet_normal_Tex;
        public static Texture2D planet_small_Tex;
        public static Texture2D planet_large_Tex;
        public static Texture2D pixel_Tex;
        public static Texture2D fleet_Tex;
        public static Texture2D laser_Tex;
        public static Texture2D font_Tex;
        public static int fighterSize = 25;
        public static int fleetSize = 75;
        public static int planet_normal_Size = 300;
        public static int planet_small_Size = 150;
        public static int planet_large_Size = 600;
        public static int laser_size = 15;
        public static int background_Width = 1500;
        public static int background_Height = 800;
        public static int letter_size = 16;
        public static int mapReduction_Constant = 40;
        public static int mapPlanet_Size = 30;
        public static String characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ" + "abcdefghijklmnopqrstuvwxyz" + "0123456789" + ".,!?<>:";
    }
}
