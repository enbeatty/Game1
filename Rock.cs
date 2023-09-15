using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Collisions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;

namespace Game1
{
    public class Rock
    {
        private BoundingRectangle _bounds;

        private Texture2D _rock;

        private Vector2 _position;
        private double _movingTimer;
        private double _upTimer;
        private bool _up = true;

        private Random rand = new Random();

        /// <summary>
        /// Is the rock collected yet?
        /// </summary>
        public bool Collected { get; set; } = false;

        /// <summary>
        /// The bounding volume of the sprite
        /// </summary>
        public BoundingRectangle Bounds => _bounds;

        public Rock(Vector2 position) 
        {
            _position = position;
            _bounds = new BoundingRectangle(position + new Vector2(16, 16), 32, 32);
            int num = rand.Next(0, 2);
            if(num == 0)
            { 
                _up = false;
            }
        }
 
        /// <summary>
        /// Loads the sprite texture using the provided ContentManager
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            int num = rand.Next(1, 6);
            string name = "";
            switch(num)
            {
                case 1:
                    name = "Assets_shadow/Black_crystal3";
                    break;
                case 2:
                    name = "Assets_shadow/Blue_crystal3";
                    break;
                case 3:
                    name = "Assets_shadow/Pink_crystal3";
                    break;
                case 4:
                    name = "Assets_shadow/Violet_crystal3";
                    break;
                case 5:
                    name = "Assets_shadow/White_crystal3";
                    break;
            }

            _rock = content.Load<Texture2D>(name);
        }

        /// <summary>
        /// Updates the sprite's _position based on user input
        /// </summary>
        /// <param name="gameTime">The GameTime</param>
        public void Update(GameTime gameTime)
        {
            //Update direction timer
            _movingTimer += gameTime.ElapsedGameTime.TotalSeconds;
            _upTimer += gameTime.ElapsedGameTime.TotalSeconds;

            //If the Cloud leaves screen fully have it appear on other side.
            if (_movingTimer > .005)
            {
                if(_up)
                {
                    _position += new Vector2(0, 0.025f);
                }
                else
                {
                    _position -= new Vector2(0, 0.025f);
                }
                _movingTimer -= .005;
            }

            if(_upTimer > .5)
            {
                _up = !_up;
                _upTimer -= .5;
            }
        }

        /// <summary>
        /// Draws the animated sprite using the supplied SpriteBatch
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The spritebatch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Collected)
            {
                return;
            }

            spriteBatch.Draw(_rock, _position, null, Color.White, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 0);
        }
    }
}
