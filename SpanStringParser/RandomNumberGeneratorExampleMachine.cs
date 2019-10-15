using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpanStringParser
{
    class RandomNumberGeneratorExampleMachine
    {
        public async Task RandomGeneratorExample()
        {
            await foreach (var i in AsyncEnumerable.Skip<int>(RandomIntGeneratorOnTheInternet(), 10).Take(10))
            {
                Console.WriteLine(i);
            }
        }

        public async IAsyncEnumerable<int> RandomIntGeneratorOnTheInternet()
        {
            while (true)
            {
                await Task.Delay(100);
                yield return new Random().Next();
            }
        }
    }
}