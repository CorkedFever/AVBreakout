using System;
using System.IO;
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
    class Level: Microsoft.Xna.Framework.DrawableGameComponent
    {
        //ContentManager content;
       
        AutomatedSprite holder;
        SpriteBatch spriteBatch;
        string blockLocation;// location of Level Text in computer

        const int BLOCK_HEIGHT = 16, BLOCK_WIDTH = 32, BOUNDRY = -1;
        int currentHeight = 50, currentWidth = 50;
        List<Sprite> levelList = new List<Sprite>();

        //Display Level
        bool showLevel = false;

        //Constructor
        public Level(Game game)
            :base(game)
        {
            
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            //setLevel(@"Content/Levels/Level1.txt");

            base.LoadContent();
        }

        //generate level
        public void setLevel(string addressLocation)
        {
           string[] individualBlockTypes;// string array
            string individualLines;// +string
            StreamReader file = new StreamReader(addressLocation);

            //read in lines
            while ((individualLines = file.ReadLine()) != null)
            {
                individualBlockTypes = individualLines.Split('|');

                placeBlocks(individualBlockTypes);

            }
            
        }
        //Store block types into levelList
        private void placeBlocks(string[] blockTypes)
        {
            
            //foreach loop and switch, generates block matrix
            foreach (string a in blockTypes)
            {
                
                if (a != "" || a != "\n")
                {
                    switch (a)
                    {
                        case "-6":
                            holder = new AutomatedSprite(Game.Content.Load<Texture2D>(@"Sprites/ammo_block"), new Vector2(currentWidth, currentHeight),
                                new Point(32, 16), BOUNDRY, new Point(0, 0), new Point(0, 0), Vector2.Zero);

                            holder.setType("AMMO");
                            levelList.Add(holder);
                            currentWidth = currentWidth + BLOCK_WIDTH;
                            break;

                        case "-5":
                            holder = new AutomatedSprite(Game.Content.Load<Texture2D>(@"Sprites/life_block"), new Vector2(currentWidth, currentHeight),
                                new Point(32, 16), BOUNDRY, new Point(0, 0), new Point(0, 0), Vector2.Zero);

                            holder.setType("LIFE");
                            levelList.Add(holder);
                            currentWidth = currentWidth + BLOCK_WIDTH;
                            break;
                        case "-4":
                            holder = new AutomatedSprite(Game.Content.Load<Texture2D>(@"Sprites/random_block"), new Vector2(currentWidth, currentHeight),
                                new Point(32, 16), BOUNDRY, new Point(0, 0), new Point(0, 0), Vector2.Zero);

                            holder.setType("QUESTION");
                            levelList.Add(holder);
                            currentWidth = currentWidth + BLOCK_WIDTH;
                            break;

                        case "-3":
                            holder = new AutomatedSprite(Game.Content.Load<Texture2D>(@"Sprites/glass_block"),new Vector2(currentWidth,currentHeight),
                                new Point(32, 16), BOUNDRY, new Point(0, 0), new Point(0, 0), Vector2.Zero);

                            holder.setType("TRANSPARENT");
                            levelList.Add(holder);
                            currentWidth = currentWidth + BLOCK_WIDTH;
                            break;

                        case "-2":
                            holder = new AutomatedSprite(Game.Content.Load<Texture2D>(@"Sprites/indestruct_block"),new Vector2(currentWidth,currentHeight),
                                new Point(32, 16), BOUNDRY, new Point(0, 0), new Point(0, 0), Vector2.Zero);

                            holder.setType("INDESTRUCTABLE");
                            levelList.Add(holder);
                            currentWidth = currentWidth + BLOCK_WIDTH;
                            break;

                        case "-1":
                            holder = new AutomatedSprite(Game.Content.Load<Texture2D>(@"Sprites/multi_block"),new Vector2(currentWidth,currentHeight),
                                new Point(32, 16), BOUNDRY, new Point(0, 0), new Point(0, 0), Vector2.Zero);

                            holder.setType("MULTIBALL");
                            levelList.Add(holder);
                            currentWidth = currentWidth + BLOCK_WIDTH;
                            break;
                        case "0":
                            currentWidth = currentWidth + BLOCK_WIDTH;
                            break;

                        case "1":
                            holder = new AutomatedSprite(Game.Content.Load<Texture2D>(@"Sprites/black_block"), new Vector2(currentWidth, currentHeight),
                                new Point(32, 16), BOUNDRY, new Point(0, 0), new Point(0, 0), Vector2.Zero);
                            holder.setType("NORMAL");
                            levelList.Add(holder);
                            currentWidth = currentWidth + BLOCK_WIDTH;
                            break;

                        case "2":
                            holder = new AutomatedSprite(Game.Content.Load<Texture2D>(@"Sprites/blue_block"), new Vector2(currentWidth, currentHeight),
                                new Point(32, 16), BOUNDRY, new Point(0, 0), new Point(0, 0), Vector2.Zero);
                            holder.setType("NORMAL");
                            levelList.Add(holder);
                            currentWidth = currentWidth + BLOCK_WIDTH;
                            break;

                        case "3":
                            holder = new AutomatedSprite(Game.Content.Load<Texture2D>(@"Sprites/brown_block"), new Vector2(currentWidth, currentHeight),
                                new Point(32, 16), BOUNDRY, new Point(0, 0), new Point(0, 0), Vector2.Zero);
                            holder.setType("NORMAL");
                            levelList.Add(holder);
                            currentWidth = currentWidth + BLOCK_WIDTH;
                            break;

                        case "4":
                            holder = new AutomatedSprite(Game.Content.Load<Texture2D>(@"Sprites/dark_purple_block"), new Vector2(currentWidth, currentHeight),
                                new Point(32, 16), BOUNDRY, new Point(0, 0), new Point(0, 0), Vector2.Zero);
                            holder.setType("NORMAL");
                            levelList.Add(holder);
                            currentWidth = currentWidth + BLOCK_WIDTH;
                            break;

                        case "5":
                             holder = new AutomatedSprite(Game.Content.Load<Texture2D>(@"Sprites/green_block"), new Vector2(currentWidth, currentHeight),
                                new Point(32, 16), BOUNDRY, new Point(0, 0), new Point(0, 0), Vector2.Zero);
                            holder.setType("NORMAL");
                            levelList.Add(holder);
                            currentWidth = currentWidth + BLOCK_WIDTH;
                            break;
                            
                        case "6":
                             holder = new AutomatedSprite(Game.Content.Load<Texture2D>(@"Sprites/orange_block"), new Vector2(currentWidth, currentHeight),
                                new Point(32, 16), BOUNDRY, new Point(0, 0), new Point(0, 0), Vector2.Zero);
                            holder.setType("NORMAL");
                            levelList.Add(holder);
                            currentWidth = currentWidth + BLOCK_WIDTH;
                            break;

                        case "7":
                             holder = new AutomatedSprite(Game.Content.Load<Texture2D>(@"Sprites/purple_block"), new Vector2(currentWidth, currentHeight),
                                new Point(32, 16), BOUNDRY, new Point(0, 0), new Point(0, 0), Vector2.Zero);
                            holder.setType("NORMAL");
                            levelList.Add(holder);
                            currentWidth = currentWidth + BLOCK_WIDTH;
                            break;

                        case "8":
                             holder = new AutomatedSprite(Game.Content.Load<Texture2D>(@"Sprites/red_block"), new Vector2(currentWidth, currentHeight),
                                new Point(32, 16), BOUNDRY, new Point(0, 0), new Point(0, 0), Vector2.Zero);
                            holder.setType("NORMAL");
                            levelList.Add(holder);
                            currentWidth = currentWidth + BLOCK_WIDTH;
                            break;

                        case "9":
                             holder = new AutomatedSprite(Game.Content.Load<Texture2D>(@"Sprites/teal_block"), new Vector2(currentWidth, currentHeight),
                                new Point(32, 16), BOUNDRY, new Point(0, 0), new Point(0, 0), Vector2.Zero);
                            holder.setType("NORMAL");
                            levelList.Add(holder);
                            currentWidth = currentWidth + BLOCK_WIDTH;
                            break;

                        case "10":
                             holder = new AutomatedSprite(Game.Content.Load<Texture2D>(@"Sprites/yellow_block"), new Vector2(currentWidth, currentHeight),
                                new Point(32, 16), BOUNDRY, new Point(0, 0), new Point(0, 0), Vector2.Zero);
                            holder.setType("NORMAL");
                            levelList.Add(holder);
                            currentWidth = currentWidth + BLOCK_WIDTH;
                            break;

                        default:

                            break;
                    }
                }

            }
            currentHeight = currentHeight + BLOCK_HEIGHT;
            currentWidth = 50;

        }

        //return Level list to user
        public List<Sprite> getLevel()
        {
            return levelList;
        }

        //set Level to on or off
        public void setShowLevel(bool status)
        {
            showLevel = status;
        }

        //get status of Level
        public bool getLevelStatus()
        {
            return showLevel;
        }


    }
}
