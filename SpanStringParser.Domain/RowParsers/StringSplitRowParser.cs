namespace SpanStringParser.Domain.RowParsers
{
    public class StringSplitRowParser : ICsvRowParser
    {
        public (string timestamp, string csvValue) ParseRow(string row)
        {
            var lineParts = row.Split(';');
            return (lineParts[0], lineParts[5]);
        }
    }
}