#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace DOTP
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static Camera camera;
        GameStates gameStates;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            camera = new Camera(graphics.GraphicsDevice.Viewport);
            gameStates = new GameStates(Vector2.Zero + new Vector2((20 * Constant.background_Width) / 2, (20 * Constant.background_Height) / 2));

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Constant.player_Tex = Content.Load<Texture2D>("Sprites/player_ship.png");
            Constant.cursor_Tex = Content.Load<Texture2D>("Sprites/cursor.png");
            Constant.stars_Tex = Content.Load<Texture2D>("Textures/stars.png");
            Constant.planet_normal_Tex = Content.Load<Texture2D>("Textures/planet_01.png");
            Constant.planet_small_Tex = Content.Load<Texture2D>("Textures/planet_02.png");
            Constant.planet_large_Tex = Content.Load<Texture2D>("Textures/planet_03.png");
            Constant.pixel_Tex = Content.Load<Texture2D>("Sprites/pixel_SPR.png");
            Constant.fleet_Tex = Content.Load<Texture2D>("Sprites/fleet_ship.png");
            Constant.laser_Tex = Content.Load<Texture2D>("Sprites/laser_01.png");
            Constant.font_Tex = Content.Load<Texture2D>("Fonts/font_01.png");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            gameStates.update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            gameStates.draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
