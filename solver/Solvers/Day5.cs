using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Threading;

namespace AoC2025.Solvers
{
    public class Day5 : ISolver
    {
        public string SolvePartOne(string puzzleInput, string[] puzzleInputArray)
        {
            List<Range> ranges = new List<Range>();
            List<long> products = new List<long>();
            List<long> okProducts = new List<long>();

            int i = 0;

            while(puzzleInputArray[i].Contains('-'))
            {
                var split = puzzleInputArray[i].Split('-');
                long a = Convert.ToInt64(split[0]);
                long b = Convert.ToInt64(split[1]);

                if(a<=b)
                    ranges.Add(new Range(a, b));
                else
                    ranges.Add(new Range(b, a));

                i++;
            }

            while(i < puzzleInputArray.Length)
            {
                long productId = Convert.ToInt64(puzzleInputArray[i]);
                products.Add(productId);
                i++;
            }

            products = products.Distinct().Order().ToList();

            foreach(Range range in ranges)
            {
                products.RemoveAll(product =>
                {
                    if (product >= range.Start && product <= range.Stop)
                    {
                        okProducts.Add(product);
                        return true;
                    }
                    return false;
                });
            }

            return $"{okProducts.Count}";
        }

        public string SolvePartTwo(string puzzleInput, string[] puzzleInputArray)
        {
            return "";
        }
    }

    public class Range
    {
        public long Start {get;set;}
        public long Stop {get;set;}

        public Range(long start, long stop)
        {
            Start = start;
            Stop = stop;
        }
    }
}