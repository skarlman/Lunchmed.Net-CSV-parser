using System;
using System.Security.Authentication.ExtendedProtection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SpanStringParser.Domain;
using SpanStringParser.Domain.FileParsers;
using SpanStringParser.Domain.RowParsers;

namespace SpanStringParser
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = SetupDependencies();

            var fileParser = serviceProvider.GetService<AsyncFileParser>();
            var result = await fileParser.ParseFiles(@"data");

            result.ForEach(r => Console.WriteLine($"{r.timestamp}: {r.csvValue}"));
        }

        private static IServiceProvider SetupDependencies()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<ICsvRowParser, SpanRowParser>()
                .AddSingleton<IFileRetriever, FileRetriever>()
                .AddSingleton<AsyncFileParser>()
                .BuildServiceProvider();
            return serviceProvider;
        }
    }
}
