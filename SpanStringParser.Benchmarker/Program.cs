using System;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using SpanStringParser.RowParsers;

namespace SpanStringParser.Benchmarker
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<ParserBenchmarker>();
        }
    }

    [RPlotExporter, RankColumn, MemoryDiagnoser]
    public class ParserBenchmarker
    {
        [Benchmark]
        public void SyncSpanParser()
        {
            var result = new FileParser(new SpanRowParser()).ParseFilesSync(@"data");

        }

        [Benchmark]
        public async Task IAsyncEnumerableSpanParser()
        {
            var result = await new FileParser(new SpanRowParser()).ParseFiles(@"data");

        }

        [Benchmark]
        public async Task StringSplitParserAsync()
        {
            var result = await new FileParser(new StringSplitRowParser()).ParseFiles(@"data");

        }

        [Benchmark]
        public void StringSplitParserSync()
        {
            var result = new FileParser(new StringSplitRowParser()).ParseFilesSync(@"data");
        }
    }
}
