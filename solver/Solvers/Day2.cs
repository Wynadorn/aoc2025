using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2025.Solvers
{
    public class Day2 : ISolver
    {
        public string SolvePartOne(string puzzleInput, string[] puzzleInputArray)
        {
            Int128 sum = 0;

            puzzleInput = puzzleInput.Replace("\n", "");
            string[] ranges = puzzleInput.Split(',');
            foreach(string range in ranges)
            {
                string[] bounds = range.Split('-');
                Int128 lower = Int128.Parse(bounds[0]);
                Int128 upper = Int128.Parse(bounds[1]);

                //Console.WriteLine($"Range: {range}, lower: {lower}, upper: {upper}");
                for(Int128 i = lower; i<=upper; i++)
                {
                    //Console.WriteLine($"    Processing: {i}");
                    
                    string iString = i.ToString();
                    int halfLength = iString.Length / 2;

                    // if is even
                    if(iString.Length % 2 == 0)
                    {
                        //Console.WriteLine($"        IsEven");
                        string left = iString[..halfLength];
                        string right = iString[halfLength..];
                        
                        //Console.WriteLine($"        {left}");
                        //Console.WriteLine($"        {right}");

                        if(left == right)
                            sum += i;
                    }
                }
            }

            return sum.ToString();
        }

        public string SolvePartTwo(string puzzleInput, string[] puzzleInputArray)
        {
            Int128 sum = 0;

            puzzleInput = puzzleInput.Replace("\n", "");
            string[] ranges = puzzleInput.Split(',');
            foreach(string range in ranges)
            {
                string[] bounds = range.Split('-');
                Int128 lower = Int128.Parse(bounds[0]);
                Int128 upper = Int128.Parse(bounds[1]);

                //Console.WriteLine($"Range: {range}, lower: {lower}, upper: {upper}");
                for(Int128 i = lower; i<=upper; i++)
                {
                    //Console.WriteLine($"    Processing: {i}");
                    
                    string iString = i.ToString();
                    int halfLength = iString.Length / 2;

                    for(int parts = 2; parts <= iString.Length; parts++)
                    {
                        string foo = iString;
                        if(iString.Length % parts == 0)
                        {
                            //Console.WriteLine($"        {iString} is divisible in {parts}");

                            List<string> stringParts = new List<string>();
                            int length = foo.Length / parts;
                            while(foo != string.Empty)
                            {
                                string currentPart = foo[..length];
                                stringParts.Add(currentPart);
                                foo = foo[length..];

                                //Console.WriteLine($"            {currentPart}");
                            }

                            if(stringParts.Distinct().Count() == 1)
                            {
                                //Console.WriteLine($"            all {parts} parts in {i} are equal");
                                sum += i;
                                break;
                            }
                        }
                    }
                }
            }

            return sum.ToString();
        }
    }
}
