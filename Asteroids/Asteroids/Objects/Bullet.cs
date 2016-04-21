using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids.Objects
{
    public class Bullet
    {
        private Texture2D texture;

        private readonly Vector2 origin;
        private float speed = 300f;
        private Rectangle gameBounds;

        public Vector2 Location { get; set; }        
        public Vector2 Velocity { get; set; }
        public float Rotation { get; set; }
        public bool IsActive { get; set; }

        public int Width => texture.Width;
        public int Height => texture.Height;
        public Rectangle BoundingBox => new Rectangle((int) Location.X, (int) Location.Y, Width, Height);

        public Bullet (Texture2D texture, Rectangle gameBounds)
        {
            this.texture = texture;
            this.gameBounds = gameBounds;
            Velocity = Vector2.Zero;
            origin.X = Width / 2;
            origin.Y = Height / 2;            
        }

        public void Draw (SpriteBatch spriteBatch, Ship ship)
        {
            spriteBatch.Draw(texture, Location, null, Color.White, Rotation, origin, 1.0f, SpriteEffects.None, 0f);            
        }

        public void Update (GameTime gameTime, GameObjects gameObjects)
        {
            var dt = (float) gameTime.ElapsedGameTime.TotalSeconds;
            var dirX = (float) Math.Cos(Rotation);
            var dirY = (float) Math.Sin(Rotation);

            Velocity = new Vector2(dirX * dt * speed, dirY * dt * speed);

            for (int i = 0; i < gameObjects.Asteroids.Count; i++)
            {
                Asteroid asteroid = gameObjects.Asteroids[i];
                if (asteroid == null) continue;
                if (BoundingBox.Intersects(asteroid.BoundingBox))
                {
                    //asteroid.IsAlive = false;
                    gameObjects.Asteroids[i] = null;                    
                    IsActive = false;
                }
            }

            Location += Velocity;
            CheckBounds();
        }

        protected void CheckBounds ()
        {
            if (Location.X + texture.Width < 0 || Location.X > gameBounds.Width ||
                Location.Y + texture.Height < 0 || Location.Y > gameBounds.Height)
            {
                IsActive = false;
            }            
        }
    }
}
