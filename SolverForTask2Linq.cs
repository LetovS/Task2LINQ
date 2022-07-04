using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2LINQ
{
    public class Datas
    {
        public string Word { get; set; }
        public int Count { get; set; }

        public override string ToString()
        {
            return Word;
        }
    }
    public class MethodComparer : IComparer<Datas>
    {
        public int Compare(Datas x, Datas y)
        {
            return -x.Count.CompareTo(y.Count);
        }
    }
    public static class SolverForTask2Linq
    {

    }
    public interface ITextSolver
    {
        string GetLongestWord(string path);
        List<Datas> GetTopWords(string path, int values);
        Dictionary<int, int> GetStatisticOnWordsLenght();
        Dictionary<int, int> GetFileWithMaxCountUniqueWords();
    }
    public abstract class BaseTextFileSolver
    {
        protected char[] separators = { ' ', ':', ';', '/', '*', '-', '+', '\r', '\n', '\t', ',', '.', '?', '!', '"', '[', ']', '{', '}', '@', '%', '$', '&' };
        protected string[] separators1 = { " ", ":", ";", "/", "*", "-", "+", "\r", "\n", "\t", ",", ".", "?", "!", "\"", "[", "]", "{", "}", "@", "%", "$", "&" };
        protected List<string[]> GetFilesFromPath(string path)
        {
            var files = Directory.EnumerateFiles(path, "*.txt").ToArray();
            string[] words;
            List<string[]> app = new List<string[]>();
            foreach (var file in files)
            {
                words = File.ReadAllLines(file, Encoding.UTF8);
                var p = words.SelectMany(x => x.Split(separators1, StringSplitOptions.RemoveEmptyEntries)).Select(x => x).ToArray();
                app.Add(p);
            }

            return app;
        }

    }

    public class LinqSolver : BaseTextFileSolver, ITextSolver
    {
        private List<string[]> sourceData { get; set; }
        public LinqSolver(string path)
        {
            sourceData = GetFilesFromPath(path);
        }

        public Dictionary<int,int> GetFileWithMaxCountUniqueWords()
        {
            throw new NotImplementedException();
        }

        public string GetLongestWord(string path)
        {
            var p = sourceData.SelectMany(x => x).Distinct().OrderByDescending(x => x.Length).First();
            return p;
        }

        public Dictionary<int, int> GetStatisticOnWordsLenght()
        {
            var result = sourceData
                .SelectMany(x => x)
                .Aggregate(new Dictionary<int, int>(),
                           (dic, w) =>
                           {
                               dic[w.Length] = dic.ContainsKey(w.Length) ? dic[w.Length] + 1 : 1; return dic;
                           });
            var q = Sorted(result, result.Count);
            Dictionary<int, int> resultishe = new Dictionary<int, int>();
            foreach (var item in q)
            {
                resultishe.Add(item.Count,int.Parse(item.Word));
            }
            return resultishe;
        }

        internal static List<Datas> Sorted(Dictionary<int, int> groupedMethods, int count)
        {
            List<Datas> datas = GetNeedStruct(groupedMethods);
            MethodComparer compar = new MethodComparer();
            datas.Sort(compar);
            datas.Take(count).ToList();
            return datas;
        }
        private static List<Datas> GetNeedStruct(Dictionary<int, int> groupedMethods)
        {
            List<Datas> datas = new List<Datas>();
            foreach (KeyValuePair<int, int> dataMethod in groupedMethods)
            {
                Datas temp = new Datas();
                temp.Word = dataMethod.Value.ToString();
                temp.Count = dataMethod.Key;
                datas.Add(temp);
                temp = null;
            }
            return datas;
        }


        public List<Datas> GetTopWords(string path, int values)
        {
            var p = sourceData
                    .SelectMany(x => x)
                    .Aggregate(new Dictionary<string, int>(), (dic, w) => { dic[w] = dic.ContainsKey(w) ? dic[w] + 1 : 1; return dic; });
            var q = Sorted(p, values).Take(10).ToList();
            return q;
        }

        internal static List<Datas> Sorted(Dictionary<string, int> groupedMethods, int count)
        {
            List<Datas> datas = GetNeedStruct(groupedMethods);
            MethodComparer compar = new MethodComparer();
            datas.Sort(compar);
            datas.Take(count).ToList();
            return datas;
        }
        /// <summary>
        /// Объединяет данные для сортировки.
        /// </summary>
        private static List<Datas> GetNeedStruct(Dictionary<string, int> groupedMethods)
        {
            List<Datas> datas = new List<Datas>();
            foreach (KeyValuePair<string, int> dataMethod in groupedMethods)
            {
                Datas temp = new Datas();
                temp.Word = dataMethod.Key;
                temp.Count = dataMethod.Value;
                datas.Add(temp);
                temp = null;
            }
            return datas;
        }
    }
    public class ParallelLinqSolver : BaseTextFileSolver, ITextSolver
    {
        public Dictionary<int, int> GetFileWithMaxCountUniqueWords()
        {
            throw new NotImplementedException();
        }

        public string GetLongestWord(string path)
        {
            throw new NotImplementedException();
        }

        public Dictionary<int, int> GetStatisticOnWordsLenght()
        {
            throw new NotImplementedException();
        }

        public List<Datas> GetTopWords(string path, int values)
        {
            throw new NotImplementedException();
        }
    }
    public class SequentialSolver : BaseTextFileSolver, ITextSolver
    {
        public Dictionary<int, int> GetFileWithMaxCountUniqueWords()
        {
            throw new NotImplementedException();
        }

        public string GetLongestWord(string path)
        {
            throw new NotImplementedException();
        }

        public Dictionary<int, int> GetStatisticOnWordsLenght()
        {
            throw new NotImplementedException();
        }

        public List<Datas> GetTopWords(string path, int values)
        {
            throw new NotImplementedException();
        }
    }
}
