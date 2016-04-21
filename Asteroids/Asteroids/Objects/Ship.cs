using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Asteroids.EntityManagement;

namespace Asteroids.Objects
{
    public enum PlayerType
    {
        Human,
        Computer
    }

    public class Ship : Sprite
    {
        private float shipSpeed = 2f;
        private float maxShipSpeed = 3f;
        private readonly Vector2 origin;
        private float velX = 0;
        private float velY = 0;
        private bool firing = false;

        public float Rotation { get; protected set; }

        public Ship (Texture2D texture, Vector2 pos, Rectangle screenBounds) 
            : base(texture, pos, screenBounds)
        {
            origin.X = texture.Width / 2;
            origin.Y = texture.Height / 2;
        }

        public override void Update (GameTime gameTime, GameObjects gameObjects)
        {
            var dt = (float) gameTime.ElapsedGameTime.TotalSeconds;
            var dirX = (float) Math.Cos(Rotation);
            var dirY = (float) Math.Sin(Rotation);

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                velX += dirX * dt * shipSpeed;
                velY += dirY * dt * shipSpeed;

            }
            else
            {
                // Apply air resistance to slow down ship.
                velX *= 0.99f;
                velY *= 0.99f;
            }

            // Ship rotation
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                Rotation -= 0.05f;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                Rotation += 0.05f;

            // Fire bullets
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                // Create a bullet and keep track of it until it is deleted.
                // One bullet until it hits an asteroid or the edge of the screen.
                var bullet = gameObjects.Bullet;
                if (!bullet.IsActive) { 
                    bullet.IsActive = true;
                    bullet.Location = Location;
                    bullet.Rotation = Rotation;
                }
            }

            // Check Collisions
            //foreach (var asteroid in gameObjects.Asteroids)
            //{
            //    if (BoundingBox.Intersects(asteroid.BoundingBox))
            //    {
                      // End game 
            //    }

            Velocity = new Vector2(velX, velY);

            base.Update(gameTime, gameObjects);
        }

        public override void Draw (SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Location, null, Color.White, Rotation, origin, 1.0f, SpriteEffects.None, 0f);
        }

        protected override void CheckBounds ()
        {
            if (Location.X + texture.Width < 0)
            {
                Location = new Vector2(gameBounds.Width - 5, Location.Y);
            }
            else if (Location.X > gameBounds.Width)
            {
                Location = new Vector2(5, Location.Y);
            }
            else if (Location.Y + texture.Height < 0)
            {
                Location = new Vector2(gameBounds.Width - Location.X, gameBounds.Height - 5);
            }
            else if (Location.Y > gameBounds.Height)
            {
                Location = new Vector2(gameBounds.Width - Location.X, 5);
            }
        }
    }
}
