using AoC2025.Util;
using System;

namespace AoC2025
{
    internal class Program
    {
        public static bool UseBig { get; private set; }
        public static bool Interactive { get; private set; }
        public static bool ForceRefresh { get; private set; }
        public static bool PrintAll { get; private set; }

        private static bool ApplicationIsRunning = true;
        
        private static void Main(string[] args)
        {
            ParseArgs(args);
            
            if (PrintAll)
            {
                PrintAllAndExit();
                return;
            }

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
            PrintAll = args != null && Array.Exists(args, a => string.Equals(a, "-all", StringComparison.OrdinalIgnoreCase));
        }

        private static void PrintAllAndExit()
        {
            Console.WriteLine("Day   -Part 1-   -Part 2-");

            for (int day = 1; day <= 25; day++)
            {
                try
                {
                    var type = Type.GetType($"AoC2025.Solvers.Day{day}", throwOnError: false);
                    if (type == null)
                        continue; // skip days without a solver implementation

                    var solver = Activator.CreateInstance(type) as AoC2025.Solvers.ISolver;
                    if (solver == null)
                        continue;

                    string rawInput = PuzzleAPI.GetPuzzleInput(day);
                    string[] inputArray = rawInput.Split('\n', StringSplitOptions.RemoveEmptyEntries);

                    var puzzle = new Puzzle(day)
                    {
                        Star1Solver = (input, arr) => solver!.SolvePartOne(input, arr),
                        Star2Solver = (input, arr) => solver!.SolvePartTwo(input, arr)
                    };

                    // Run both parts to measure durations
                    _ = puzzle.GetSolution(Star.Star1, rawInput, inputArray);
                    _ = puzzle.GetSolution(Star.Star2, rawInput, inputArray);

                    string p1 = FormatDuration(puzzle.Star1Millis);
                    string p2 = FormatDuration(puzzle.Star2Millis);

                    Console.WriteLine($"{day,3}   {p1,8}   {p2,8}");
                }
                catch
                {
                    // On any failure (missing input, exceptions), silently skip to meet the no-extra-output requirement
                    continue;
                }
            }
        }

        private static string FormatDuration(long millis)
        {
            if (millis <= 0) return "0ms";
            if (millis >= 1000000) return "999999ms";
            return $"{millis}ms";
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