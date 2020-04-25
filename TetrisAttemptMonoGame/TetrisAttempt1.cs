﻿using Microsoft.Xna.Framework;
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
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		static bool linecleared;

		static KeyboardState oldKeyboardState;
		static Random random = new Random();

		static Tetromino.type CurrentPieceType;
		static int CurrentRotation = 0;

		static TimerCallback callback = new TimerCallback(TimerTick);
		public static System.Threading.Timer timer = new System.Threading.Timer(callback, null, 0, 50);//TIMER!!!


		static bool collision = false;
		static bool LeftCollision = false;
		static bool RightCollision = false;


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
			Tetromino.PieceList.Add(new Tetromino(-10,-99999999));

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
				counter++;
				if (counter <= 4 && i >= 0)//TIMER TICK MOVEMENT
				{
					Tetromino.PieceList[i].Y++;
				}
				for (int j = 0; j < Tetromino.PieceList.Count - 4; j++)//then scan for collision against every other piece
				{
					if (Tetromino.PieceList[i].Y == Tetromino.PieceList[j].Y - 1
						&& Tetromino.PieceList[i].X == Tetromino.PieceList[j].X
						|| Tetromino.PieceList[i].Y == 19)//COLLISION WITH ALL PIECES
					{
						collision = true;
						break;
					}
				}
			}
			if (collision)
			{
				CheckLine();
				NewPiece();
			}
		}
		/// <summary>
		/// disgusting verbose code. checking each row to see
		/// if there are ten values of the same on each row.
		/// </summary>
		private static void CheckLine()
		{
			Tetromino.ResetVals();
			for (int i = 0; i <= Tetromino.PieceList.Count - 1; i++)
			{
				Tetromino piece = Tetromino.PieceList[i];
				if (piece.Y == 1)
					Tetromino.row1Count++;
				if (piece.Y == 2)
					Tetromino.row2Count++;
				if (piece.Y == 3)
					Tetromino.row3Count++;
				if (piece.Y == 4)
					Tetromino.row4Count++;
				if (piece.Y == 5)
					Tetromino.row5Count++;
				if (piece.Y == 6)
					Tetromino.row6Count++;
				if (piece.Y == 7)
					Tetromino.row7Count++;
				if (piece.Y == 8)
					Tetromino.row8Count++;
				if (piece.Y == 9)
					Tetromino.row9Count++;
				if (piece.Y == 10)
					Tetromino.row10Count++;
				if (piece.Y == 11)
					Tetromino.row11Count++;
				if (piece.Y == 12)
					Tetromino.row12Count++;
				if (piece.Y == 13)
					Tetromino.row13Count++;
				if (piece.Y == 14)
					Tetromino.row14Count++;
				if (piece.Y == 15)
					Tetromino.row15Count++;
				if (piece.Y == 16)
					Tetromino.row16Count++;
				if (piece.Y == 17)
					Tetromino.row17Count++;
				if (piece.Y == 18)
					Tetromino.row18Count++;
				if (piece.Y == 19)
					Tetromino.row19Count++;


				if (Tetromino.row1Count == 10)
					LineClear(1);
				if (Tetromino.row2Count == 10)
					LineClear(2);
				if (Tetromino.row3Count == 10)
					LineClear(3);
				if (Tetromino.row4Count == 10)
					LineClear(4);
				if (Tetromino.row5Count == 10)
					LineClear(5);
				if (Tetromino.row6Count == 10)
					LineClear(6);
				if (Tetromino.row7Count == 10)
					LineClear(7);
				if (Tetromino.row8Count == 10)
					LineClear(8);
				if (Tetromino.row9Count == 10)
					LineClear(9);
				if (Tetromino.row10Count == 10)
					LineClear(10);
				if (Tetromino.row11Count == 10)
					LineClear(11);
				if (Tetromino.row12Count == 10)
					LineClear(12);
				if (Tetromino.row13Count == 10)
					LineClear(13);
				if (Tetromino.row14Count == 10)
					LineClear(14);
				if (Tetromino.row15Count == 10)
					LineClear(15);
				if (Tetromino.row16Count == 10)
					LineClear(16);
				if (Tetromino.row17Count == 10)
					LineClear(17);
				if (Tetromino.row18Count == 10)
					LineClear(18);
				if (Tetromino.row19Count == 10)
					LineClear(19);
			}
			Tetromino.ResetVals();
		}
		private static void LineClear(int Row)
		{
			Tetromino.ResetVals();
			for (int i = 0; i < Tetromino.PieceList.Count; i++)
			{
				if (Tetromino.PieceList[i].Y == Row)
				{
					Tetromino.PieceList.RemoveAt(i);
				}
			}
			foreach (Tetromino piece in Tetromino.PieceList)
			{
				if(piece.Y <= Row)
					piece.Y++;
			}
			linecleared = true;

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
			Tetromino.ResetVals();
			
			collision = false;

			KeyboardState OldKeyboardState;
			KeyboardState NewKeyboardState = Keyboard.GetState();

		/**/////////////////////////////////////////////////////////////////
		/**/////////////////////////ROTATE CALLED///////////////////////////
		/**/////////////////////////////////////////////////////////////////
		/**/if (NewKeyboardState.IsKeyUp(Keys.RightShift)///////////////////
		/**/	&& oldKeyboardState.IsKeyDown(Keys.RightShift))			 ///
		/**/{															 ///
		/**/	RotatePiece(CurrentPieceType);//////////////////////////////
		/**/}                                                            ///
			/**/////////////////////////////////////////////////////////////////
			/**/////////////////////////////////////////////////////////////////
			/**/////////////////////////////////////////////////////////////////
			LeftCollision = false;
			RightCollision = false;
			for (int j = Tetromino.PieceList.Count - 4; j <= Tetromino.PieceList.Count - 1; j++)
			{
				if (Tetromino.PieceList[j].X == 0)
				{
					LeftCollision = true;
					break;
				}
				if (Tetromino.PieceList[j].X == 9)
				{
					RightCollision = true;
					break;
				}
			}
			for (int i = 0; i < Tetromino.PieceList.Count; i++)
			{
				if (i > Tetromino.PieceList.Count - 5)//IF CURRENT FALLING PIECE
				{
					
					
					
					//===============CONTROLS===============
					if (NewKeyboardState.IsKeyUp(Keys.Right)
						&& oldKeyboardState.IsKeyDown(Keys.Right)
						&& !RightCollision)//window bounds
					{
						 Tetromino.PieceList[i].X++;
					}
					if (NewKeyboardState.IsKeyUp(Keys.Left)
						&& oldKeyboardState.IsKeyDown(Keys.Left)
						&& !LeftCollision)//window bounds
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
			timer.Change(1000, 100);
			CheckLine();
			if (linecleared) 
			{
				Thread.Sleep(200);
				CheckLine();
				if (linecleared)
				{
					Thread.Sleep(200);
					CheckLine();
					if (linecleared)
					{
						Thread.Sleep(200);
						CheckLine();
					}
				}
			}
			linecleared = false;
			collision = false;
			int typeInt = random.Next(0, 3);
			//int typeInt = 0;
			CurrentRotation = 0;
			if (typeInt == 0)//I
			{
				CurrentPieceType = Tetromino.type.I;
				Tetromino.PieceList.Add(new Tetromino(4,0));
				Tetromino.PieceList.Add(new Tetromino(4,1));
				Tetromino.PieceList.Add(new Tetromino(4,2));
				Tetromino.PieceList.Add(new Tetromino(4,3));
			}
			else if (typeInt == 1)//L
			{
				CurrentPieceType = Tetromino.type.L;
				Tetromino.PieceList.Add(new Tetromino(4, 0));
				Tetromino.PieceList.Add(new Tetromino(4, 1));
				Tetromino.PieceList.Add(new Tetromino(4, 2));
				Tetromino.PieceList.Add(new Tetromino(5, 2));
			}
			else if (typeInt == 2)//T
			{
				CurrentPieceType = Tetromino.type.T;
				Tetromino.PieceList.Add(new Tetromino(5, 0));
				Tetromino.PieceList.Add(new Tetromino(4, 1));
				Tetromino.PieceList.Add(new Tetromino(5, 1));
				Tetromino.PieceList.Add(new Tetromino(6, 1));
			}
		}
		private static void RotatePiece(Tetromino.type CurrentType)
		{
			int currentPieceCount = Tetromino.PieceList.Count;

			if (CurrentType == Tetromino.type.I)
			{
				if (CurrentRotation == 0)
				{
					Tetromino.PieceList[currentPieceCount - 1].X += 2;
					Tetromino.PieceList[currentPieceCount - 1].Y -= 2;
					Tetromino.PieceList[currentPieceCount - 2].X++;
					Tetromino.PieceList[currentPieceCount - 2].Y--;
					Tetromino.PieceList[currentPieceCount - 4].X--;
					Tetromino.PieceList[currentPieceCount - 4].Y++;
					CurrentRotation = 90;
					goto BreakOut;
				}
				if (CurrentRotation == 90)
				{
					Tetromino.PieceList[currentPieceCount - 1].X -= 2;
					Tetromino.PieceList[currentPieceCount - 1].Y += 2;
					Tetromino.PieceList[currentPieceCount - 2].X--;
					Tetromino.PieceList[currentPieceCount - 2].Y++;
					Tetromino.PieceList[currentPieceCount - 4].X++;
					Tetromino.PieceList[currentPieceCount - 4].Y--;

					CurrentRotation = 0;
					goto BreakOut;
				}
			}
			if (CurrentType == Tetromino.type.L)
			{
				if (CurrentRotation == 0)
				{
					Tetromino.PieceList[currentPieceCount - 1].X -= 2;
					Tetromino.PieceList[currentPieceCount - 1].Y--;
					Tetromino.PieceList[currentPieceCount - 2].X--;
					Tetromino.PieceList[currentPieceCount - 2].Y -= 2;
					Tetromino.PieceList[currentPieceCount - 3].Y--;
					Tetromino.PieceList[currentPieceCount - 3].X++;

					CurrentRotation = 90;
					goto BreakOut;
				}
				if (CurrentRotation == 90)
				{

					Tetromino.PieceList[currentPieceCount - 1].Y--;
					Tetromino.PieceList[currentPieceCount - 2].X++;
					Tetromino.PieceList[currentPieceCount - 3].Y++;
					Tetromino.PieceList[currentPieceCount - 3].X--;
					Tetromino.PieceList[currentPieceCount - 4].Y += 2;

					CurrentRotation = 180;

					goto BreakOut;
				}
				if (CurrentRotation == 180)
				{

					Tetromino.PieceList[currentPieceCount - 1].X += 2;
					Tetromino.PieceList[currentPieceCount - 2].X++;
					Tetromino.PieceList[currentPieceCount - 2].Y++;

					Tetromino.PieceList[currentPieceCount - 4].X--;
					Tetromino.PieceList[currentPieceCount - 4].Y--;


					CurrentRotation = 270;

					goto BreakOut;
				}
				if (CurrentRotation == 270)
				{

					Tetromino.PieceList[currentPieceCount - 1].Y++;
					Tetromino.PieceList[currentPieceCount - 2].X--;
					Tetromino.PieceList[currentPieceCount - 3].Y--;

					Tetromino.PieceList[currentPieceCount - 4].X++;
					Tetromino.PieceList[currentPieceCount - 4].Y -= 2;


					CurrentRotation = 0;

					goto BreakOut;
				}
			}
			if (CurrentType == Tetromino.type.T)
			{
				if(CurrentRotation == 0)
				{
					Tetromino.PieceList[currentPieceCount - 1].Y++;
					Tetromino.PieceList[currentPieceCount - 1].X -= 2;
					Tetromino.PieceList[currentPieceCount - 2].X--;
					Tetromino.PieceList[currentPieceCount - 3].Y--;
					Tetromino.PieceList[currentPieceCount - 4].Y++;

					CurrentRotation = 90;
					goto BreakOut;
				}
				if (CurrentRotation == 90)
				{
					Tetromino.PieceList[currentPieceCount - 1].Y -= 2;
					Tetromino.PieceList[currentPieceCount - 2].X++;
					Tetromino.PieceList[currentPieceCount - 2].Y--;
					Tetromino.PieceList[currentPieceCount - 3].X += 2;

					CurrentRotation = 180;
					goto BreakOut;
				}
				if (CurrentRotation == 180)
				{
					Tetromino.PieceList[currentPieceCount - 1].X += 2;
					Tetromino.PieceList[currentPieceCount - 2].X++;
					Tetromino.PieceList[currentPieceCount - 2].Y++;
					Tetromino.PieceList[currentPieceCount - 3].Y += 2;


					CurrentRotation = 270;
					goto BreakOut;
				}
				if (CurrentRotation == 270)
				{
					Tetromino.PieceList[currentPieceCount - 1].Y += 2;
					Tetromino.PieceList[currentPieceCount - 2].X--;
					Tetromino.PieceList[currentPieceCount - 2].Y++;
					Tetromino.PieceList[currentPieceCount - 3].X -= 2;


					CurrentRotation = 0;
					goto BreakOut;
				}
			}
		BreakOut:;//JUMP HERE AFTER ROTATION IS CALCULATED! OOOOUUUU I WROTE A GOTO STATEMENT!
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

			//if (collision && linecleared)
			//{
			//	CheckLine();

			//}
			//if (linecleared)
			//{
			//	Thread.Sleep(500);
			//	CheckLine();
			//}


			base.Draw(gameTime);
		}
	}
}
