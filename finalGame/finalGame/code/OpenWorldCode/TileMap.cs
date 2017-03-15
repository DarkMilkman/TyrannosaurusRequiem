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
    class TileMap
    {
        Texture2D _tileTexture;
        int _tilePixWidth;
        int _tilePixHeight;
        int _tileWidth;
        int _tileHeight;

        Texture2D _tileMapTexture;
        int _mapTileWidth;
        int _mapTileHeight;
        int[] _mapTile;

        //initialize
        public void Init(ContentManager Content)
        {
            _tileTexture = Content.Load<Texture2D>("tilemap_assets/mapTiles");
            _tilePixHeight = 50;
            _tilePixWidth = 50;
            _tileHeight = 2;
            _tileWidth = 5;

            //set base text for map
            _tileMapTexture = Content.Load<Texture2D>("tilemap_assets/landmap");
            Color[] colors = new Color[_tileMapTexture.Width * _tileMapTexture.Height];
            _tileMapTexture.GetData<Color>(colors);

            //set map info
            _mapTileHeight = _tileMapTexture.Height;
            _mapTileWidth = _tileMapTexture.Width;
            _mapTile = new int[_mapTileWidth * _mapTileHeight];

            //setColors
            for (int i = 0; i < _mapTileHeight * _mapTileWidth; i++)
            {
                if (colors[i] == Color.AliceBlue)
                {
                    _mapTile[i] = 0;
                }
                else if (colors[i] == Color.AntiqueWhite)
                {
                    _mapTile[i] = 1;
                }
                else if (colors[i] == Color.Chocolate)
                {
                    _mapTile[i] = 2;
                }
                else if (colors[i] == Color.Aquamarine)
                {
                    _mapTile[i] = 3;
                }
                else if (colors[i] == Color.Azure)
                {
                    _mapTile[i] = 4;
                }
                else if (colors[i] == Color.Beige)
                {
                    _mapTile[i] = 5;
                }
                else if (colors[i] == Color.Bisque)
                {
                    _mapTile[i] = 6;
                }
                else if (colors[i] == Color.Black)
                {
                    _mapTile[i] = 7;
                }
                else if (colors[i] == Color.DarkGray)
                {
                    _mapTile[i] = 8;
                }
                else 
                {
                    _mapTile[i] = 9;
                }
            }
        }

        //draw
        public void Draw(SpriteBatch spriteBatch, Vector2 drawLocation)
        {
            //landMap
            for (int row = 0; row < _mapTileHeight; row++)
            {
                for (int col = 0; col < _mapTileWidth; col++)
                {
                    //Dest
                    int destX = col * _tilePixWidth;
                    int destY = row * _tilePixHeight;
                    //calc source rect
                    int tileID = _mapTile[row * _mapTileWidth + col];
                    int srcX = (tileID % _tileWidth) * _tilePixWidth;
                    int srcY = tileID / _tileWidth * _tilePixHeight;

                    spriteBatch.Draw(_tileTexture, new Vector2(destX - 3060, destY - 4410) - drawLocation, new Rectangle(srcX, srcY, _tilePixWidth, _tilePixHeight), Color.White, 0.0f, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.0f);
                }
            }
        }
    }
}
