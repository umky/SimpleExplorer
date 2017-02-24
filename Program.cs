using System;

namespace ConsoleExplorer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WindowHeight = 50;
            var startPath = "C:\\";
            var explorer = new Explorer(startPath);

            while (true)
            {
                var exit = false;
                var c = Console.ReadKey();
                switch (c.Key)
                {
                    case ConsoleKey.Enter:
                        explorer.StepInto();
                        break;
                    case ConsoleKey.UpArrow:
                        explorer.NavigateUp();
                        break;
                    case ConsoleKey.DownArrow:
                        explorer.NavigateDown();
                        break;
                    case ConsoleKey.Backspace:
                        explorer.StepBack();
                        break;
                    case ConsoleKey.Escape:
                        exit = true;
                        break;
                }
                if (exit)
                {
                    break;
                }
            }
        }
    }
}