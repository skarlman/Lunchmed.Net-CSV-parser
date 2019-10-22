namespace SpanStringParser.RowParsers
{
    public interface ICsvRowParser
    {
        (string, string) ParseRow(string row);
    }
}