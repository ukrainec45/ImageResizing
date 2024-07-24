using BenchmarkDotNet.Running;

namespace ImageResizing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run(typeof(Benchmark).Assembly);
        }
    }
}
