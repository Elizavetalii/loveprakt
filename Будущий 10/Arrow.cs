﻿namespace Будущий_10
{
    internal static class Arrows
    {
        public static int startLine = 4;
        public static ConsoleKeyInfo key;

        public static int Show(int nlength)
        {

            int minPosition = 0;
            int maxPosition = 0;

            if (nlength > 0)
            {
                minPosition = 4;
                maxPosition = minPosition + nlength - 1;
            }
            int position = minPosition;

            while (true)
            {
                Console.SetCursorPosition(0, position);
                if (maxPosition >= minPosition)
                {
                    Console.WriteLine("->");
                }

                key = Console.ReadKey();
                Console.SetCursorPosition(0, position);
                Console.WriteLine("  ");

                if (key.Key == ConsoleKey.UpArrow)

                    if (position != minPosition)
                        position--;
                    else
                        position = maxPosition;
                else if (key.Key == ConsoleKey.DownArrow)
                    if (position != maxPosition)
                        position++;
                    else
                        position = minPosition;
                else if (key.Key == ConsoleKey.Enter)
                    break;
                else if (key.Key == ConsoleKey.Escape)
                    return (int)Keys.Escape;
                if (key.Key == ConsoleKey.F1)
                    return (int)Keys.F1;
                else if (key.Key == ConsoleKey.F2)
                    return (int)Keys.F2;
                else if (key.Key == ConsoleKey.F10)
                    return (int)Keys.F10;
                else if (key.Key == ConsoleKey.S)
                    return (int)Keys.S;
                else if (key.Key == ConsoleKey.OemPlus)
                    return (int)Keys.plus;
                else if (key.Key == ConsoleKey.OemMinus)
                    return (int)Keys.minus;

            }
            return position - minPosition;
        }
    }
}
