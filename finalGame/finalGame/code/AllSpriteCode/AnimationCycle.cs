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
    class AnimationCycle
    {
        float _maxSpeed = 70.0f;
        public Vector2 _playerPosition = new Vector2(320, 320);//make this the player start point (320, 320)

        //update position
        public void UpdatePosition(GameTime gameTime, InputHandler input)
        {
            //adjust velocity and clamp to limits
            input._Velocity *= 0.80f;
            input._Velocity.X = MathHelper.Clamp(input._Velocity.X, -_maxSpeed, _maxSpeed);
            input._Velocity.Y = MathHelper.Clamp(input._Velocity.Y, -_maxSpeed, _maxSpeed);

            //adjust position
            _playerPosition.X += (float)(input._Velocity.X * gameTime.ElapsedGameTime.TotalSeconds);
            _playerPosition.Y += (float)(input._Velocity.Y * gameTime.ElapsedGameTime.TotalSeconds);
        }

        //update
        public void Update(GameTime gameTime, Animation anim, InputHandler input)
        {
            input.HandleInputInGame(anim);
            UpdatePosition(gameTime, input);
            if (((input._Velocity.X < 10.0f && input._Velocity.X > -10.0f)&&(input._Velocity.Y < 10.0f && input._Velocity.Y > -10.0f)))
            {
                anim._IsPlaying = false;
            }
            else
            {
                anim._IsPlaying = true;
            }
            anim.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 drawLocation, Animation anim)
        {
            anim.Draw(spriteBatch, _playerPosition - drawLocation, Color.White, 0.0f, 1.1f, 0.0f);
        }
    }
}

