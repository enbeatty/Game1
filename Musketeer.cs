using Game1.Collisions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{

    public class Musketeer
    {
        private KeyboardState _keyboardState;

        private double _animationTimer;
        
        private double _movingTimer;

        private bool _moving = false;

        private Texture2D _walk;

        private Texture2D _idle;

        private Vector2 _position = new Vector2(20, 300);

        private bool _flipped;

        private short _animationFrame = 0;

        private BoundingRectangle _bounds = new BoundingRectangle(new Vector2(-64, -64), 64, 128);

        /// <summary>
        /// The bounding volume of the sprite
        /// </summary>
        public BoundingRectangle Bounds => _bounds;

        /// <summary>
        /// The color to blend with the ghost
        /// </summary>
        public Color Color { get; set; } = Color.White;

        /// <summary>
        /// Loads the sprite texture using the provided ContentManager
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            _walk = content.Load<Texture2D>("Musketeer/walk");
            _idle = content.Load<Texture2D>("Musketeer/idle");
        }

        /// <summary>
        /// Updates the sprite's _position based on user input
        /// </summary>
        /// <param name="gameTime">The GameTime</param>
        public void Update(GameTime gameTime)
        {
            _keyboardState = Keyboard.GetState();

            //Update direction timer
            _movingTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (_movingTimer > .3)
            {
                _moving = false;
                _movingTimer -= .3;

                // Update the _bounds
                _bounds.X = _position.X - 32; //TODO
                _bounds.Y = _position.Y - 32; //TODO
            }

            // Apply keyboard movement
            if (_keyboardState.IsKeyDown(Keys.Up) || _keyboardState.IsKeyDown(Keys.W))
            {
                _moving = true;
                if( _position.Y > 220 )
                {
                    _position += new Vector2(0, -1.35f);
                    
                    // Update the _bounds
                    if (_flipped)
                    {
                        _bounds.X = _position.X; //TODO
                    }
                    else
                    {
                        _bounds.X = _position.X - 64; //TODO
                    }
                }

            }
            if (_keyboardState.IsKeyDown(Keys.Down) || _keyboardState.IsKeyDown(Keys.S))
            {
                if(_position.Y < 460)
                {
                    _position += new Vector2(0, 1.35f);
                    _moving = true;

                    // Update the _bounds
                    if(_flipped)
                    {
                        _bounds.X = _position.X - 64; //TODO
                    }
                    else
                    {
                        _bounds.X = _position.X - 64; //TODO
                    }
                }
            }
            if (_keyboardState.IsKeyDown(Keys.Left) || _keyboardState.IsKeyDown(Keys.A))
            {
                if( _position.X > -128)
                {
                _moving = true;
                _position += new Vector2(-1.35f, 0);
                _flipped = true;

                // Update the _bounds
                _bounds.X = _position.X ; //TODO
                }
            }
            if (_keyboardState.IsKeyDown(Keys.Right) || _keyboardState.IsKeyDown(Keys.D))
            {
                if (_position.X < 1140)
                {
                _moving = true;
                _position += new Vector2(1.35f, 0);
                _flipped = false;

                // Update the _bounds
                _bounds.X = _position.X - 64; //TODO
                }
            }

                _bounds.Y = _position.Y; 
        }

        /// <summary>
        /// Draws the sprite using the supplied SpriteBatch
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The spritebatch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Rectangle source;
            Texture2D texture;
            SpriteEffects spriteEffects = (_flipped) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            if ( _moving )
            {
                //Update animation timer
                _animationTimer += gameTime.ElapsedGameTime.TotalSeconds;

                //Update animation frame
                if (_animationTimer > 0.2)
                {
                    _animationFrame++;
                    if (_animationFrame > 6)
                    {
                        _animationFrame = 0;
                    }
                    _animationTimer -= 0.2;
                }
                source = new Rectangle(_animationFrame * 128, 0, 128, 128);
                texture = _walk;
            }
            else
            {
                //Update animation timer
                _animationTimer += gameTime.ElapsedGameTime.TotalSeconds;

                //Update animation frame
                if (_animationTimer > 0.4)
                {
                    _animationFrame++;
                    if (_animationFrame > 3)
                    {
                        _animationFrame = 0;
                    }
                    _animationTimer -= 0.4;
                }
                source = new Rectangle(_animationFrame * 128, 0, 128, 128);
                texture = _idle;
            }

            if( !_flipped )
            {
                spriteBatch.Draw(texture, _position, source, Color.White, 0f, new Vector2(32, 64), 2f, spriteEffects, 0);

            }
            else
            {
                spriteBatch.Draw(texture, _position, source, Color.White, 0f, new Vector2(64, 64), 2f, spriteEffects, 0);
            }

        }
    }
}
