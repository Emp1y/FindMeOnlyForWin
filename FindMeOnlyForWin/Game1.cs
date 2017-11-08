using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;


namespace FindMeOnlyForWin
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteClass hero;

        float screenWidth;
        float screenHeight;

        bool gameStarted;
        bool spaceDown;
        bool gameOver;
        float heroSpeed;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
          //  Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>



        protected override void Initialize()
        {
            base.Initialize();
            
            // graphics.ToggleFullScreen();  //Fullscreen mode
            
            if (graphics.IsFullScreen) Console.WriteLine("\n***POLNII***\n");
            else  Console.WriteLine("\n***NE POLNII***\n");

            this.IsMouseVisible = false; //видимость курсора в игре

            screenHeight = 600;
            screenWidth = 800;

            gameStarted = false;
            spaceDown = false;
            gameOver = false;
            heroSpeed = 100f;
        }                                        

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
 
            hero = new SpriteClass(GraphicsDevice, "Content/hero.png", 1f);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            KeyboardHandler(); // Включаем отслеживание клавиш
            
            // TODO: Add your update logic here
            hero.Update(elapsedTime);
            base.Update(gameTime);

            /* границы экрана
            if (dino.x > screenWidth - dino.texture.Width / 2)
            {
                dino.x = screenWidth - dino.texture.Width / 2;
                dino.dX = 0;
            }
            if (dino.x < 0 + dino.texture.Width / 2)
            {
                dino.x = 0 + dino.texture.Width / 2;
                dino.dX = 0;
            }
            */

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /**Отрисовка сцены**/
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
          
            hero.Draw(spriteBatch);
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        public void StartGame()
        {
          hero.x = screenWidth / 2;
          hero.y = screenHeight / 2;
        }

        /**Ввод с клавиатуры**/
        void KeyboardHandler()
        {
            KeyboardState state = Keyboard.GetState();

            // Quit the game if Escape is pressed.
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Start the game if Space is pressed.
            if (!gameStarted)
            {
                if (state.IsKeyDown(Keys.Space))
                {
                    StartGame();
                    gameStarted = true;
                    
                    gameOver = false;
                }
                return;
            }
  

            // Handle left and right
            if (state.IsKeyDown(Keys.Left)) hero.dX = heroSpeed * -1;
            else if (state.IsKeyDown(Keys.Right)) hero.dX = heroSpeed;
                    else hero.dX = 0;
            if (state.IsKeyDown(Keys.Up)) hero.dY = heroSpeed * -1;
            else if (state.IsKeyDown(Keys.Down)) hero.dY = heroSpeed;
                  else hero.dY = 0;
        }

    }
}
