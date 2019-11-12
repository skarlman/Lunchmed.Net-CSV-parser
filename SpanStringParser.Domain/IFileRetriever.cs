using System.Collections.Generic;

namespace SpanStringParser.Domain
{
    public interface IFileRetriever
    {
        IEnumerable<string> GetFilenames(string dataFolderPath);
        IAsyncEnumerable<string> FileRowsAsync(string fileName);
        string[] FileRows(string fileName);
    }
}