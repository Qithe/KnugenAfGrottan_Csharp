using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KnugenAfGrottan
{
    /// <summary>
    /// World boulder. Creator Oscar.
    /// </summary>
    public class WorldBoulder : WorldObjects
    {
        public WorldBoulder(Texture2D texture, float X, float Y) : base(texture, X, Y, 0, 0, true)
        {

        }

        /// <summary>
        /// Draw the specified spriteBatch.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(texture, vector, Color.White);

        }
    }
}
