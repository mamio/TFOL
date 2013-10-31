using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

using System;


namespace Breakout
{
    class Resume
    {
        private Texture2D resumeButton;
        private Vector2 resumeButtonPosition;
        private Rectangle screenBound;

        public Resume(Texture2D sprite, Rectangle screenBound)
        {
            this.resumeButton = sprite;
            this.screenBound = screenBound;
            setPosition();
        }

        private void setPosition()
        {
            resumeButtonPosition.X = (screenBound.Width / 2) - (resumeButton.Width / 2);
            resumeButtonPosition.Y = (screenBound.Height / 2) - (resumeButton.Height / 2);
        }

        public float getPositionX()
        {
            return resumeButtonPosition.X;
        }

        public float getPositionY()
        {
            return resumeButtonPosition.Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(resumeButton, resumeButtonPosition, Color.White);
        }
    }
}
