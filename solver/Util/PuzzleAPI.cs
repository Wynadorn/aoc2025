using AoC2025.Solvers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AoC2025.Util
{
    public static class PuzzleAPI
    {
        private static readonly string BASE_INPUT_URL = "https://adventofcode.com/2025/day/";
        private static readonly string CACHE_ROOT = @"C:\Src\usr\aoc\aoc2025\cache\";

        public static void Solve(int day)
        {
            ISolver? solver;

            try
            {
                Type? type = Type.GetType($"AoC2025.Solvers.Day{day}", true);
                if (type == null)
                    throw new Exception("Couln't find solver");

                solver = Activator.CreateInstance(type) as ISolver;
                if (solver == null)
                    throw new Exception("Couln't find solver");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ShowFail(day);
                return;
            }

            string rawInput = GetPuzzleInput(day);
            solver.Solve(rawInput);
            return;
        }

        private static void ShowFail(int day)
        {
            Console.WriteLine($"Unable to find a solution for day {day}.");
        }

        public static string GetPuzzleInput(int day, int star = 1, bool forceRefresh = false)
        {
            if (day < 1 || day > 25)
                throw new ArgumentOutOfRangeException(nameof(day), "Out of festive range exception");

            string cacheFileName = Path.Combine(CACHE_ROOT, $"day{day}-input.txt.big");

            if (File.Exists(cacheFileName) && !forceRefresh)
                return File.ReadAllText(cacheFileName);

            var url = new Uri($"{BASE_INPUT_URL}{day}/input");

            var handler = new HttpClientHandler();
            handler.AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate;
            handler.UseCookies = true;
            handler.CookieContainer = new System.Net.CookieContainer();
            var sessionToken = GetSessionCookie();
            handler.CookieContainer.Add(new System.Net.Cookie("session", sessionToken, "/", ".adventofcode.com"));
            
            using HttpClient client = new HttpClient(handler);
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/142.0.0.0 Safari/537.36 Edg/142.0.0.0");
            client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
            client.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.9");
            client.DefaultRequestHeaders.Add("Referer", $"https://adventofcode.com/2025/day/{day}");

            var request = new HttpRequestMessage(HttpMethod.Get, url)
            {
                Version = new Version(1, 1),
                VersionPolicy = HttpVersionPolicy.RequestVersionOrLower
            };

            HttpResponseMessage response = client.SendAsync(request).Result;
            
            if (!response.IsSuccessStatusCode)
            {
                string errorContent = response.Content.ReadAsStringAsync().Result;
                throw new Exception($"Failed to get puzzle input for day {day}. Status: {response.StatusCode}\nResponse: {errorContent}");
            }
            
            string? stringResult = response.Content.ReadAsStringAsync().Result;
            if (!string.IsNullOrEmpty(stringResult))
            {
                var directory = Path.GetDirectoryName(cacheFileName);

                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                File.WriteAllText(cacheFileName, stringResult);
                return stringResult;
            }
            else
            {
                throw new Exception($"Unable to get puzzle input for day {day}.");
            }
        }

        private static string GetSessionCookie()
        {
            string path = Path.Combine(CACHE_ROOT, $"cookie.txt");
            
            if (!File.Exists(path))
            {
                File.WriteAllText(path, string.Empty);
                throw new FileNotFoundException($"'cookie.txt' not found in '{CACHE_ROOT}'");
            }

            string raw = File.ReadAllText(path).Trim();
            if (string.IsNullOrEmpty(raw))
                throw new Exception($"'cookie.txt' in '{CACHE_ROOT}' is empty. Paste your AoC session token.");

            if (raw.StartsWith("session="))
                raw = raw.Substring("session=".Length);

            return raw;
        }
    }
}