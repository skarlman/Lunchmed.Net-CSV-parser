using System;
using System.Collections.Generic;
using System.IO;
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
        public static IEnumerable<string> GetFilenames(string dataFolderPath)
        {
            foreach (var filename in Directory.EnumerateFiles(dataFolderPath, "*.csv"))
            {
                yield return filename;
            }
        }

        private string[] FileRowsSync(string fileName)
        {
            return File.ReadAllLines(fileName);
        }
    }
}