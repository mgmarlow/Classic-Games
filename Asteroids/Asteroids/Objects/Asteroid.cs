using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids.Objects
{
    public class Asteroid : Sprite
    {
        public Asteroid (Texture2D texture, Vector2 pos, Rectangle screenBounds)
            : base (texture, pos, screenBounds)
        {
            
        }

        public override void Draw (SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void Update (GameTime gameTime, GameObjects gameObjects)
        {
            base.Update(gameTime, gameObjects);
        }

        protected override void CheckBounds ()
        {
            
        }
    }
}
