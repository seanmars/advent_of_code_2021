using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Core;

namespace Day01
{
    class Program
    {
        static int Get_1_1_Answer(IReadOnlyList<int> input)
        {
            var count = 0;
            int? last = null;

            foreach (var current in input)
            {
                if (!last.HasValue)
                {
                    last = current;
                    continue;
                }

                if (current > last)
                {
                    count++;
                }

                last = current;
            }

            return count;
        }

        static int Get_1_2_Answer(IReadOnlyList<int> input)
        {
            var count = 0;
            int? last = null;

            var newInput = new List<int>();
            for (var i = 0; i < input.Count - 2; i++)
            {
                var tmp = (new[] { input[i], input[i + 1], input[i + 2] }).Sum();
                newInput.Add(tmp);
            }

            foreach (var current in newInput)
            {
                if (!last.HasValue)
                {
                    last = current;
                    continue;
                }

                if (current > last)
                {
                    count++;
                }

                last = current;
            }

            return count;
        }

        static int Get_1_2_Answer_2(IReadOnlyList<int> input)
        {
            var count = 0;
            int? last = null;

            for (var i = 2; i < input.Count; i++)
            {
                var current = input[i];

                if (!last.HasValue)
                {
                    last = input[i - 2];
                    continue;
                }

                if (current > last)
                {
                    count++;
                }

                last = input[i - 2];
            }


            return count;
        }

        static async Task Main(string[] args)
        {
            try
            {
                var inputManager = new InputManager(1);

                var lines = await inputManager.GetInput();
                var input = lines.Select(x => Convert.ToInt32(x)).ToList();

                var count = Get_1_1_Answer(input);
                Console.WriteLine(count);

                count = Get_1_2_Answer_2(input);
                Console.WriteLine(count);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    }
}