using System;
using System.Net.Http;
using System.Threading.Tasks;
using Core;

namespace Day02
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var inputManager = new InputManager(2);

                var lines = await inputManager.GetInput();

                Console.WriteLine();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    }
}