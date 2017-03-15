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
    class Camera
    {
        public Vector2 _cameraPosition = new Vector2(0, 0);
        public Vector2 _cameraOffset = new Vector2(320, 320);
        const float _mult = 0.05f;
        public float _fZoomLevel = 2.0f;
        public Vector2 _drawLocation;

        public void Update(GameTime gameTime, Vector2 playerPos)
        {
            ////camera zoom
            if (Keyboard.GetState().IsKeyDown(Keys.Subtract))
            {
                if (_fZoomLevel <= 0.05)
                    _fZoomLevel = 0.05f;
                else
                    _fZoomLevel -= 0.2f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Add))
            {
                _fZoomLevel += 0.2f;
            }

            _cameraPosition = playerPos + new Vector2(2, 0);//(166, 153)
            if (_cameraPosition.X < playerPos.X)
            {
                _cameraPosition.X -= ((_cameraPosition.X - playerPos.X) * _mult);
            }
            else if (_cameraPosition.X > playerPos.X)
            {
                _cameraPosition.X += ((_cameraPosition.X - playerPos.X) * _mult);
            }
            _drawLocation = _cameraPosition - (_cameraOffset / _fZoomLevel);
        }
    }
}
