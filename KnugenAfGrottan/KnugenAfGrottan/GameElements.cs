using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KnugenAfGrottan
{
    class GameElements
    {
        //State
        public enum State { Menu, World, Combat, Quit };
        public static State currentState;

        //Textures
        static BackgroundSprite background;
        static Texture2D menuSprite;
        static Texture2D wolfSprite;
        static Texture2D trollKarlSprite;
        static Texture2D combatPlayerSprite;
        static Texture2D combatGrassSprite;
        static Texture2D combatGrassSpriteBig;

        //Text


        //Objects
        public static Menu menu;
        public static WorldPlayer worldPlayer;
        public static List<WorldBoulder> boulders;
        public static List<CombatEnemy> combatEnemyList;
        public static Wolf wolf;
        public static TrollKarl trollKarl;
        public static CombatPlayer combatPlayer;
        public static CombatGrass combatGrassPlayer;
        public static CombatGrass combatGrassEnemy;

        //Misc
        static Random random = new Random();
        public static int enemyEncountered;
        static Text prepareCombatText;
        static Text combatChoice;
        static Text bamPlayer;
        static Text bamEnemy;
        public static int attackChoice;
        public static int enemyAttackChoice;

        /// <summary>
        /// Gameelements Initilizer
        /// </summary>
        public static void Initilize()
        {
            combatEnemyList = new List<CombatEnemy>();
            playerStats = new int[5];
        }
        
        /// <summary>
        /// Loads the content
        /// </summary>
        /// <param name="content"></param>
        /// <param name="window"></param>
        public static void LoadContent (ContentManager content, GameWindow window)
        {
            Save.SaveFile(playerStats);

            background = new BackgroundSprite(content.Load<Texture2D>("images/world/backgrounds/BackgroundTiles"), 0, 0);
            menu = new Menu((int)State.Menu);
            menu.AddItem(content.Load<Texture2D>("images/Menu/MenuSprite/Menu"), (int)State.Menu);
            menu.AddItem(content.Load<Texture2D>("images/Menu/MenuSprite/NewGame"), (int)State.World);
            menu.AddItem(content.Load<Texture2D>("images/Menu/MenuSprite/SaveGame"), (int)State.Menu);
            menu.AddItem(content.Load<Texture2D>("images/Menu/MenuSprite/LoadGame"), (int)State.Menu);
            menu.AddItem(content.Load<Texture2D>("images/Menu/MenuSprite/Quit"), (int)State.Quit);

            combatPlayerSprite = content.Load<Texture2D>("images/Combat/PlayerSprite/Player");
            wolfSprite = content.Load<Texture2D>("images/Combat/Enemies/Wolf2");
            trollKarlSprite = content.Load<Texture2D>("images/Combat/Enemies/TrollKarl");

            worldPlayer = new WorldPlayer(content.Load<Texture2D>("images/world/player/Player"), 0, 0, 5f, 5f, true);
            combatPlayer = new CombatPlayer(combatPlayerSprite, 0, window.ClientBounds.Height - combatPlayerSprite.Height, 0, 0, true, 0);

            boulders = new List<WorldBoulder>();
            GenerateBoulders(content, window);

            prepareCombatText = new Text(content.Load<Texture2D>("images/world/text/EnterCombatText"), window.ClientBounds.Width / 4, window.ClientBounds.Height / 3);
            combatChoice = new Text(content.Load<Texture2D>("images/Combat/Text/CombatText"), window.ClientBounds.Width / 4, window.ClientBounds.Height / 3);
            bamEnemy = new Text(content.Load<Texture2D>("images/Combat/Text/BamEffect"), window.ClientBounds.Width - (trollKarlSprite.Width * 2), trollKarlSprite.Height / 3);
            bamPlayer = new Text(content.Load<Texture2D>("images/Combat/Text/BamEffect"), combatPlayerSprite.Width, window.ClientBounds.Height - ((combatPlayerSprite.Height / 3) * 2));

            combatGrassSpriteBig = content.Load<Texture2D>("images/Combat/Props/GrassBig");
            combatGrassSprite = content.Load<Texture2D>("images/Combat/Props/Grass");
            combatGrassPlayer = new CombatGrass(combatGrassSpriteBig, 0 - combatGrassSpriteBig.Width / 5, window.ClientBounds.Height - (combatGrassSpriteBig.Height / 3) * 2);
            combatGrassEnemy = new CombatGrass(combatGrassSprite, window.ClientBounds.Width - combatGrassSprite.Width, (trollKarlSprite.Height / 4) * 3);

            wolf = new Wolf(wolfSprite, window.ClientBounds.Width - wolfSprite.Width, 0, true);
            trollKarl = new TrollKarl(trollKarlSprite, window.ClientBounds.Width - wolfSprite.Width, 0, true);

            combatEnemyList.Add(trollKarl);
            combatEnemyList.Add(wolf);
        }

        /// <summary>
        /// Updates world
        /// </summary>
        /// <param name="content"></param>
        /// <param name="window"></param>
        /// <param name="gameTime"></param>
        /// <returns></returns>
        public static State UpdateWorld(ContentManager content, GameWindow window, GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (!worldPlayer.Encounter)
            {
                worldPlayer.Update(window, gameTime);
            }

            foreach (WorldBoulder wb in boulders)
                if (wb.CheckCollision(worldPlayer))
                {
                    if (keyboardState.IsKeyDown(Keys.Right))
                        worldPlayer.vectorX -= 5;
                    if (keyboardState.IsKeyDown(Keys.Left))
                        worldPlayer.vectorX += 5;
                    if (keyboardState.IsKeyDown(Keys.Down))
                        worldPlayer.vectorY -= 5;
                    if (keyboardState.IsKeyDown(Keys.Up))
                        worldPlayer.vectorY += 5;
                }
            // Checks if player has encountered an enemy
            if (worldPlayer.Encounter)
            {
                // Waits for player to be ready for cambat
                if (keyboardState.IsKeyDown(Keys.Enter))
                {
                    // Waits for 2 secounds
                    System.Threading.Thread.Sleep(2000);
                    // Resetes enocunter
                    worldPlayer.Encounter = false;
                    // Randoms an ecountered enemy
                    enemyEncountered = random.Next(0, 1);
                    // Returns gamestate to combat
                    return State.Combat;
                }
            }

            return State.World;
        }

        /// <summary>
        /// Draws the world
        /// </summary>
        /// <param name="spriteBatch"></param>
        public static void DrawWorld(SpriteBatch spriteBatch)
        {
            background.Draw(spriteBatch);
            worldPlayer.Draw(spriteBatch);

            foreach (WorldBoulder wb in boulders)
                wb.Draw(spriteBatch);

            // If encounter then draws text preparing player for combat
            if (worldPlayer.Encounter)
            {
                prepareCombatText.Draw(spriteBatch);
            }
        }

        /// <summary>
        /// Update the combat
        /// </summary>
        /// <param name="content"></param>
        /// <param name="window"></param>
        /// <param name="gameTime"></param>
        /// <returns></returns>
        public static State UpdateCombat(ContentManager content, GameWindow window, GameTime gameTime)
        {

            // Resets attack choice for player
            attackChoice = 0;

            // Checks what player wants to do in combat
            KeyboardState keyboardState = Keyboard.GetState();
            // Sets players choice to attack
            if (keyboardState.IsKeyDown(Keys.D1))
            {
                attackChoice = 1;
                System.Threading.Thread.Sleep(2000);
            }
            // Sets players choice to defend
            else if (keyboardState.IsKeyDown(Keys.D2))
            {
                attackChoice = 2;
                System.Threading.Thread.Sleep(2000);
            }

            // Randoms enemys combat choice
            enemyAttackChoice = random.Next(0, 2);

            // Checks if player is faster/has more agility than enemy then player attacks first
            if (combatPlayer.Agility >= combatEnemyList[enemyEncountered].Agility)
            {
                // If player and enemy choose to attack
                if (attackChoice == 1 && enemyAttackChoice == 0)
                {
                    // Deal damage to Enemy
                    combatEnemyList[enemyEncountered].Health -= combatPlayer.Strength;

                    // Deal damage to player if enemy is still alive
                    if (combatEnemyList[enemyEncountered].IsAlive)
                    {
                        combatPlayer.Health -= combatEnemyList[enemyEncountered].Strength;
                    }
                }
                // If player choose to defend and enemy attacks
                else if (attackChoice == 2 && enemyAttackChoice == 0)
                {

                    // Deals damage to player minus his defence
                    combatPlayer.Health -= (combatEnemyList[enemyEncountered].Strength - combatPlayer.Defence);
                }
            }
            // Checks if enemy is faster/has more agility than player then enemy attacks first
            else if (combatPlayer.Agility < combatEnemyList[enemyEncountered].Agility)
            {
                // If both choose to attack
                if (attackChoice == 1 && enemyAttackChoice == 0)
                {
                    // Deals damage to player
                    combatPlayer.Health -= combatEnemyList[enemyEncountered].Strength;

                    // Deals damage to enemy if player is still alive
                    if (combatPlayer.IsAlive)
                    {
                        combatEnemyList[enemyEncountered].Health -= combatPlayer.Strength;
                    }
                }
                // If player choose to defend and enemy attakcs
                else if (attackChoice == 2 && enemyAttackChoice == 0)
                {

                    // Deals damage to player minus his defence
                    combatPlayer.Health -= (combatEnemyList[enemyEncountered].Strength - combatPlayer.Defence);
                }
            }
            // If player attacks and enemy defends
            if (attackChoice == 1 && enemyAttackChoice == 1)
            {
                // Deals damage to enemy minus his defence
                combatEnemyList[enemyEncountered].Health -= (combatPlayer.Strength - combatEnemyList[enemyEncountered].Defence);
            }

            // Checks if player is alive
            if (combatPlayer.Health <= 0)
            {
                combatPlayer.IsAlive = false;
            }
            // Checks if enemy is alive
            if (combatEnemyList[enemyEncountered].Health <= 0)
            {
                combatEnemyList[enemyEncountered].IsAlive = false;
            }

            // Checks if anyone is dead
            if (!combatPlayer.IsAlive || !wolf.IsAlive || !trollKarl.IsAlive)
            {

                // Sends stats for player to savefile array
                playerStats[0] = combatPlayer.Health;
                playerStats[1] = combatPlayer.Strength;
                playerStats[2] = combatPlayer.Agility;
                playerStats[3] = combatPlayer.Defence;
                playerStats[4] = combatPlayer.PlayerXp;

                // Revives enemies
                wolf.Health = 20;
                wolf.IsAlive = true;
                trollKarl.Health = 16;
                trollKarl.IsAlive = true;

                // Returns state to world
                return State.World;
            }

            return State.Combat;
        }

        /// <summary>
        /// Draws the combat
        /// </summary>
        /// <param name="spriteBatch"></param>
        public static void DrawCombat(SpriteBatch spriteBatch)
        {
            // Draws the ground for player and enemy
            combatGrassPlayer.Draw(spriteBatch);
            combatGrassEnemy.Draw(spriteBatch);
            // Checks which enemy is encountered and then draws it
            if (combatEnemyList[enemyEncountered] == trollKarl)
            {
                trollKarl.Draw(spriteBatch);
            }
            if (combatEnemyList[enemyEncountered] == wolf)
            {
                wolf.Draw(spriteBatch);
            }
            // Draws attackeffects on enemy and player if they are attacked and the other is alive
            if (attackChoice == 1 && combatPlayer.IsAlive)
            {
                bamEnemy.Draw(spriteBatch);
            }
            if (enemyAttackChoice == 1 && attackChoice != 0 && combatEnemyList[enemyEncountered].IsAlive)
            {
                bamPlayer.Draw(spriteBatch);
            }
            // Draws the player in combat
            combatPlayer.Draw(spriteBatch);

            /*System.Threading.Thread.Sleep(500);
			if (combatPlayer.Agility >= combatEnemyList [enemyEncountered].Agility) {
				if (attackChoice == 1 && enemyAttackChoice == 0) {
					bamEnemy.Draw (spriteBatch);
					System.Threading.Thread.Sleep(500);

					if (combatEnemyList [enemyEncountered].IsAlive) {
						bamPlayer.Draw (spriteBatch);
						System.Threading.Thread.Sleep(500);
					}
				} else if (attackChoice == 2 && enemyAttackChoice == 0) {
					bamPlayer.Draw (spriteBatch);
					System.Threading.Thread.Sleep(500);
				}
			}
			else if (combatPlayer.Agility <= combatEnemyList [enemyEncountered].Agility) {
				if (attackChoice == 1 && enemyAttackChoice == 0) {
					bamPlayer.Draw (spriteBatch);
					System.Threading.Thread.Sleep(500);

					if (combatPlayer.IsAlive) {
						bamEnemy.Draw (spriteBatch);
						System.Threading.Thread.Sleep(500);
					}
				} else if (attackChoice == 2 && enemyAttackChoice == 0) {

					bamPlayer.Draw (spriteBatch);
					System.Threading.Thread.Sleep(500);
				}
			}
			else if (attackChoice == 1 && enemyAttackChoice == 1) {
				bamEnemy.Draw (spriteBatch);
				System.Threading.Thread.Sleep(500);
			}*/
            // Ritar ut valen i combat för player
            combatChoice.Draw(spriteBatch);
        }

        /// <summary>
		/// Updates the menu.
		/// </summary>
		/// <returns>The menu.</returns>
		/// <param name="gameTime">Game time.</param>
		public static State UpdateMenu(GameTime gameTime)
        {
            return (State)menu.Update(gameTime);
        }

        /// <summary>
        /// Draws the menu.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch.</param>
        public static void DrawMenu(SpriteBatch spriteBatch)
        {
            background.Draw(spriteBatch);
            menu.Draw(spriteBatch);
        }

        /// <summary>
        /// Generates the boulders.  Creator Oscar.
        /// </summary>
        /// <param name="content">Content.</param>
        /// <param name="window">Window.</param>
        private static void GenerateBoulders(ContentManager content, GameWindow window)
        {
            Random rnd = new Random();

            //Setts the texture of boulder
            Texture2D tmpSprite = content.Load<Texture2D>("images/world/props/Stone");

            //Creates the boulders and placing them in a list
            for (int i = 0; i < 8; i++)
            {
                int rndX = rnd.Next(128, window.ClientBounds.Width - tmpSprite.Width - 160);
                int rndY = rnd.Next(0, window.ClientBounds.Height - tmpSprite.Height);
                WorldBoulder temp = new WorldBoulder(tmpSprite, rndX, rndY);
                boulders.Add(temp);
            }
        }
    }
}
