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
    class InputHandler
    {
        KeyboardState _currentPressed;
        KeyboardState _prevPressed;

        public Boolean _isDown;
        public Boolean _isUp;
        public Boolean _isLeft;
        public Boolean _isRight;
        public Vector2 _Velocity;

        public Boolean _inGame;//true when walking around outside of battle, no menus up
        public Boolean _inMenu;//true when menus are up
        public Boolean _inBattle;//true inside of a battle
        public Boolean _isPaused;

        public static Boolean _IsEnglish;
        public static Boolean _IsJapanese;
        public static Boolean _IsPolish;

        public Boolean _BattleStartMenu;
        public Boolean _BattleMovesMenu;
        public Boolean _ItemMenu;

        public Boolean _BattleMenuUp;  
        public Boolean _BattleMenuDown;
        public Boolean _BattleMenuLeft;
        public Boolean _BattleMenuRight;

        public Boolean _Attack1;
        public Boolean _Attack2;
        public Boolean _Attack3;
        public Boolean _Attack4;

        public Boolean _APressed;

        public int _LineNum;
        public int _InsideLineNum;

        //music
        public Boolean _musicOn;

        public Boolean _justQuit;

        //collision map number
        int row;
        int col;
        int width;
        int height;
        Vector2 playerLoc;
        string heightName;
        string widthName;


//////////////////////init booleans
        public void Init()
        {
            _isDown = true;
            _isUp = false;
            _isLeft = false;
            _isRight = false;

            //menus
            _inGame = false;//change to false when actually make title screen
            _inBattle = false;
            _inMenu = true;
            _isPaused = false;

            //music
            _musicOn = true;

            //LanguagesSelect
            _IsEnglish = true;
            _IsJapanese = false;
            _IsPolish = false;

            //Battle booleans
            _BattleMovesMenu = false;
            _BattleStartMenu = false;
            _ItemMenu = false;
            //battleMenu booleans
            _BattleMenuUp = true;
            _BattleMenuDown = false;
            _BattleMenuLeft = true;
            _BattleMenuRight = false;
            //Attack booleans
            _Attack1 = false;
            _Attack2 = false;
            _Attack3 = false;
            _Attack4 = false;

            //everywhere
            _APressed = false;
            _LineNum = 0;
            _InsideLineNum = 0;
            _justQuit = false;

            //collision
            col = 93;
            row = 66;
            width = 0;
            height = 0;
            heightName = "h";
            widthName = "w";
            playerLoc = new Vector2(3207, 4562);
        }

        //battle booleans init
        public void initBattleBooleans()
        {
            _BattleMenuLeft = true;
            _BattleMenuUp = true;
            _BattleMenuRight = false;
            _BattleStartMenu = true;
            _ItemMenu = false;
            _inBattle = true;
            _BattleMovesMenu = false;
            _inGame = false;
        }

        public void resetBattleBooleans()
        {
            _BattleMenuLeft = true;
            _BattleMenuUp = true;
            _BattleMenuRight = false;
            _BattleStartMenu = true;
            _ItemMenu = false;
            _inBattle = false;
            _BattleMovesMenu = false;
            _inGame = true;
            _APressed = false;
        }

        //return the languages
        public static Boolean returnIsEnglish()
        {
            return _IsEnglish;
        }
        public static Boolean returnIsJapanese()
        {
            return _IsJapanese;
        }
        public static Boolean returnIsPolish()
        {
            return _IsPolish;
        }

////////////////////////////Handler input in menus
        public void HandleInputInMenus()
        {
            _currentPressed = Keyboard.GetState();
            if (_currentPressed.IsKeyDown(Keys.Down) && _prevPressed.IsKeyUp(Keys.Down))
            {
///////////////////////////////in game menu
                if (!_APressed)
                {
                    if (_isPaused)
                    {
                        if (_LineNum >= 2)
                            _LineNum = 2;
                        else
                            _LineNum++;
                    }
                }
                if (_APressed)
                {
                    if (_isPaused)
                    {
                        if (_InsideLineNum >= 1)
                            _InsideLineNum = 1;
                        else
                            _InsideLineNum++;
                    }
                }
////////////////////////////main menu 
                if (!_APressed)
                {
                    if (!_isPaused)
                    {
                        if (_LineNum >= 5)
                            _LineNum = 5;
                        else
                            _LineNum++;
                    }
                }
                if (_APressed)
                {
                    if (!_isPaused)
                    {
                        if (_LineNum == 3)
                        {
                            if (_InsideLineNum >= 1)
                                _InsideLineNum = 1;
                            else
                                _InsideLineNum++;
                        }
                        else
                        {
                            if (_InsideLineNum >= 2)
                                _InsideLineNum = 2;
                            else
                                _InsideLineNum++;
                        }
                    }
                }
            }
            if (_currentPressed.IsKeyDown(Keys.Up) && _prevPressed.IsKeyUp(Keys.Up))
            {
///////////////////////////////////////in game menu
                if (!_APressed)
                {
                    if (_isPaused)
                    {
                        if (_LineNum <= 0)
                            _LineNum = 0;
                        else
                            _LineNum--;
                    }
                }
                if (_APressed)
                {
                    if (_isPaused)
                    {
                        if (_InsideLineNum <= 0)
                            _InsideLineNum = 0;
                        else
                            _InsideLineNum--;
                    }
                }
///////////////////////////////main menu 
                if (!_APressed)
                {
                    if (!_isPaused)
                    {
                        if (_LineNum <= 0)
                            _LineNum = 0;
                        else
                            _LineNum--;
                    }
                }
                if (_APressed)
                {
                    if (!_isPaused)
                    {
                        if (_InsideLineNum <= 0)
                            _InsideLineNum = 0;
                        else
                            _InsideLineNum--;
                    }
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
/////////////////////////in game menu
                if (_isPaused)
                {
                    _APressed = false;
                    _inMenu = true;
                    _isPaused = true;
                    _InsideLineNum = 0;
                }
////////////////////////////main menu 
                if (!_isPaused)
                {
                    _APressed = false;
                    _inMenu = true;
                    _isPaused = false;
                    _InsideLineNum = 0;
                }
            }
            if (_currentPressed.IsKeyDown(Keys.A) && _prevPressed.IsKeyUp(Keys.A))
            {
/////////////////////////////////main menu
                if (!_isPaused)
                {
                    if (_LineNum == 0)
                    {
                        _APressed = false;
                        _inGame = true;
                        _inMenu = false;
                        _LineNum = 0;
                        _InsideLineNum = 0;
                    }
                    else if (_LineNum == 5)
                        _justQuit = true;
                    else
                        _APressed = true;
                }
//////////////////////////////////////in Game menu
                if (_isPaused)
                {
                    if (_LineNum == 2)
                    {
                        _InsideLineNum = 0;
                        _LineNum = 0;
                        _isPaused = false;
                        _inGame = false;
                        _inMenu = true;
                        _inBattle = false;
                        Game1.transitionStart = 0;
                    }
                    else
                        _APressed = true;
                }
            }
            if (_currentPressed.IsKeyDown(Keys.Z) && _prevPressed.IsKeyUp(Keys.Z))
            {
/////////////////////////in game menu
                if (_isPaused)
                {
                    if (_inMenu)
                    {
                        _InsideLineNum = 0;
                        _LineNum = 0;
                        _isPaused = false;
                        _inGame = true;
                        _inMenu = false;
                        _inBattle = false;
                        _APressed = false;
                    }
                    else
                    {
                        _isPaused = true;
                        _inGame = false;
                        _inMenu = true;
                        _inBattle = false;
                    }
                }
            }
            _prevPressed = _currentPressed;
        }

///////////////////////////////////////////////handle input in battle
        public void InputInBattle()
        {
            _currentPressed = Keyboard.GetState();
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                if (_BattleMenuRight == true)
                {
                    _BattleMenuLeft = false;
                    _BattleMenuDown = true;
                    _BattleMenuRight = true;
                    _BattleMenuUp = false;

                    _Attack1 = false;
                    _Attack2 = false;
                    _Attack3 = false;
                    _Attack4 = true;
                }
                else if (_BattleMenuLeft == true)
                {
                    _BattleMenuLeft = true;
                    _BattleMenuDown = true;
                    _BattleMenuRight = false;
                    _BattleMenuUp = false;

                    _Attack1 = false;
                    _Attack2 = false;
                    _Attack3 = true;
                    _Attack4 = false;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                if (_BattleMenuRight == true)
                {
                    _BattleMenuLeft = false;
                    _BattleMenuDown = false;
                    _BattleMenuRight = true;
                    _BattleMenuUp = true;

                    _Attack1 = false;
                    _Attack2 = true;
                    _Attack3 = false;
                    _Attack4 = false;
                }
                else if (_BattleMenuLeft == true)
                {
                    _BattleMenuLeft = true;
                    _BattleMenuDown = false;
                    _BattleMenuRight = false;
                    _BattleMenuUp = true;

                    _Attack1 = true;
                    _Attack2 = false;
                    _Attack3 = false;
                    _Attack4 = false;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if (_BattleMenuUp == true)
                {
                    _BattleMenuLeft = true;
                    _BattleMenuDown = false;
                    _BattleMenuRight = false;
                    _BattleMenuUp = true;

                    _Attack1 = true;
                    _Attack2 = false;
                    _Attack3 = false;
                    _Attack4 = false;
                }
                else if (_BattleMenuDown == true)
                {
                    _BattleMenuLeft = true;
                    _BattleMenuDown = true;
                    _BattleMenuRight = false;
                    _BattleMenuUp = false;

                    _Attack1 = false;
                    _Attack2 = false;
                    _Attack3 = true;
                    _Attack4 = false;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if (_BattleMenuUp == true)
                {
                    _BattleMenuLeft = false;
                    _BattleMenuDown = false;
                    _BattleMenuRight = true;
                    _BattleMenuUp = true;

                    _Attack1 = false;
                    _Attack2 = true;
                    _Attack3 = false;
                    _Attack4 = false;
                }
                else if (_BattleMenuDown == true)
                {
                    _BattleMenuLeft = false;
                    _BattleMenuDown = true;
                    _BattleMenuRight = true;
                    _BattleMenuUp = false;

                    _Attack1 = false;
                    _Attack2 = false;
                    _Attack3 = false;
                    _Attack4 = true;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                _BattleStartMenu = true;
                _ItemMenu = false;
                _BattleMovesMenu = false;
                _APressed = false;

            }
            if (_currentPressed.IsKeyDown(Keys.A) && _prevPressed.IsKeyUp(Keys.A))
            {
                //attacks
                if (_BattleMovesMenu)
                    _APressed = true;

                if ((_BattleMenuRight == true) && (_BattleStartMenu))
                {
                    _ItemMenu = true;
                    _BattleStartMenu = false;
                }
                else
                {
                    //menus
                    _ItemMenu = false;
                    _BattleStartMenu = false;
                    _BattleMovesMenu = true;
                }
            }
            _prevPressed = _currentPressed;
        }

/////////////////////////////////////////////////////handle the input in the game
        public void HandleInputInGame(Animation anim)
        {
            _currentPressed = Keyboard.GetState();
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                anim._IsPlaying = true;
                _isDown = true;
                _isLeft = false;
                _isRight = false;
                _isUp = false;

                if(GenFunctions.isMoveable(height + 10, row, col, heightName))
                {
                    height += 10;
                    _Velocity.Y += 10f;
                    if(height >= 50)
                    {
                        height = 0;
                        col += 1;
                    }
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                anim._IsPlaying = true;
                _isDown = false;
                _isLeft = true;
                _isRight = false;
                _isUp = false;

                if (GenFunctions.isMoveable(width - 10, row, col, widthName))
                {
                    width -= 10;
                    _Velocity.X -= 10f;
                    if (width <= 0)
                    {
                        width = 50;
                        width -= 1;
                    }
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                anim._IsPlaying = true;
                _isDown = false;
                _isLeft = false;
                _isRight = true;
                _isUp = false;

                if (GenFunctions.isMoveable(width + 10, row, col, widthName))
                {
                    width += 10;
                    _Velocity.X += 10f;
                    if (width >= 50)
                    {
                        width = 0;
                        width += 1;
                    }
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                anim._IsPlaying = true;
                _isDown = false;
                _isLeft = false;
                _isRight = false;
                _isUp = true;

                if (GenFunctions.isMoveable(height - 10, row, col, heightName))
                {
                    height -= 10;
                    _Velocity.Y -= 10f;
                    if (height <= 0)
                    {
                        height = 50;
                        col -= 1;
                    }
                }
            }
            if (_currentPressed.IsKeyDown(Keys.Z) && _prevPressed.IsKeyUp(Keys.Z))
            {
                if (_inMenu)
                {
                    _isPaused = false;
                    _inGame = true;
                    _inMenu = false;
                    _inBattle = false;
                }
                else
                {
                    _isPaused = true;
                    _inGame = false;
                    _inMenu = true;
                    _inBattle = false;
                }
            }
            _prevPressed = _currentPressed;
//////////////////////////testing battle
            if (Keyboard.GetState().IsKeyDown(Keys.B))
            {
                _BattleMenuLeft = true;
                _BattleMenuUp = true;
                _BattleMenuRight = false;
                _BattleStartMenu = true;
                _ItemMenu = false;
                _inBattle = true;
                _BattleMovesMenu = false;
                _inGame = false;
            }
        }
    }
}
