using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

using System;

namespace Breakout
{
    class Heart
    {
        private Texture2D sprite;
        private Vector2 position;

        public Heart(Texture2D sprite, Vector2 position)
        {
            this.sprite = sprite;
            this.position = position;
        }

        public void Update()
        {
            // TODO: faire de quoi
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, Color.White);
        }


    }
}
