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
    class Animation
    {
        Texture2D _texture;
        Vector2 _srcStart;
        Vector2 _srcDimentions;
        public Vector2 _origin;
        int _numCels;
        int _msPerCel;
        int _currentCell;
        int _msUntilNextCel;

        //boolean
        public Boolean _IsPlaying;

        public Animation(Texture2D texture, Vector2 start, Vector2 dimensions, int numCels, int msPerCel, Vector2 origin)
        {
            _texture = texture;
            _srcStart = start;
            _srcDimentions = dimensions;
            _numCels = numCels;
            _msPerCel = msPerCel;
            _origin = origin;

            _currentCell = 0;
            _msUntilNextCel = msPerCel;
            _IsPlaying = false;
        }

        public void Update(GameTime gameTime)
        {
            if (_IsPlaying)
                _msUntilNextCel -= gameTime.ElapsedGameTime.Milliseconds;
            if (_msUntilNextCel <= 0)
            {
                _currentCell = (_currentCell + 1) % _numCels;
                _msUntilNextCel = _msPerCel;
            }
        }

        public void Draw(SpriteBatch spritebatch, Vector2 destLoc, Color tint, float rotation = 0.0f, float scale = 1.0f, float layer = 1.0f)
        {
            SpriteEffects effect = SpriteEffects.None;
            float SrcX = _srcStart.X + (_srcDimentions.X * _currentCell);

            spritebatch.Draw(_texture, destLoc, new Rectangle((int)SrcX, (int)_srcStart.Y, (int)_srcDimentions.X, (int)_srcDimentions.Y),
                             tint, rotation, _origin, scale, effect, layer);

        }
    }
}
