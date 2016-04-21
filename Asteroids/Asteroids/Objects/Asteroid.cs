using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Asteroids.EntityManagement;

namespace Asteroids.Objects
{
    public class Asteroid : Sprite
    {
        private static readonly Random RandomGen = new Random();

        private float speed = 40f;
        private readonly Vector2 origin;
        private float dirX;
        private float dirY;

        public float Rotation { get; set; }      

        public Asteroid (Texture2D texture, Vector2 pos, Rectangle screenBounds)
            : base (texture, pos, screenBounds)
        {
            origin.X = texture.Width / 2;
            origin.Y = texture.Height / 2;            
            dirX = (float) Math.Cos(RandomGen.Next(360));
            dirY = (float) Math.Sin(RandomGen.Next(360));
        }

        public override void Draw (SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Location, null, Color.White, Rotation, origin, 1.0f, SpriteEffects.None, 0f);
        }

        public override void Update (GameTime gameTime, GameObjects gameObjects)
        {
            var dt = (float) gameTime.ElapsedGameTime.TotalSeconds;
            Rotation += 0.02f;
            
            Velocity = new Vector2(dirX * dt * speed, dirY * dt * speed);

            base.Update(gameTime, gameObjects);
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
