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
        //static List<Tetromino> PieceList = new List<Tetromino>();
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        static KeyboardState oldKeyboardState;
        static Random random = new Random();
        static int row19;

        Texture2D background;
        Texture2D tile;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            base.Initialize();
            base.Window.Title = "Tetris by Matthew Jury";
            graphics.PreferredBackBufferWidth = 250;
            graphics.PreferredBackBufferHeight = 500;
            this.IsMouseVisible = true;
            graphics.ApplyChanges();
            var callback = new TimerCallback(TimerTick);
            var timer = new System.Threading.Timer(callback, null, 0,75);//TIMER!!!
            Tetromino.PieceList.Add(new Tetromino());
            Tetromino.PieceList[0].X = -5;
            NewPiece();
        }

        private static void TimerTick(Object StateInfo)
        {
            MouseState ms = Mouse.GetState();
            if (ms.LeftButton == ButtonState.Pressed)
            {
                NewPiece();
            }
            int currentPieceIndex = (Tetromino.PieceList.Count - 1);
            for (int i = 1; i < Tetromino.PieceList.Count; i++)
            {
                if (Tetromino.PieceList[i].Y == Tetromino.PieceList[currentPieceIndex].Y + 1
                    && Tetromino.PieceList[i].X == Tetromino.PieceList[currentPieceIndex].X
                    || Tetromino.PieceList[currentPieceIndex].Y == 19)//COLLISION WITH ALL PIECES
                {
                    NewPiece();
                    break;
                }
                if (i == currentPieceIndex)
                {
                    if (Tetromino.PieceList[i].Y < 19)
                    {
                        Tetromino.PieceList[i].Y++;
                    }
                }
            }
            
        }
        private static void CheckLine()
        {
            row19 = 0;
            foreach (Tetromino piece in Tetromino.PieceList)
            {
                if (piece.Y == 19)
                {
                    row19++;
                }
            }

            if (row19 == 10)
            {
                LineClear(19);
            }
        }
        private static void LineClear(int Row)
        {
            for (int i = 0; i < Tetromino.PieceList.Count; i++)
            {
                if (Tetromino.PieceList[i].Y == Row)
                {
                    Tetromino.PieceList.RemoveAt(i);
                }
            }
            foreach (Tetromino piece in Tetromino.PieceList)
            {
                piece.Y++;
            }
        }
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>("background");
            tile = Content.Load<Texture2D>("tile");

        }
        protected override void UnloadContent()
        {
        }
        protected override void Update(GameTime gameTime)
        {
            KeyboardState OldKeyboardState;
            KeyboardState NewKeyboardState = Keyboard.GetState();
            for (int i = 0; i < Tetromino.PieceList.Count; i++)
            {
                if (i == Tetromino.PieceList.Count - 1)//IF CURRENT FALLING PIECE
                {
                    //===============CONTROLS===============
                    if (NewKeyboardState.IsKeyUp(Keys.Right) 
                        && oldKeyboardState.IsKeyDown(Keys.Right)
                        && Tetromino.PieceList[i].X < 9)//window bounds
                    {
                        Tetromino.PieceList[i].X++;
                    }
                    if (NewKeyboardState.IsKeyUp(Keys.Left)
                        && oldKeyboardState.IsKeyDown(Keys.Left)
                        && Tetromino.PieceList[i].X > 0)//window bounds
                    {
                        Tetromino.PieceList[i].X--;
                    }
                }
            }
            base.Update(gameTime);
            oldKeyboardState = NewKeyboardState;
        }
        private static void NewPiece()
        {//================================TODO: ADD DIFFERENT TYPES OF SHAPES=====================
            int typeInt = random.Next(0,1);
            if (typeInt == 0)//I
            {
                
            }
            Tetromino.PieceList.Add(new Tetromino());
            CheckLine();
            ResetVals();
        }
        private static void ResetVals()
        {
            row19 = 0;
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            foreach (Tetromino piece in Tetromino.PieceList)
            {
                spriteBatch.Draw(tile, new Vector2(piece.X * 25, piece.Y * 25), Color.White);
            }
            //spriteBatch.Draw(background, new Vector2(0,0), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
