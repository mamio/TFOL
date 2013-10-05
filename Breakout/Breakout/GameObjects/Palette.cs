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
        public Vector2 position;
        private Texture2D sprite;
        private Rectangle screenBound;

        public Palette(Texture2D sprite, Rectangle screenBound)
        {
            position = new Vector2();
            this.sprite = sprite;
            this.screenBound = screenBound;
            setInitPosition();
        }

        private void setInitPosition()
        {
            position.X = screenBound.Width / 2 - 50;
            position.Y = screenBound.Height - 50;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, Color.White);
        }
    }
}
