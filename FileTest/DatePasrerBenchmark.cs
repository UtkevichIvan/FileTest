using BenchmarkDotNet.Attributes;
using FileTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTest
{
    [MemoryDiagnoser]
    public class DatePasrerBenchmark
    {
        private static readonly DateParser parser = new DateParser();

        [Benchmark]
        public void Test()
        {
            parser.Get();
        }

    }
}
