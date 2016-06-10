using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KnugenAfGrottan
{
    /// <summary>
    /// Menu. Creator Oscar.
    /// </summary>
    public class Menu
    {
        List<MenuItem> menu;    //List of menuItems
        int selected = 0;       //list choise
        float currentHeight = 0;
        double lastChange = 0;

        int defaultMenuState;   //Deafult stae of menu

        /// <summary>
        /// Initializes a new instance of the <see cref="Projrect1337.Menu"/> class.
        /// </summary>
        /// <param name="defaultMenuState">Default menu state.</param>
        public Menu(int defaultMenuState)
        {
            menu = new List<MenuItem>();
            this.defaultMenuState = defaultMenuState;
        }

        /// <summary>
        /// Adds the item.
        /// </summary>
        /// <param name="itemTexture">Item texture.</param>
        /// <param name="state">State.</param>
        public void AddItem(Texture2D itemTexture, int state)
        {
            //Sets hight and width
            float X = 0;
            float Y = 0 + currentHeight;

            //sets the heigth to the heingt of the item
            currentHeight += itemTexture.Height + 20;

            //Adds temp object and place it in the list
            MenuItem temp = new MenuItem(itemTexture, new Vector2(X, Y), state);
            menu.Add(temp);
        }

        /// <summary>
        /// Update the specified gameTime.
        /// </summary>
        /// <param name="gameTime">Game time.</param>
        public int Update(GameTime gameTime)
        {
            //Reads keys
            KeyboardState keyboardState = Keyboard.GetState();

            //checks time sence last change
            if (lastChange + 130 < gameTime.TotalGameTime.TotalMilliseconds)
            {

                if (keyboardState.IsKeyDown(Keys.Down))
                {
                    selected++;
                    //if menu is out of range, sett to base value
                    if (selected > menu.Count - 1)
                        selected = 0;       //base value
                }
                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    selected--;
                    //if menu is out of range, sett to base value
                    if (selected < 0)
                        selected = menu.Count - 1;      //base value
                }
                //Sets game time
                lastChange = gameTime.TotalGameTime.TotalMilliseconds;
            }
            if (keyboardState.IsKeyDown(Keys.Enter))
                return menu[selected].State;
            return defaultMenuState;
        }

        /// <summary>
        /// Draw the specified spriteBatch.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < menu.Count; i++)
            {
                if (i == selected)
                    spriteBatch.Draw(menu[i].Texture, menu[i].Position, Color.RosyBrown);
                else
                    spriteBatch.Draw(menu[i].Texture, menu[i].Position, Color.White);
            }
        }

    }

    /// <summary>
    /// Menu item. Creator Oscar
    /// </summary>
    class MenuItem
    {
        Texture2D texture;
        Vector2 position;
        int currentState;

        /// <summary>
        /// Initializes a new instance of the <see cref="Projrect1337.MenuItem"/> class.
        /// </summary>
        /// <param name="Texture">Texture.</param>
        /// <param name="position">Position.</param>
        /// <param name="currentState">Current state.</param>
        public MenuItem(Texture2D texture, Vector2 position, int currentState)
        {
            this.texture = texture;
            this.position = position;
            this.currentState = currentState;
        }

        //Getters
        public Texture2D Texture { get { return texture; } }
        public Vector2 Position { get { return position; } }
        public int State { get { return currentState; } }
    }

}