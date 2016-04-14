using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.Sprites;

namespace Pong
{
    
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private GameObjects gameObjects;
        private Paddle computerPaddle;
        private Paddle playerPaddle;
        private Ball ball;
        private Score score;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //var gameBoundaries = new Rectangle
            var paddleTexture = Content.Load<Texture2D>("paddle");
            playerPaddle = new Paddle(paddleTexture, Vector2.Zero, Window.ClientBounds, PlayerType.Human);      
            var computerLocation = new Vector2(Window.ClientBounds.Width - paddleTexture.Width, 0);      
            computerPaddle = new Paddle(paddleTexture, computerLocation, Window.ClientBounds, PlayerType.Computer);

            ball = new Ball(Content.Load<Texture2D>("ball"), Vector2.Zero, Window.ClientBounds);
            ball.AttachTo(playerPaddle);

            score = new Score(Content.Load<SpriteFont>("score"), Window.ClientBounds);

            gameObjects = new GameObjects
            {
                PlayerPaddle = playerPaddle,
                ComputerPaddle = computerPaddle,
                Ball = ball,
                Score = score
            };
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            playerPaddle.Update(gameTime, gameObjects);
            computerPaddle.Update(gameTime, gameObjects);
            ball.Update(gameTime, gameObjects);
            score.Update(gameTime, gameObjects);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            playerPaddle.Draw(spriteBatch);
            computerPaddle.Draw(spriteBatch);
            ball.Draw(spriteBatch);
            score.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
