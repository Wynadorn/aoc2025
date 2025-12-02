using System;

namespace AoC2025.Solvers
{
    public interface ISolver
    {
        public virtual void Solve(string puzzleInput)
        {
            string answerOne = SolvePartOne(puzzleInput, puzzleInput.Split('\n', StringSplitOptions.RemoveEmptyEntries));
            string answerTwo = SolvePartTwo(puzzleInput, puzzleInput.Split('\n', StringSplitOptions.RemoveEmptyEntries));

            Console.WriteLine(answerOne);
            Console.WriteLine(answerTwo);
        }

        public abstract string SolvePartOne(string puzzleInput, string[] puzzleInputArray);

        public abstract string SolvePartTwo(string puzzleInput, string[] puzzleInputArray);
    }
}
