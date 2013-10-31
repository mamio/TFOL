using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

using System;

namespace Breakout
{
    class BoutonExit
    {
        private Texture2D exitButton;
        private Vector2 exitButtonPosition;
        private Rectangle screenBound;

        public BoutonExit(Texture2D sprite, Rectangle screenBound)
        {
            setSprite(sprite);
            this.screenBound = screenBound;
            setPosition();
        }

        private void setPosition()
        {
            exitButtonPosition.X = (screenBound.Width / 2) - (exitButton.Width / 2);
            exitButtonPosition.Y = screenBound.Height - 200;
        }

        public float getPositionX()
        {
            return exitButtonPosition.X;
        }

        public float getPositionY()
        {
            return exitButtonPosition.Y;
        }

        public void setSprite(Texture2D sprite)
        {
            this.exitButton = sprite;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(exitButton, exitButtonPosition, Color.White);
        }
    }
}
