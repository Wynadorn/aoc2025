using System;
using System.Linq;

namespace AoC2025.Solvers
{
    public class Day3 : ISolver
    {
        public string SolvePartOne(string puzzleInput, string[] puzzleInputArray)
        {
            int maxJoltage = 0;

            foreach(string bankString in puzzleInputArray)
            {
                Bank bank = new Bank(bankString);
                int joltage = bank.GetBankJoltage();
                maxJoltage += joltage;
                //Console.WriteLine($"    {bankString} = {joltage}");
            }

            return $"{maxJoltage}";
        }

        public string SolvePartTwo(string puzzleInput, string[] puzzleInputArray)
        {
            

            return "";
        }
    }

    public class Battery
    {
        public int Joltage { get; set; }
        public int Index { get; set; }

        public Battery(int joltage, int index)
        {
            Joltage = joltage;
            Index = index;
        }
    }

    public class Bank
    {
        public Battery[] Batteries { get; set;}

        public Bank(string serializedBank)
        {
            Batteries = new Battery[serializedBank.Length];
            
            for(int i = 0; i<serializedBank.Length; i++)
                Batteries[i] = new Battery(serializedBank[i] - 48, i);
        }

        public int GetBankJoltage()
        {
            Battery a = Batteries.SkipLast(1).MaxBy(b => b.Joltage);
            var b = Batteries.Skip(a.Index+1).Max(b => b.Joltage);

            return a.Joltage * 10 + b;
        }
    }
}
