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
                "session=");
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
