using System.Collections.Generic;
using System.IO;

namespace SpanStringParser
{
    public class FileRetriever : IFileRetriever
    {
        public IEnumerable<string> GetFilenames(string dataFolderPath) 
            => Directory.EnumerateFiles(dataFolderPath, "*.csv");

        public string[] FileRows(string fileName) 
            => File.ReadAllLines(fileName);

        public async IAsyncEnumerable<string> FileRowsAsync(string fileName)
        {
            using var reader = new StreamReader(fileName);

            while (!reader.EndOfStream)
            {
                yield return await reader.ReadLineAsync();
            }
        }
    }
}