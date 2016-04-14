using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pong.Sprites;

namespace Pong
{
    public class Score
    {
        private readonly SpriteFont font;
        private readonly Rectangle gameBounds;

        public int PlayerScore { get; set; }
        public int ComputerScore { get; set; }

        public Score (SpriteFont font, Rectangle gameBounds)
        {
            this.font = font;
            this.gameBounds = gameBounds;
        }

        public void Draw (SpriteBatch spriteBatch)
        {
            var scoreText = $"{PlayerScore}:{ComputerScore}";
            var xPos = (gameBounds.Width / 2) - (font.MeasureString(scoreText).X / 2);
            var position = new Vector2(xPos, gameBounds.Height - 100);

            spriteBatch.DrawString(font, scoreText, position, Color.Black);
        }

        public void Update (GameTime gameTime, GameObjects gameObjects)
        {
            if (gameObjects.Ball.Location.X + gameObjects.Ball.Width < 0)
            {
                ComputerScore++;
                // Reset ball
                gameObjects.Ball.AttachTo(gameObjects.PlayerPaddle);
            }
            else if (gameObjects.Ball.Location.X > gameBounds.Width)
            {
                PlayerScore++;
                gameObjects.Ball.AttachTo(gameObjects.PlayerPaddle);
            }
        }
    }
}