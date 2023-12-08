using System;
using System.IO.Pipelines;
using System.Linq.Expressions;
using System.Security.Cryptography;

namespace AoC2025.Solvers
{
    public class Day1 : ISolver
    {
        public string SolvePartOne(string puzzleInput, string[] puzzleInputArray)
        {
            int count = 0;
            ElvishInt rotor = new ElvishInt(99);
            rotor.Value = 50;

            foreach(string line in puzzleInputArray)
            {
                Console.WriteLine($"Processing line: {line}");
                if(line[0] == 'L')
                    rotor -= int.Parse(line[1..]);
                else
                    rotor += int.Parse(line[1..]);

                if(rotor.Value == 0)
                    count++;
            }

            return $"Count: {count}, Final Value: {rotor.Value}";
        }

        public string SolvePartTwo(string puzzleInput, string[] puzzleInputArray)
        {
            return "day 2 not implemented";
        }
    }

    public struct ElvishInt
    {
        private int Size {get; set; }
        public int Value {get;set; }

        public ElvishInt(int maxSize)
        {
            if (maxSize <= 0)
                throw new ArgumentException("Max size cannot be euqal or less than zero.", nameof(maxSize));

            Size = maxSize;
        }

        public static ElvishInt operator +(ElvishInt operand) => operand;
        public static ElvishInt operator -(ElvishInt operand) => new ElvishInt(-operand.Size);

        public static ElvishInt operator +(ElvishInt left, ElvishInt right)
        {
            left = left + right.Value;
            return left;
        }

        public static ElvishInt operator -(ElvishInt left, ElvishInt right)
        {
            left = left - right.Value;
            return left;
        }

        public static ElvishInt operator +(ElvishInt left, int right)
        {
            int originalValue = left.Value;

            if(right > 0)
            {
                left.Value = (left.Value + right) % (left.Size +1);
            }
            else if (right < 0)
            {
                left.Value = (left.Value + right) % (left.Size +1);
                
                if(left.Value < 0)
                    left.Value = left.Size + 1 + left.Value;
            }

            Console.WriteLine($"{originalValue} + {right} = {left.Value}");

            return left;
        }

        public static ElvishInt operator -(ElvishInt left, int right)
        {
            left = left + (right*-1);
            return left;
        }

        public static ElvishInt operator ++(ElvishInt operand)
        {
            operand = operand + 1;
            return operand;
        }

        public static ElvishInt operator --(ElvishInt operand)
        {
            operand = operand - 1;
            return operand;
        }

        // New operators allowed in C# 14:
        public void operator +=(ElvishInt operand)
        {
            this = this + operand;
        }

        public void operator -=(ElvishInt operand)
        {
            this = this - operand;
        }

        public void operator +=(int operand)
        {
            this = this + operand;
        }

        public void operator -=(int operand)
        {
            this = this - operand;
        }

        public void operator ++()
        {
            this = this+1;
        }

        public void operator --()
        {
            this = this-1;
        }
    }
}
