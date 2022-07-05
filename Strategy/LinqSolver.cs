using System;
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
        private static readonly string[] seporators = { " ", ".", ",", "!", "#", "$", "%", "^", "&", "*", "(", ")", "-", "_", "+", "/", "[", "]", ":", ";", "\"", "\r", "\n", "\t" };
        public LinqSolver(List<string[]> data) => _data = data ?? throw new ArgumentNullException();
        public string FindLongestWord()
        {
            var result = _data.SelectMany(x => x).Distinct().OrderByDescending(len => len.Length).First();
            return result;
        }
        public Dictionary<int, int> GetStatisticWords()
        {
            // Получить словарь где ключ длина слова, значение число слов такое длины
            var dictionary = _data
                                .SelectMany(x => x)
                                .Aggregate(new Dictionary<int, int>(),
                                    (dic, w) =>
                                    {
                                        dic[w.Length] = dic.ContainsKey(w.Length) ?
                                        dic[w.Length] + 1 : 1;
                                        return dic;
                                    });
            // Получить нужную структуру
            List<(int LenghtName, int Count)> someStruct = GetStruct(TypeTask.GetStatisticByWords, dictionary) as List<(int LenghtName, int Count)>;
            // Упорядочить по убыванию
            someStruct.Sort(new ComparerToIntAndInt() { Field = ComparerToIntAndInt.CompareField.ByDescending});

            dictionary = new Dictionary<int, int>();
            foreach (var pair in someStruct)
            {
                dictionary.Add(pair.LenghtName, pair.Count);
            }
            return dictionary;
        }
        public string GetUniqueFileName(string path)
        {
            var pathFiles = Directory.EnumerateFiles(path).ToArray();
            var wordsArr = pathFiles.Select(x => File.ReadAllLines(x, Encoding.UTF8).SelectMany(w => w.Split(seporators, StringSplitOptions.RemoveEmptyEntries))).ToArray();

            List<string[]> dataSource = new List<string[]>();

            for (int i = 0; i < length; i++)
            {

            }

            return default;
        }
        public string[] GetWordsFrequency(int countWord)
        {
            // Получить словарь где ключ - слово, значение - число повторений
            var dictionary = _data
                                .SelectMany(x => x)
                                .Aggregate(new Dictionary<string, int>(),
                                    (dic, w) =>
                                    {
                                        dic[w] = dic.ContainsKey(w) ?
                                        dic[w] + 1 : 1;
                                        return dic;
                                    });
            // Получить нужную структуру
            List<(string Name, int Count)> listForSort = GetStruct(TypeTask.GetWordFrequency, dictionary) as List<(string Name, int Count)>;

            listForSort.Sort(new ComparerToStringAndInt() { Field = ComparerToStringAndInt.CompareField.ByDescending });

            List<(string Name, int Count)> qqq = new List<(string Name, int Count)>();
            if (listForSort.Count > countWord)
            {
                qqq = listForSort.Take(countWord).ToList();
            }
            else
                qqq = listForSort.ToList();
            string[] result = qqq.Select(x => x.Name).ToArray();
            return result;
        }
    }
}
