using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Game1.StateManagement;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using SharpDX.Direct2D1;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;


namespace Game1.Screens
{
    public class CastleGameplayScreen : GameScreen
    {
        private ContentManager _content;

        private Random _random = new Random();

        /// <summary>
        /// Characters
        /// </summary>
        private Musketeer _musketeer;
        private Rock[] _rocks;

        private int _rockCount = 8;
        private int _rocksLeft = 8;
        private SoundEffect _rockPickup;

        public CastleGameplayScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        public override void Activate()
        {
            if (_content == null)
                _content = new ContentManager(ScreenManager.Game.Services, "Content");

            _musketeer = new Musketeer();
            _rocks = new Rock[_rockCount];
            for (int i = 0; i < _rockCount; i++)
            {
                _rocks[i] = new Rock(new Vector2(_random.Next(150, 1240), _random.Next(420, 620)));
            }


            _musketeer.LoadContent(_content);

            foreach (Rock r in _rocks)
            {
                r.LoadContent(_content);
            }

            _rockPickup = _content.Load<SoundEffect>("pickupCoin");
            SoundEffect.MasterVolume = .3f;
        }

        public override void Deactivate()
        {
            base.Deactivate();
        }

        public override void Unload()
        {
            _content.Unload();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);

            if(_rocksLeft <= 0)
            {
                LoadingScreen.Load(ScreenManager, true, 0, new RuinsScreen(), new CastleGameplayScreen());
            }

            _musketeer.Update(gameTime);

            foreach (Rock r in _rocks)
            {
                r.Update(gameTime);
                if (!r.Collected && r.Bounds.CollidesWith(_musketeer.Bounds))
                {
                    _rocksLeft--;
                    r.Collected = true;
                    _rockPickup.Play();
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            var spriteBatch = ScreenManager.SpriteBatch;
            var font = ScreenManager.Font;
            spriteBatch.Begin();
            foreach (Rock r in _rocks)
            {
                r.Draw(gameTime, spriteBatch);

                
            }
            _musketeer.Draw(gameTime, spriteBatch);

            spriteBatch.DrawString(font, $"Crystals left: {_rocksLeft}", new Vector2(0, 0), Color.LightGoldenrodYellow); //TODO

            spriteBatch.End();
        }
    }
}
