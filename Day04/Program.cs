using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Core;

namespace Day04
{
    class Program
    {
        public const int MaxColumn = 5;
        public const int MaxRow = 5;

        public struct BoardCell
        {
            public int Value { get; set; }
            public bool Marked { get; set; }
        }

        public struct Position
        {
            public int Row { get; set; }
            public int Column { get; set; }
        }

        public class Board
        {
            public BoardCell[][] Cells { get; set; } = new BoardCell[MaxRow][];
            public int FinalValue { get; set; } = -1;

            public Board()
            {
                for (var r = 0; r < MaxRow; r++)
                {
                    Cells[r] = new BoardCell[MaxColumn];
                    for (var c = 0; c < MaxColumn; c++)
                    {
                        Cells[r][c] = new BoardCell { Value = 0, Marked = false };
                    }
                }
            }

            public void SetValue(int row, int column, int value)
            {
                Cells[row][column].Value = value;
            }

            public void Mark(Position? position)
            {
                if (!position.HasValue)
                {
                    return;
                }

                Cells[position.Value.Row][position.Value.Column].Marked = true;
            }

            public bool IsBingo()
            {
                for (var row = 0; row < MaxRow; row++)
                {
                    if (Cells[row].All(x => x.Marked))
                    {
                        return true;
                    }
                }

                for (var column = 0; column < MaxColumn; column++)
                {
                    for (var row = 0; row < MaxRow; row++)
                    {
                        if (!Cells[row][column].Marked)
                        {
                            continue;
                        }

                        return true;
                    }
                }

                return false;
            }

            public Position? FindValue(int value)
            {
                for (var row = 0; row < MaxRow; row++)
                {
                    for (var column = 0; column < MaxColumn; column++)
                    {
                        if (Cells[row][column].Value == value)
                        {
                            return new Position { Row = row, Column = column };
                        }
                    }
                }

                return null;
            }

            public int SumOfUnmarked()
            {
                var values = new List<int>();

                for (var row = 0; row < MaxRow; row++)
                {
                    for (var column = 0; column < MaxColumn; column++)
                    {
                        if (!Cells[row][column].Marked)
                        {
                            values.Add(Cells[row][column].Value);
                        }
                    }
                }

                return values.Sum();
            }
        }

        static async Task Main(string[] args)
        {
            try
            {
                var inputManager = new InputManager(4);

                var lines = await inputManager.GetInput();
                var drawNumbers = lines.First().Split(',').Select(x => Convert.ToInt32(x));
                lines.RemoveAt(0);
                var boardInputs = lines.ChunkBy(MaxRow);
                var boards = new List<Board>();

                foreach (var boardInput in boardInputs)
                {
                    var board = new Board();

                    for (var row = 0; row < MaxRow; row++)
                    {
                        var rowInput = boardInput[row]
                            .TrimStart()
                            .TrimEnd()
                            .Replace("  ", " ")
                            .Split(" ")
                            .Select(x => Convert.ToInt32(x.Trim()))
                            .ToArray();
                        for (var column = 0; column < MaxColumn; column++)
                        {
                            board.SetValue(row, column, rowInput[column]);
                        }
                    }

                    boards.Add(board);
                }

                Console.WriteLine(Get_1_1_Answer(drawNumbers, boards));
                // Console.WriteLine(Get_1_2_Answer(lines));
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }

        private static int Get_1_1_Answer(IEnumerable<int> drawNumbers, List<Board> boards)
        {
            Board bingoedBoard = null;

            foreach (var drawNumber in drawNumbers)
            {
                if (bingoedBoard != null)
                {
                    break;
                }

                foreach (var board in boards)
                {
                    var pos = board.FindValue(drawNumber);
                    board.Mark(pos);
                    if (board.IsBingo())
                    {
                        board.FinalValue = drawNumber;
                        bingoedBoard = board;
                        break;
                    }
                }
            }

            return bingoedBoard.SumOfUnmarked() * bingoedBoard.FinalValue;
        }
    }
}

public static class ListExtensions
{
    public static List<List<T>> ChunkBy<T>(this List<T> source, int chunkSize)
    {
        return source
            .Select((x, i) => new { Index = i, Value = x })
            .GroupBy(x => x.Index / chunkSize)
            .Select(x => x.Select(v => v.Value).ToList())
            .ToList();
    }
}