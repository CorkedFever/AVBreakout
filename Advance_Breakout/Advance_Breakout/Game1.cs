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

namespace Advance_Breakout
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        GraphicsDevice device;
        SpriteBatch spriteBatch;
        SpriteManager spriteManager;
        Texture2D backgroundTexture;
        int screenWidth, screenHeight;
        AutomatedSprite titleSprite;

        //Sound Effects

        //Background Music
        SoundEffect paddleHit, brickBurst1, brickBurst2, ballServe;
        Song backgroundMusic;
        public Game1()
        {

            graphics = new GraphicsDeviceManager(this);
            spriteManager = new SpriteManager(this);
            Components.Add(spriteManager);
            Content.RootDirectory = "Content";
            Window.Title = "ADVANCE BREAKOUT";

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            device = graphics.GraphicsDevice;
            // TODO: use this.Content to load your game content here     
            
            //load SoundEffects
            paddleHit = Content.Load<SoundEffect>(@"SoundEffects\paddleBallHit");
            ballServe = Content.Load<SoundEffect>(@"SoundEffects\ballserve");
            brickBurst1 = Content.Load<SoundEffect>(@"SoundEffects\pop1");
            brickBurst2 = Content.Load<SoundEffect>(@"SoundEffects\pop2");


            //background loading
            backgroundTexture = Content.Load<Texture2D>("background");
            screenWidth = device.PresentationParameters.BackBufferWidth;
            screenHeight = device.PresentationParameters.BackBufferHeight;

            //music loading
            backgroundMusic = Content.Load<Song>(@"Music/Dustin's Song");

            MediaPlayer.Play(backgroundMusic);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.3f;
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();



            //KeyboardState keyState = Keyboard.GetState();
            //if (keyState.IsKeyDown(Keys.S))
            //{
            //    spriteManager.setGameStart(true);
            //}

            //spriteManager.Update(gameTime);

            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            DrawBackground();
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawBackground()
        {
            Rectangle screenRectangle = new Rectangle(0, 0, screenWidth, screenHeight);
            spriteBatch.Draw(backgroundTexture, screenRectangle, Color.White);
            //spriteBatch.Draw(foregroundTexture, screenRectangle, Color.White);
        }

        public void PlaySounds(string soundName)
        {
            if (soundName == "paddleHit")
            {

                paddleHit.Play();
            }
            if (soundName == "brickBurst1")
            {
                brickBurst1.Play();
            }
            if (soundName == "brickBurst2")
            {
                brickBurst2.Play();
            }
            if (soundName == "ballServe")
            {
                ballServe.Play();
            }
        }
    }
}
