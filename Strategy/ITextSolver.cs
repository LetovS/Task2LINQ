using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2LINQ.Strategy
{
    public interface ITextSolver
    {
        string FindLongestWord();
        string[] GetWordsFrequency(string path, int countWord);
        Dictionary<int, int> GetStatisticWords(string path);
        string GetUniqueFileName(string path);
    }
}
