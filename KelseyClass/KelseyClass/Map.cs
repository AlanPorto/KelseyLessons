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

        private Tile[,] MapArray; // Multi dimensional array

        private int mFrameCounter = 0;

        private Player mPlayer = new Player();
        private Monster mMonster = new Monster();

        public Map()
        {

            char[][] jaggedArray = new char[][]
            {
               new char[] { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*' },
               new char[] { '*', '-', '-', '-', '-', '-', '-', '-', '-', '*' },
               new char[] { '*', '-', '-', '-', '-', '-', '-', '-', '-', '*' },
               new char[] { '*', '-', '-', '*', '*', '-', '-', '-', '-', '*' },
               new char[] { '*', '-', '-', '*', '*', '-', '-', '-', '-', '*' },
               new char[] { '*', '-', '-', '-', '-', '-', '-', '-', '-', '*' },
               new char[] { '*', '-', '-', '-', '-', '-', '-', '-', '-', '*' },
               new char[] { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*' },
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
                    tile.MyRow = i;
                    tile.MyCol = j;

                    MapArray[i, j] = tile;
                }
            }

            mPlayer.MyRow = mPlayer.MyCol = 1;
            mMonster.MyRow = mMonster.MyCol = 6;
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

            mMonster.MyRow = targetRow;
            mMonster.MyCol = targetCol;
        }

        public bool MovePlayer(Directions dir)
        {
            int row, col;
            GetRowAndColForDirection(dir, out row, out col);

            int targetRow = mPlayer.MyRow + row;
            int targetCol = mPlayer.MyCol + col;

            if (IsPositionEmptyAndValid(targetRow, targetCol, MapArray))
            {
                mPlayer.MyRow = targetRow;
                mPlayer.MyCol = targetCol;
                return true;
            }
            else
            {
                return false;
            }

        }

        public static bool IsPositionEmptyAndValid(int row, int col, Tile[,] tileMap)
        {
            bool validRow = (row >= 0) && (row < MaxRow);
            bool validCol = (col >= 0) && (col < MaxCol);

            bool isEmpty = (validRow && validCol) && tileMap[row, col].IsEmpty();

            return validRow && validCol && isEmpty;
        }

        public static void GetRowAndColForDirection(Directions dir, out int row, out int col)
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

                Tile monsterTile = MapArray[mMonster.MyRow, mMonster.MyCol];
                Tile playerTile = MapArray[mPlayer.MyRow, mPlayer.MyCol];

                List<Tile> path = Pathfinder.GetPath(monsterTile, playerTile, MapArray);

                for (int i = 0; i < MaxRow; i++)
                {
                    for (int j = 0; j < MaxCol; j++)
                    {
                        char symbol;

                        if (mPlayer.MyRow == i && mPlayer.MyCol == j)
                        {
                            symbol = mPlayer.Symbol;
                        }
                        else if (mMonster.MyRow == i && mMonster.MyCol == j)
                        {
                            symbol = mMonster.Symbol;
                        }
                        else
                        {
                            Tile currentTile = MapArray[i, j];

                            if (path.Contains(currentTile))
                            {
                                symbol = 'x';
                            }
                            else
                            {
                                symbol = currentTile.Symbol;
                            }
                        }

                        Console.Write(symbol);
                    }

                    Console.WriteLine();
                }
            }
        }
    }
}
