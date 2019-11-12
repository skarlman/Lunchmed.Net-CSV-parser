using System.Collections.Generic;
using System.Threading.Tasks;
using SpanStringParser.Domain.RowParsers;

namespace SpanStringParser.Domain.FileParsers
{
    public class AsyncFileParser
    {
        public readonly ICsvRowParser _csvRowParser;
        private readonly IFileRetriever _fileRetriever;

        public AsyncFileParser(ICsvRowParser csvRowParser, IFileRetriever fileRetriever)
        {
            _csvRowParser = csvRowParser;
            _fileRetriever = fileRetriever;
        }

        public async Task<List<(string timestamp, string csvValue)>> ParseFiles(string dataFolderPath)
        {
            var result = new List<(string timestamp, string csvValue)>();

            foreach (var file in _fileRetriever.GetFilenames(dataFolderPath))
            {
                await foreach (var row in _fileRetriever.FileRowsAsync(file))
                {
                    result.Add(_csvRowParser.ParseRow(row));
                }
            }

            return result;
        }
    }
}