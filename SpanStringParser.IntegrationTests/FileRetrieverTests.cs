using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using NSubstitute;
using SpanStringParser.Domain;
using SpanStringParser.Domain.FileParsers;
using SpanStringParser.Domain.RowParsers;
using Xunit;

namespace SpanStringParser.IntegrationTests
{
    public class FileRetrieverTests
    {
        [Fact]
        public async Task WHEN_data_file_exists_THEN_it_is_read_and_parsed_by_some_CsvRowParser()
        {
            var testdataRealFilename = $"{Environment.CurrentDirectory}\\testdata-deleteme-{Guid.NewGuid()}.csv";

            var theFileContents = GetSomeFileContents();

            File.WriteAllText(testdataRealFilename, theFileContents);

            try
            {
                var csvRowParser = _fixture.Freeze<ICsvRowParser>();

                var SUT = new AsyncFileParser(csvRowParser, new FileRetriever());

                await SUT.ParseFiles(Environment.CurrentDirectory);

                theFileContents.Split(Environment.NewLine)
                    .Select(r => csvRowParser.Received(1).ParseRow(r)).ToArray();
            }
            finally
            {
                File.Delete(testdataRealFilename);
            }


        }

        private string GetSomeFileContents()
        {
            return @"2019-07-08 06:05:58;11455.5;11451.76;11447.67;11451.64333;523.187246;539.886408;523.07;2019-07-08 06:04:16;493.4936892;493.38
2019-07-08 06:06:58;11455.5;11455.53;11451.095;11454.04167;523.3003152;539.9994772;523.07;2019-07-08 06:04:16;493.600477;493.38
2019-07-08 06:07:58;11455.5;11455.73;11450.005;11453.745;523.2863288;539.9854908;523.07;2019-07-08 06:04:16;493.5872677;493.38";
        }

        IFixture _fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
    }
}
