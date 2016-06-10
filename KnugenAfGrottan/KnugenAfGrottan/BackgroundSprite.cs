using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KnugenAfGrottan
{
    public class BackgroundSprite : GameObjects
    {
        public BackgroundSprite(Texture2D texture, float X, float Y) : base(texture, X, Y)
        {
        }
        public void Update()
        {

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, vector, Color.White);
        }
    }
}

