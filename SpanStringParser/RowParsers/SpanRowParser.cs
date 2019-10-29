using System;

namespace SpanStringParser.RowParsers
{
    public class SpanRowParser : ICsvRowParser
    {
        public (string timestamp, string csvValue) ParseRow(string row)
        {
            var rowSpan = row.AsSpan();

            var separatorIndex = rowSpan.IndexOf(';');
            var timestamp = rowSpan.Slice(0, separatorIndex);

            rowSpan = rowSpan.Slice(separatorIndex + 1);

            for (int i = 0; i < 4; i++)
            {
                separatorIndex = rowSpan.IndexOf(';');
                rowSpan = rowSpan.Slice(separatorIndex + 1);
            }

            separatorIndex = rowSpan.IndexOf(';');
            var value = rowSpan.Slice(0, separatorIndex);

            return (timestamp.ToString(), value.ToString());
        }
    }
}