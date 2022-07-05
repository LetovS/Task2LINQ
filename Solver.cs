using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2LINQ.Strategy;

namespace Task2LINQ
{
    public class Solver : ITextSolver
    {
        private ITextSolver _solver;
        public Solver(ITextSolver solver) => _solver = solver ?? throw new ArgumentNullException();
        /// <summary>
        /// Получение самого длинного слова.
        /// </summary>
        public string FindLongestWord()
        {
            return _solver.FindLongestWord();
        }
        /// <summary>
        /// Получение статистики слов в файлах
        /// </summary>
        public Dictionary<int, int> GetStatisticWords()
        {
            return _solver.GetStatisticWords();
        }
        /// <summary>
        /// Получение имени файла с макс числом слов.
        /// </summary>
        /// <param name="path">Путь к каталогу.</param>
        public string GetUniqueFileName(string path)
        {
            return _solver.GetUniqueFileName(path);
        }
        /// <summary>
        /// Получение часто встречающихся слов
        /// </summary>
        /// <param name="countWord">Число слов</param>
        /// <returns>Массив слов</returns>
        public string[] GetWordsFrequency(int countWord)
        {
            return _solver.GetWordsFrequency(countWord);
        }
    }
}
