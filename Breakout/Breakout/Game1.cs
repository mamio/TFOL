using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Breakout
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Breakout : Microsoft.Xna.Framework.Game
    {
        public const int WIN_HEIGHT = 480;
        public const int WIN_WIDTH = 845;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle screenBound = new Rectangle( 0, 0, WIN_WIDTH, WIN_HEIGHT );

        List<Brique> briques;
        Palette palette;
        Balle balle;

        KeyboardState lastKeyboardState;
        int briquesBrisees = 0;

        public Breakout()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = WIN_HEIGHT;
            graphics.PreferredBackBufferWidth = WIN_WIDTH;

            briques = new List<Brique>();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D paletteSprite = Content.Load<Texture2D>("palette");
            palette = new Palette(paletteSprite, screenBound);

            Texture2D briqueSprite = Content.Load<Texture2D>("brique");
            for (int i = 0; i < screenBound.Width; i += briqueSprite.Width)
                briques.Add(new Brique(briqueSprite, Color.Fuchsia, new Vector2(i, 100), 1));

            Texture2D balleSprite = Content.Load<Texture2D>("balle");
            balle = new Balle(balleSprite, screenBound, new Vector2(screenBound.Width / 2 -10, screenBound.Height - 70));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            Content.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();


            if (balle.getPositionY() > screenBound.Bottom)
            {
                Texture2D balleSprite = Content.Load<Texture2D>("balle");
                balle = new Balle(balleSprite, screenBound, new Vector2(screenBound.Width / 2 - 10, screenBound.Height - 70));
                palette.returnToStart();
                balle.setEnable(false);
            }

            // TODO: Add your update logic here
            KeyboardState state = Keyboard.GetState();

            for (int i = 0; i < briques.Count; ++i)
            {
                if (balle.checkBrickCollision(briques[i].getLocation()))
                {
                    briques[i] = null;
                    briques.Remove(briques[i]);
                }
            }

            palette.Update(state);
            balle.Update(state);
            balle.checkPaddleCollision(palette.getLocation());

            //Doit absolument etre apres tous les verifications du clavier
            lastKeyboardState = state;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            // TODO: Add your drawing code here
            spriteBatch.Begin();

            // TODO: changer l'emplacement une fois que notre taille de fenêtre est décidé
            palette.Draw(spriteBatch);
            balle.Draw(spriteBatch);

            foreach (Brique brique in briques)
            {
                if (brique != null)
                    brique.Draw(spriteBatch);
            }

            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
