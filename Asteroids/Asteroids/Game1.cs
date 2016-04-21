using System.Collections.Generic;
using Asteroids.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Asteroids.EntityManagement;

namespace Asteroids
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Ship ship;
        private Bullet bullet;
        private List<Asteroid> asteroids; 
        private GameObjects gameObjects;
        private Score score;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            this.Window.Title = "Asteroids";
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
            // TODO: Add your initialization logic here            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {            
            spriteBatch = new SpriteBatch(GraphicsDevice);

            var shipTexture = Content.Load<Texture2D>("ship");
            var shipPos = new Vector2((Window.ClientBounds.Width / 2 - (shipTexture.Width/2)), Window.ClientBounds.Height / 2 - (shipTexture.Height/2));
            ship = new Ship(shipTexture, shipPos, Window.ClientBounds);

            var bulletTexture = Content.Load<Texture2D>("bullet");
            bullet = new Bullet(bulletTexture, Window.ClientBounds);

            asteroids = new List<Asteroid>();
            var asteroidTexture = Content.Load<Texture2D>("asteroid");
            for (int i = 0; i <= 10; i++)
            {
                // Create asteroids
                asteroids.Add(new Asteroid(asteroidTexture, new Vector2(0 + 50 * i, 20), Window.ClientBounds));
            }

            score = new Score(Content.Load<SpriteFont>("score"), Window.ClientBounds);

            gameObjects = new GameObjects()
            {
                Ship = ship,
                Bullet = bullet,
                Asteroids = asteroids,
                Score = score
            };
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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

            ship.Update(gameTime, gameObjects);
            if (bullet.IsActive)
                bullet.Update(gameTime, gameObjects);

            asteroids.ForEach((a) =>
            {
                a?.Update(gameTime, gameObjects);
            });

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            ship.Draw(spriteBatch);            
            if (bullet.IsActive)
                bullet.Draw(spriteBatch, gameObjects.Ship);

            asteroids.ForEach((a) =>
            {
                a?.Draw(spriteBatch);
            });

            score.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
