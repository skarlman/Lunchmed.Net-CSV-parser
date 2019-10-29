namespace SpanStringParser.RowParsers
{
    public interface ICsvRowParser
    {
        (string timestamp, string csvValue) ParseRow(string row);
    }
}