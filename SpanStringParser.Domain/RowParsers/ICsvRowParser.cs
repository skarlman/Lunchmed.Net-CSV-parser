namespace SpanStringParser.Domain.RowParsers
{
    public interface ICsvRowParser
    {
        (string timestamp, string csvValue) ParseRow(string row);
    }
}