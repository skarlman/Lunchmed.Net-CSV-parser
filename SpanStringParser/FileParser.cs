using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SpanStringParser.RowParsers;

namespace SpanStringParser
{
    public class FileParser
    {
        private readonly ICsvRowParser _csvRowParser;

        public FileParser(ICsvRowParser csvRowParser)
        {
            _csvRowParser = csvRowParser;
        }

        public async Task<List<(string, string)>> ParseFiles(string dataFolderPath)
        {
            var result = new List<(string, string)>();
            
            foreach (var file in GetFilenames(dataFolderPath))
            {
                await foreach (var row in FileRows(file))
                {
                    result.Add(_csvRowParser.ParseRow(row));
                }
            }

            return result;
        }
        public List<(string, string)> ParseFilesSync(string dataFolderPath)
        {
            var result = new List<(string, string)>();
            
            foreach (var file in GetFilenames(dataFolderPath))
            {
                foreach (var row in FileRowsSync(file))
                {
                    result.Add(_csvRowParser.ParseRow(row));
                }
            }

            return result;
        }

        private static async IAsyncEnumerable<string> FileRows(string fileName)
        {
            using var reader = new StreamReader(fileName);

            while (!reader.EndOfStream)
            {
                yield return await reader.ReadLineAsync();
            }
        }
        private string[] FileRowsSync(string fileName)
        {
            return File.ReadAllLines(fileName);
        }

        private static IEnumerable<string> GetFilenames(string dataFolderPath)
        {
            foreach (var filename in Directory.EnumerateFiles(dataFolderPath, "*.csv"))
            {
                yield return filename;
            }
        }
    }
}