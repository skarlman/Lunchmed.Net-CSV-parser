using System.Linq;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using NSubstitute;
using SpanStringParser.RowParsers;
using Xunit;

namespace SpanStringParser.Tests
{
    public class FileParserTests
    {
        [Fact]
        public void WHEN_given_a_path_THEN_filenames_are_retrieved()
        {
            var someDataFolderPath = _fixture.Create<string>();
            
            _sut.ParseFilesSync(someDataFolderPath);

            _fileRetriever.Received(1).GetFilenames(someDataFolderPath);
        }

        [Fact]
        public void WHEN_files_exist_THEN_file_contents_is_retrieved_for_all()
        {
            var someFilenames = _fixture.CreateMany<string>().ToArray();
            _fileRetriever.GetFilenames(Arg.Any<string>())
                .Returns(someFilenames);

            _sut.ParseFilesSync(_fixture.Create<string>());

            someFilenames.Select(f => _fileRetriever.Received(1).FileRows(f)).ToArray();
        }

        [Fact]
        public void WHEN_data_files_exist_THEN_all_content_is_sent_to_a_CsvParser()
        {
            var someFileContents = _fixture.CreateMany<string>().ToArray();
            var someFilenames = _fixture.CreateMany<string>().ToArray();

            _fileRetriever.FileRows(someFilenames.First())
                .Returns(someFileContents);
            _fileRetriever.GetFilenames(Arg.Any<string>())
                .Returns(someFilenames);
            
            _sut.ParseFilesSync(_fixture.Create<string>());

            someFileContents.Select(f => _csvParser.Received(1).ParseRow(f))
                .ToArray();
        }

        private readonly IFileRetriever _fileRetriever;
        private readonly ICsvRowParser _csvParser;
        private readonly FileParser _sut;
        
        public FileParserTests()
        {
            _fileRetriever = _fixture.Freeze<IFileRetriever>();
            _csvParser = _fixture.Freeze<ICsvRowParser>();

            _sut = _fixture.Create<FileParser>();
        }

        IFixture _fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
    }
}