using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;


namespace TetrisAttemptMonoGame
{
    class Tetromino
    {
        SpriteBatch spriteBatch;
        public static List<Tetromino> PieceList = new List<Tetromino>();
        public int X;
        public int Y;
        GraphicsDeviceManager graphics;


        #region ROWCOUNT INTS
        public static int row1Count;
        public static int row2Count;
        public static int row3Count;
        public static int row4Count;
        public static int row5Count;
        public static int row6Count;
        public static int row7Count;
        public static int row8Count;
        public static int row9Count;
        public static int row10Count;
        public static int row11Count;
        public static int row12Count;
        public static int row13Count;
        public static int row14Count;
        public static int row15Count;
        public static int row16Count;
        public static int row17Count;
        public static int row18Count;
        public static int row19Count;

        #endregion
        public static void ResetVals()
        {
            row1Count = 0;
            row2Count = 0;
            row3Count = 0;
            row4Count = 0;
            row5Count = 0;
            row6Count = 0;
            row7Count = 0;
            row8Count = 0;
            row9Count = 0;
            row10Count = 0;
            row11Count = 0;
            row12Count = 0;
            row13Count = 0;
            row14Count = 0;
            row15Count = 0;
            row16Count = 0;
            row17Count = 0;
            row18Count = 0;
            row19Count = 0;
        }
        public enum type
        {
            I,
            O,
            T,
            S,
            Z,
            J,
            L
        }
        public Tetromino(int xVal, int yVal)
        {
            X = xVal;
            Y = yVal;
        }
    }
}
