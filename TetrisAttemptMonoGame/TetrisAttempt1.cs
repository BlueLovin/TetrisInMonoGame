using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Timers;
using System;
using System.Collections.Generic;
using System.Threading;

namespace TetrisAttemptMonoGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        static List<Tetromino> Tetrominos = new List<Tetromino>();
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Tetromino tetromino = new Tetromino();
        static KeyboardState oldKeyboardState;

        Texture2D background;
        public Texture2D tile;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            base.Window.Title = "Tetris by Matthew Jury";
            graphics.PreferredBackBufferWidth = 790;
            graphics.PreferredBackBufferHeight = 650;
            this.IsMouseVisible = true;
            graphics.ApplyChanges();
            var callback = new TimerCallback(TimerTick);
            var timer = new System.Threading.Timer(callback, null, 0,200);
        }

        private static void TimerTick(Object StateInfo)
        {
            for (int i = 1; i < Tetrominos.Count - 1; i++)
            {
                if (Tetrominos[i].Y < 22)
                {
                    Tetrominos[i].Y++;
                }


                if (i == Tetrominos.Count - 1) 
                {
                    if (Tetrominos[i - 1].Y + 1 == Tetrominos[i].Y)
                    {
                        NewPiece();
                        break;
                    }
                    if (Tetrominos[i].Y == 22)
                    {
                        NewPiece();
                    }
                    
                }
            }
        }
        private static void updateInput()
        {
            KeyboardState NewKeyboardState = Keyboard.GetState();
            for(int i = 1; i < Tetrominos.Count; i++)
            {
                if (i == Tetrominos.Count - 2)
                {
                    if (Tetrominos[i].Y == Tetrominos[i - 1].Y
                        && Tetrominos[i].X == Tetrominos[i - 1].X)
                    {
                        Tetrominos.Add(new Tetromino());
                        Tetrominos[i].Y -= 1;
                        
                        //break;
                    }
                    if (NewKeyboardState.IsKeyDown(Keys.Left)
                    && oldKeyboardState.IsKeyUp(Keys.Left)
                    && Tetrominos[i].X > 12)
                    {
                        Tetrominos[i].X--;
                    }
                    if (NewKeyboardState.IsKeyDown(Keys.Right)
                    && oldKeyboardState.IsKeyUp(Keys.Right)
                    && Tetrominos[i].X < 21)
                    {
                        Tetrominos[i].X++;
                    }
                    
                }
                //else
                //{
                //    NewPiece();
                //}
            }
            
            
            oldKeyboardState = NewKeyboardState;

        }
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            tile = Content.Load<Texture2D>("tile");
            background = Content.Load<Texture2D>("background");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 
        
        protected override void Update(GameTime gameTime)
        {
            MouseState MS = Mouse.GetState();
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (MS.LeftButton == ButtonState.Pressed)
                NewPiece();
            updateInput();
            


            // TODO: Add your update logic here
            
            base.Update(gameTime);
        }
        private static void NewPiece()
        {
            Tetrominos.Add(new Tetromino());
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            if (Tetrominos.Count > 1) {
                for (int i = 0; i < Tetrominos.Count; i++)
                {
                    spriteBatch.Draw(tile, new Vector2(Tetrominos[i].X * 25, Tetrominos[i].Y * 25), Color.Orange);
                }
            }
            //spriteBatch.Draw(background, new Vector2(0,0), Color.White);
            // TODO: Add your drawing code here




            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
