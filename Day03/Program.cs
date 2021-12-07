using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Core;

namespace Day03
{
    class Program
    {
        static int Get_1_1_Answer(List<string> input)
        {
            var halfCount = input.Count / 2;

            var result = new int[input[0].Length];
            foreach (var l in input)
            {
                var bits = l.AsSpan();
                for (var index = 0; index < bits.Length; index++)
                {
                    var bit = bits[index];
                    result[index] += bit == '1' ? 1 : 0;
                }
            }

            var gamma = new int[result.Length];
            var epsilon = new int[result.Length];

            for (var index = 0; index < result.Length; index++)
            {
                gamma[index] = result[index] > halfCount ? 1 : 0;
                epsilon[index] = gamma[index] == 1 ? 0 : 1;
            }

            var gammaBinary = string.Join("", gamma);
            var epsilonBinary = string.Join("", epsilon);

            // Console.WriteLine(gammaBinary);
            // Console.WriteLine(epsilonBinary);

            var gammaValue = Convert.ToInt32(gammaBinary, 2);
            var epsilonValue = Convert.ToInt32(epsilonBinary, 2);

            return gammaValue * epsilonValue;
        }

        static int Get_1_2_Answer(List<string> input)
        {
            var length = input[0].Length;

            var oxygen = new List<string>(input);
            var idx = 0;
            while (oxygen.Any())
            {
                if (oxygen.Count == 1)
                {
                    break;
                }

                var count = oxygen.Count;
                var countOne = oxygen.Count(x => x[idx] == '1');
                var countZero = count - countOne;

                oxygen = countOne >= countZero
                    ? oxygen.FindAll(x => x[idx] == '1')
                    : oxygen.FindAll(x => x[idx] == '0');

                idx++;
            }

            var co2 = new List<string>(input);
            idx = 0;
            while (co2.Any())
            {
                if (co2.Count == 1)
                {
                    break;
                }

                var count = co2.Count;
                var countOne = co2.Count(x => x[idx] == '1');
                var countZero = count - countOne;

                co2 = countOne >= countZero
                    ? co2.FindAll(x => x[idx] == '0')
                    : co2.FindAll(x => x[idx] == '1');

                idx++;
            }

            var oxygenBinary = oxygen.First();
            var co2Binary = co2.First();

            var oxygenValue = Convert.ToInt32(oxygenBinary, 2);
            var co2Value = Convert.ToInt32(co2Binary, 2);

            return oxygenValue * co2Value;
        }

        static async Task Main(string[] args)
        {
            try
            {
                var inputManager = new InputManager(3);

                var lines = await inputManager.GetInput();

                Console.WriteLine(Get_1_1_Answer(lines));
                Console.WriteLine(Get_1_2_Answer(lines));
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    }
}