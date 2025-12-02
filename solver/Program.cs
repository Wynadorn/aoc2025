using AoC2025.Util;
using System;

namespace AoC2025
{
    internal class Program
    {
        //static bool ApplicationIsRunning = true;
        
        static void Main(string[] args)
        {
            //while (ApplicationIsRunning)
                SolvePuzzles();
        }

        private static void SolvePuzzles()
        {
            int dayToSolve = GetDayToSolve();
            PuzzleAPI.Solve(dayToSolve);

            Console.WriteLine();
            //Console.WriteLine("Would you like to solve another puzzle? (Y/n)");
            //var key = Console.ReadKey();
            //var key = new ConsoleKeyInfo()

            //if (key.Key == ConsoleKey.N || key.Key == ConsoleKey.Escape)
                //ApplicationIsRunning = false;
        }

        private static int GetDayToSolve()
        {
            return 2;

            // int day;

            // Console.WriteLine("Which day would you like to solve?");
            // string? userInput = Console.ReadLine();

            // while (!int.TryParse(userInput, out day))
            // {
            //     Console.WriteLine("Invalid input, try again.");
            //     userInput = Console.ReadLine();
            // }

            // if (day < 1 || day > 25)
            // {
            //     Console.WriteLine("Out of festive range exception");
            //     return GetDayToSolve();
            // }

            // return day;
        }
    }
}