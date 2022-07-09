using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2LINQ.Strategy
{
    public class ParallelLinqSolver : BasePartAllSolvers, ITextSolver
    {
        protected override List<string[]> _data { get; set; }
        public ParallelLinqSolver(List<string[]> data) => _data = data ?? throw new ArgumentNullException();
        public string FindLongestWord()
        {
            return _data.AsParallel().SelectMany(x => x).Distinct().OrderByDescending(len => len.Length).First();
        }

        public Dictionary<int, int> GetStatisticWords(string path)
        {
            var statistic = Directory
                                .EnumerateFiles(path)
                                .AsParallel()
                                .SelectMany(file => File.ReadAllLines(file))
                                .SelectMany(line => line.Split(seporators, StringSplitOptions.RemoveEmptyEntries))
                                .GroupBy(word => word.Length)
                                .Select(pair => new { pair.Key, Count = pair.Count() })
                                .OrderBy(x => -x.Key).ToArray()
                                .ToDictionary(x => x.Key, x => x.Count);
            return statistic;
        }

        public string GetUniqueFileName(string path)
        {
            throw new NotImplementedException();
        }

        public string[] GetWordsFrequency(string path, int countWord)
        {
            return Directory
                                .EnumerateFiles(path)
                                .AsParallel()
                                .SelectMany(file => File.ReadAllLines(file))
                                .SelectMany(line => line.Split(seporators, StringSplitOptions.RemoveEmptyEntries))
                                .GroupBy(word => word)
                                .Select(g => new { g.Key, Value = g.Count() })
                                .OrderBy(pair => -pair.Value)
                                .Take(countWord)
                                .Select(x => x.Key)
                                .ToArray();
        }
    }
}
