using AoC2025.Util;
using System;

namespace AoC2025
{
    internal class Program
    {
        public static bool UseBig { get; private set; }
        public static bool Interactive { get; private set; }
        public static bool ForceRefresh { get; private set; }

        private static bool ApplicationIsRunning = true;
        
        private static void Main(string[] args)
        {
            ParseArgs(args);
            
            if (Interactive)
                SolvePuzzlesInteractive();
            else
                SolveTodaysPuzzleOrInteractiveFallback();
        }

        private static void ParseArgs(string[] args)
        {
            Interactive = args != null && Array.Exists(args, a => string.Equals(a, "-i", StringComparison.OrdinalIgnoreCase));
            UseBig = args != null && Array.Exists(args, a => string.Equals(a, "-big", StringComparison.OrdinalIgnoreCase));
            ForceRefresh = args != null && Array.Exists(args, a => string.Equals(a, "-refresh", StringComparison.OrdinalIgnoreCase));
        }

        private static void SolveTodaysPuzzleOrInteractiveFallback()
        {
            var now = DateTime.Now;

            if (now.Month == 12 && now.Day >= 1 && now.Day <= 25)
            {
                PuzzleAPI.Solve(now.Day);
            }
            else
            {
                Console.WriteLine("Out of festive range exception! Switching to interactive mode.");
                SolvePuzzlesInteractive();
            }

            Console.WriteLine();
        }

        private static void SolvePuzzlesInteractive()
        {
            Interactive = true;

            while(ApplicationIsRunning)
            {
                int dayToSolve = GetDayToSolve();
                PuzzleAPI.Solve(dayToSolve);
                Console.WriteLine();

                if(!AskYesNo("Would you like to solve another puzzle? (Y/n)"))
                    ApplicationIsRunning = false;
            }

            Console.WriteLine("Merry Christmas and Happy Coding!");
        }

        private static int GetDayToSolve()
        {
            int day;

            Console.WriteLine("Which day would you like to solve?");
            string? userInput = Console.ReadLine();

            while (!int.TryParse(userInput, out day))
            {
                Console.WriteLine("Invalid input, try again.");
                userInput = Console.ReadLine();
            }

            if (day < 1 || day > 25)
            {
                Console.WriteLine("Out of festive range exception");
                return GetDayToSolve();
            }

            return day;
        }

        private static bool AskYesNo(string prompt, bool defaultYes = true)
        {
            Console.Write($"{prompt} ");
            var keyInfo = Console.ReadKey(intercept: true);
            Console.WriteLine();
            Console.WriteLine();

            switch (keyInfo.Key)
            {
                case ConsoleKey.Y:
                    return true;
                case ConsoleKey.N:
                case ConsoleKey.Escape:
                    return false;
                case ConsoleKey.Enter:
                    return defaultYes;
                default:
                    char c = char.ToLowerInvariant(keyInfo.KeyChar);
                    if (c == 'y') return true;
                    if (c == 'n') return false;
                    return defaultYes;
            }
        }
    }
}