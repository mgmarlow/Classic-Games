using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.Sprites
{
    public abstract class Sprite
    {
        protected readonly Texture2D Texture;
        protected Rectangle GameBoundaries;

        public Vector2 Velocity { get; protected set; }
        public Vector2 Location;

        public int Width => Texture.Width;
        public int Height => Texture.Height;
        public Rectangle BoundingBox => new Rectangle((int) Location.X, (int) Location.Y, Width, Height);

        protected Sprite(Texture2D texture, Vector2 location, Rectangle gameBounds)
        {
            Texture = texture;
            Location = location;
            Velocity = Vector2.Zero;
            GameBoundaries = gameBounds;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Location, Color.White);
        }

        public virtual void Update(GameTime gameTime, GameObjects gameObjects)
        {
            Location += Velocity;

            CheckBounds();
        }

        protected abstract void CheckBounds();
    }
}