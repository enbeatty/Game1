using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct3D9;

namespace Game1
{
    public class Bird
    {
        private Texture2D _bird;

        private double _animationTimer;

        private double _movingTimer;

        private short _animationFrame = 0;

        /// <summary>
        /// The position of the bird
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// Loads the sprite texture using the provided ContentManager
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            _bird = content.Load<Texture2D>("Bird/Walk");
        }

        /// <summary>
        /// Updates the sprite's _position based on user input
        /// </summary>
        /// <param name="gameTime">The GameTime</param>
        public void Update(GameTime gameTime)
        {
            //Update direction timer
            _movingTimer += gameTime.ElapsedGameTime.TotalSeconds;

            //If the Cloud leaves screen fully have it appear on other side.
            if (_movingTimer > .005)
            {
                if (Position.X > 1280)
                {
                    Position.X = 0;
                }
                Position += new Vector2(1, 0);
                _movingTimer -= .005;
            }
        }

        /// <summary>
        /// Draws the sprite using the supplied SpriteBatch
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The spritebatch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //Update animation timer
            _animationTimer += gameTime.ElapsedGameTime.TotalSeconds;

            //Update animation frame
            if (_animationTimer > 0.2)
            {
                _animationFrame++;
                if (_animationFrame > 3)
                {
                    _animationFrame = 1;
                }
                _animationTimer -= 0.2;
            }

            //Draw the sprite
            var source = new Rectangle(_animationFrame * 32, 0, 32, 32);
            spriteBatch.Draw(_bird, Position, source, Color.White);
        }
    }
}
