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
    class Collision
    {
        Texture2D _tileMapTexture;
        int _mapTileWidth;
        int _mapTileHeight;
        int counter = 0;
        public static int[,] _CollisionMap;

        public void Init(ContentManager Content)
        {
            //set base text for map
            _tileMapTexture = Content.Load<Texture2D>("tilemap_assets/collisionmap2");
            Color[] colors = new Color[_tileMapTexture.Height * _tileMapTexture.Width];
            _tileMapTexture.GetData<Color>(colors);

            //set map info
            _mapTileHeight = _tileMapTexture.Height;
            _mapTileWidth = _tileMapTexture.Width;
            _CollisionMap = new int[_mapTileWidth, _mapTileHeight];

            //setColors
            for (int i = 0; i < _mapTileHeight; i++)
            {
                for (int j = 0; j < _mapTileWidth; j++)
                {
                    if (colors[counter] == Color.White)
                        _CollisionMap[i, j] = 1;
                    else
                        _CollisionMap[i, j] = 0;
                }
                counter++;
            }
        }
    }
}
