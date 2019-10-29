using System.Collections.Generic;

namespace SpanStringParser
{
    public interface IFileRetriever
    {
        IEnumerable<string> GetFilenames(string dataFolderPath);
        IAsyncEnumerable<string> FileRowsAsync(string fileName);
    }
}