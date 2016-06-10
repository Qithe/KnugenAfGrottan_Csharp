using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KnugenAfGrottan
{
    public class CombatEnemy : CombatObject
    {

        public CombatEnemy(Texture2D texture, float X, float Y, float speedX, float speedY, bool isAlive, int health, int strength, int agility, int defence) : base(texture, X, Y, speedX, speedY, isAlive, health, strength, agility, defence)
        {

        }
    }
}