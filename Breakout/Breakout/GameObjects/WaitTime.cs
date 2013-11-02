using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

using System;

namespace Breakout
{
    class WaitTime
    {
        private Texture2D chiffre;
        private Vector2 chiffrePosition;
        private Rectangle screenBound;

        public WaitTime(Texture2D sprite, Rectangle screenBound)
        {
            setSprite(sprite);
            this.screenBound = screenBound;
            setPosition();
        }

        private void setPosition()
        {
            chiffrePosition.X = (screenBound.Width / 2) - (chiffre.Width / 2);
            chiffrePosition.Y = screenBound.Height - 300;
        }

        public float getPositionX()
        {
            return chiffrePosition.X;
        }

        public float getPositionY()
        {
            return chiffrePosition.Y;
        }
        public Texture2D getSprite()
        {
            return chiffre;
        }

        public void setSprite(Texture2D sprite)
        {
            this.chiffre = sprite;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(chiffre, chiffrePosition, Color.White);
        }

    }
}
