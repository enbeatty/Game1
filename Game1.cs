using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace Game1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Random _random = new Random();

        /// <summary>
        /// Background Sprites
        /// </summary>
        private Texture2D _background;
        private Texture2D _backgroundFloor;
        private Texture2D _backgroundColumns;
        private Texture2D _backgroundDragon;
        private Texture2D _backgroundMount;
        private Texture2D _backgroundCandeliar;
        private Texture2D _backgroundWall;

        /// <summary>
        /// Characters
        /// </summary>
        private Musketeer _musketeer;
        private Bird[] _birds;
        private Rock[] _rocks;

        private int _rockCount = 8;
        private int _rocksCollected = 0;

        /// <summary>
        /// Sound
        /// </summary>
        private Song _backgroundMusic;

        /// <summary>
        /// Fonts
        /// </summary>
        private SpriteFont _pixelUltima;

        /// <summary>
        /// Testing
        /// </summary>
        private Texture2D ball;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            
            //1080p
            //_graphics.PreferredBackBufferWidth = 1920;
            //_graphics.PreferredBackBufferHeight = 1080;

            //720p
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _background = Content.Load<Texture2D>("Battleground2/bg");
            _backgroundFloor = Content.Load<Texture2D>("Battleground2/floor");
            _backgroundCandeliar = Content.Load<Texture2D>("Battleground2/candeliar");
            _backgroundColumns = Content.Load<Texture2D>("Battleground2/columns&falgs");
            _backgroundDragon = Content.Load<Texture2D>("Battleground2/dragon");
            _backgroundMount = Content.Load<Texture2D>("Battleground2/mountaims");
            _backgroundWall = Content.Load<Texture2D>("Battleground2/wall@windows");

            _musketeer = new Musketeer();
            _birds = new Bird[]
                {
                new Bird() { Position = new Vector2(100, 100) },
                new Bird() { Position = new Vector2(80, 110) },
                new Bird() { Position = new Vector2(80, 80) },
                new Bird() { Position = new Vector2(60, 120) },
                new Bird() { Position = new Vector2(60, 60) }
                };

            _rocks = new Rock[_rockCount];
            for(int i = 0; i < _rockCount; i++)
            {
            _rocks[i] = new Rock(new Vector2(_random.Next(150, 1240), _random.Next(420, 620)));
            }

            ball = Content.Load<Texture2D>("rectangle");
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _musketeer.LoadContent(Content);
            foreach(Bird b in _birds)
            {
                b.LoadContent(Content);
            }

            foreach(Rock r in  _rocks)
            {
                r.LoadContent(Content);
            }

            _backgroundMusic = Content.Load<Song>("WaterDropletWalk");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(_backgroundMusic);

            _pixelUltima = Content.Load<SpriteFont>("pixel_ultima");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            _musketeer.Update(gameTime);

            foreach(Bird b in _birds)
            {
            b.Update(gameTime);
            }

            foreach (Rock r in _rocks)
            {
                if (!r.Collected && r.Bounds.CollidesWith(_musketeer.Bounds))
                {
                    r.Collected = true;
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(_background, new Vector2(0, 0), null, Color.White, 0f, new Vector2(0, 0), .6666f, SpriteEffects.None, 0);
            _spriteBatch.Draw(_backgroundMount, new Vector2(0, 0), null, Color.White, 0f, new Vector2(0, 0), .6666f, SpriteEffects.None, 0);
            foreach(Bird b in _birds)
            {
            b.Draw(gameTime, _spriteBatch);
            }
            _spriteBatch.Draw(_backgroundWall, new Vector2(0, 0), null, Color.White, 0f, new Vector2(0, 0), .6666f, SpriteEffects.None, 0);
            _spriteBatch.Draw(_backgroundColumns, new Vector2(0, 0), null, Color.White, 0f, new Vector2(0, 0), .6666f, SpriteEffects.None, 0);
            _spriteBatch.Draw(_backgroundFloor, new Vector2(0, 0), null, Color.White, 0f, new Vector2(0, 0), .6666f, SpriteEffects.None, 0);
            _spriteBatch.Draw(_backgroundDragon, new Vector2(0,0), null, Color.White, 0f, new Vector2(0,0), .6666f, SpriteEffects.None, 0);
            _spriteBatch.Draw(_backgroundCandeliar, new Vector2(0, 0), null, Color.White, 0f, new Vector2(0, 0), .6666f, SpriteEffects.None, 0);

            foreach(Rock r in _rocks)
            {
                r.Draw(gameTime, _spriteBatch);
                
                /*var rect = new Rectangle((int)r.Bounds.X, (int)r.Bounds.Y, (int)r.Bounds.Width, (int)r.Bounds.Height);
                _spriteBatch.Draw(ball, rect, Color.White);*/
                
            }

             /*var newrect = new Rectangle((int)_musketeer.Bounds.X, (int)_musketeer.Bounds.Y, (int)_musketeer.Bounds.Width, (int)_musketeer.Bounds.Height);
            _spriteBatch.Draw(ball, newrect, Color.White);*/
            _musketeer.Draw(gameTime, _spriteBatch);

            _spriteBatch.DrawString(_pixelUltima, "Welcome", new Vector2(315, 100), Color.Black); //TODO

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}