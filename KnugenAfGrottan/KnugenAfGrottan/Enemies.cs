using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KnugenAfGrottan
{
    public class Wolf : CombatEnemy
    {
        public bool attackCondition = false;

        public Wolf(Texture2D texture, float X, float Y, bool isAlive) : base(texture, X, Y, 0, 0, isAlive, 20, 4, 6, 4)
        {

        }

        public void Update(GameWindow window, GameTime gameTime)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(texture, vector, Color.White);

        }
    }

    public class TrollKarl : CombatEnemy
    {
        public TrollKarl(Texture2D texture, float X, float Y, bool isAlive) : base(texture, X, Y, 0, 0, isAlive, 16, 6, 4, 6)
        {

        }

        public void Update(GameWindow window, GameTime gameTime)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(texture, vector, Color.White);

        }
    }
}
