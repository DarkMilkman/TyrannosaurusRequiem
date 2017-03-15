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
    class BattleStat
    {
        int _healthNum;
        int _playerAttackAmount;
        int _enemyAttackAmount;
        //enemy attack numbers
        int _enemyAttack1 = 17;
        int _enemyAttack2 = 13;
        int _enemyAttack3 = 5;
        int _enemyAttack4 = 0;
        public int _attackNumber;

        public int _maxHealth = 150;
        public int _minHealth = 0;
        Boolean _attack1;
        Boolean _attack2;
        Boolean _attack3;
        Boolean _attack4;

        public void Init()
        {
            _healthNum = _maxHealth;
            _attack1 = false;
            _attack2 = false;
            _attack3 = false;
            _attack4 = false;
        }

        public int ReturnHealth()
        {
            return _healthNum;
        }

        public void CalculateEnemyAttack(InputHandler input)
        {
            if (input._APressed)
            {
                _enemyAttackAmount = CalculateEnemyAttack();
                _healthNum -= _enemyAttackAmount;
            }
        }

        public void CalculatePlayerAttack(InputHandler input)
        {
            if (input._APressed)
            {
                _playerAttackAmount = _attackNumber;
                _healthNum -= _playerAttackAmount;
            }
        }

        public int CalculateEnemyAttack()
        {
            int attackNum = GenFunctions.RandomNumber();
            if (attackNum == 0)
            {
                attackNum = _enemyAttack1;
                return attackNum;
            }
            if (attackNum == 1)
            {
                attackNum = _enemyAttack2;
                return attackNum;
            }
            if (attackNum == 2)
            {
                attackNum = _enemyAttack3;
                return attackNum;
            }
            else
            {
                attackNum = _enemyAttack4;
                return attackNum;
            }
        }

        public void ChangeAttackBooleans(int actualAttack, int attack1, int attack2, int attack3, int attack4)
        {
            if (actualAttack == attack1)
            {
                _attack1 = true;
                _attack2 = false;
                _attack3 = false;
                _attack4 = false;
            }
            else if (actualAttack == attack2)
            {
                _attack1 = false;
                _attack2 = true;
                _attack3 = false;
                _attack4 = false;
            }
            else if (actualAttack == attack3)
            {
                _attack1 = false;
                _attack2 = false;
                _attack3 = true;
                _attack4 = false;
            }
            else
            {
                _attack1 = false;
                _attack2 = false;
                _attack3 = false;
                _attack4 = true;
            }
        }
    }
}
