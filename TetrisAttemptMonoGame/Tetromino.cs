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
