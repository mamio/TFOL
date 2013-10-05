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

namespace Breakout
{
    class Palette
    {
        private Texture2D sprite;
        private Rectangle screenBound;
        private int speedX;
        private Vector2 position;

        public float xPos
        {
            get
            {
                return position.X;
            }
            set
            {
                if (value < 0)
                    position.X = 0;
                else if (value > screenBound.Width - sprite.Width)
                    position.X = screenBound.Width - sprite.Width;
                else
                    position.X = value;
            }
        }
        public Palette(Texture2D sprite, Rectangle screenBound)
        {
            position = new Vector2();
            this.sprite = sprite;
            this.screenBound = screenBound;
            speedX = 5;
            setInitPosition();
        }

        private void setInitPosition()
        {
            position.X = screenBound.Width / 2 - 50;
            position.Y = screenBound.Height - 50;
        }

        public void Update(KeyboardState state)
        {
            if (state.IsKeyDown(Keys.Left))
            {
                xPos -= speedX;
            }
            else if(state.IsKeyDown(Keys.Right))
            {
                xPos += speedX;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, Color.White);
        }
    }
}
