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
        public static int MaxRow;
        public static int MaxCol;

        private Tile[,] MapArray;

        private int mFrameCounter = 0;

        private Player mPlayer = new Player();
        private Monster mMonster = new Monster();

        public Map()
        {

            char[][] jaggedArray = new char[][]
            {
               new char[] { '*', '*', '*', '*', '*', '*', '*', '*', },
               new char[] { '*', '-', '-', '-', '-', '-', '-', '*', },
               new char[] { '*', '-', '-', '-', '-', '-', '-', '*', },
               new char[] { '*', '-', '-', '*', '*', '-', '-', '*', },
               new char[] { '*', '-', '-', '*', '*', '-', '-', '*', },
               new char[] { '*', '-', '-', '-', '-', '-', '-', '*', },
               new char[] { '*', '-', '-', '-', '-', '-', '-', '*', },
               new char[] { '*', '*', '*', '*', '*', '*', '*', '*', },
            };

            MaxRow = jaggedArray.Length;
            MaxCol = jaggedArray[0].Length;

            MapArray = new Tile[MaxRow, MaxCol];
            for (int i = 0; i < MaxRow; i++)
            {
                for (int j = 0; j < MaxCol; j++)
                {
                    char symbol = jaggedArray[i][j];
                    Tile tile = new Tile(symbol);
                    MapArray[i, j] = tile;
                }
            }

            mPlayer.MyRow = mPlayer.MyCol = 1;
            mMonster.MyRow = mMonster.MyCol = 6;

            MapArray[mPlayer.MyRow, mPlayer.MyCol].CharRef = mPlayer;
            MapArray[mMonster.MyRow, mMonster.MyCol].CharRef = mMonster;
        }

        public void MoveEnemy()
        {
            return;
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

            if (CanMoveTo(targetRow, targetCol))
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

        public bool CanMoveTo(int row, int col)
        {
            bool validRow = (row >= 0) && (row < MaxRow);
            bool validCol = (col >= 0) && (col < MaxCol);

            bool isEmpty = MapArray[row, col].IsEmpty();

            return validRow && validCol && isEmpty;
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
