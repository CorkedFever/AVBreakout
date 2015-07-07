using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Advance_Breakout
{
    class UserControlledSprite: Sprite
    {
        MouseState prevMouseState;

        public override Vector2 direction
        {
            get
            {
                //If player pressed the left or right arrow keys
                //move ths player sprite accordingly
                Vector2 inputDirection = Vector2.Zero;
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                    inputDirection.X -= 1;
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                    inputDirection.X += 1;

                return inputDirection * speed;
            }
        }

        public override Vector2 currentPosition
        {
            get { return position; }
        }

        public UserControlledSprite(Texture2D textureImage, Vector2 position,
            Point frameSize, int collisionOffset, Point currentFrame, Point sheetSize,
            Vector2 speed)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed)
        {
        }

        public UserControlledSprite(Texture2D textureImage, Vector2 position,
            Point frameSize, int collisionOffset, Point currentFrame, Point sheetSize,
            Vector2 speed, int millisecondsPerFrame)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, millisecondsPerFrame)
        {
        }


        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            position += direction;

           //If player moved the mouse, move the sprite
            MouseState currMouseState = Mouse.GetState();
            if (currMouseState.X != prevMouseState.X)
            {
                position = new Vector2(currMouseState.X, position.Y);
            }
            prevMouseState = currMouseState;

            //If sprite is off the screen, move it back within the game window
            //This way the player sprite is always shown on screen
            if (position.X < 0)
                position.X = 0;
            if (position.Y < 0)
                position.Y = 0;
            if (position.X > clientBounds.Width - frameSize.X)
                position.X = clientBounds.Width - frameSize.X;
            if (position.Y > clientBounds.Height - frameSize.Y)
                position.Y = clientBounds.Height - frameSize.Y;


            base.Update(gameTime, clientBounds);
        }

    }
}
