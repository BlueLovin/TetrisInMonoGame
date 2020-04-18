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

		static bool collision = false;


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
			var timer = new System.Threading.Timer(callback, null, 0, 150);//TIMER!!!
			Tetromino.PieceList.Add(new Tetromino(0, -10));
			Tetromino.PieceList.Add(new Tetromino(0, -10));

			NewPiece();
		}

		private static void TimerTick(Object StateInfo)
		{
			MouseState ms = Mouse.GetState();
			if (ms.LeftButton == ButtonState.Pressed)
			{
				NewPiece();
			}
			int CurrentCount = (Tetromino.PieceList.Count);
			collision = false;
			int counter = 0;
			for (int i = CurrentCount - 4; i < CurrentCount; i++)//check current piece
			{
				if (collision && counter > 3 || Tetromino.PieceList[i].Y == 19)//if we have iterated over each current tile
				{
					break;
				}
				else
				{
					counter++;
					Tetromino.PieceList[i].Y++;
				}
				for (int j = 0; j <= Tetromino.PieceList.Count - 4; j++)//then scan for collision against every other piece
				{
					if (Tetromino.PieceList[i].Y == Tetromino.PieceList[j].Y - 1
						&& Tetromino.PieceList[i].X == Tetromino.PieceList[j].X
						&& Tetromino.PieceList[i].Y != 0
						|| Tetromino.PieceList[i].Y == 19)//COLLISION WITH ALL PIECES
					{
						collision = true;
						NewPiece();
					}
					if (collision)
						break;
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
				if (i >= Tetromino.PieceList.Count - 4)//IF CURRENT FALLING PIECE
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
			int typeInt = /*random.Next(0, 3)*/1;
			if (typeInt == 0)//I
			{
				Tetromino.PieceList.Add(new Tetromino(4,0));
				Tetromino.PieceList.Add(new Tetromino(4,1));
				Tetromino.PieceList.Add(new Tetromino(4,2));
				Tetromino.PieceList.Add(new Tetromino(4,3));
			}
			if (typeInt == 1)//L =============== PROBLEMS!
			{
				Tetromino.PieceList.Add(new Tetromino(4, 0));
				Tetromino.PieceList.Add(new Tetromino(4, 1));
				Tetromino.PieceList.Add(new Tetromino(4, 2));
				Tetromino.PieceList.Add(new Tetromino(5, 2));
			}
			if (typeInt == 2)//T
			{
				Tetromino.PieceList.Add(new Tetromino(5, 0));
				Tetromino.PieceList.Add(new Tetromino(4, 1));
				Tetromino.PieceList.Add(new Tetromino(5, 1));
				Tetromino.PieceList.Add(new Tetromino(6, 1));
			}

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
