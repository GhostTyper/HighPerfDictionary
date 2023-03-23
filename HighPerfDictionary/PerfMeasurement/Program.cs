using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using HighPerfDictionary;

namespace PerfMeasurement
{
    public class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<Program>();

            //Program prog = new Program();

            //foreach (object[] objs in prog.GiveHighPerfDictionary())
            //    prog.HighPerfDictionary((HighPerfDictionary<string>)objs[0], (string)objs[1]);
        }

        public IEnumerable<object[]> GiveDictionary()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();

            for (int count = 0; count < 1024; count++)
                dict.Add($"I{count:X03}", count.ToString());

            yield return new object[] { dict, $"I{666:X03}" };
        }

        public IEnumerable<object[]> GiveHighPerfDictionary()
        {
            KeyValuePair<string, string>[] kvps = new KeyValuePair<string, string>[1024];

            for (int count = 0; count < 1024; count++)
                kvps[count] = new KeyValuePair<string, string>($"I{count:X03}", count.ToString());

            yield return new object[] { new HighPerfDictionary<string>(kvps), $"I{666:X03}" };
        }

        [Benchmark]
        [ArgumentsSource(nameof(GiveDictionary))]
        public string DotNetDictionary(Dictionary<string, string> dict, string search)
        {
            string? str;

            if (dict.TryGetValue(search, out str))
                return str;

            throw new InvalidOperationException("String not found.");
        }

        [Benchmark]
        [ArgumentsSource(nameof(GiveHighPerfDictionary))]
        public string HighPerfDictionary(HighPerfDictionary<string> dict, string search)
        {
            string? str;

            if (dict.TryGetValue(search, out str))
                return str;

            throw new InvalidOperationException("String not found.");
        }
    }
}