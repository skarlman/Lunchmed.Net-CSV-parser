using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;

namespace SpanStringParser
{
    class Program
    {
        static async Task Main(string[] args)
        {
            
            FasterFileParser();
            
            //AdhocFileParser();

            //await new RandomNumberGeneratorExampleMachine().RandomGeneratorExample();
        }

        private static async void FasterFileParser()
        {
            foreach (var file in GetFilenames())
            {
                await foreach (var row in FileRows(file))
                {
                    var (timestamp, value) = ParseRow(row);
                    Console.WriteLine($"{timestamp}: {value}");
                }
            }
        }

        private static (string, string) ParseRow(string row)
        {
            var rowSpan = row.AsSpan();

            var separatorIndex = rowSpan.IndexOf(';');
            var timestamp = rowSpan.Slice(0, separatorIndex);

            rowSpan = rowSpan.Slice(separatorIndex + 1);

            for (int i = 0; i < 4; i++)
            {
                separatorIndex = rowSpan.IndexOf(';');
                rowSpan = rowSpan.Slice(separatorIndex + 1);
            }

            separatorIndex = rowSpan.IndexOf(';');
            var value = rowSpan.Slice(0, separatorIndex);

            return (timestamp.ToString(), value.ToString());
        }

        private static async IAsyncEnumerable<string> FileRows(string fileName)
        {
            using var reader = new StreamReader(fileName);

            while (!reader.EndOfStream)
            {
                yield return await reader.ReadLineAsync();
            }
        }

        private static IEnumerable<string> GetFilenames()
        {
            foreach (var filename in Directory.EnumerateFiles(@"..\..\..\..\data\", "*.csv"))
            {
                yield return filename;
            }
        }

        private static void AdhocFileParser()
        {
            var files = Directory.EnumerateFiles(@"..\..\..\..\data\", "*.csv");

            foreach (var file in files)
            {
                using (var reader = new StreamReader(file))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var lineParts = line.Split(';');
                        Console.WriteLine($"{lineParts[0]}: {lineParts[5]}");
                    }
                }
            }
        }

        private static int AddTen(int i)
        {
            i = 7;
            return i + 10;
        }
    }
}
