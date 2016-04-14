using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong.Sprites
{
    public enum PlayerType
    {
        Human,
        Computer
    }

    public class Paddle : Sprite
    {
        private readonly PlayerType playerType;

        public Paddle(Texture2D texture, Vector2 pos, Rectangle screenBounds, PlayerType playerType) 
            : base(texture, pos, screenBounds)
        {
            this.playerType = playerType;
        }

        public override void Update(GameTime gameTime, GameObjects gameObjects)
        {
            if (playerType == PlayerType.Computer)
            {
                /*
                 * Threshold added to the height of the paddle to slow down reaction time randomly.
                 */
                var random = new Random();
                var reactionThreshold = random.Next(60, 130);

                // AI based on Ball movement
                var ball = gameObjects.Ball;
                if (ball.Location.Y + ball.Height < Location.Y + reactionThreshold)
                {
                    // Bottom of ball is above paddle
                    Velocity = new Vector2(0, -5f);
                }
                if (ball.Location.Y > Location.Y + Height + reactionThreshold)
                {
                    // Top of the ball is below bottom of paddle
                    Velocity = new Vector2(0, 5f);
                }
            }
            else
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                    Velocity = new Vector2(0, -5f);
                else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                    Velocity = new Vector2(0, 5f);
                else
                    Velocity = new Vector2(0, 0);
            }

            base.Update(gameTime, gameObjects);
        }

        protected override void CheckBounds()
        {
            Location.Y = MathHelper.Clamp(Location.Y, 0, GameBoundaries.Height - Texture.Height);
        }
    }
}
