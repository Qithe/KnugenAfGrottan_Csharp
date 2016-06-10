using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace KnugenAfGrottan
{
    public class GameObjects
    {
        protected Texture2D texture;
        protected Vector2 vector;

        /// <summary>
		/// Initializes a new instance of the <see cref="Projrect1337.GameObjects"/> class.
		/// </summary>
		/// <param name="texture">Texture.</param>
		/// <param name="X">X.</param>
		/// <param name="Y">Y.</param>
		public GameObjects(Texture2D texture, float X, float Y)
        {
            this.texture = texture; //Textures
            this.vector.X = X;      //coordinates x-axle
            this.vector.Y = Y;      //coordinates y-axle
        }


        public virtual void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(texture, vector, Color.White);

        }

        //gets cooridinates and texture sizes
        public float X { get { return vector.X; } }
        public float Y { get { return vector.Y; } }
        public float Width { get { return texture.Width; } }
        public float Height { get { return texture.Height; } }
    }

    public class Text : GameObjects
    {
        public Text(Texture2D texture, float X, float Y) : base(texture, X, Y)
        {

        }
    }

    /// <summary>
    /// Creator Oscar. Moving objects.
    /// </summary>
    public class MovingObjects : GameObjects
    {
        protected Vector2 speed;
        protected bool isAlive;

        /// <summary>
        /// Initializes a new instance of the <see cref="Projrect1337.MovingObjects"/> class.
        /// </summary>
        /// <param name="texture">Texture.</param>
        /// <param name="X">X.</param>
        /// <param name="Y">Y.</param>
        /// <param name="speedX">Speed x.</param>
        /// <param name="speedY">Speed y.</param>
        public MovingObjects(Texture2D texture, float X, float Y, float speedX, float speedY, bool isAlive) : base(texture, X, Y)
        {
            this.speed.X = speedX;      //Speed x-axle
            this.speed.Y = speedY;      //Speed Y-axle
            this.isAlive = isAlive;     //used to kill objects
        }
        //Getts if object is alive, and also setts if the object is alive
        public bool IsAlive { get { return isAlive; } set { isAlive = value; } }
    }

    /// <summary>
    /// Physical object. Creator Oscar.
    /// </summary>
    abstract public class PhysicalObjects : MovingObjects
    {
        public PhysicalObjects(Texture2D texture, float X, float Y, float speedX, float speedY, bool isAlive) : base(texture, X, Y, speedX, speedY, isAlive)
        {

        }

        //Adds the possibility to check for collision
        public bool CheckCollision(PhysicalObjects other)
        {
            Rectangle myRect = new Rectangle(Convert.ToInt32(X), Convert.ToInt32(Y), Convert.ToInt32(Width), Convert.ToInt32(Height));
            Rectangle otherRect = new Rectangle(Convert.ToInt32(other.X), Convert.ToInt32(other.Y), Convert.ToInt32(other.Width), Convert.ToInt32(other.Height));
            return myRect.Intersects(otherRect);
        }
    }

    /// <summary>
    /// World player.
    /// </summary>
    abstract public class WorldObjects : PhysicalObjects
    {

        public WorldObjects(Texture2D texture, float X, float Y, float speedX, float speedY, bool isAlive) : base(texture, X, Y, speedX, speedY, isAlive)
        {

        }

    }

    /// <summary>
    /// CombatObject
    /// </summary>
    abstract public class CombatObject : MovingObjects
    {

        protected int health;
        protected int strength;
        protected int agility;
        protected int defence;

        public CombatObject(Texture2D texture, float X, float Y, float speedX, float speedY, bool isAlive, int health, int strength, int agility, int defence) : base(texture, X, Y, speedX, speedY, isAlive)
        {
            this.health = health;
            this.strength = strength;
            this.agility = agility;
            this.defence = defence;
        }

        public int Health { get { return health; } set { health = value; } }
        public int Strength { get { return strength; } set { strength = value; } }
        public int Agility { get { return agility; } set { agility = value; } }
        public int Defence { get { return defence; } set { defence = value; } }
    }
}
