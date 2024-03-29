﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2LINQ.Comparers;

namespace Task2LINQ.Strategy
{
    public class LinqSolver : BasePartAllSolvers, ITextSolver
    {
        protected override List<string[]> _data { get; set ; }
        
        public LinqSolver(List<string[]> data) => _data = data ?? throw new ArgumentNullException();
        public string FindLongestWord()
        {
            var q = from arr in _data
                    from word in arr
                    orderby word.Length descending
                    select word;

            var p = q.ToArray();
            return p[0];
            //return _data.SelectMany(x => x).Distinct().OrderByDescending(x => x.Length).First();
        }
        public Dictionary<int, int> GetStatisticWords(string path)
        {
            return Directory
                                .EnumerateFiles(path)
                                .SelectMany(file => File.ReadAllLines(file))
                                .SelectMany(line => line.Split(seporators, StringSplitOptions.RemoveEmptyEntries))
                                .GroupBy(word => word.Length)
                                .Select(pair => new { pair.Key, Count = pair.Count() })
                                .OrderBy(x => -x.Key).ToArray()
                                .ToDictionary(x => x.Key, x => x.Count); ;
        }
        public string GetUniqueFileName(string path)
        {
            var pathFiles = Directory.EnumerateFiles(path).ToArray();
            

            List<string[]> dataSource = new List<string[]>();

            foreach (var item in pathFiles)
            {
                // Выборка слов с одного файла
                var res = File.ReadAllLines(item, Encoding.UTF8)
                    .SelectMany(x => x.Split(seporators, StringSplitOptions.RemoveEmptyEntries))
                    .Select(x => x.ToLower())
                    .GroupBy(x => x)
                    .OrderByDescending(x => x.Key)
                    ;
                // Добавляем слова в структуру
                //dataSource.Add(res);
            }


            return default;
        }
        public string[] GetWordsFrequency(string path, int countWord)
        {
            return Directory
                                .EnumerateFiles(path)
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
