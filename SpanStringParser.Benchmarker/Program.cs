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
        public void SyncFileRead_SpanParser()
        {
            var result = new FileParser(new SpanRowParser(), new FileRetriever()).ParseFilesSync(@"data");

        }

        [Benchmark]
        public async Task IAsyncEnumerableFileRead_SpanParser()
        {
            var result = await new AsyncFileParser(new SpanRowParser(), new FileRetriever()).ParseFiles(@"data");

        }

        [Benchmark]
        public async Task IAsyncEnumerableFileRead_StringSplitParser()
        {
            var result = await new AsyncFileParser(new StringSplitRowParser(), new FileRetriever()).ParseFiles(@"data");

        }

        [Benchmark]
        public void SyncFileRead_StringSplitParser()
        {
            var result = new FileParser(new StringSplitRowParser(), new FileRetriever()).ParseFilesSync(@"data");
        }
    }
}
