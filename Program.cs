using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2LINQ.GetterData;
using Task2LINQ.Strategy;

namespace Task2LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь: ");
            string path = @"D:\C#\practic1_DP\OOP_Main\Task2\Chesterton"; //Console.ReadLine();
            var data = GetterDataWords.GetWords(path);
            ITextSolver obj = new LinqSolver(data);

            Solver solver = new Solver(obj);
            var res = solver.GetUniqueFileName(path);
            //var longestWord = solver.FindLongestWord();
            //Console.WriteLine(longestWord);

            //var statistic = solver.GetStatisticWords();
            //foreach (var item in statistic)
            //{
            //    Console.WriteLine(item.Key + " " + item.Value);
            //}

            //var frequencyWords = solver.GetWordsFrequency(10);





        }
    }

    
}
