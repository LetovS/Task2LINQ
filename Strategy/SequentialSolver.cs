using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Task2LINQ.Comparers;
using static Task2LINQ.Comparers.BaseComparer;

namespace Task2LINQ.Strategy
{
    public class SequentialSolver : BasePartAllSolvers, ITextSolver
    {
        protected override List<string[]> _data { get; set ; }
        public SequentialSolver(List<string[]> data) => _data = data ?? throw new ArgumentNullException();

        public string FindLongestWord()
        {
            string longestWord = "";
            foreach (var item in _data)
            {
                string[] temp = item;
                for (int i = 0; i < temp.Length; i++)
                {
                    string tempWord = temp[i];
                    if (tempWord.Length > longestWord.Length)
                    {
                        longestWord = tempWord;
                    }
                }
            }
            return longestWord;
        }

        public Dictionary<int, int> GetStatisticWords(string path)
        {
            Dictionary<int, int> dic = new Dictionary<int, int>();

            string[] some = Directory.EnumerateFiles(path, "*.txt").ToArray();


            var data = new List<string[]>();
            foreach (var item in some)
            {
                // Выборка слов с одного файла
                var res = File.ReadAllLines(item, Encoding.UTF8).SelectMany(x => x.Split(seporators, StringSplitOptions.RemoveEmptyEntries)).ToArray();
                // Добавляем слова в структуру
                data.Add(res);
            }
            foreach (var arrStr in data)
            {
                string[] temp = arrStr;
                for (int i = 0; i < temp.Length; i++)
                {
                    string tempWord = temp[i];
                    if (dic.ContainsKey(tempWord.Length))
                    {
                        dic[tempWord.Length]++;
                    }
                    else
                        dic[tempWord.Length] = 1;
                }
            }
            List<(int lenghtName, int Count)> someTuple = GetStruct(TypeTask.GetStatisticByWords, dic) as List<(int lenghtName, int Count)>;

            someTuple.Sort(new ComparerToIntAndInt() { Field = CompareField.ByDescending});
            dic = new Dictionary<int, int>();
            foreach (var pair in someTuple)
            {
                dic.Add(pair.lenghtName, pair.Count);
            }
            return dic;
        }


        public string GetUniqueFileName(string path)
        {
            
            
            string[] some = Directory.EnumerateFiles(path, "*.txt").ToArray();
            string[] fileNames = new string[some.Length];

            for (int i = 0; i < some.Length; i++)
            {
                var q = some[i].Split('\\');
                fileNames[i] = q[q.Length - 1];
            }

            List<string[]> vs = new List<string[]>();

            
            foreach (var item in some)
            {
                // Выборка слов с одного файла
                var res = File.ReadAllLines(item, Encoding.UTF8).SelectMany(x => x.Split(seporators, StringSplitOptions.RemoveEmptyEntries)).ToArray();
                // Добавляем слова в структуру
                vs.Add(res);
            }

            List<string[]> temp = new List<string[]>();
            List<string> arrStr = new List<string>();

            foreach (var strArr in vs)
            {
                string[] tempr = strArr;
                string word;
                for (int i = 0; i < tempr.Length; i++)
                {
                    word = tempr[i];
                    if (!arrStr.Contains(word))
                        arrStr.Add(word);
                    
                }
                temp.Add(arrStr.ToArray());
            }

            vs = new List<string[]>();
            int index = -1;
            string[] empty = new string [0]; 
            for (int i = 0; i < temp.Count; i++)
            {
                string[] current = temp[i];
                for (int j = 0; j < temp.Count; j++)
                {
                    if (i != j)
                    {
                        string[] vs1 = temp[j];
                        current = current.Except(vs1).ToArray();
                    }
                    if (current.Length == 0) break;
                }
                if (current.Length > empty.Length)
                {
                    index = i;
                }
            }
            return fileNames[index];
        }

        public string[] GetWordsFrequency(string path, int countWord)
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();

            string[] some = Directory.EnumerateFiles(path, "*.txt").ToArray();


            var data = new List<string[]>();
            foreach (var item in some)
            {
                // Выборка слов с одного файла
                var res = File.ReadAllLines(item, Encoding.UTF8).SelectMany(x => x.Split(seporators, StringSplitOptions.RemoveEmptyEntries)).ToArray();
                // Добавляем слова в структуру
                data.Add(res);
            }
            foreach (var arrStr in data)
            {
                string[] temp = arrStr;
                for (int i = 0; i < temp.Length; i++)
                {
                    string tempWord = temp[i];
                    if (dic.ContainsKey(tempWord))
                    {
                        dic[tempWord]++;
                    }
                    else
                        dic[tempWord] = 1;
                }
            }
            List<(string Name, int Count)> someTuple = GetStruct(TypeTask.GetWordFrequency, dic) as List<(string Name, int Count)>;

            someTuple.Sort(new ComparerToStringAndInt() { Field = CompareField.ByDescending });
            string[] vs = new string[countWord];
            for (int i = 0; i < countWord; i++)
            {
                vs[i] = someTuple[i].Name;
            }
            return vs;
        }
    }
}
