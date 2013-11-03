using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

using System;

namespace Breakout
{
    class Bouton
    {
        private List<Texture2D> boutons;
        private Vector2 boutonPosition;
        private Rectangle bound;
        private enum Etat : int { Normal, Highlighted, Activated };
        private Etat etatPresent;

        public event EventHandler Clicked;

        /***************************************************************************/
        /* Nom: Constructeur par parametre                                         */
        /* Description: Cette fonction cree un etudiant et initialise ses          */
        /*              parametres aux valeurs entrees en parametre                */
        /* Parametres:  sprite (IN): Texture2D du sprite du bouton                 */
        /*              positionX (IN): float de la position en X du bouton        */
        /*              positionY (IN): float de la position en Y du bouton        */
        /* Valeur de retour: Aucune                                                */
        /* Remarque: Ce constructeur initialise un bouton possedant un sprite,     */
        /*          une fenetre et une position en X et en Y.                      */
        /***************************************************************************/
        public Bouton(Texture2D spriteNormal, Texture2D spriteHighlighted, Texture2D spriteActivated, float positionX, float positionY)
        {
            boutons = new List<Texture2D>{spriteNormal, spriteHighlighted, spriteActivated};
            setPosition(positionX, positionY);
            etatPresent = Etat.Normal;
            bound = new Rectangle((int)positionX, (int)positionY, spriteNormal.Width, spriteNormal.Height);
        }

        private void setPosition(float positionX, float positionY)
        {
            boutonPosition.X = positionX;
            boutonPosition.Y = positionY;
        }

        public float getPositionX()
        {
            return boutonPosition.X;
        }

        public float getPositionY()
        {
            return boutonPosition.Y;
        }

        public void Update(KeyboardState kState, MouseState mState)
        {
            if (bound.Contains(mState.X, mState.Y))
            {
                if (mState.LeftButton == ButtonState.Pressed)
                    etatPresent = Etat.Activated;
                else if (etatPresent == Etat.Activated && Clicked != null)
                    Clicked(this, EventArgs.Empty);
                else
                    etatPresent = Etat.Highlighted;
            }
            else
                etatPresent = Etat.Normal;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(boutons[(int)etatPresent], boutonPosition, Color.White);
        }
    }
}
