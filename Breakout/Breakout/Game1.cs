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
            Paused,
            Loading,
            LevelEditor
        }

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle screenBound = new Rectangle( 0, 0, WIN_WIDTH, WIN_HEIGHT );

        Texture2D backgroundPlay;
        Texture2D backgroundMenu;
        List<Brique> briques;
        Palette palette;
        Balle balle;
        Balle balleAnim;
        List<Heart> hearts;
        int lives;

        KeyboardState lastKeyboardState;

        Bouton boutonStart;
        Bouton boutonExit;
        Bouton boutonResume;
        Bouton boutonLevelEditor;
        WaitTime chiffre;
        float loadingTime = 1;
        bool pressed = false;
        private GameState gameState;

        MouseState mouseState;
        MouseState previousMouseState;

        Song mainMenuMusic;
        Song gameMusic;

        SoundEffectInstance soundEngineInstance;
        SoundEffect balleMur;
        SoundEffect ballePalette;
        SoundEffect explosionBrique;
        SoundEffect death;
        SoundEffect deathFinal;
        SoundEffect countdown;

        public Breakout()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = WIN_HEIGHT;
            graphics.PreferredBackBufferWidth = WIN_WIDTH;
            
            briques = new List<Brique>();
            hearts = new List<Heart>();
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

            backgroundPlay = Content.Load<Texture2D>("background_play");
            backgroundMenu = Content.Load<Texture2D>("background_menu");

            Texture2D paletteSprite = Content.Load<Texture2D>("palette");
            palette = new Palette(paletteSprite, screenBound);

            Texture2D briqueSprite = Content.Load<Texture2D>("brique");
            for (int i = 0; i < screenBound.Width; i += briqueSprite.Width)
                briques.Add(new Brique(briqueSprite, Color.LimeGreen, new Vector2(i, 100 + briqueSprite.Height), 1));
            for (int i = 0; i < screenBound.Width; i += briqueSprite.Width)
                briques.Add(new Brique(briqueSprite, Color.Fuchsia, new Vector2(i, 100), 1));
            for (int i = 0; i < screenBound.Width; i += briqueSprite.Width)
                briques.Add(new Brique(briqueSprite, Color.Orange, new Vector2(i, 100 - briqueSprite.Height), 1));
            for (int i = 0; i < screenBound.Width; i += briqueSprite.Width)
                briques.Add(new Brique(briqueSprite, Color.DodgerBlue, new Vector2(i, 100 - 2*briqueSprite.Height), 2));
            for (int i = 0; i < screenBound.Width; i += briqueSprite.Width)
                briques.Add(new Brique(briqueSprite, Color.DarkOrchid, new Vector2(i, 100 - 3*briqueSprite.Height), 1));
            for (int i = 0; i < screenBound.Width; i += briqueSprite.Width)
                briques.Add(new Brique(briqueSprite, Color.DeepPink, new Vector2(i, 100 - 4*briqueSprite.Height), 1));

            Texture2D balleSprite = Content.Load<Texture2D>("balle");
            balle = new Balle(balleSprite, screenBound, new Vector2(screenBound.Width / 2 - (balleSprite.Width), screenBound.Height - 70));

            Texture2D boutonStartSprite = Content.Load<Texture2D>("boutonStart");
            Texture2D boutonStartHighlighted = Content.Load<Texture2D>("boutonStart_highlighted");
            Texture2D boutonStartActivated = Content.Load<Texture2D>("boutonStart_activated");
            boutonStart = new Bouton(boutonStartSprite, boutonStartHighlighted, boutonStartActivated, (screenBound.Width / 4) - (boutonStartSprite.Width / 2), screenBound.Height - 100);
            boutonStart.Clicked += (s, e) => gameState = GameState.Loading;
            Texture2D boutonExitSprite = Content.Load<Texture2D>("boutonExit");
            Texture2D boutonExitHigh = Content.Load<Texture2D>("boutonExit_highlighted");
            Texture2D boutonExitAct = Content.Load<Texture2D>("boutonExit_Activated");
            boutonExit = new Bouton(boutonExitSprite, boutonExitHigh, boutonExitAct, 3*(screenBound.Width / 4) - (boutonStartSprite.Width / 2), screenBound.Height - 100);
            boutonExit.Clicked += (s, e) => Exit();
            Texture2D boutonResumeSprite = Content.Load<Texture2D>("boutonResume");
            Texture2D boutonResumeHigh = Content.Load<Texture2D>("boutonResume_Highlighted");
            Texture2D boutonResumeAct = Content.Load<Texture2D>("boutonResume_Activated");
            boutonResume = new Bouton(boutonResumeSprite, boutonResumeHigh, boutonResumeAct, screenBound.Width/2 - boutonResumeSprite.Width/2, screenBound.Height/2 - boutonResumeSprite.Height/2);
            boutonResume.Clicked += (s, e) => gameState = GameState.Loading;
            Texture2D chiffre3Sprite = Content.Load<Texture2D>("3");
            chiffre = new WaitTime(chiffre3Sprite, screenBound);

            Texture2D balleAnimSprite = Content.Load<Texture2D>("balle");
            balleAnim = new Balle(balleSprite, screenBound, new Vector2(screenBound.Width / 2 - (balleSprite.Width), screenBound.Height - 70));

            lives = 3;
            Texture2D heartSprite = Content.Load<Texture2D>("heart");
            for (int i = 0; i < lives * (heartSprite.Width + 5); i += heartSprite.Width + 5)
                hearts.Add(new Heart(heartSprite, new Vector2(i + 5, screenBound.Height - 30)));

            balleMur = Content.Load<SoundEffect>("HitWall");
            ballePalette = Content.Load<SoundEffect>("HitPalette");
            explosionBrique = Content.Load<SoundEffect>("Explosion");
            death = Content.Load<SoundEffect>("Death");
            deathFinal = Content.Load<SoundEffect>("DeathFinal");
            countdown = Content.Load<SoundEffect>("321");

            mainMenuMusic = Content.Load<Song>("mainMenuMusic");
            gameMusic = Content.Load<Song>("gameMusic");
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
            // TODO: Add your update logic here
            KeyboardState keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.Escape))
                this.Exit();

            if (gameState == GameState.StartMenu)
            {

                UpdateStartMenu(keyboardState, mouseState);
            }

            if (gameState == GameState.Loading)
            {
                UpdateLoading(gameTime);
            }

            if (gameState == GameState.Paused)
            {
                UpdatePause(keyboardState, mouseState);
            }

            if (gameState == GameState.Playing)
            {
                UpdatePlaying(gameTime, keyboardState);
            }
            if (gameState == GameState.LevelEditor)
            {
                UpdateLevelEditor(keyboardState, mouseState);
            }

            //Doit absolument etre apres tous les verifications du clavier
            lastKeyboardState = keyboardState;

            
            if (previousMouseState.LeftButton == ButtonState.Pressed &&
                mouseState.LeftButton == ButtonState.Released)
            {
               // MouseClicked(mouseState.X, mouseState.Y);
            }

            previousMouseState = mouseState;

            base.Update(gameTime);
        }

        private void UpdateLevelEditor(KeyboardState state, MouseState mouseState)
        {
            throw new NotImplementedException();
        }

        private void UpdatePlaying(GameTime gameTime, KeyboardState state)
        {
            if (MediaPlayer.State == MediaState.Playing)
            {
                MediaPlayer.Volume = 0.20f;
            }
            if (MediaPlayer.State == MediaState.Stopped)
            {
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Volume = 0.25f;
                MediaPlayer.Play(gameMusic);
            }

            if (balle.getEnable() == false)
            {
                balle.setPositionX(palette.getPositionX() + 40);
            }

            // Vérification si balle sort par le bas de l'écran
            soundEngineInstance = death.CreateInstance();
            if (balle.getPositionY() > screenBound.Bottom)
            {
                lives -= 1;
                hearts[lives] = null;
                hearts.Remove(hearts[lives]);
                if (lives == 0)
                {
                    LoadContent();
                }
                Texture2D balleSprite = Content.Load<Texture2D>("balle");
                balle = new Balle(balleSprite, screenBound, new Vector2(screenBound.Width / 2 - 10, screenBound.Height - 70));
                palette.returnToStart();
                balle.setEnable(false);

                soundEngineInstance.Volume = 0.50f;
                soundEngineInstance.Play();
            }


            if (state.IsKeyDown(Keys.Enter))
            {
                pressed = true;
                gameState = GameState.Paused;
            }

            // Update la position de la palette
            palette.Update(state);

            // Update position de la balle + vérification collision avec mur
            soundEngineInstance = balleMur.CreateInstance();
            balle.Update(state, gameTime, soundEngineInstance);

            // Vérification collision balle avec palette
            soundEngineInstance = ballePalette.CreateInstance();
            balle.checkPaddleCollision(palette.getLocation(), soundEngineInstance);

            // Vérification collision balle avec briques
            soundEngineInstance = explosionBrique.CreateInstance();
            for (int i = 0; i < briques.Count; ++i)
            {
                if (balle.checkBrickCollision(briques[i].getLocation()))
                {
                    briques[i].setHp(briques[i].getHp() - 1);
                    if (briques[i].getHp() == 0)
                    {
                        briques[i] = null;
                        briques.Remove(briques[i]);

                        soundEngineInstance.Volume = 0.50f;
                        soundEngineInstance.Play();
                    }
                    else
                    {
                        briques[i].setColor(Color.LightBlue);
                    }
                    if (briques.Count == 0)
                    {
                        LoadContent();
                    }
                }
            }
        }

        private void UpdatePause(KeyboardState kState, MouseState mState)
        {
            MediaPlayer.Volume = 0.10f;

            boutonResume.Update(kState, mState);

            if (kState.IsKeyDown(Keys.Back))
            {
                gameState = GameState.Loading;
            }
        }

        private void UpdateLoading(GameTime gameTime)
        {
            if (soundEngineInstance == null || soundEngineInstance.State == SoundState.Stopped)
            {
                soundEngineInstance = countdown.CreateInstance();
                soundEngineInstance.Volume = 0.75f;
                soundEngineInstance.Play();
            }

            Texture2D chiffre1Sprite = Content.Load<Texture2D>("1");
            Texture2D chiffre2Sprite = Content.Load<Texture2D>("2");
            Texture2D chiffre3Sprite = Content.Load<Texture2D>("3");
            loadingTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (loadingTime <= 0)
            {
                if (chiffre.getSprite() == chiffre3Sprite)
                {
                    chiffre.setSprite(chiffre2Sprite);
                    loadingTime = 1;
                }
                else if (chiffre.getSprite() == chiffre2Sprite)
                {
                    chiffre.setSprite(chiffre1Sprite);
                    loadingTime = 1;
                }
                else if (chiffre.getSprite() == chiffre1Sprite)
                {
                    gameState = GameState.Playing;
                    loadingTime = 1;
                    chiffre.setSprite(chiffre3Sprite);
                }
            }
        }

        private void UpdateStartMenu(KeyboardState kState, MouseState mState)
        {
            if (MediaPlayer.State == MediaState.Stopped)
            {
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Play(mainMenuMusic);
            }

            if (kState.IsKeyDown(Keys.Enter))
            {
                MediaPlayer.Stop();
                pressed = true;
                gameState = GameState.Loading;
            }

            boutonExit.Update(kState, mState);
            boutonStart.Update(kState, mState);

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
                spriteBatch.Draw(backgroundMenu,
                 new Rectangle(0, 0, screenBound.Width,
                 screenBound.Height), null,
                 Color.White, 0, Vector2.Zero,
                 SpriteEffects.None, 0);
                boutonStart.Draw(spriteBatch);
                boutonExit.Draw(spriteBatch);

                balleAnim.Draw(spriteBatch);
            }

            if (gameState == GameState.Loading)
            {
                chiffre.Draw(spriteBatch);
            }

            if (gameState == GameState.Paused)
            {
                boutonResume.Draw(spriteBatch);
            }

            if (gameState == GameState.Playing)
            {
                // TODO: changer l'emplacement une fois que notre taille de fen�tre est d�cid�
                spriteBatch.Draw(backgroundPlay,
                 new Rectangle(0, 0, screenBound.Width,
                 screenBound.Height), null,
                 Color.White, 0, Vector2.Zero,
                 SpriteEffects.None, 0);

                // TODO: changer l'emplacement une fois que notre taille de fenêtre est décidé
                palette.Draw(spriteBatch);
                balle.Draw(spriteBatch);

                foreach (Brique brique in briques)
                {
                    if (brique != null)
                        brique.Draw(spriteBatch);
                }

                foreach (Heart heart in hearts)
                {
                    if (heart != null)
                        heart.Draw(spriteBatch);
                }
            }

            spriteBatch.End();
            
            base.Draw(gameTime);
        }

        /*
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
                    MediaPlayer.Stop();
                    gameState = GameState.Loading;
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
                    gameState = GameState.Loading;
                }
            }

        }*/
    }
}
