using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Advance_Breakout
{
    class SpriteManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        //defines state of the game, either shows title screen, end screen, or Start of the game.
        enum GameState
        {
            TitleScreen = 0,
            GameStarted,
            GameEnded,
        }


        //Variables needed
        //Starting State
        GameState currentState = GameState.TitleScreen;

        //Counters for Score, Lives, and Ammo
        int Score = 0, Lives = 6, Ammo = 0;

        //Start the first level boolean
        bool startLevels = true;

        //In Game Font
        SpriteFont ScoreFont;

        //Title Screen Sprite, must be automated sprite because Sprite class is abstract
        AutomatedSprite titleScreen;

        //The Bullet List on the screen
        List<AutomatedSprite> bullets = new List<AutomatedSprite>();

        //Used for Drawing
        SpriteBatch spriteBatch;

        //Ball List
        List<BallSprite> ball = new List<BallSprite>();

        //User Sprite aka paddle
        UserControlledSprite player;

        //Level stuff
        Level Level1, Level2, Level3, Level4, Level5;
        List<Sprite> Level1_List = new List<Sprite>(),
            Level2_List = new List<Sprite>(),
            Level3_List = new List<Sprite>(),
            Level4_List = new List<Sprite>(),
            Level5_List = new List<Sprite>();
        static string text1 = @"Content\Levels\Level1.txt",
            text2 = @"Content\Levels\Level2.txt",
            text3 = @"Content\Levels\Level3.txt",
            text4 = @"Content\Levels\Level4.txt",
            text5 = @"Content\Levels\Level5.txt";

        string score_text, life_text, currentLevel_text = "";


        //Controller Stuff
        KeyboardState oldState = Keyboard.GetState();

        public SpriteManager(Game game)
            : base(game)
        {
            Level1 = new Level(game);
            Level2 = new Level(game);
            Level3 = new Level(game);
            Level4 = new Level(game);
            Level5 = new Level(game);

        }


        public override void Initialize()
        {
            oldState = Keyboard.GetState();

            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice); 
            
            player = new UserControlledSprite(Game.Content.Load<Texture2D>(@"Sprites/paddel"), new Vector2(200, 400), new Point(64, 8), 0, new Point(0, 0), new Point(0, 0), Vector2.Zero);

            //make score sprite font
            ScoreFont = Game.Content.Load<SpriteFont>(@"Fonts/Score");
            titleScreen = new AutomatedSprite(Game.Content.Load<Texture2D>(@"Sprites/Logo"), new Vector2(25, 0), new Point(761, 292), 0, new Point(0, 0), new Point(0, 0), Vector2.Zero);
            //Get Level's from Text Files
            Level1.setLevel(text1);
            Level2.setLevel(text2);
            Level3.setLevel(text3);
            Level4.setLevel(text4);
            Level5.setLevel(text5);

            //Get the Generated List of Sprites from the Level ADT
            Level1_List = Level1.getLevel();
            Level2_List = Level2.getLevel();
            Level3_List = Level3.getLevel();
            Level4_List = Level4.getLevel();
            Level5_List = Level5.getLevel();

            base.LoadContent();
        }


        public override void Update(GameTime gameTime)
        {            
            
            //Control for starting and ending the game
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.S) && currentState != GameState.GameEnded)
            {
                currentState = GameState.GameStarted;

            }
            if (keyState.IsKeyDown(Keys.Escape) || Lives <= 0 || numOfDestroyableBlocks(Level5_List) <= 0)
            {
                currentState = GameState.GameEnded;
            }


            //Execution of Game if Started
            if (currentState == GameState.GameStarted)
            {
            player.Update(gameTime, Game.Window.ClientBounds);
            
            foreach (BallSprite A in ball)
            {
                A.Update(gameTime, Game.Window.ClientBounds);
            }

            foreach (AutomatedSprite B in bullets)
            {
                B.Update(gameTime, Game.Window.ClientBounds);
            }

            KeyboardState keyPress = Keyboard.GetState();
            if (keyPress.IsKeyDown(Keys.Space) &&(ball.Count==0) && (Lives > 0))
            {
                addBall();
            }

            ballManager(ball);
            bulletDeleter();


            //Fire Bullets out of paddle
            KeyboardState keyFire = Keyboard.GetState();
            if (keyFire.IsKeyDown(Keys.LeftControl) && bullets.Count <2 && Ammo >0)
            {
                if (!oldState.IsKeyDown(Keys.LeftControl))
                {
                    {
                        addBullet();
                    }
                }
            }
            keyFire = oldState;
            //Ball Interaction with Player
            foreach (BallSprite A in ball)
            {
                if (A.collisionRect.Intersects(player.collisionRect))
                {
                    if (A.collisionRect.Intersects(player.collisionRect))
                    {
                        ((Game1)Game).PlaySounds("brickBurst2");
                        A.collisionHandler(player);

                    }

                }
            }


            //Ball interaction with Levels
            if (Level1.getLevelStatus() == true)
            {
                levelManager(Level1_List);
                bulletManager(Level1_List);
                currentLevel_text = "Level 1";
            }
            else if (Level2.getLevelStatus() == true)
            {
                levelManager(Level2_List);
                bulletManager(Level2_List);
                currentLevel_text = "Level 2";
            }
            else if (Level3.getLevelStatus() == true)
            {
                levelManager(Level3_List);
                bulletManager(Level3_List);
                currentLevel_text = "Level 3";
            }
            else if (Level4.getLevelStatus() == true)
            {
                levelManager(Level4_List);
                bulletManager(Level4_List);
                currentLevel_text = "Level 4";
            }
            else if (Level5.getLevelStatus() == true)
            {
                levelManager(Level5_List);
                bulletManager(Level5_List);
                currentLevel_text = "Level 5";
            }

            //Switch Level when ball destroys all the blocks

                levelSwitch();
            
            //Displaying Score

            score_text = "Score: " + Score.ToString();
            score_text = score_text + " X " + ball.Count.ToString();
            life_text = "Lives: " + Lives.ToString();


            }
            //makes it so that the player cannot do anything at all
            //End execution of game
            else if (currentState == GameState.GameEnded)
            {
                ball.RemoveRange(0, ball.Count); //removes every ball in the game so
                Lives = 0;
                Ammo = 0;
                if (keyState.IsKeyDown(Keys.Enter))
                {
                    Game.Exit();
                }


            }
            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            if(currentState == GameState.TitleScreen)
            {
                //Draw Title Screen With Instructions
                spriteBatch.Begin();
                titleScreen.Draw(gameTime, spriteBatch);
                spriteBatch.DrawString(ScoreFont, "Press 'S' to start game", new Vector2(250, 400), Color.White);
                spriteBatch.End();
            }
            else if (currentState == GameState.GameStarted)
            {
                spriteBatch.Begin();

                player.Draw(gameTime, spriteBatch);

                foreach (BallSprite A in ball)
                {
                    A.Draw(gameTime, spriteBatch);
                }
                foreach (AutomatedSprite shot in bullets)
                {
                    shot.Draw(gameTime, spriteBatch);
                }
                //Draw Currently Shown Level
                if (Level1.getLevelStatus() == true)
                {
                    foreach (Sprite a in Level1_List)
                    {
                        a.Draw(gameTime, spriteBatch);
                    }
                }
                else if (Level2.getLevelStatus() == true)
                {
                    foreach (Sprite b in Level2_List)
                    {
                        b.Draw(gameTime, spriteBatch);
                    }
                }
                else if (Level3.getLevelStatus() == true)
                {
                    foreach (Sprite c in Level3_List)
                    {
                        c.Draw(gameTime, spriteBatch);
                    }
                }
                else if (Level4.getLevelStatus() == true)
                {
                    foreach (Sprite d in Level4_List)
                    {
                        d.Draw(gameTime, spriteBatch);
                    }
                }
                else if (Level5.getLevelStatus() == true)
                {
                    foreach (Sprite e in Level5_List)
                    {
                        e.Draw(gameTime, spriteBatch);
                    }
                }

                spriteBatch.DrawString(ScoreFont, score_text, new Vector2(0, 0), Color.White);
                spriteBatch.DrawString(ScoreFont, life_text, new Vector2(200, 0), Color.White);
                spriteBatch.DrawString(ScoreFont, currentLevel_text, new Vector2(400, 0), Color.White);
                spriteBatch.DrawString(ScoreFont, "Ammo: "+Ammo.ToString(), new Vector2(550,0), Color.White);
                spriteBatch.End();

            }
            else if (currentState == GameState.GameEnded)
            {
                spriteBatch.Begin();

                spriteBatch.DrawString(ScoreFont, "Game Over", new Vector2(350, 250), Color.White);
                spriteBatch.DrawString(ScoreFont, "Total Score: " + Score.ToString(), new Vector2(330, 270), Color.White);
                spriteBatch.DrawString(ScoreFont, "Press 'Enter' to Exit Game", new Vector2(275, 290), Color.White);
                spriteBatch.End();
            }
            base.Draw(gameTime);

        }

        //does handling for the current level present
        private void levelManager(List<Sprite> LIST_OF_CURRENT_LEVEL)
        {
            for (int i = 0; i < LIST_OF_CURRENT_LEVEL.Count; ++i)
            {
                Sprite Z = LIST_OF_CURRENT_LEVEL[i];

                for(int j = 0; j < ball.Count; ++j)
                {
                    BallSprite A = ball[j];
                    if ((A.collisionRect.Intersects(Z.collisionRect)) && (LIST_OF_CURRENT_LEVEL[i] != null))
                    {
                       

                        if (Z.getType() == "NORMAL")
                        {
                            A.collisionHandler(Z);
                            LIST_OF_CURRENT_LEVEL.RemoveAt(i);
                            ((Game1)Game).PlaySounds("brickBurst1");
                            Score = Score + (10 * ball.Count);
                        }
                        if (Z.getType() == "MULTIBALL")
                        {
                            A.collisionHandler(Z);
                            LIST_OF_CURRENT_LEVEL.RemoveAt(i);
                            ((Game1)Game).PlaySounds("brickBurst1");
                            addBall();
                            Score = Score + (20* ball.Count);
                        }
                        if (Z.getType() == "INDESTRUCTABLE")
                        {
                            A.collisionHandler(Z);
                        }
                        if (Z.getType() == "QUESTION")
                        {
                            //sets randomness to the game
                            //add new ball
                            //add 1 life
                            //add ammo (future implementation)
                            randomBlockEffect();
                            A.collisionHandler(Z);
                            ((Game1)Game).PlaySounds("brickBurst1");
                            LIST_OF_CURRENT_LEVEL.RemoveAt(i);
                            Score = Score + (50 * ball.Count);
                        }
                        if (Z.getType() == "LIFE")
                        {
                            A.collisionHandler(Z);
                            LIST_OF_CURRENT_LEVEL.RemoveAt(i);
                            ((Game1)Game).PlaySounds("brickBurst1");
                            addLife(1);
                            Score = Score + (20 * ball.Count);
                        }
                        if (Z.getType() == "AMMO")
                        {
                            A.collisionHandler(Z);
                            LIST_OF_CURRENT_LEVEL.RemoveAt(i);
                            ((Game1)Game).PlaySounds("brickBurst1");
                            Ammo = Ammo + 50; //add 50 bullets to the ammo count
                            Score = Score + (20 * ball.Count);
                        }

                    }

                    
                }
            }

        }
        //handles bullet block interaction, deletes bullet afterwards
        private void bulletManager(List<Sprite> LIST_OF_CURRENT_LEVEL)
        {
            for (int i = 0; i < LIST_OF_CURRENT_LEVEL.Count; ++i)
            {
                Sprite Z = LIST_OF_CURRENT_LEVEL[i];
                for (int r = 0; r < bullets.Count; ++r)
                {
                    AutomatedSprite B = bullets[r];
                    if ((B.collisionRect.Intersects(Z.collisionRect)) && (LIST_OF_CURRENT_LEVEL[i] != null))
                    {


                        if (Z.getType() == "NORMAL")
                        {
                            LIST_OF_CURRENT_LEVEL.RemoveAt(i);
                            ((Game1)Game).PlaySounds("brickBurst1");
                            Score = Score + 10;
                        }
                        if (Z.getType() == "MULTIBALL")
                        {
                            LIST_OF_CURRENT_LEVEL.RemoveAt(i);
                            ((Game1)Game).PlaySounds("brickBurst1");
                            addBall();
                            Score = Score + 20;
                        }
                        if (Z.getType() == "INDESTRUCTABLE")
                        {
                            
                        }
                        if (Z.getType() == "QUESTION")
                        {
                            //sets randomness to the game
                            //add new ball
                            //add 1 life
                            //add ammo (future implementation)
                            randomBlockEffect();

                            ((Game1)Game).PlaySounds("brickBurst1");
                            LIST_OF_CURRENT_LEVEL.RemoveAt(i);
                            Score = Score + 50;
                        }
                        if (Z.getType() == "LIFE")
                        {

                            LIST_OF_CURRENT_LEVEL.RemoveAt(i);
                            ((Game1)Game).PlaySounds("brickBurst1");
                            addLife(1);
                            Score = Score + 20;
                        }
                        if (Z.getType() == "AMMO")
                        {

                            LIST_OF_CURRENT_LEVEL.RemoveAt(i);
                            ((Game1)Game).PlaySounds("brickBurst1");
                            Ammo = Ammo + 50; //add 50 bullets to the ammo count
                            Score = Score + 20 ;
                        }

                        bullets.RemoveAt(r);
                    }
                    
                }
            }
        }
        private void bulletDeleter()
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                Sprite A = bullets[i];
                if (A.currentPosition.Y < 0)
                {
                    bullets.RemoveAt(i);
                }
            }
        }
        //Handles the number of destroyable blocks, so that we can transition to the next screen
        //always a positive number or 0.
        private int numOfDestroyableBlocks(List<Sprite> LIST_OF_CURRENT_LEVEL)
        {
            int numOfDestroyableBlocks = 0;
            for (int i = 0; i < LIST_OF_CURRENT_LEVEL.Count; ++i)
            {
               Sprite a = LIST_OF_CURRENT_LEVEL[i];
                if(a.getType() != "INDESTRUCTABLE" && a.getType() != "TRANSPARENT")
                {
                    numOfDestroyableBlocks = numOfDestroyableBlocks + 1;
                }
            }
            return numOfDestroyableBlocks;
        }
        
        //Handles the amount of balls on the screen
        private void ballManager(List<BallSprite> ballList)
        {
            for (int i = 0; i < ballList.Count; ++i)
            {
                BallSprite currentCheck = ballList[i];
                
                    if (currentCheck.currentPosition.Y > Game.Window.ClientBounds.Bottom)
                    {
                        ballList.RemoveAt(i);
                        
                    }
                if (ballList.Count == 0)
                {
                Lives = Lives - 1;
                }
            }
            
        }


        private void levelSwitch()
        {
            //displaying levels, start with Level 1
            if (startLevels == true)
            {
                Level1.setShowLevel(true);
                Level2.setShowLevel(false);
                Level3.setShowLevel(false);
                Level4.setShowLevel(false);
                Level5.setShowLevel(false);
                startLevels = false;
                
            }
            else if(numOfDestroyableBlocks(Level1_List) == 0 
                && Level2_List.Count != 0 
                && Level1.getLevelStatus() == true)
            {
                //Level1 is finished, show level 2
                Level1.setShowLevel(false);
                Level2.setShowLevel(true);
                Level3.setShowLevel(false);
                Level4.setShowLevel(false);
                Level5.setShowLevel(false);
            }
            else if (numOfDestroyableBlocks(Level2_List) == 0
                && Level3_List.Count != 0 
                && Level2.getLevelStatus() == true)
            {
                //Level2 is finished, show level 3
                Level1.setShowLevel(false);
                Level2.setShowLevel(false);
                Level3.setShowLevel(true);
                Level4.setShowLevel(false);
                Level5.setShowLevel(false);
            }
            else if (numOfDestroyableBlocks(Level3_List) == 0
                && Level4_List.Count != 0
                && Level3.getLevelStatus() == true)
            {
                //Level3 is finished, show Level 4
                Level1.setShowLevel(false);
                Level2.setShowLevel(false);
                Level3.setShowLevel(false);
                Level4.setShowLevel(true);
                Level5.setShowLevel(false);
            }
            else if (numOfDestroyableBlocks(Level4_List) == 0
                && Level5_List.Count != 0
                && Level4.getLevelStatus() == true)
            {
                Level1.setShowLevel(false);
                Level2.setShowLevel(false);
                Level3.setShowLevel(false);
                Level4.setShowLevel(false);
                Level5.setShowLevel(true);
            }
        }

        private void addBall()
        {
            ball.Add(new BallSprite(Game.Content.Load<Texture2D>(@"Sprites/ball"), 
                new Vector2(player.currentPosition.X + 30, player.currentPosition.Y - 10), 
                new Point(8, 8), 0, new Point(0, 0), new Point(0, 0), new Vector2(3, -3)));
            ((Game1)Game).PlaySounds("ballServe");
        }

        private void addBullet()
        {
                    //fire bullets
                    bullets.Add(new AutomatedSprite(Game.Content.Load<Texture2D>(@"Sprites/bullet"),
                        new Vector2(player.currentPosition.X + 15, player.currentPosition.Y - 32),
                        new Point(8, 32), 0, new Point(0, 0), new Point(0, 0), new Vector2(0, -7)));
                    ((Game1)Game).PlaySounds("ballServe");
                    bullets.Add(new AutomatedSprite(Game.Content.Load<Texture2D>(@"Sprites/bullet"),
                                new Vector2(player.currentPosition.X + 45, player.currentPosition.Y - 32),
                                new Point(8, 32), 0, new Point(0, 0), new Point(0, 0), new Vector2(0, -7)));
                    ((Game1)Game).PlaySounds("ballServe");

                    //Deplete Ammo
                    Ammo = Ammo - 2; // minus 2 since there are 2 bullets fired
             
        }

        private void addLife(int numOfUps)
        {
            Lives = Lives + numOfUps;
        }

        private void randomBlockEffect()
        {
            Random newRandomNumber = new Random();
            int ranNumber = newRandomNumber.Next(0,20);

            if (ranNumber < 5)
            {
                addBall();
            }
            else if (ranNumber >= 5 && ranNumber < 10)
            {
                addLife(2);
            }
            else if (ranNumber >= 10 && ranNumber < 15)
            {
                Score = Score + 1000;
            }
            else if (ranNumber >= 15 && ranNumber <= 20)
            {
                //add ammo to ammo count
                //Give 100 ammo pieces for success
                Ammo = Ammo + 100;
            }
        }
        


    }
}
