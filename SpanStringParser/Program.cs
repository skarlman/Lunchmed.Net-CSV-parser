using System;
using System.Threading.Tasks;
using SpanStringParser.RowParsers;

namespace SpanStringParser
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var result = await new FileParser(new StringSplitRowParser()).ParseFiles(@"data");

            result.ForEach(r => Console.WriteLine($"{r.Item1}: {r.Item2}"));
        }
    }
}
