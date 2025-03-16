using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using XNADash.Textures;
using XNADash.Levels;
using System.IO;
using XNADash.Sound;

namespace XNADash
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class DashGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        DashBoard             board;
        SpriteBatch           spriteBatch;
        SpriteFont            defaultSpriteFont;
        SpriteFont            smallSpriteFont;

        const int STATUSSIZE   = 30;
        const int FrameSkip    = 8;
        int FrameNumber        = 0;

        bool HelpVisible = false;

        public DashGame()
        {
            graphics = new GraphicsDeviceManager( this );

            graphics.PreferredBackBufferWidth  = GetWindowSizeX;
            graphics.PreferredBackBufferHeight = GetWindowSizeY + STATUSSIZE;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
            Window.Title          = "XNADash 0.3 (c) 2011-2025";
        }

        public int GetWindowSizeX
        {
            get
            {
                return DashBoard.BLOCKSIZE * DashBoard.BOARDSIZEX;
            }
        }

        public int GetWindowSizeY
        {
            get
            {
                return DashBoard.BLOCKSIZE * DashBoard.BOARDSIZEY;
            }
        }


        int _currentLevelNumber = 0;
        public int CurrentLevelNumber
        {
            get
            {
                return _currentLevelNumber;
            }
            set
            {
                _currentLevelNumber = value;
                SoundFactory.Instance.PlaySong(this.Content, CurrentLevelNumber );
            }
        }


        public static string ExecutableDirectory
        {
            get
            {
                string ExePath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                return Path.GetDirectoryName( ExePath );
            }
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
            spriteBatch = new SpriteBatch( GraphicsDevice );

            defaultSpriteFont = this.Content.Load<SpriteFont>( "DefaultSpriteFont" );
            smallSpriteFont = this.Content.Load<SpriteFont>( "smallSpriteFont" );

            // force the mp3 to be played
            CurrentLevelNumber = 0;
            this.ReloadBoard();

            new TextureFactory( this.GraphicsDevice );

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        KeyboardState prevState = Keyboard.GetState();

        BoardBlocks.Directions playerKnock = BoardBlocks.Directions.None;
        BoardBlocks.Directions playerDirection = BoardBlocks.Directions.None;

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update( GameTime gameTime )
        {
            // Allows the game to exit
            KeyboardState state = Keyboard.GetState();

            playerDirection = BoardBlocks.Directions.None;

            // warunki na zakoñczenie gry lub planszy
            if ( board.MustRestart )
                this.ReloadBoard();
            if ( board.Completed )
                this.MoveToNextBoard();
            if ( state.IsKeyDown( Keys.Escape ) )
                board.ExplodePlayer();

            // obs³uga klawiatury - pyk
            if ( !state.IsKeyDown( Keys.Space ) && !prevState.IsKeyDown( Keys.Left ) && state.IsKeyDown( Keys.Left ) )
                playerKnock = BoardBlocks.Directions.W;
            if ( !state.IsKeyDown( Keys.Space ) && !prevState.IsKeyDown( Keys.Right ) && state.IsKeyDown( Keys.Right ) )
                playerKnock = BoardBlocks.Directions.E;
            if ( !state.IsKeyDown( Keys.Space ) && !prevState.IsKeyDown( Keys.Up ) && state.IsKeyDown( Keys.Up ) )
                playerKnock = BoardBlocks.Directions.N;
            if ( !state.IsKeyDown( Keys.Space ) && !prevState.IsKeyDown( Keys.Down ) && state.IsKeyDown( Keys.Down ) )
                playerKnock = BoardBlocks.Directions.S;

            // obs³uga klawiatury - player
            if ( state.IsKeyDown( Keys.F10 ) )
                this.Exit();
            if ( !prevState.IsKeyDown( Keys.Left ) &&
                 state.IsKeyDown( Keys.Space ) && state.IsKeyDown( Keys.Left ) )
                this.MoveToPrevBoard();
            if ( !prevState.IsKeyDown( Keys.Right ) &&
                 state.IsKeyDown( Keys.Space ) && state.IsKeyDown( Keys.Right ) )
                this.MoveToNextBoard();
            if ( state.IsKeyDown( Keys.F1 ) && !prevState.IsKeyDown( Keys.F1 ) )
                HelpVisible = !HelpVisible;

            // obs³uga klawiatury - przejscia miêdzy levelami
            if ( !state.IsKeyDown( Keys.Space ) )
            {
                if ( state.IsKeyDown( Keys.Left ) )
                    playerDirection = BoardBlocks.Directions.W;
                else
                    if ( state.IsKeyDown( Keys.Right ) )
                        playerDirection = BoardBlocks.Directions.E;
                    else
                        if ( state.IsKeyDown( Keys.Up ) )
                            playerDirection = BoardBlocks.Directions.N;
                        else
                            if ( state.IsKeyDown( Keys.Down ) )
                                playerDirection = BoardBlocks.Directions.S;
            }

            // update œwiata co 6 ramek
            FrameNumber++;
            if ( FrameNumber >= FrameSkip )
            {
                FrameNumber = 0;

                board.UpdateBoard( gameTime, state );

                if ( playerDirection != BoardBlocks.Directions.None )
                {
                    board.UpdatePlayer( playerDirection );
                    playerDirection = BoardBlocks.Directions.None;
                    playerKnock = BoardBlocks.Directions.None;
                }
                else
                    if ( playerKnock != BoardBlocks.Directions.None )
                    {
                        board.UpdatePlayer( playerKnock );
                        playerKnock = BoardBlocks.Directions.None;
                    }

                SoundFactory.Instance.PlayEffect();
            }

            base.Update( gameTime );

            prevState = state;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw( GameTime gameTime )
        {
            GraphicsDevice.Clear( Color.Black );

            spriteBatch.Begin();
            board.DrawBoard( spriteBatch, defaultSpriteFont );
            this.DrawStatus();
            spriteBatch.End();

            base.Draw( gameTime );
        }

        private void DrawStatus()
        {
            var statusText = string.Format( "Hearts:{0:00}/{1:00}", this.board.HeartsEaten, this.board.HeartsToComplete );
            var statusSize = defaultSpriteFont.MeasureString( statusText );

            spriteBatch.DrawString( defaultSpriteFont, statusText, new Vector2( GetWindowSizeX - statusSize.X, GetWindowSizeY - 2 ), Color.Yellow );

            var levelName   = string.Format( "{2} [{0}/{1}] ", CurrentLevelNumber+1, LevelFactory.Instance.Levels.Count(), board.LevelName );
            var levelAuthor = string.Format( "by {0}", board.LevelAuthor );

            spriteBatch.DrawString( smallSpriteFont, levelName, new Vector2( 0, GetWindowSizeY  ), Color.White );
            spriteBatch.DrawString( smallSpriteFont, levelAuthor, new Vector2( 0, GetWindowSizeY + 12 ), Color.White );

            if ( HelpVisible )
                DrawHelp();
        }

        private void DrawHelp()
        {
            string[] HelpData = 
                new[] 
                {
                    "XNADash",
                    "a Heartlight clone",
                    "F1 - Toggle help on/off",
                    "F10 - exit game",
                    "Arrow keys - move",
                    "Spc+L/R - prev/next level",
                    "Escape - restart level",
                };

            int index = 0;
            foreach ( var helpString in HelpData )
            {
                spriteBatch.DrawString( defaultSpriteFont, helpString, new Vector2( 2 + 0, 2 + ( ( index ) * 35 ) ), Color.Black );
                spriteBatch.DrawString( defaultSpriteFont, helpString, new Vector2( 0, ( index ) * 35 ), Color.White );
                index++;
            }
        }

        private void ReloadBoard()
        {
            LevelFactory.Instance.Reset();
            board = LevelFactory.Instance.Levels.ElementAt( CurrentLevelNumber );
        }

        private void MoveToNextBoard()
        {
            CurrentLevelNumber++;
            if ( CurrentLevelNumber >= LevelFactory.Instance.Levels.Count() )
                CurrentLevelNumber = 0;

            ReloadBoard();
        }

        private void MoveToPrevBoard()
        {
            CurrentLevelNumber--;
            if ( CurrentLevelNumber < 0 )
                CurrentLevelNumber = LevelFactory.Instance.Levels.Count()-1;

            ReloadBoard();
        }
    }
}
