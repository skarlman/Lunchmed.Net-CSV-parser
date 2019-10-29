using System;
using System.Threading.Tasks;
using SpanStringParser.RowParsers;

namespace SpanStringParser
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var result = await new AsyncFileParser(new StringSplitRowParser(), new FileRetriever()).ParseFiles(@"data");

            result.ForEach(r => Console.WriteLine($"{r.timestamp}: {r.csvValue}"));
        }
    }
}
