using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace finalGame
{
    class GenFunctions
    {
        public static int RandomNumber()
        {
            Random rng = new Random();
            int RandomNum = rng.Next(0, 4);
            return RandomNum;
        }

        public static int RandomPokemonLocation()
        {
            Random rng = new Random();
            int RandomNum = rng.Next(1000, 3000);
            return RandomNum;
        }

        public static int RandomGhostLocation()
        {
            Random rng = new Random();
            int RandomNum = rng.Next(50, 500);
            return RandomNum;
        }

        public static SpriteEffects RandomSpritesEffects()
        {
            int rand = RandomNumber();
            if ((rand == 0) || (rand == 1))
            {
                return SpriteEffects.FlipHorizontally;
            }
            else
            {
                return SpriteEffects.None;
            }
        }

        public static Boolean isMoveable(int playerLoc, int row, int col, string type)
        {
            if (playerLoc < 0)
            {
                if (type == "h")
                {
                    if (Collision._CollisionMap[col - 1, row] == 1)
                        return false;
                    else
                        return true;
                }
                else
                {
                    if (Collision._CollisionMap[col, row - 1] == 1)
                        return false;
                    else
                        return true;
                }
            }
            else if (playerLoc > 50)
            {
                if (type == "h")
                {
                    if (Collision._CollisionMap[col + 1, row] == 1)
                        return false;
                    else
                        return true;
                }
                else
                {
                    if (Collision._CollisionMap[col, row + 1] == 1)
                        return false;
                    else
                        return true;
                }
            }
            return true;
        }

        public static Sprite returnSprite(Sprite sprite1, Sprite sprite2, Sprite sprite3, Sprite sprite4)
        {
            int spriteNum = RandomNumber();
            if (spriteNum == 0)
            {
                return sprite1;
            }
            else if (spriteNum == 1)
            {
                return sprite2;
            }
            else if (spriteNum == 2)
            {
                return sprite3;
            }
            else
            {
                return sprite4;
            }
        }

        public static List<string> returnList(List<string> list1, List<string> list2, List<string> list3)
        {
            Boolean isEnglish = InputHandler.returnIsEnglish();
            Boolean isPolish = InputHandler.returnIsPolish();
            Boolean isJapanese = InputHandler.returnIsJapanese();

            if (isEnglish)
            {
                return list1;
            }
            else if (isJapanese)
            {
                return list2;
            }
            else
            {
                return list3;
            }
        }
    }
}
