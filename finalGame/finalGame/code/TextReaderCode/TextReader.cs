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
    class TextReader
    {
        SpriteFont _startScreenFont;
        public SpriteFont _inGameFont;
        List<string> _wordsE;
        List<string> _wordsJ;
        List<string> _wordsP;
        List<string> _words = new List<string>();
        float _screenCenter = 320f;
        //int _ySpace = 10;
        //int _lineNum;

        //Load content into stream
        public void LoadContent(ContentManager Content)
        {
            _startScreenFont = Content.Load<SpriteFont>("fonts/StartScreen");
            _inGameFont = Content.Load<SpriteFont>("fonts/InGame");
            //English
            StreamReader Englishfile = new StreamReader("Content/textFiles/English.txt");
            _wordsE = new List<string>();
            while (!Englishfile.EndOfStream)
            {
                string theStringE = Englishfile.ReadLine();
                _wordsE.Add(theStringE);
            }
            ////Japanese
            StreamReader Japanesefile = new StreamReader("Content/textFiles/Japanese.txt");
            _wordsJ = new List<string>();
            while (!Japanesefile.EndOfStream)
            {
                string theStringJ = Japanesefile.ReadLine();
                _wordsJ.Add(theStringJ);
            }
            //Polish
            StreamReader Polishfile = new StreamReader("Content/textFiles/Polish.txt");
            _wordsP = new List<string>();
            while (!Polishfile.EndOfStream)
            {
                string theStringP = Polishfile.ReadLine();
                _wordsP.Add(theStringP);
            }
        }

        public void Update(GameTime gameTime)
        {
            _words = GenFunctions.returnList(_wordsE, _wordsJ, _wordsP);
            //_words = _wordsP;
        }

        //Battle Text
        public void Draw(SpriteBatch spriteBatch, InputHandler input, BattleStat stat)
        {
            //battle start menu
            if ((input._BattleStartMenu) && (!input._BattleMovesMenu) && (!input._ItemMenu))
            {
                if (input._BattleMenuLeft)
                {
                    spriteBatch.DrawString(_startScreenFont, _words[1], new Vector2(20, 10), Color.Red);
                    spriteBatch.DrawString(_startScreenFont, _words[2], new Vector2(250, 10), Color.Black);
                }
                if (input._BattleMenuRight)
                {
                    spriteBatch.DrawString(_startScreenFont, _words[1], new Vector2(20, 10), Color.Black);
                    spriteBatch.DrawString(_startScreenFont, _words[2], new Vector2(250,10), Color.Red);
                }
            }

            //battle item menu
            else if ((input._ItemMenu) && (!input._BattleMovesMenu))
            {
                spriteBatch.DrawString(_inGameFont, _words[27], new Vector2(10, 10), Color.Black);
                spriteBatch.DrawString(_inGameFont, _words[28], new Vector2(10, 50), Color.Black);
            }

            //battle moves menu
            else if (input._BattleMovesMenu)
            {
                if ((input._Attack1)||(input._BattleMenuLeft) && (input._BattleMenuUp))
                {
                    spriteBatch.DrawString(_inGameFont, _words[5], new Vector2(10, 10), Color.Red);
                    spriteBatch.DrawString(_inGameFont, _words[6], new Vector2(10, 50), Color.Black);
                    spriteBatch.DrawString(_inGameFont, _words[7], new Vector2(300, 10), Color.Black);
                    spriteBatch.DrawString(_inGameFont, _words[8], new Vector2(300, 50), Color.Black);
                    stat._attackNumber = 10;
                }
                else if (input._Attack2)
                {
                    spriteBatch.DrawString(_inGameFont, _words[5], new Vector2(10, 10), Color.Black);
                    spriteBatch.DrawString(_inGameFont, _words[6], new Vector2(10, 50), Color.Black);
                    spriteBatch.DrawString(_inGameFont, _words[7], new Vector2(300, 10), Color.Red);
                    spriteBatch.DrawString(_inGameFont, _words[8], new Vector2(300, 50), Color.Black);
                    stat._attackNumber = 15;
                }
                else if (input._Attack3)
                {
                    spriteBatch.DrawString(_inGameFont, _words[5], new Vector2(10, 10), Color.Black);
                    spriteBatch.DrawString(_inGameFont, _words[6], new Vector2(10, 50), Color.Red);
                    spriteBatch.DrawString(_inGameFont, _words[7], new Vector2(300, 10), Color.Black);
                    spriteBatch.DrawString(_inGameFont, _words[8], new Vector2(300, 50), Color.Black);
                    stat._attackNumber = 17;
                }
                else if (input._Attack4)
                {
                    spriteBatch.DrawString(_inGameFont, _words[5], new Vector2(10, 10), Color.Black);
                    spriteBatch.DrawString(_inGameFont, _words[6], new Vector2(10, 50), Color.Black);
                    spriteBatch.DrawString(_inGameFont, _words[7], new Vector2(300, 10), Color.Black);
                    spriteBatch.DrawString(_inGameFont, _words[8], new Vector2(300, 50), Color.Red);
                    stat._attackNumber = 0;
                }
            }
        }

        //Pause Menu
        public void Draw(SpriteBatch spriteBatch, InputHandler input)
        {
            //pause menu
            if ((input._inMenu) && (input._isPaused) && (input._LineNum == 0) && (!input._APressed))
            {
                spriteBatch.DrawString(_inGameFont, _words[31], new Vector2(_screenCenter, 50) - (_inGameFont.MeasureString(_words[31])) / 2, Color.Red);
                spriteBatch.DrawString(_inGameFont, _words[49], new Vector2(_screenCenter, 100) - (_inGameFont.MeasureString(_words[49])) / 2, Color.White);
                spriteBatch.DrawString(_inGameFont, _words[57], new Vector2(_screenCenter, 150) - (_inGameFont.MeasureString(_words[57])) / 2, Color.White);
            }
            else if ((input._inMenu) && (input._isPaused) && (input._LineNum == 1) && (!input._APressed))
            {
                spriteBatch.DrawString(_inGameFont, _words[31], new Vector2(_screenCenter, 50) - (_inGameFont.MeasureString(_words[31])) / 2, Color.White);
                spriteBatch.DrawString(_inGameFont, _words[49], new Vector2(_screenCenter, 100) - (_inGameFont.MeasureString(_words[49])) / 2, Color.Red);
                spriteBatch.DrawString(_inGameFont, _words[57], new Vector2(_screenCenter, 150) - (_inGameFont.MeasureString(_words[57])) / 2, Color.White);
            }
            else if ((input._inMenu) && (input._isPaused) && (input._LineNum == 2) && (!input._APressed))
            {
                spriteBatch.DrawString(_inGameFont, _words[31], new Vector2(_screenCenter, 50) - (_inGameFont.MeasureString(_words[31])) / 2, Color.White);
                spriteBatch.DrawString(_inGameFont, _words[49], new Vector2(_screenCenter, 100) - (_inGameFont.MeasureString(_words[49])) / 2, Color.White);
                spriteBatch.DrawString(_inGameFont, _words[57], new Vector2(_screenCenter, 150) - (_inGameFont.MeasureString(_words[57])) / 2, Color.Red);
            }

            //Controls
            if ((input._inMenu) && (input._isPaused) && (input._LineNum == 0) && (input._APressed))
            {
                spriteBatch.DrawString(_startScreenFont, _words[31], new Vector2(_screenCenter, 50) - (_startScreenFont.MeasureString(_words[31])) / 2, Color.White);
                spriteBatch.DrawString(_inGameFont, _words[32], new Vector2(_screenCenter, 150) - (_inGameFont.MeasureString(_words[32])) / 2, Color.White);
                spriteBatch.DrawString(_inGameFont, _words[33], new Vector2(_screenCenter, 200) - (_inGameFont.MeasureString(_words[33])) / 2, Color.White);
                spriteBatch.DrawString(_inGameFont, _words[34], new Vector2(_screenCenter, 250) - (_inGameFont.MeasureString(_words[34])) / 2, Color.White);
                spriteBatch.DrawString(_inGameFont, _words[35], new Vector2(_screenCenter, 300) - (_inGameFont.MeasureString(_words[35])) / 2, Color.White);
            }

            //music toggle
            if ((input._inMenu) && (input._isPaused) && (input._LineNum == 1) && (input._InsideLineNum == 0) && (input._APressed))
            {
                spriteBatch.DrawString(_startScreenFont, _words[49], new Vector2(_screenCenter, 50) - (_startScreenFont.MeasureString(_words[49])) / 2, Color.White);
                spriteBatch.DrawString(_inGameFont, _words[50], new Vector2(_screenCenter, 150) - (_inGameFont.MeasureString(_words[50])) / 2, Color.Red);
                spriteBatch.DrawString(_inGameFont, _words[51], new Vector2(_screenCenter, 200) - (_inGameFont.MeasureString(_words[51])) / 2, Color.White);
                input._musicOn = true;
            }
            else if ((input._inMenu) && (input._isPaused) && (input._LineNum == 1) && (input._InsideLineNum == 1) && (input._APressed))
            {
                spriteBatch.DrawString(_startScreenFont, _words[49], new Vector2(_screenCenter, 50) - (_startScreenFont.MeasureString(_words[49])) / 2, Color.White);
                spriteBatch.DrawString(_inGameFont, _words[50], new Vector2(_screenCenter, 150) - (_inGameFont.MeasureString(_words[50])) / 2, Color.White);
                spriteBatch.DrawString(_inGameFont, _words[51], new Vector2(_screenCenter, 200) - (_inGameFont.MeasureString(_words[51])) / 2, Color.Red);
                input._musicOn = false;
            }

            //main menu
            if ((input._inMenu) && (!input._isPaused) && (input._LineNum == 0) && (!input._APressed))
            {
                spriteBatch.DrawString(_inGameFont, _words[54], new Vector2(_screenCenter, 225) - (_inGameFont.MeasureString(_words[54])) / 2, Color.Red);
                spriteBatch.DrawString(_inGameFont, _words[31], new Vector2(_screenCenter, 255) - (_inGameFont.MeasureString(_words[31])) / 2, Color.Black);
                spriteBatch.DrawString(_inGameFont, _words[38], new Vector2(_screenCenter, 285) - (_inGameFont.MeasureString(_words[38])) / 2, Color.Black);
                spriteBatch.DrawString(_inGameFont, _words[49], new Vector2(_screenCenter, 315) - (_inGameFont.MeasureString(_words[49])) / 2, Color.Black);
                spriteBatch.DrawString(_inGameFont, _words[43], new Vector2(_screenCenter, 345) - (_inGameFont.MeasureString(_words[43])) / 2, Color.Black);
                spriteBatch.DrawString(_inGameFont, _words[60], new Vector2(_screenCenter, 375) - (_inGameFont.MeasureString(_words[60])) / 2, Color.Black);
            }
            else if ((input._inMenu) && (!input._isPaused) && (input._LineNum == 1) && (!input._APressed))
            {
                spriteBatch.DrawString(_inGameFont, _words[54], new Vector2(_screenCenter, 225) - (_inGameFont.MeasureString(_words[54])) / 2, Color.Black);
                spriteBatch.DrawString(_inGameFont, _words[31], new Vector2(_screenCenter, 255) - (_inGameFont.MeasureString(_words[31])) / 2, Color.Red);
                spriteBatch.DrawString(_inGameFont, _words[38], new Vector2(_screenCenter, 285) - (_inGameFont.MeasureString(_words[38])) / 2, Color.Black);
                spriteBatch.DrawString(_inGameFont, _words[49], new Vector2(_screenCenter, 315) - (_inGameFont.MeasureString(_words[49])) / 2, Color.Black);
                spriteBatch.DrawString(_inGameFont, _words[43], new Vector2(_screenCenter, 345) - (_inGameFont.MeasureString(_words[43])) / 2, Color.Black);
                spriteBatch.DrawString(_inGameFont, _words[60], new Vector2(_screenCenter, 375) - (_inGameFont.MeasureString(_words[60])) / 2, Color.Black);
            }
            else if ((input._inMenu) && (!input._isPaused) && (input._LineNum == 2) && (!input._APressed))
            {
                spriteBatch.DrawString(_inGameFont, _words[54], new Vector2(_screenCenter, 225) - (_inGameFont.MeasureString(_words[54])) / 2, Color.Black);
                spriteBatch.DrawString(_inGameFont, _words[31], new Vector2(_screenCenter, 255) - (_inGameFont.MeasureString(_words[31])) / 2, Color.Black);
                spriteBatch.DrawString(_inGameFont, _words[38], new Vector2(_screenCenter, 285) - (_inGameFont.MeasureString(_words[38])) / 2, Color.Red);
                spriteBatch.DrawString(_inGameFont, _words[49], new Vector2(_screenCenter, 315) - (_inGameFont.MeasureString(_words[49])) / 2, Color.Black);
                spriteBatch.DrawString(_inGameFont, _words[43], new Vector2(_screenCenter, 345) - (_inGameFont.MeasureString(_words[43])) / 2, Color.Black);
                spriteBatch.DrawString(_inGameFont, _words[60], new Vector2(_screenCenter, 375) - (_inGameFont.MeasureString(_words[60])) / 2, Color.Black);
            }
            else if ((input._inMenu) && (!input._isPaused) && (input._LineNum == 3) && (!input._APressed))
            {
                spriteBatch.DrawString(_inGameFont, _words[54], new Vector2(_screenCenter, 225) - (_inGameFont.MeasureString(_words[54])) / 2, Color.Black);
                spriteBatch.DrawString(_inGameFont, _words[31], new Vector2(_screenCenter, 255) - (_inGameFont.MeasureString(_words[31])) / 2, Color.Black);
                spriteBatch.DrawString(_inGameFont, _words[38], new Vector2(_screenCenter, 285) - (_inGameFont.MeasureString(_words[38])) / 2, Color.Black);
                spriteBatch.DrawString(_inGameFont, _words[49], new Vector2(_screenCenter, 315) - (_inGameFont.MeasureString(_words[49])) / 2, Color.Red);
                spriteBatch.DrawString(_inGameFont, _words[43], new Vector2(_screenCenter, 345) - (_inGameFont.MeasureString(_words[43])) / 2, Color.Black);
                spriteBatch.DrawString(_inGameFont, _words[60], new Vector2(_screenCenter, 375) - (_inGameFont.MeasureString(_words[60])) / 2, Color.Black);
            }
            else if ((input._inMenu) && (!input._isPaused) && (input._LineNum == 4) && (!input._APressed))
            {
                spriteBatch.DrawString(_inGameFont, _words[54], new Vector2(_screenCenter, 225) - (_inGameFont.MeasureString(_words[54])) / 2, Color.Black);
                spriteBatch.DrawString(_inGameFont, _words[31], new Vector2(_screenCenter, 255) - (_inGameFont.MeasureString(_words[31])) / 2, Color.Black);
                spriteBatch.DrawString(_inGameFont, _words[38], new Vector2(_screenCenter, 285) - (_inGameFont.MeasureString(_words[38])) / 2, Color.Black);
                spriteBatch.DrawString(_inGameFont, _words[49], new Vector2(_screenCenter, 315) - (_inGameFont.MeasureString(_words[49])) / 2, Color.Black);
                spriteBatch.DrawString(_inGameFont, _words[43], new Vector2(_screenCenter, 345) - (_inGameFont.MeasureString(_words[43])) / 2, Color.Red);
                spriteBatch.DrawString(_inGameFont, _words[60], new Vector2(_screenCenter, 375) - (_inGameFont.MeasureString(_words[60])) / 2, Color.Black);
            }
            else if ((input._inMenu) && (!input._isPaused) && (input._LineNum == 5) && (!input._APressed))
            {
                spriteBatch.DrawString(_inGameFont, _words[54], new Vector2(_screenCenter, 225) - (_inGameFont.MeasureString(_words[54])) / 2, Color.Black);
                spriteBatch.DrawString(_inGameFont, _words[31], new Vector2(_screenCenter, 255) - (_inGameFont.MeasureString(_words[31])) / 2, Color.Black);
                spriteBatch.DrawString(_inGameFont, _words[38], new Vector2(_screenCenter, 285) - (_inGameFont.MeasureString(_words[38])) / 2, Color.Black);
                spriteBatch.DrawString(_inGameFont, _words[49], new Vector2(_screenCenter, 315) - (_inGameFont.MeasureString(_words[49])) / 2, Color.Black);
                spriteBatch.DrawString(_inGameFont, _words[43], new Vector2(_screenCenter, 345) - (_inGameFont.MeasureString(_words[43])) / 2, Color.Black);
                spriteBatch.DrawString(_inGameFont, _words[60], new Vector2(_screenCenter, 375) - (_inGameFont.MeasureString(_words[60])) / 2, Color.Red);
            }

            //controls
            else if ((input._inMenu) && (!input._isPaused) && (input._LineNum == 1) && (input._APressed))
            {
                spriteBatch.DrawString(_startScreenFont, _words[31], new Vector2(_screenCenter, 235) - (_startScreenFont.MeasureString(_words[31])) / 2, Color.Black);
                spriteBatch.DrawString(_inGameFont, _words[32], new Vector2(_screenCenter, 290) - (_inGameFont.MeasureString(_words[32])) / 2, Color.Black);
                spriteBatch.DrawString(_inGameFont, _words[33], new Vector2(_screenCenter, 320) - (_inGameFont.MeasureString(_words[33])) / 2, Color.Black);
                spriteBatch.DrawString(_inGameFont, _words[34], new Vector2(_screenCenter, 350) - (_inGameFont.MeasureString(_words[34])) / 2, Color.Black);
                spriteBatch.DrawString(_inGameFont, _words[35], new Vector2(_screenCenter, 380) - (_inGameFont.MeasureString(_words[35])) / 2, Color.Black);
            }
            //changle languages
            else if ((input._inMenu) && (!input._isPaused) && (input._LineNum == 2) && (input._InsideLineNum == 0) && (input._APressed))
            {
                spriteBatch.DrawString(_startScreenFont, _words[38], new Vector2(_screenCenter, 235) - (_startScreenFont.MeasureString(_words[38])) / 2, Color.Black);
                spriteBatch.DrawString(_inGameFont, _words[39], new Vector2(_screenCenter, 290) - (_inGameFont.MeasureString(_words[39])) / 2, Color.Red);
                spriteBatch.DrawString(_inGameFont, _words[40], new Vector2(_screenCenter, 320) - (_inGameFont.MeasureString(_words[40])) / 2, Color.Black);
                spriteBatch.DrawString(_inGameFont, _words[41], new Vector2(_screenCenter, 350) - (_inGameFont.MeasureString(_words[41])) / 2, Color.Black);
                InputHandler._IsEnglish = true;
                InputHandler._IsJapanese = false;
                InputHandler._IsPolish = false;
            }
            else if ((input._inMenu) && (!input._isPaused) && (input._LineNum == 2) && (input._InsideLineNum == 1) && (input._APressed))
            {
                spriteBatch.DrawString(_startScreenFont, _words[38], new Vector2(_screenCenter, 235) - (_startScreenFont.MeasureString(_words[38])) / 2, Color.Black);
                spriteBatch.DrawString(_inGameFont, _words[39], new Vector2(_screenCenter, 290) - (_inGameFont.MeasureString(_words[39])) / 2, Color.Black);
                spriteBatch.DrawString(_inGameFont, _words[40], new Vector2(_screenCenter, 320) - (_inGameFont.MeasureString(_words[40])) / 2, Color.Red);
                spriteBatch.DrawString(_inGameFont, _words[41], new Vector2(_screenCenter, 350) - (_inGameFont.MeasureString(_words[41])) / 2, Color.Black);
                InputHandler._IsEnglish = false;
                InputHandler._IsJapanese = false;
                InputHandler._IsPolish = true;
            }
            else if ((input._inMenu) && (!input._isPaused) && (input._LineNum == 2) && (input._InsideLineNum == 2) && (input._APressed))
            {
                spriteBatch.DrawString(_startScreenFont, _words[38], new Vector2(_screenCenter, 235) - (_startScreenFont.MeasureString(_words[38])) / 2, Color.Black);
                spriteBatch.DrawString(_inGameFont, _words[39], new Vector2(_screenCenter, 290) - (_inGameFont.MeasureString(_words[39])) / 2, Color.Black);
                spriteBatch.DrawString(_inGameFont, _words[40], new Vector2(_screenCenter, 320) - (_inGameFont.MeasureString(_words[40])) / 2, Color.Black);
                spriteBatch.DrawString(_inGameFont, _words[41], new Vector2(_screenCenter, 350) - (_inGameFont.MeasureString(_words[41])) / 2, Color.Red);
                InputHandler._IsEnglish = false;
                InputHandler._IsJapanese = true;
                InputHandler._IsPolish = false;
            }
            //toggle music
            else if ((input._inMenu) && (!input._isPaused) && (input._LineNum == 3) && (input._InsideLineNum == 0) && (input._APressed))
            {
                spriteBatch.DrawString(_startScreenFont, _words[49], new Vector2(_screenCenter, 235) - (_startScreenFont.MeasureString(_words[49])) / 2, Color.Black);
                spriteBatch.DrawString(_inGameFont, _words[50], new Vector2(_screenCenter, 290) - (_inGameFont.MeasureString(_words[50])) / 2, Color.Red);
                spriteBatch.DrawString(_inGameFont, _words[51], new Vector2(_screenCenter, 320) - (_inGameFont.MeasureString(_words[51])) / 2, Color.Black);
                input._musicOn = true;
            }
            else if ((input._inMenu) && (!input._isPaused) && (input._LineNum == 3) && (input._InsideLineNum == 1) && (input._APressed))
            {
                spriteBatch.DrawString(_startScreenFont, _words[49], new Vector2(_screenCenter, 235) - (_startScreenFont.MeasureString(_words[49])) / 2, Color.Black);
                spriteBatch.DrawString(_inGameFont, _words[50], new Vector2(_screenCenter, 290) - (_inGameFont.MeasureString(_words[50])) / 2, Color.Black);
                spriteBatch.DrawString(_inGameFont, _words[51], new Vector2(_screenCenter, 320) - (_inGameFont.MeasureString(_words[51])) / 2, Color.Red);
                input._musicOn = false;
            }
            //credits
            else if ((input._inMenu) && (!input._isPaused) && (input._LineNum == 4) && (input._APressed))
            {
                spriteBatch.DrawString(_startScreenFont, _words[63], new Vector2(_screenCenter, 235) - (_startScreenFont.MeasureString(_words[63])) / 2, Color.Black);
                spriteBatch.DrawString(_inGameFont, _words[64], new Vector2(_screenCenter, 275) - (_inGameFont.MeasureString(_words[64])) / 2, Color.Black);
                spriteBatch.DrawString(_inGameFont, _words[65], new Vector2(_screenCenter, 305) - (_inGameFont.MeasureString(_words[65])) / 2, Color.Black);
                spriteBatch.DrawString(_inGameFont, _words[66], new Vector2(_screenCenter, 335) - (_inGameFont.MeasureString(_words[66])) / 2, Color.Black);
                spriteBatch.DrawString(_inGameFont, _words[67], new Vector2(_screenCenter, 365) - (_inGameFont.MeasureString(_words[67])) / 2, Color.Black);
                spriteBatch.DrawString(_inGameFont, _words[68], new Vector2(_screenCenter, 395) - (_inGameFont.MeasureString(_words[68])) / 2, Color.Black);
                spriteBatch.DrawString(_inGameFont, _words[69], new Vector2(_screenCenter, 425) - (_inGameFont.MeasureString(_words[69])) / 2, Color.Black);
            }
        }
    }
}
