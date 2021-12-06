using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Core;

namespace Day02
{
    class Program
    {
        public static class CommandName
        {
            public const string Forward = "forward";
            public const string Down = "down";
            public const string Up = "up";
        }

        public enum CommandType
        {
            Forward,
            Down,
            Up
        }

        public class Position
        {
            public int Horizontal { get; set; }
            public int Aim { get; set; }
            public int Depth { get; set; }
            public int NoSenseDepth => Horizontal * Aim;
            public int FinalDepth => Horizontal * Depth;
        }

        public struct Command
        {
            public CommandType Type { get; set; }
            public int Value { get; set; }
        }

        static Position Get_1_1_Answer(IEnumerable<Command> input)
        {
            var pos = new Position();
            foreach (var command in input)
            {
                switch (command.Type)
                {
                    case CommandType.Forward:
                        pos.Horizontal += command.Value;
                        break;
                    case CommandType.Down:
                        pos.Aim += command.Value;
                        break;
                    case CommandType.Up:
                        pos.Aim -= command.Value;
                        break;
                }
            }

            return pos;
        }

        static Position Get_1_2_Answer(IEnumerable<Command> input)
        {
            var pos = new Position();
            foreach (var command in input)
            {
                switch (command.Type)
                {
                    case CommandType.Forward:
                        pos.Horizontal += command.Value;
                        pos.Depth += command.Value * pos.Aim;
                        break;
                    case CommandType.Down:
                        pos.Aim += command.Value;
                        break;
                    case CommandType.Up:
                        pos.Aim -= command.Value;
                        break;
                }
            }

            return pos;
        }

        static async Task Main(string[] args)
        {
            try
            {
                var inputManager = new InputManager(2);

                var lines = await inputManager.GetInput();

                var commands = lines.Select(x =>
                {
                    var tmp = x.Split(' ');
                    return tmp[0] switch
                    {
                        CommandName.Forward => new Command { Type = CommandType.Forward, Value = Convert.ToInt32(tmp[1]) },
                        CommandName.Down => new Command { Type = CommandType.Down, Value = Convert.ToInt32(tmp[1]) },
                        CommandName.Up => new Command { Type = CommandType.Up, Value = Convert.ToInt32(tmp[1]) },
                        _ => throw new ArgumentOutOfRangeException()
                    };
                }).ToList();

                var pos = Get_1_1_Answer(commands);
                Console.WriteLine(JsonSerializer.Serialize(pos));

                pos = Get_1_2_Answer(commands);
                Console.WriteLine(JsonSerializer.Serialize(pos));
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    }
}