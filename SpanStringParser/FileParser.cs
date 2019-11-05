using System;
using System.Collections.Generic;
using System.IO;
using SpanStringParser.RowParsers;

namespace SpanStringParser
{
    public class FileParser
    {
        private readonly ICsvRowParser _csvRowParser;
        private readonly IFileRetriever _fileRetriever;

        public FileParser(ICsvRowParser csvRowParser, IFileRetriever fileRetriever)
        {
            _csvRowParser = csvRowParser;
            _fileRetriever = fileRetriever;
        }

        public List<(string, string)> ParseFilesSync(string dataFolderPath)
        {
            var result = new List<(string, string)>();
            
            foreach (var file in _fileRetriever.GetFilenames(dataFolderPath))
            {
                foreach (var row in _fileRetriever.FileRows(file))
                {
                    result.Add(_csvRowParser.ParseRow(row));
                }
            }

            return result;
        }
        
    }
}