using System;

namespace SpanStringParser.RowParsers
{
    public static class CsvSpanExtensions
    {
        public static ReadOnlySpan<char> MoveNext(this ReadOnlySpan<char> rowSpan, int times)
        {
            for (var i = 0; i < times; i++)
            {
                rowSpan = rowSpan.Slice(rowSpan.IndexOf(';') + 1);
            }

            return rowSpan;
        }

        public static ReadOnlySpan<char> GetValue(this ReadOnlySpan<char> rowSpan)
        {
            var separatorIndex = rowSpan.IndexOf(';');
            var timestamp = rowSpan.Slice(0, separatorIndex);
            return timestamp;
        }
    }
}