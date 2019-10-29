using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.AutoNSubstitute.Extensions;
using NSubstitute;
using SpanStringParser.RowParsers;
using Xunit;

namespace SpanStringParser.Tests
{
    public class AsyncFileParserTests
    {
        IFixture _fixture = new Fixture().Customize(new AutoNSubstituteCustomization());

        [Fact]
        public async Task When_given_a_path_filenames_are_retrieved()
        {

            var fileRetriever = _fixture.Freeze<IFileRetriever>();
            
            var SUT = _fixture.Create<AsyncFileParser>();

            var someDataFolderPath = _fixture.Create<string>();
            var result = await SUT.ParseFiles(someDataFolderPath);

            fileRetriever.Received(1).GetFilenames(someDataFolderPath);

        }
        
        [Fact]
        public async Task AllFilesAreIterated()
        {
            var filenames = _fixture.CreateMany<string>().ToArray();
            
            var fileRetriever = _fixture.Freeze<IFileRetriever>();
            fileRetriever.GetFilenames(Arg.Any<string>())
                .Returns(filenames);

            var SUT = _fixture.Create<AsyncFileParser>();

            var result = await SUT.ParseFiles(_fixture.Create<string>());

            filenames.Select(f => fileRetriever.Received(1).FileRowsAsync(f))
                     .ToArray();
        }
    }
}