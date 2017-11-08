using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;


namespace FindMeOnlyForWin
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        Song stepSound;
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
            heroSpeed = 20f;
        }                                        


        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
 
            hero = new SpriteClass(GraphicsDevice, "Content/hero.png", 1f);
            this.stepSound = Content.Load<Song>("Content/Sounds/steps_with_echo");
            
            MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;
            
       
        }

        /**Настройки плеера для воспроиведения звука**/
        void MediaPlayer_MediaStateChanged(object sender, EventArgs e)
        {
            // 0.0f is silent, 1.0f is full volume
            MediaPlayer.IsRepeating = false;
            MediaPlayer.Volume = 0.5f;
            
        }
   
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
     
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

       
        /**Отрисовка сцены**/
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
          
            hero.Draw(spriteBatch);
            spriteBatch.End();

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
  
            
            if (state.IsKeyDown(Keys.Left)) 
                {   
                   hero.dX = heroSpeed * -1; 
                }
            else if (state.IsKeyDown(Keys.Right)) 
				{
					hero.dX = heroSpeed;
				}
                 else 
				 {
					hero.dX = 0;
				 } 
           
          if (state.IsKeyDown(Keys.Up)) 
                {   
                   hero.dY = heroSpeed * -1; 
                }
            else if (state.IsKeyDown(Keys.Down)) 
				{
					hero.dY = heroSpeed;
				}
                 else 
				 {
					hero.dY = 0;
				 } 

		//**звук шагов**//
		  if ((hero.dX!=0 || hero.dY!=0) && MediaPlayer.State.Equals(MediaState.Stopped)) MediaPlayer.Play(stepSound);
		  if(hero.dY == 0 && hero.dX == 0) MediaPlayer.Stop();
        }

    }
}
