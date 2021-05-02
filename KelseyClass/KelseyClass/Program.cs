using System;
using System.Collections.Generic;

namespace KelseyClass
{
    class Program
    {
        static void Main(string[] args)
        {
            Map myMap = new Map();

            myMap.Render();

            while (true)
            {
                bool playerMoved = false;

                ConsoleKey key = Console.ReadKey(false).Key;

                switch (key)
                {
                    case ConsoleKey.Escape:
                        return;

                    case ConsoleKey.UpArrow:
                        playerMoved = myMap.MovePlayer(Directions.Up);
                        break;

                    case ConsoleKey.RightArrow:
                        playerMoved = myMap.MovePlayer(Directions.Right);
                        break;

                    case ConsoleKey.DownArrow:
                        playerMoved = myMap.MovePlayer(Directions.Down);
                        break;

                    case ConsoleKey.LeftArrow:
                        playerMoved = myMap.MovePlayer(Directions.Left);
                        break;
                }

                if (playerMoved)
                {
                    myMap.MoveEnemy();
                    myMap.Render();

                    if (myMap.IsGameOver())
                    {
                        break;
                    }
                }
            }
        }
    }
}
