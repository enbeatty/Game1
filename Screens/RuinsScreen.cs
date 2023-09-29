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
using static System.TimeZoneInfo;

namespace Game1.Screens
{
    public class RuinsScreen : GameScreen
    {
        private ContentManager _content;

        /// <summary>
        /// Background Sprites
        /// </summary>
        private Texture2D _background;
        private Texture2D _backgroundFloor;
        private Texture2D _backgroundRuins;
        private Texture2D _backgroundRuins2;
        private Texture2D _backgroundStatue;
        private Texture2D _backgroundRuinsBG;
        private Texture2D _backgroundHill;

        /// <summary>
        /// Sound
        /// </summary>
        private Song _backgroundMusic;

        public RuinsScreen()
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

            _background = _content.Load<Texture2D>("Battleground1/sky");
            _backgroundFloor = _content.Load<Texture2D>("Battleground1/stones&grass");
            _backgroundStatue = _content.Load<Texture2D>("Battleground1/statue");
            _backgroundRuins = _content.Load<Texture2D>("Battleground1/ruins");
            _backgroundRuins2 = _content.Load<Texture2D>("Battleground1/ruins2");
            _backgroundRuinsBG = _content.Load<Texture2D>("Battleground1/ruins_bg");
            _backgroundHill = _content.Load<Texture2D>("Battleground1/hills&trees");


            _backgroundMusic = _content.Load<Song>("D_for_Death");
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
        }

        public override void Draw(GameTime gameTime)
        {
            var spriteBatch = ScreenManager.SpriteBatch;
            var viewport = ScreenManager.GraphicsDevice.Viewport;
            var fullscreen = new Rectangle(0, 0, viewport.Width, viewport.Height);

            spriteBatch.Begin();
            spriteBatch.Draw(_background, new Vector2(0, 0), null, Color.White, 0f, new Vector2(0, 0), .6666f, SpriteEffects.None, 0);
            spriteBatch.Draw(_backgroundRuinsBG, new Vector2(0, 0), null, Color.White, 0f, new Vector2(0, 0), .6666f, SpriteEffects.None, 0);
            spriteBatch.Draw(_backgroundHill, new Vector2(0, 0), null, Color.White, 0f, new Vector2(0, 0), .6666f, SpriteEffects.None, 0);
            spriteBatch.Draw(_backgroundRuins2, new Vector2(0, 0), null, Color.White, 0f, new Vector2(0, 0), .6666f, SpriteEffects.None, 0);
            spriteBatch.Draw(_backgroundRuins, new Vector2(0, 0), null, Color.White, 0f, new Vector2(0, 0), .6666f, SpriteEffects.None, 0);
            spriteBatch.Draw(_backgroundFloor, new Vector2(0, 0), null, Color.White, 0f, new Vector2(0, 0), .6666f, SpriteEffects.None, 0);
            spriteBatch.Draw(_backgroundStatue, new Vector2(0, 0), null, Color.White, 0f, new Vector2(0, 0), .6666f, SpriteEffects.None, 0);

            spriteBatch.End();
        }
    }
}
