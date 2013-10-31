using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

using System;

namespace Breakout
{
    class BoutonStart
    {
        private Texture2D startButton;
        private Vector2 startButtonPosition;
        private Rectangle screenBound;

        public BoutonStart(Texture2D sprite, Rectangle screenBound)
        {
            this.startButton = sprite;
            this.screenBound = screenBound;
            setPosition();
        }

        private void setPosition()
        {
            startButtonPosition.X = (screenBound.Width / 2) - (startButton.Width / 2);
            startButtonPosition.Y = screenBound.Height - 300;
        }

        public float getPositionX()
        {
            return startButtonPosition.X;
        }

        public float getPositionY()
        {
            return startButtonPosition.Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(startButton, startButtonPosition, Color.White);
        }
    }
}
