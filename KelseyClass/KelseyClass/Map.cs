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
        public const int MaxRow = 8;
        public const int MaxCol = 8;

        private char[,] MapArray = new char[MaxRow, MaxCol];

        private int mFrameCounter = 0;

        private Player mPlayer = new Player();
        private Monster mMonster = new Monster();

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
            MapArray[mMonster.MyRow, mMonster.MyCol] = mMonster.Symbol;
        }

        public bool MovePlayer(Directions dir)
        {
            int row, col;
            GetRowAndColForDirection(dir, out row, out col);

            int targetRow = mPlayer.MyRow + row;
            int targetCol = mPlayer.MyCol + col;

            if (CanMoveTo(targetRow, targetCol, mMonster))
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

        public bool CanMoveTo(int row, int col, Character enemy)
        {
            bool validRow = (row >= 0) && (row < MaxRow);
            bool validCol = (col >= 0) && (col < MaxCol);

            bool isEnemyPos = (row == enemy.MyRow) && (col == enemy.MyCol);

            return validRow && validCol && !isEnemyPos;
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
