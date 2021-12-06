using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Core
{
    public class InputManager
    {
        private readonly HttpClient _http = new HttpClient();

        private readonly int _day;

        public InputManager(int day)
        {
            _day = day;
        }

        public async Task<List<string>> GetInput()
        {
            _http.DefaultRequestHeaders.Add("Cookie",
                "session=53616c7465645f5f5f44b3f0bbefb6f70eb1601fbc0138659399f063586e724ba1c691e02d8a012cb1e426bbe2f8464d");
            var response = await _http.GetAsync($"https://adventofcode.com/2021/day/{_day}/input");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();

            var lines = responseBody.Split("\n")
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToList();

            return lines;
        }
    }
}