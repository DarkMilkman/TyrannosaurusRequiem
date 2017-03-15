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
    class Sprite
    {
        Texture2D _texture;
        Rectangle _srcRec;
        Vector2 _originLoc;

        //Constructors
        public Sprite(Texture2D theTexture, Rectangle theRectangle)
            : this(theTexture, theRectangle, Vector2.Zero)
        {
        }

        public Sprite(Texture2D theTexture, Rectangle theRectangle, Vector2 theOrigin)
        {
            _texture = theTexture;
            _srcRec = theRectangle;
            _originLoc = theOrigin;
        }

        //Draw 
        public void Draw(SpriteBatch spriteBatch, Vector2 dectLoc, float rotation = 0.0f, float scale = 1.0f, SpriteEffects effects = SpriteEffects.None, float layer = 0.0f)
        {
            Draw(spriteBatch, dectLoc, Color.White, rotation, scale, effects, layer);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 destLoc, Color theColor, float rotation = 0.0f, float scale = 1.0f, SpriteEffects effects = SpriteEffects.None, float layer = 0.0f)
        {
            spriteBatch.Draw(_texture, destLoc, _srcRec, theColor, rotation, _originLoc, scale, effects, layer);
        }
    }
}
