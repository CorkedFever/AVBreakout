using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Advance_Breakout
{
    abstract class Sprite
    {
        // Stuff needed to draw the sprite
        Texture2D textureImage;
        protected Point frameSize;
        Point currentFrame;
        Point sheetSize;

        // Collision data
        int collisionOffset;

        // Framerate stuff
        int timeSinceLastFrame = 0;
        int millisecondsPerFrame;
        const int defaultMillisecondsPerFrame = 16;

        // Movement data
        protected Vector2 speed;
        protected Vector2 position;
        
        //Type of Sprite
        string typeOfSprite = "UNKNOWN";


        // Abstract definition of direction property
        public abstract Vector2 direction
        {
            get;
        }

        public abstract Vector2 currentPosition
        {
            get;
        }

        public Sprite(Texture2D textureImage, Vector2 position, Point frameSize,
    int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed)
            : this(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, defaultMillisecondsPerFrame)
        {
        }

        public Sprite(Texture2D textureImage, Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed,
            int millisecondsPerFrame)
        {
            this.textureImage = textureImage;
            this.position = position;
            this.frameSize = frameSize;
            this.collisionOffset = collisionOffset;
            this.currentFrame = currentFrame;
            this.sheetSize = sheetSize;
            this.speed = speed;
            this.millisecondsPerFrame = millisecondsPerFrame;
        }


        //Set type of Sprite
        public void setType(string Type)
        {
            typeOfSprite = Type;
        }
        //Get type of Sprite
        public string getType()
        {
            return typeOfSprite;
        }





        public virtual void Update(GameTime gameTime, Rectangle clientBounds)
        {

            // Update frame if time to do so based on framerate
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                // Increment to next frame
                timeSinceLastFrame = 0;
                ++currentFrame.X;
                if (currentFrame.X >= sheetSize.X)
                {
                    currentFrame.X = 0;
                    ++currentFrame.Y;
                    if (currentFrame.Y >= sheetSize.Y)
                        currentFrame.Y = 0;
                }
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Draw the sprite
            spriteBatch.Draw(textureImage,
                position,
                new Rectangle(currentFrame.X * frameSize.X,
                    currentFrame.Y * frameSize.Y,
                    frameSize.X, frameSize.Y),
                Color.White, 0, Vector2.Zero,
                1f, SpriteEffects.None, 0);
        }

        // Gets the collision rect based on position, framesize and collision offset
        public Rectangle collisionRect
        {
            get
            {
                return new Rectangle(
                    (int)position.X + collisionOffset,
                    (int)position.Y + collisionOffset,
                    frameSize.X - (collisionOffset * 2),
                    frameSize.Y - (collisionOffset * 2));
            }
        }


        public void collisionHandler(Sprite a)
        {

            ////checks left side of the Sprite Collision
            ////1

            if ((collisionRect.Left <= a.collisionRect.Left) 
                && (collisionRect.Bottom >= a.collisionRect.Top)
                && (collisionRect.Top <= a.collisionRect.Top)
                && (collisionRect.Right >= a.collisionRect.Left))
            {
                speed = new Vector2((Math.Abs(speed.X) *-1), (Math.Abs(speed.Y)*-1));
            }
            //2
            else if ((collisionRect.Left >= a.collisionRect.Left)
                && (collisionRect.Right <= a.collisionRect.Right)
                && (collisionRect.Bottom >= a.collisionRect.Top)
                && (collisionRect.Bottom <= a.collisionRect.Bottom))
            {
                speed = new Vector2(speed.X, (speed.Y * -1));
            }
            //3
            else if ((collisionRect.Right > a.collisionRect.Right) 
                && (collisionRect.Bottom >= a.collisionRect.Top)
                && (collisionRect.Top <= a.collisionRect.Top)
                && (collisionRect.Left <= a.collisionRect.Right))
            {
                speed = new Vector2((Math.Abs(speed.X)), (Math.Abs(speed.Y)*-1));
            }
            //4
            else if ((collisionRect.Left <= a.collisionRect.Left) 
                && (collisionRect.Right >= a.collisionRect.Left)
                && (collisionRect.Top >= a.collisionRect.Top)
                && (collisionRect.Bottom <= a.collisionRect.Bottom))
            {
                speed = new Vector2((speed.X * -1), speed.Y);
            }
            //5
            else if ((collisionRect.Right >= a.collisionRect.Right) 
                && (collisionRect.Left <= a.collisionRect.Right)
                && (collisionRect.Top >= a.collisionRect.Top)
                && (collisionRect.Bottom <= a.collisionRect.Bottom))
            {
                speed = new Vector2((speed.X * -1), speed.Y);
            }
            //6
            else if ((collisionRect.Left <= a.collisionRect.Left)
                && (collisionRect.Right >= a.collisionRect.Left)
                && (collisionRect.Top <= a.collisionRect.Bottom)
                && (collisionRect.Bottom >= a.collisionRect.Bottom))
            {
                speed = new Vector2((Math.Abs(speed.X)*-1), (Math.Abs(speed.Y)));
            }
            //7
            else if ((collisionRect.Left >= a.collisionRect.Left)
                && (collisionRect.Top <= a.collisionRect.Bottom)
                && (collisionRect.Bottom >= a.collisionRect.Bottom)
                && (collisionRect.Right <= a.collisionRect.Right))
            {
                speed = new Vector2(speed.X, (speed.Y * -1));
            }
            //8
            else if ((collisionRect.Right >= a.collisionRect.Right)
                && (collisionRect.Right >= a.collisionRect.Left)
                && (collisionRect.Top <= a.collisionRect.Bottom)
                && (collisionRect.Bottom >=a.collisionRect.Bottom))
            {
                speed = new Vector2((Math.Abs(speed.X)), (Math.Abs(speed.Y)));
            }

            //else if ((collisionRect.Left < a.collisionRect.Left) && (collisionRect.Right < a.collisionRect.Right))
            //{

            //    speed = new Vector2((speed.X * -1), speed.Y);

            //}
            ////Checks Right side of Sprite Collision
            //else if ((collisionRect.Left > a.collisionRect.Left) && (collisionRect.Right > a.collisionRect.Right))
            //{
            //    speed = new Vector2((speed.X * -1), speed.Y);
            //}
            //////Impossible part of code, will never happen
            ////if ((collisionRect.Left < a.collisionRect.Left) && (collisionRect.Right > a.collisionRect.Right))
            ////{
            ////    speed = new Vector2(speed.X, (speed.Y * -1));
            ////}
            ////Checks Top side or Bottom side of the sprite collision
            //else if ((collisionRect.Left > a.collisionRect.Left) && (collisionRect.Right < a.collisionRect.Right))
            //{
            //    speed = new Vector2(speed.X, (speed.Y * -1));
            //}

            //if ((collisionRect.Bottom >= a.collisionRect.Top) && (collisionRect.Top <= a.collisionRect.Top))
            //{
            //    speed = new Vector2(speed.X, (speed.Y * -1));
            //}
            //else if ((collisionRect.Top <= a.collisionRect.Bottom) && (collisionRect.Bottom >= a.collisionRect.Bottom))
            //{
            //    speed = new Vector2(speed.X, (speed.Y * -1));
            //}

            //if ((collisionRect.Right >= a.collisionRect.Left) && (collisionRect.Left <= a.collisionRect.Left))
            //{
            //    speed = new Vector2((speed.X * -1), speed.Y);
            //}
            //else if ((collisionRect.Left <= a.collisionRect.Right) && (collisionRect.Right >= a.collisionRect.Right))
            //{
            //    speed = new Vector2((speed.X * -1), speed.Y);
            //}

        }

        //trying to give the player a bit more control over where the ball goes
        //public void collisionWithPlayerHandler(Sprite player)
        //{
        //    if (position.Y > (player.position.Y - 32))
        //    {
        //        speed = new Vector2(speed.X, (speed.Y * -1));

        //    }
        //    if (position.Y < (player.position.Y - 32))
        //    {
        //        speed = new Vector2((speed.X *-1), (speed.Y * -1));


        //    }
        //}

        //public void wallCollision(Rectangle Window)
        //{
        //    if (position.X < 0)
        //    {
        //        speed = new Vector2(speed.X, (speed.Y * -1));
        //    }
        //    if (position.Y < 0)
        //    {
        //        speed = new Vector2((speed.X * -1), speed.Y);
        //    }
        //    if (position.Y > Window.Height)
        //    {
        //        speed = new Vector2((speed.X * -1), speed.Y);
        //    }
        //}



    }
}
