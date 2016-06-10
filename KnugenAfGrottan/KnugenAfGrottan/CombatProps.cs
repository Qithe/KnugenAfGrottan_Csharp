using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KnugenAfGrottan
{
    public class CombatGrass : GameObjects
    {
        // Konstruktor
        public CombatGrass(Texture2D texture, float X, float Y) : base(texture, X, Y)
        {

        }

        /// <summary>
        /// Draws CombatGrass
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(texture, vector, Color.White);

        }
    }
}
