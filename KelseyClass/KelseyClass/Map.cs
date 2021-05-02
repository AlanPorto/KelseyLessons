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

        private Tile[,] MapArray = new Tile[MaxRow, MaxCol];

        private int mFrameCounter = 0;

        private Player mPlayer = new Player();
        private Monster mMonster = new Monster();

        public Map()
        {
            for (int i = 0; i < MaxRow; i++)
            {
                for (int j = 0; j < MaxCol; j++)
                {
                    Tile tile = new Tile('*');
                    MapArray[i, j] = tile;
                }
            }

            MapArray[mPlayer.MyRow, mPlayer.MyCol].CharRef = mPlayer;
            MapArray[mMonster.MyRow, mMonster.MyCol].CharRef = mMonster;
        }

        public void MoveEnemy()
        {
            int targetRow = mMonster.MyRow;
            int targetCol = mMonster.MyCol;

            if (targetRow > mPlayer.MyRow)
            {
                targetRow--;
            }
            else if (targetRow < mPlayer.MyRow)
            {
                targetRow++;
            }

            if (targetCol > mPlayer.MyCol)
            {
                targetCol--;
            }
            else if (targetCol < mPlayer.MyCol)
            {
                targetCol++;
            }

            MapArray[mMonster.MyRow, mMonster.MyCol].CharRef = null;

            mMonster.MyRow = targetRow;
            mMonster.MyCol = targetCol;

            MapArray[mMonster.MyRow, mMonster.MyCol].CharRef = mMonster;
        }

        public bool MovePlayer(Directions dir)
        {
            int row, col;
            GetRowAndColForDirection(dir, out row, out col);

            int targetRow = mPlayer.MyRow + row;
            int targetCol = mPlayer.MyCol + col;

            if (CanMoveTo(targetRow, targetCol, mMonster))
            {
                MapArray[mPlayer.MyRow, mPlayer.MyCol].CharRef = null;
                MapArray[targetRow, targetCol].CharRef = mPlayer;

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

        public bool IsGameOver()
        {
            return ((mPlayer.MyRow == mMonster.MyRow) && (mPlayer.MyCol == mMonster.MyCol));
        }

        public void Render()
        {
            Console.Clear();

            if (IsGameOver())
            {
                Console.WriteLine("Game Over!");
            }
            else
            {
                mFrameCounter++;

                Console.WriteLine("--- Frame: " + mFrameCounter + " ---");

                for (int i = 0; i < MaxRow; i++)
                {
                    for (int j = 0; j < MaxCol; j++)
                    {
                        Console.Write(MapArray[i, j].Symbol);
                    }

                    Console.WriteLine();
                }
            }
        }
    }
}
