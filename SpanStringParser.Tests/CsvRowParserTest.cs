using SpanStringParser.RowParsers;
using Xunit;

namespace SpanStringParser.Tests
{
    public class CsvRowParserTest
    {
        public class When_given_proper_csv_row
        {
            [Fact]
            public void Then_col_0_is_timestamp_and_col_5_is_value() =>
                Assert.Equal(("2019-07-04 06:10:01", "528.193428"), _result);


            public When_given_proper_csv_row()
            {
                _result = SUT.ParseRow(_row);

            }

            private (string, string) _result;

            private string _row = @"2019-07-04 06:10:01;11698.25;11713.35;11705.11;11705.57;528.193428;544.746208;527.96;2019-07-04 05:09:50;502.4428924;502.22";
            private SpanRowParser SUT => new SpanRowParser();
        }
    }
}