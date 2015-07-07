using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Advance_Breakout
{
    class BallSprite: Sprite
    {
                // Sprite is automated. Direction is same as speed
        public override Vector2 direction
        {
            get { return speed; }
        }

        public override Vector2 currentPosition
        {
            get { return position; }
        }
        
        

        public BallSprite(Texture2D textureImage, Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed)
        {
        }

        public BallSprite(Texture2D textureImage, Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed,
            int millisecondsPerFrame)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, millisecondsPerFrame)
        {
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            // Move sprite based on direction
            position += direction;

            
            //ball's reaction with the wall left wall
            if (position.X < 0)
            {
                speed = new Vector2((Math.Abs(speed.X)), speed.Y);
            }
            //Ball's reaction with top screen
            if (position.Y < 0)
            {
                speed = new Vector2(speed.X, (speed.Y * -1));
            }
            //Ball's reaction with Right Screen
            if (position.X > clientBounds.Width - frameSize.X)
            {
                speed = new Vector2((Math.Abs(speed.X) *-1), speed.Y);
            }
            if (position.Y > clientBounds.Height - frameSize.Y)
            {
                //should have some sort of kill result here not sure though
                //speed = new Vector2((speed.X * -1), speed.Y);
            }
                




            base.Update(gameTime, clientBounds);
        }


    }
}
