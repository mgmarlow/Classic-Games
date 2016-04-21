using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Asteroids.EntityManagement;

namespace Asteroids.Objects
{
    public class Score
    {
        private readonly SpriteFont font;
        private readonly Rectangle gameBounds;

        public int PlayerScore { get; set; }

        public Score (SpriteFont font, Rectangle gameBounds)
        {
            this.font = font;
            this.gameBounds = gameBounds;
        }

        public void Draw (SpriteBatch spriteBatch)
        {
            var scoreText = $"Score: {PlayerScore}";
            // Position in middle of screen
            var xPos = (gameBounds.Width / 2) - (font.MeasureString(scoreText).X / 2);
            var position = new Vector2(xPos, 20);

            spriteBatch.DrawString(font, scoreText, position, Color.Black);
        }      
    }
}
