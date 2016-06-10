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

namespace KnugenAfGrottan
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            GameElements.Initialize();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            GameElements.LoadContent(Content, Window);
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // For Mobile devices, this logic will close the Game when the Back button is pressed
            // Exit() is obsolete on iOS
#if !__IOS__
            if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape)) && GameElements.currentState != GameElements.State.Combat)
            {
                GameElements.currentState = GameElements.State.Menu;
            }
#endif

            switch (GameElements.currentState)
            {
                case GameElements.State.World:
                    GameElements.currentState = GameElements.UpdateWorld(Content, Window, gameTime);
                    break;
                case GameElements.State.Combat:
                    GameElements.currentState = GameElements.UpdateCombat(Content, Window, gameTime);
                    break;
                case GameElements.State.Quit:
                    this.Exit();
                    break;
                default:
                    GameElements.currentState = GameElements.UpdateMenu(gameTime);
                    break;
            }

            /*if (GameElements.combat) {
				GameElements.currentState = GameElements.State.Combat;
			}
			else {
				GameElements.currentState = GameElements.State.World;
			}*/

            base.Update(gameTime);
        }


        protected override void Draw (GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            switch (GameElements.currentState)
            {
                case GameElements.State.World:
                    GameElements.DrawWorld(spriteBatch);
                    break;
                case GameElements.State.Combat:
                    GameElements.DrawCombat(spriteBatch);
                    break;
                case GameElements.State.Quit:
                    this.Exit();
                    break;
                default:
                    GameElements.DrawMenu(spriteBatch);
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
