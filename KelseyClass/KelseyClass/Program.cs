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
                bool shouldRender = false;

                ConsoleKey key = Console.ReadKey(false).Key;

                switch (key)
                {
                    case ConsoleKey.Escape:
                        return;

                    case ConsoleKey.UpArrow:
                        shouldRender = myMap.MovePlayer(Directions.Up);
                        break;

                    case ConsoleKey.RightArrow:
                        shouldRender = myMap.MovePlayer(Directions.Right);
                        break;

                    case ConsoleKey.DownArrow:
                        shouldRender = myMap.MovePlayer(Directions.Down);
                        break;

                    case ConsoleKey.LeftArrow:
                        shouldRender = myMap.MovePlayer(Directions.Left);
                        break;
                }

                if (shouldRender)
                {
                    myMap.Render();
                }
            }
        }
    }
}
