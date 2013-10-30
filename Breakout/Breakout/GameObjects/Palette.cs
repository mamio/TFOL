
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    class Palette
    {
        private Texture2D sprite;
        private Rectangle screenBound;
        private int speedX;
        private Vector2 position;
        KeyboardState lastKeyboardState;

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

        public void returnToStart()
        {
            position.X = screenBound.Width / 2 - 50;
            position.Y = screenBound.Height - 50;
        }

        public Rectangle getLocation()
        {
            return new Rectangle((int)position.X, (int)position.Y,
                sprite.Width, sprite.Height);
        }

        public void Update(KeyboardState state)
        {
            if (state.IsKeyDown(Keys.Left) || state.IsKeyDown(Keys.A) )
            {
                if ((position.X -= speedX) < 0)
                    position.X = 0;
            }
            if (state.IsKeyDown(Keys.Right) || state.IsKeyDown(Keys.D))
            {
                if ((position.X += speedX) > screenBound.Width - sprite.Width)
                    position.X = screenBound.Width - sprite.Width;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, Color.White);
        }
    }
}
