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

        enum GameState
        {
            StartMenu,
            Playing,
            Paused
        }

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle screenBound = new Rectangle( 0, 0, WIN_WIDTH, WIN_HEIGHT );

        List<Brique> briques;
        Palette palette;
        Balle balle;

        KeyboardState lastKeyboardState;
        int briquesBrisees = 0;

        BoutonStart boutonStart;
        BoutonExit boutonExit;
        Resume boutonResume;
        private GameState gameState;

        MouseState mouseState;
        MouseState previousMouseState;

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
            IsMouseVisible = true;
           
            gameState = GameState.StartMenu;

            mouseState = Mouse.GetState();
            previousMouseState = mouseState;

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

            Texture2D boutonStartSprite = Content.Load<Texture2D>("boutonStart");
            boutonStart = new BoutonStart(boutonStartSprite, screenBound);

            Texture2D boutonExitSprite = Content.Load<Texture2D>("boutonExit");
            boutonExit = new BoutonExit(boutonExitSprite, screenBound);

            Texture2D boutonResumeSprite = Content.Load<Texture2D>("boutonResume");
            boutonResume = new Resume(boutonResumeSprite, screenBound);
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

            // TODO: Add your update logic here
            KeyboardState state = Keyboard.GetState();

            if (gameState == GameState.StartMenu)
            {

                Texture2D boutonStartSprite = Content.Load<Texture2D>("boutonStart");
                if ((mouseState.X < boutonStart.getPositionX() + boutonStartSprite.Width)
                     && mouseState.X > boutonStart.getPositionX() &&
                    mouseState.Y < (boutonStart.getPositionY() + boutonStartSprite.Height)
                    && mouseState.Y > boutonStart.getPositionY())
                {
                    Texture2D boutonStartHighlightedSprite = Content.Load<Texture2D>("boutonStart_highlighted");
                    boutonStart.setSprite(boutonStartHighlightedSprite);
                }
                else
                {
                    boutonStart.setSprite(boutonStartSprite);
                }

                Texture2D boutonExitSprite = Content.Load<Texture2D>("boutonExit");
                if ((mouseState.X < boutonExit.getPositionX() + boutonExitSprite.Width)
                     && mouseState.X > boutonExit.getPositionX() &&
                    mouseState.Y < (boutonExit.getPositionY() + boutonExitSprite.Height)
                    && mouseState.Y > boutonExit.getPositionY())
                {
                    Texture2D boutonExitHighlightedSprite = Content.Load<Texture2D>("boutonExit_highlighted");
                    boutonExit.setSprite(boutonExitHighlightedSprite);
                }
                else
                {
                    boutonExit.setSprite(boutonExitSprite);
                }
            }

            if (gameState == GameState.Paused)
            {
                Texture2D boutonResumeSprite = Content.Load<Texture2D>("boutonResume");
                if ((mouseState.X < boutonResume.getPositionX() + boutonResumeSprite.Width)
                     && mouseState.X > boutonResume.getPositionX() &&
                    mouseState.Y < (boutonResume.getPositionY() + boutonResumeSprite.Height)
                    && mouseState.Y > boutonResume.getPositionY())
                {
                    Texture2D boutonResumeHighlightedSprite = Content.Load<Texture2D>("boutonResume_highlighted");
                    boutonResume.setSprite(boutonResumeHighlightedSprite);
                }
                else
                {
                    boutonResume.setSprite(boutonResumeSprite);
                }
            }

            if (gameState == GameState.Playing)
            {
                if (balle.getEnable() == false)
                {
                    balle.setPositionX(palette.getPositionX() + 40);

                }

                if (balle.getPositionY() > screenBound.Bottom)
                {
                    Texture2D balleSprite = Content.Load<Texture2D>("balle");
                    balle = new Balle(balleSprite, screenBound, new Vector2(screenBound.Width / 2 - 10, screenBound.Height - 70));
                    palette.returnToStart();
                    balle.setEnable(false);
                }


                if (state.IsKeyDown(Keys.Enter))
                {
                    gameState = GameState.Paused;
                }

                palette.Update(state);
                balle.Update(state, gameTime);
                balle.checkPaddleCollision(palette.getLocation());

                for (int i = 0; i < briques.Count; ++i)
                {
                    if (balle.checkBrickCollision(briques[i].getLocation()))
                    {
                        briques[i] = null;
                        briques.Remove(briques[i]);
                    }
                }

            }

            //Doit absolument etre apres tous les verifications du clavier
            lastKeyboardState = state;

            mouseState = Mouse.GetState();
            if (previousMouseState.LeftButton == ButtonState.Pressed &&
                mouseState.LeftButton == ButtonState.Released)
            {
                MouseClicked(mouseState.X, mouseState.Y);
            }

            previousMouseState = mouseState;

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

            if (gameState == GameState.StartMenu)
            {
                boutonStart.Draw(spriteBatch);
                boutonExit.Draw(spriteBatch);
            }

            if (gameState == GameState.Paused)
            {
                boutonResume.Draw(spriteBatch);
            }

            if (gameState == GameState.Playing)
            {
                // TODO: changer l'emplacement une fois que notre taille de fenêtre est décidé
                palette.Draw(spriteBatch);
                balle.Draw(spriteBatch);

                foreach (Brique brique in briques)
                {
                    if (brique != null)
                        brique.Draw(spriteBatch);
                }
            }

            spriteBatch.End();
            
            base.Draw(gameTime);
        }

        void MouseClicked(int x, int y)
        {
            //creates a rectangle of 10x10 around the place where the mouse was clicked
            Rectangle mouseClickRect = new Rectangle(x, y, 20, 20);

            //check the startmenu
            if (gameState == GameState.StartMenu)
            {
                Rectangle startButtonRect = new Rectangle((int)boutonStart.getPositionX(), (int)boutonStart.getPositionY(), 145, 50);
                Rectangle exitButtonRect = new Rectangle((int)boutonExit.getPositionX(), (int)boutonExit.getPositionY(), 145, 50);

                if (mouseClickRect.Intersects(startButtonRect)) //player clicked start button
                {
                    Texture2D boutonStartActivatedSprite = Content.Load<Texture2D>("boutonStart_activated");
                    boutonStart.setSprite(boutonStartActivatedSprite);
                    gameState = GameState.Playing;
                }

                if (mouseClickRect.Intersects(exitButtonRect)) //player clicked exit button
                {
                    Texture2D boutonExitActivatedSprite = Content.Load<Texture2D>("boutonExit_activated");
                    boutonExit.setSprite(boutonExitActivatedSprite);
                    Exit();
                }
            }
            if (gameState == GameState.Paused)
            {
                Rectangle resumeButtonRect = new Rectangle((int)boutonResume.getPositionX(), (int)boutonResume.getPositionY(), 145, 50);

                if (mouseClickRect.Intersects(resumeButtonRect)) //player clicked start button
                {
                    Texture2D boutonResumeActivatedSprite = Content.Load<Texture2D>("boutonResume_activated");
                    boutonResume.setSprite(boutonResumeActivatedSprite);
                    gameState = GameState.Playing;
                }
            }

        }
    }
}
