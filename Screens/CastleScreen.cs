using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Game1.StateManagement;
using Microsoft.Xna.Framework.Media;

namespace Game1.Screens
{
    public class CastleScreen : GameScreen
    {
        private ContentManager _content;

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
        private Bird[] _birds;

        /// <summary>
        /// Sound
        /// </summary>
        private Song _backgroundMusic;

        public CastleScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        /// <summary>
        /// Loads graphics _content for this screen. The background texture is quite
        /// big, so we use our own local _contentManager to load it. This allows us
        /// to unload before going from the menus into the game itself, whereas if we
        /// used the shared _contentManager provided by the Game class, the _content
        /// would remain loaded forever.
        /// </summary>
        public override void Activate()
        {
            if (_content == null)
                _content = new ContentManager(ScreenManager.Game.Services, "Content");

            _background = _content.Load<Texture2D>("Battleground2/bg");
            _backgroundFloor = _content.Load<Texture2D>("Battleground2/floor");
            _backgroundCandeliar = _content.Load<Texture2D>("Battleground2/candeliar");
            _backgroundColumns = _content.Load<Texture2D>("Battleground2/columns&falgs");
            _backgroundDragon = _content.Load<Texture2D>("Battleground2/dragon");
            _backgroundMount = _content.Load<Texture2D>("Battleground2/mountaims");
            _backgroundWall = _content.Load<Texture2D>("Battleground2/wall@windows");

            _birds = new Bird[]
                {
                new Bird() { Position = new Vector2(100, 100) },
                new Bird() { Position = new Vector2(80, 110) },
                new Bird() { Position = new Vector2(80, 80) },
                new Bird() { Position = new Vector2(60, 120) },
                new Bird() { Position = new Vector2(60, 60) }
                };

            foreach (Bird b in _birds)
            {
                b.LoadContent(_content);
            }

            _backgroundMusic = _content.Load<Song>("WaterDropLetWalkOrgan");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(_backgroundMusic);
        }

        public override void Unload()
        {
            MediaPlayer.Stop();
            _content.Unload();
        }

        // Unlike most screens, this should not transition off even if
        // it has been covered by another screen: it is supposed to be
        // covered, after all! This overload forces the coveredByOtherScreen
        // parameter to false in order to stop the base Update method wanting to transition off.
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);
            foreach (Bird b in _birds)
            {
                b.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            var spriteBatch = ScreenManager.SpriteBatch;
            var viewport = ScreenManager.GraphicsDevice.Viewport;
            var fullscreen = new Rectangle(0, 0, viewport.Width, viewport.Height);

            spriteBatch.Begin();
            spriteBatch.Draw(_background, new Vector2(0, 0), null, Color.White, 0f, new Vector2(0, 0), .6666f, SpriteEffects.None, 0);
            spriteBatch.Draw(_backgroundMount, new Vector2(0, 0), null, Color.White, 0f, new Vector2(0, 0), .6666f, SpriteEffects.None, 0);
            foreach (Bird b in _birds)
            {
                b.Draw(gameTime, spriteBatch);
            }
            spriteBatch.Draw(_backgroundWall, new Vector2(0, 0), null, Color.White, 0f, new Vector2(0, 0), .6666f, SpriteEffects.None, 0);
            spriteBatch.Draw(_backgroundColumns, new Vector2(0, 0), null, Color.White, 0f, new Vector2(0, 0), .6666f, SpriteEffects.None, 0);
            spriteBatch.Draw(_backgroundFloor, new Vector2(0, 0), null, Color.White, 0f, new Vector2(0, 0), .6666f, SpriteEffects.None, 0);
            spriteBatch.Draw(_backgroundDragon, new Vector2(0, 0), null, Color.White, 0f, new Vector2(0, 0), .6666f, SpriteEffects.None, 0);
            spriteBatch.Draw(_backgroundCandeliar, new Vector2(0, 0), null, Color.White, 0f, new Vector2(0, 0), .6666f, SpriteEffects.None, 0);

            spriteBatch.End();
        }
    }
}
