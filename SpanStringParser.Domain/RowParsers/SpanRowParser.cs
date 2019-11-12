using System;

namespace SpanStringParser.Domain.RowParsers
{
    public class SpanRowParser : ICsvRowParser
    {
        public (string timestamp, string csvValue) ParseRow(string row)
        {
            var rowSpan = row.AsSpan();

            var timestamp = rowSpan.GetValue();

            rowSpan = rowSpan.MoveNext(5);

            var value = rowSpan.GetValue();

            return (timestamp.ToString(), value.ToString());
        }
    }
}