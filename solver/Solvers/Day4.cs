using System;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace AoC2025.Solvers
{
    public class Day4 : ISolver
    {
        public string SolvePartOne(string puzzleInput, string[] puzzleInputArray)
        {
            int sum = 0;
            int roomX = puzzleInputArray[0].Length;
            int roomY = puzzleInputArray.Length;
            RollOfPaper[,] room = new RollOfPaper[roomX,roomY];

            Console.WriteLine($"Room size is {roomX}x{roomY}");

            for (int x = 0; x < puzzleInputArray.Length; x++)
            {
                string row = puzzleInputArray[x];
                for (int y = 0; y < row.Length; y++)
                {
                    char charValue = row[y];
                    if (charValue == '.')
                        continue;

                    if(charValue != null)
                        room[x,y] = new RollOfPaper(){location = new Point(x,y), roomRef = room};
                }
            }

            foreach(RollOfPaper roll in room)
            {
                if(roll != null && roll.HasFewerThanFour())
                    sum++;
            }
            
            return $"{sum}";
        }

        public string SolvePartTwo(string puzzleInput, string[] puzzleInputArray)
        {
            int sum = 0;
            int roomX = puzzleInputArray[0].Length;
            int roomY = puzzleInputArray.Length;
            RollOfPaper[,] room = new RollOfPaper[roomX,roomY];

            Console.WriteLine($"Room size is {roomX}x{roomY}");

            for (int x = 0; x < puzzleInputArray.Length; x++)
            {
                string row = puzzleInputArray[x];
                for (int y = 0; y < row.Length; y++)
                {
                    char charValue = row[y];
                    if (charValue == '.')
                        continue;

                    if(charValue != null)
                        room[x,y] = new RollOfPaper(){location = new Point(x,y), roomRef = room};
                }
            }

            int previousSum = -1;

            while(previousSum != sum)
            {
                previousSum = sum;

                foreach(RollOfPaper roll in room)
                {
                    if(roll != null && roll.HasFewerThanFour())
                    {
                        sum++;
                        room[roll.location.X,roll.location.Y] = null;
                    }
                }

                Console.WriteLine($"sum is {sum} previous sum was {previousSum}");
            }
            
            return $"{sum}";
        }
    }

    public class RollOfPaper
    {
        public Point location {get;set;}
        public RollOfPaper[,] roomRef {get;set;}

        public bool HasFewerThanFour()
        {
            int count = 0;

            try{ if(roomRef[location.X-1,location.Y-1] != null) { count++;}}catch{}
            try{ if(roomRef[location.X-1,location.Y] != null) { count++;}}catch{}
            try{ if(roomRef[location.X-1,location.Y+1] != null) { count++;}}catch{}

            try{ if(roomRef[location.X,location.Y-1] != null) { count++;}}catch{}
            try{ if(roomRef[location.X,location.Y+1] != null) { count++;}}catch{}

            try{ if(roomRef[location.X+1,location.Y-1] != null) { count++;}}catch{}
            try{ if(roomRef[location.X+1,location.Y] != null) { count++;}}catch{}
            try{ if(roomRef[location.X+1,location.Y+1] != null) { count++;}}catch{}

            return count < 4;
        }
    }
}