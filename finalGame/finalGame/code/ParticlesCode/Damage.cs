using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace finalGame
{
    public class Damage
    {
        static Sprite starSprite;

        Vector2 _origin;
        int _radius;
        BlendState _blendType;
        int _effectDurInMs;
        int _newParticleAmount;
        int _burstFreqInMs;
        int _burstCountdownInMs;
        int _screenHeight;

        List<Particle> _particles;
        Random _random;

        public Damage()
        {
            _particles = new List<Particle>();
            _random = new Random();
        }

        static public void LoadContent(ContentManager content)
        {
            Texture2D starTexture = content.Load<Texture2D>("particleEffects/whiteStar");
            starSprite = new Sprite(starTexture, new Rectangle(0, 0, starTexture.Width, starTexture.Height), new Vector2(starTexture.Width / 2, starTexture.Height / 2));
        }

        public Boolean IsAlive()
        {
            if (_effectDurInMs > 0)
            {
                return true;
            }
            if (_particles.Count() > 0)
            {
                return true;
            }

            return false;
        }

        public void Initialize(int x, int y)
        {
            _effectDurInMs = 16;
            _newParticleAmount = 1;
            _burstFreqInMs = 16;
            _effectDurInMs = _burstFreqInMs;

            _origin = new Vector2(x, y);
            _radius = 10;
            _blendType = BlendState.NonPremultiplied;
        }

        void CreateParticle()
        {
            int duration = 1000;

            Vector2 pos = _origin;
            Vector2 offset;
            offset.X = ((float)(_random.Next(_radius) * Math.Cos(_random.Next(360))));
            offset.Y = ((float)(_random.Next(_radius) * Math.Sin(_random.Next(360))));
            pos += offset;

            Vector2 vel = Vector2.Zero;
            Vector2 acc = new Vector2(0.0f, 5.0f);
            float velDamp = 0.99f;

            float rot = 0.0f;
            float rotVel = 3.0f;
            float rotDamp = 0.9f;

            float scale = 0.01f;
            float scaleVel = ((float)_random.Next(10)) / 50.0f;
            float scaleAcc = 0.1f;
            float maxScale = 0.5f;

            Color initColor = Color.White;
            Color finalColor = Color.Black;
            int fadeAge = duration / 2;

            Particle temp = new Particle(starSprite, duration, pos, vel, acc, velDamp, rot, rotVel, rotDamp, scale, scaleVel, scaleAcc, maxScale, initColor, finalColor, fadeAge);

            _particles.Add(temp);
        }

        public void Update(GameTime gameTime)
        {
            _effectDurInMs -= gameTime.ElapsedGameTime.Milliseconds;
            _burstCountdownInMs -= gameTime.ElapsedGameTime.Milliseconds;

            if ((_burstCountdownInMs <= 0) && (_effectDurInMs >= 0))
            {
                for (int i = 0; i < _newParticleAmount; i++)
                {
                    CreateParticle();
                }
            }

            for (int i = _particles.Count() - 1; i >= 0; i--)
            {
                _particles[i].Update(gameTime);

                if (_particles[i].IsDead())
                {
                    _particles.RemoveAt(i);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, _blendType);
            for(int i = 0; i < _particles.Count(); i++)
            {
                _particles[i].Draw(spriteBatch);
            }
            spriteBatch.End();
        }
    }
}
