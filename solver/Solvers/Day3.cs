using System;
using System.Collections.Generic;
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
            double maxJoltage = 0;

            foreach(string bankString in puzzleInputArray)
            {
                //Console.WriteLine($"  Processing: {bankString}");
                Bank bank = new Bank(bankString);
                double joltage = bank.GetUnsafeBankJoltage();
                maxJoltage += joltage;
                //Console.WriteLine($"    {bankString} = {joltage}");
            }

            return $"{maxJoltage}";
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
        string SerializedBank;
        public Battery[] Batteries { get; set;}

        public Bank(string serializedBank)
        {
            SerializedBank = serializedBank;
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

        public double GetUnsafeBankJoltage(int batteriesPerBank = 12)
        {
            double joltage=0;
            int skip = 0;
            for(int i = 1; i <= batteriesPerBank; i++)
            {
                Battery a = Batteries.Skip(skip).SkipLast(batteriesPerBank-i).MaxBy(b => b.Joltage);
                skip = a.Index +1;
                double d = a.Joltage * Math.Pow(10, batteriesPerBank-i);
                //Console.WriteLine($"      {{0,{11+batteriesPerBank}}}", $"{d}");
                joltage += d;
            }
            
            // Battery b = Batteries.SkipLast(batteriesPerBank-2).MaxBy(b => b.Joltage);
            // Battery c = Batteries.SkipLast(batteriesPerBank-3).MaxBy(b => b.Joltage);

            //List<long> joltages = new List<long>();

            // int iStart = 0;
            // while(iStart+batteriesPerBank <= SerializedBank.Length)
            // {
            //     string sub = SerializedBank[iStart..(iStart+batteriesPerBank)];
            //     long intValue = Convert.ToInt64(sub);
            //     joltages.Add(intValue);
            //     //Console.WriteLine($"        sub[{iStart}..{iStart+batteriesPerBank}] is {sub}");
            //     iStart++;
            // }

            return joltage;
        }
    }
}