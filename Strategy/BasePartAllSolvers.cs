using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2LINQ.Strategy
{
    public abstract class BasePartAllSolvers
    {
        protected abstract List<string[]> _data { get; set; }
        protected static readonly string[] seporators = { " ", ".", ",", "!", "#", "$", "%", "^", "&", "*", "(", ")", "-", "_", "+", "/", "[", "]", ":", ";", "\"", "\r", "\n", "\t", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "|" };
        public enum TypeTask
        {
            /// <summary>
            /// Частота слов.
            /// </summary>
            GetWordFrequency = 0,
            /// <summary>
            /// Статистика слов.
            /// </summary>
            GetStatisticByWords
        }
        private List<(string Name, int Count)> GetNeedStruct(Dictionary<string, int> dic)
        {
            List<(string Name, int Count)> listForSort = new List<(string Name, int Count)>();
            foreach (KeyValuePair<string, int> pair in dic)
            {
                listForSort.Add((pair.Key, pair.Value));
            }
            return listForSort;
        }
        private List<(int LenghtName, int Count)> GetNeedStruct(Dictionary<int, int> dic)
        {
            List<(int LenghtName, int Count)> listForSort = new List<(int LenghtName, int Count)>();
            foreach (KeyValuePair<int, int> pair in dic)
            {
                listForSort.Add((pair.Key, pair.Value));
            }
            return listForSort;
        }
        protected object GetStruct(TypeTask typeTask, object obj)
        {
            switch (typeTask)
            {
                case TypeTask.GetWordFrequency:
                    return (object)GetNeedStruct(obj as Dictionary<string, int>);
                case TypeTask.GetStatisticByWords:
                    return (object)GetNeedStruct(obj as Dictionary<int, int>);
                default:
                    return default;
            }
        }
    }
}
