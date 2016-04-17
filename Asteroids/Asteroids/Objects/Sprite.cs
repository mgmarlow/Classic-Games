using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids.Objects
{
    public abstract class Sprite
    {
        protected Texture2D texture;
        protected Rectangle gameBounds;

        public Vector2 Velocity { get; protected set; }
        public Vector2 Location { get; set; }

        public int Width => texture.Width;
        public int Height => texture.Height;
        public Rectangle BoundingBox => new Rectangle((int) Location.X, (int) Location.Y, Width, Height);

        protected Sprite (Texture2D texture, Vector2 location, Rectangle gameBounds)
        {
            this.texture = texture;
            Location = location;
            Velocity = Vector2.Zero;
            this.gameBounds = gameBounds;
        }

        public virtual void Draw (SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Location, Color.White);
        }

        public virtual void Update (GameTime gameTime, GameObjects gameObjects)
        {
            Location += Velocity;
            CheckBounds();
        }

        protected abstract void CheckBounds ();
    }
}
