using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

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
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _musketeer.LoadContent(Content);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            _musketeer.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(_background, new Vector2(0, 0), null, Color.White, 0f, new Vector2(0, 0), .6666f, SpriteEffects.None, 0);
            _spriteBatch.Draw(_backgroundMount, new Vector2(0, 0), null, Color.White, 0f, new Vector2(0, 0), .6666f, SpriteEffects.None, 0);
            _spriteBatch.Draw(_backgroundWall, new Vector2(0, 0), null, Color.White, 0f, new Vector2(0, 0), .6666f, SpriteEffects.None, 0);
            _spriteBatch.Draw(_backgroundColumns, new Vector2(0, 0), null, Color.White, 0f, new Vector2(0, 0), .6666f, SpriteEffects.None, 0);
            _spriteBatch.Draw(_backgroundFloor, new Vector2(0, 0), null, Color.White, 0f, new Vector2(0, 0), .6666f, SpriteEffects.None, 0);
            _spriteBatch.Draw(_backgroundDragon, new Vector2(0,0), null, Color.White, 0f, new Vector2(0,0), .6666f, SpriteEffects.None, 0);
            _spriteBatch.Draw(_backgroundCandeliar, new Vector2(0, 0), null, Color.White, 0f, new Vector2(0, 0), .6666f, SpriteEffects.None, 0);

            _musketeer.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}