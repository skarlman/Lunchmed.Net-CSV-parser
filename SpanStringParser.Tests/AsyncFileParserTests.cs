using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using NSubstitute;
using SpanStringParser.Domain;
using SpanStringParser.Domain.FileParsers;
using SpanStringParser.Domain.RowParsers;
using Xunit;

namespace SpanStringParser.Tests
{
    public class AsyncFileParserTests
    {
        [Fact]
        public async Task WHEN_given_a_path_THEN_filenames_are_retrieved()
        {
            var someDataFolderPath = _fixture.Create<string>();
            
            await _sut.ParseFiles(someDataFolderPath);

            _fileRetriever.Received(1).GetFilenames(someDataFolderPath);
        }

        [Fact]
        public async Task WHEN_files_exist_THEN_file_contents_is_retrieved_for_all()
        {
            var someFilenames = _fixture.CreateMany<string>().ToArray();
            _fileRetriever.GetFilenames(Arg.Any<string>())
                .Returns(someFilenames);

            await _sut.ParseFiles(_fixture.Create<string>());

            someFilenames.Select(f => _fileRetriever.Received(1).FileRowsAsync(f)).ToArray();
        }

        [Fact]
        public async Task WHEN_data_files_exist_THEN_all_content_is_sent_to_a_CsvParser()
        {
            var someFileContents = _fixture.CreateMany<string>();
            var someFilenames = _fixture.CreateMany<string>().ToArray();

            _fileRetriever.FileRowsAsync(someFilenames.First())
                .Returns(someFileContents.ToAsyncEnumerable());
            _fileRetriever.GetFilenames(Arg.Any<string>())
                .Returns(someFilenames);

            await _sut.ParseFiles(_fixture.Create<string>());

            someFileContents.Select(f => _csvParser.Received(1).ParseRow(f))
                .ToArray();
        }

        private readonly IFileRetriever _fileRetriever;
        private readonly AsyncFileParser _sut;

        public AsyncFileParserTests()
        {
            _fileRetriever = _fixture.Freeze<IFileRetriever>();
            _csvParser = _fixture.Freeze<ICsvRowParser>();
            
            _sut = _fixture.Create<AsyncFileParser>();
        }

        IFixture _fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
        private readonly ICsvRowParser _csvParser;
    }
}