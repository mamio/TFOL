using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

using System;


namespace Breakout
{
    class Balle
    {
        private Texture2D sprite;
        private Rectangle screenBound;
        private Vector2 speed;
        private float vitesseScalaire;
        private Vector2 position;
        private bool enable;
        private bool inCollision;

        public Balle(Texture2D sprite, Rectangle screenBound, Vector2 position)
        {
            this.position = position;
            this.sprite = sprite;
            this.screenBound = screenBound;
            speed = new Vector2(0, -3);
            vitesseScalaire = 5;
            enable = false;
            inCollision = false;
        }

        public void Update(KeyboardState state)
        {
            if (!enable && state.IsKeyDown(Keys.Space))
                enable = true;

            if (enable) 
            {
                checkWallCollision();
                position += speed;
            }
            
        }

        private void checkWallCollision()
        {
            if (position.X < 0 || (position.X+sprite.Width) > screenBound.Width)
                speed.X *= -1;

            if (position.Y < 0)
                speed.Y *= -1;
        }

        public bool checkBrickCollision(Rectangle brick)
        {
            Rectangle ballLocation = new Rectangle((int)position.X, (int)position.Y,
               sprite.Width, sprite.Height);

            if (brick.Intersects(ballLocation) && !inCollision)
            {
                speed.Y *= -1;
                return true;
            }
            return false;
        }

        public void checkPaddleCollision(Rectangle paddle)
        {
            Rectangle ballLocation = new Rectangle((int)position.X, (int)position.Y,
                sprite.Width, sprite.Height);

            if (paddle.Intersects(ballLocation) && !inCollision)
            {
                inCollision = true;
                float ballBottom = position.X + (sprite.Width / 2);
                int centerPaddle = paddle.X + (paddle.Width / 2);


                float distanceFromCenterPaddle = MathHelper.Distance(ballBottom, centerPaddle);

                float d = paddle.Width / 2;

                if (distanceFromCenterPaddle > d)
                    distanceFromCenterPaddle = d;
                

                double angle = calculerAngleLancement(distanceFromCenterPaddle, d);
                System.Diagnostics.Debug.WriteLine("pos: " + (ballBottom - centerPaddle));
                

                //vitesse en x
                if (ballBottom < centerPaddle)
                    speed.X = vitesseScalaire * (float)Math.Cos(angle + Math.PI);
                else
                    speed.X = vitesseScalaire * (float)Math.Cos(angle);

                speed.Y = vitesseScalaire * (float)Math.Sin(angle);

                System.Diagnostics.Debug.WriteLine("angle radian: " + angle);
                if (speed.Y > 0)
                    speed.Y *= -1;
                System.Diagnostics.Debug.WriteLine("X: " + speed.X + ", Y: " + speed.Y);
                System.Diagnostics.Debug.WriteLine(Math.Pow(speed.X, 2) + Math.Pow(speed.Y, 2));
            }
            else if(!paddle.Intersects(ballLocation))
                inCollision = false;
        }

        //f(x) = 70 - 50x/d ou x est la distance a partir du centre et d 
        // est la longeur du demi palettes. l'equation nous donne un angle 
        //entre 70 et 20 degres, 70 degres quand la balle tombe plein centre et 
        //20 degres quand la balle tombe sur le bout de la palette
        private double calculerAngleLancement(double distanceFromCenterPaddle, double d)
        {
            System.Diagnostics.Debug.WriteLine("distance: " + distanceFromCenterPaddle);
            double angle = 70 - 50*distanceFromCenterPaddle/d;
            System.Diagnostics.Debug.WriteLine("angle: " + angle);
            //convertion en radian
            return angle * (180 / Math.PI);
        }

        

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, Color.White);
        }
    }
}
