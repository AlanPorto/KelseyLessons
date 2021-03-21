using System;
using System.Collections.Generic;
using System.Text;

namespace KelseyClass
{
    public enum Directions
    {
        Up,
        Right,
        Down,
        Left,
    }

    public class Map
    {
        public const int MaxRow = 4;
        public const int MaxCol = 4;

        private char[,] MapArray = new char[MaxRow, MaxCol];

        private int mFrameCounter = 0;

        private Character mPlayer = new Character();

        public Map()
        {
            for (int i = 0; i < MaxRow; i++)
            {
                for (int j = 0; j < MaxCol; j++)
                {
                    MapArray[i, j] = '*';
                }
            }

            MapArray[mPlayer.MyRow, mPlayer.MyCol] = mPlayer.Symbol;
        }

        public bool MovePlayer(Directions dir)
        {
            int row, col;
            GetRowAndColForDirection(dir, out row, out col);

            int targetRow = mPlayer.MyRow + row;
            int targetCol = mPlayer.MyCol + col;

            if (InBound(targetRow, targetCol))
            {
                MapArray[mPlayer.MyRow, mPlayer.MyCol] = '*';
                MapArray[targetRow, targetCol] = mPlayer.Symbol;

                mPlayer.MyRow = targetRow;
                mPlayer.MyCol = targetCol;
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool InBound(int row, int col)
        {
            bool validRow = (row >= 0) && (row < MaxRow);
            bool validCol = (col >= 0) && (col < MaxCol);

            return validRow && validCol;
        }

        private void GetRowAndColForDirection(Directions dir, out int row, out int col)
        {
            switch (dir)
            {
                case Directions.Up:
                    row = -1;
                    col = 0;
                    break;
                case Directions.Right:
                    row = 0;
                    col = 1;
                    break;
                case Directions.Down:
                    row = 1;
                    col = 0;
                    break;
                case Directions.Left:
                    row = 0;
                    col = -1;
                    break;

                default:
                    row = col = 0;
                    break;
            }
        }

        public void Render()
        {
            Console.Clear();

            mFrameCounter++;

            Console.WriteLine("--- Frame: " + mFrameCounter + " ---");

            for (int i = 0; i < MaxRow; i++)
            {
                for (int j = 0; j < MaxCol; j++)
                {
                    Console.Write(MapArray[i, j]);
                }

                Console.WriteLine();
            }
        }
    }
}
