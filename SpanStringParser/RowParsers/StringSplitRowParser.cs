namespace SpanStringParser.RowParsers
{
    public class StringSplitRowParser : ICsvRowParser
    {
        public (string , string) ParseRow(string row)
        {
            var lineParts = row.Split(';');
            return (lineParts[0], lineParts[5]);
        }
    }
}