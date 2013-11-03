using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

using System;

namespace Breakout
{
    class Bouton
    {
        private Texture2D bouton;
        private Vector2 boutonPosition;
        private Rectangle screenBound;

        /***************************************************************************/
        /* Nom: Constructeur par parametre                                         */
        /* Description: Cette fonction cree un etudiant et initialise ses          */
        /*              parametres aux valeurs entrees en parametre                */
        /* Parametres:  sprite (IN): Texture2D du sprite du bouton                 */
        /*              screenbound (IN): Rectangle representant la fenetre du jeu */
        /*              positionX (IN): float de la position en X du bouton        */
        /*              positionY (IN): float de la position en Y du bouton        */
        /* Valeur de retour: Aucune                                                */
        /* Remarque: Ce constructeur initialise un bouton possedant un sprite,     */
        /*          une fenetre et une position en X et en Y.                      */
        /***************************************************************************/
        public Bouton(Texture2D sprite, Rectangle screenbound, float positionX, float positionY)
        {
            setSprite(sprite);
            screenBound = screenbound;
            setPosition(positionX, positionY);
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
        public void setSprite(Texture2D sprite)
        {
            bouton = sprite;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(bouton, boutonPosition, Color.White);
        }
    }
}
