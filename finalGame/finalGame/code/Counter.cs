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
    class Counter
    {
        int frameRate = 0;
        int frameCounter = 0;
        TimeSpan elaspsedTime = TimeSpan.Zero;

        public void Update(GameTime gameTime)
        {
            elaspsedTime += gameTime.ElapsedGameTime;
            if (elaspsedTime > TimeSpan.FromSeconds(1)) 
            {
                elaspsedTime -= TimeSpan.FromSeconds(1);
                frameRate = frameCounter;
                frameCounter = 0;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, SpriteFont spriteFont)
        {
            frameCounter++;
            string fps = string.Format("FPS: {0}", frameRate);

            spriteBatch.Begin();

            spriteBatch.DrawString(spriteFont, fps, new Vector2(500, 15), Color.White);

 
            spriteBatch.End();
        }
    }
}

