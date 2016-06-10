using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace KnugenAfGrottan
{
    public class WorldPlayer : WorldObjects
    {
        bool encounter;

        public WorldPlayer(Texture2D texture, float X, float Y, float speedX, float speedY, bool isAlive) : base(texture, X, Y, speedX,speedY, isAlive)
		{
            this.encounter = encounter;
        }

        //Adds Getters and setters to be able to use these for combat
        public bool Encounter { get { return encounter; } set { encounter = value; } }
        public float speedX { get { return speed.X; } set { speed.X = value; } }
        public float speedY { get { return speed.Y; } set { speed.Y = value; } }
        public float vectorX { get { return vector.X; } set { vector.X = value; } }
        public float vectorY { get { return vector.Y; } set { vector.Y = value; } }

        /// <summary>
        /// Update the specified window and gameTime.
        /// </summary>
        /// <param name="window">Window.</param>
        /// <param name="gameTime">Game time.</param>
        public void Update(GameWindow window, GameTime gameTime)
        {

            Random rnd = new Random();
            KeyboardState keyboardState = Keyboard.GetState();

            //runs check of X-movement, and sets a random to tringer an encounter
            if (vector.X <= window.ClientBounds.Width - texture.Width - 160)
            {
                if (keyboardState.IsKeyDown(Keys.Right))
                {
                    vector.X += speed.X;
                    if (rnd.Next(500) <= 1)
                        encounter = true;
                }
                if (keyboardState.IsKeyDown(Keys.Left))
                {
                    vector.X -= speed.X;
                    if (rnd.Next(500) <= 1)
                        encounter = true;
                }
            }

            //runs checo of X-movement, and sets a random to tringer an encounter
            if (vector.Y <= window.ClientBounds.Width - texture.Width && vector.Y >= 0)
            {
                if (keyboardState.IsKeyDown(Keys.Down))
                {
                    vector.Y += speed.Y;
                    if (rnd.Next(500) <= 1)
                        encounter = true;
                }
                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    vector.Y -= speed.Y;
                    if (rnd.Next(500) <= 1)
                        encounter = true;
                }
            }

            //Makes sure player does not end up outside the game window
            if (vector.X < 0)
                vector.X = 0;
            if (vector.X > window.ClientBounds.Width - texture.Width - 160)
                vector.X = window.ClientBounds.Width - texture.Width - 160;
            if (vector.Y < 0)
                vector.Y = 0;
            if (vector.Y > window.ClientBounds.Height - texture.Height)
                vector.Y = window.ClientBounds.Height - texture.Height;

        }


        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(texture, vector, Color.White);

        }
    }

    public class CombatPlayer : CombatObject
    {

        // Gives the player a experience stat
        protected int playerXp;

        // Konstruktor
        public CombatPlayer(Texture2D texture, float X, float Y, float speedX, float speedY, bool isAlive, int playerXp) : base(texture, X, Y, speedX, speedY, isAlive, 30, 5, 5, 5)
        {
            this.playerXp = playerXp;
        }

        // Getter and setter for playerExperience
        public int PlayerXp { get { return playerXp; } set { playerXp = value; } }

        // Update function for combatPlayer
        public void Update(GameWindow window, GameTime gameTime)
        {

        }

        // Draw function for combatPlayer
        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(texture, vector, Color.White);

        }
    }
}
