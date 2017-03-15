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
    class Particle
    {
        //varibles
        public Vector2 _pos;
        public int _age;
        public Vector2 _vel;
        public Vector2 _acc;
        public float _velDampening;

        public float _rot;
        public float _rotVel;
        public float _rotDampening;

        public float _scale;
        public float _scaleVel;
        public float _scaleAcc;
        public float _scaleMax;

        public Color _color;
        public Color _initColor;
        public Color _finalColor;

        public int _fadeAge;

        public Sprite _sprite;

        //constructors
        public Particle()
        {
            _sprite = null;
            _age = -1;
        }

        public Particle(Sprite sprite, int durInMs, Vector2 pos, Vector2 vel, Vector2 acc, float velDamp, float rot, float rotVel, float rotDamp, float scale, float scaleVel, float scaleAcc, float scaleMax, Color initColor, Color finColor, int fadeAge)
        {
            Initialize(sprite, durInMs, pos, vel, acc, velDamp, rot, rotVel, rotDamp, scale, scaleVel, scaleAcc, scaleMax, initColor, finColor, fadeAge);
        }


        public void Initialize(Sprite sprite, int durInMs, Vector2 pos, Vector2 vel, Vector2 acc, float velDamp, float rot, float rotVel, float rotDamp, float scale, float scaleVel, float scaleAcc, float scaleMax, Color initColor, Color finColor, int fadeAge)
        {
            _sprite = sprite;
            _age = durInMs;
            _fadeAge = fadeAge;

            //position
            _pos = pos;
            _vel = vel;
            _acc = acc;
            _velDampening = velDamp;

            //rotation
            _rot = rot;
            _rotVel = rotVel;
            _rotDampening = rotDamp;

            //scale
            _scale = scale;
            _scaleVel = scaleVel;
            _scaleAcc = scaleAcc;
            _scaleMax = scaleMax;

            //color
            _initColor = initColor;
            _finalColor = finColor;
        }

        public void UpdatePos(GameTime gameTime)
        {
            _vel *= _velDampening;
            _vel += (_acc * (float)gameTime.ElapsedGameTime.TotalSeconds);
            _pos += (_vel * (float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        public void UpdateScale(GameTime gameTime)
        {
            _scaleVel += (_scaleAcc * (float)gameTime.ElapsedGameTime.TotalSeconds);
            _scale += (_scaleVel * (float)gameTime.ElapsedGameTime.TotalSeconds);
            _scale = MathHelper.Clamp(_scale, 0.0f, _scaleMax);
        }

        public void UpdateRotation(GameTime gameTime)
        {
            _rotVel *= _rotDampening;
            _rot += (_rotVel * (float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        public void UpdateColor(GameTime gameTime)
        {
            if ((_age > _fadeAge) && (_fadeAge != 0))
            {
                _color = _initColor;
            }
            else
            {
                float currAmount = (float)_age / (float)_fadeAge;
                float finalAmount = 1.0f - currAmount;

                _color.R = (byte)((currAmount * _initColor.R) + (finalAmount * _finalColor.R));
                _color.G = (byte)((currAmount * _initColor.G) + (finalAmount * _finalColor.G));
                _color.B = (byte)((currAmount * _initColor.B) + (finalAmount * _finalColor.B));
                _color.A = (byte)((currAmount * _initColor.A) + (finalAmount * _finalColor.A));
            }
        }

        public void Update(GameTime gameTime)
        {
            if (_age < 0)
                return;

            _age -= gameTime.ElapsedGameTime.Milliseconds;

            UpdatePos(gameTime);
            UpdateScale(gameTime);
            UpdateColor(gameTime);
            UpdateRotation(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, float depth = 0.0f)
        {
            if (_age >= 0)
            {
                _sprite.Draw(spriteBatch, _pos, _color, _rot, _scale, SpriteEffects.None, depth);
            }
        }

        public Boolean IsDead()
        {
            if (_age < 0)
            {
                return true;
            }
            else
                return false;
        }
    }
}
