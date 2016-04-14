using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong.Sprites
{
    public class Ball : Sprite
    {
        private Paddle attachedToPaddle;

        public Ball(Texture2D texture, Vector2 location, Rectangle screenBounds) : base(texture, location, screenBounds)
        {
            
        }

        protected override void CheckBounds()
        {
            if (Location.Y + Texture.Height >= GameBoundaries.Height || Location.Y <= 0)
            {
                var newVelocity = new Vector2(Velocity.X, -Velocity.Y);
                Velocity = newVelocity;
            }
        }

        public override void Update(GameTime gameTime, GameObjects gameObjects)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && attachedToPaddle != null)
            {
                var newVelocity = new Vector2(3f, attachedToPaddle.Velocity.Y * 0.75f);
                Velocity = newVelocity;
                attachedToPaddle = null;
            }

            if (attachedToPaddle != null)
            {
                Location.X = attachedToPaddle.Location.X + attachedToPaddle.Width + 5;
                Location.Y = attachedToPaddle.Location.Y;
            }
            else
            {
                if (BoundingBox.Intersects(gameObjects.PlayerPaddle.BoundingBox) ||
                    BoundingBox.Intersects(gameObjects.ComputerPaddle.BoundingBox))
                {
                    Velocity = new Vector2(-Velocity.X * 1.3f, Velocity.Y * 1.3f);
                }
            }

            base.Update(gameTime, gameObjects);
        }

        public void AttachTo(Paddle paddle)
        {
            attachedToPaddle = paddle;
        }
    }
}