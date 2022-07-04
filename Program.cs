using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "";
            Console.WriteLine("Укажите директорию: ");
            path = @"D:\C#\practic1_DP\OOP_Main\Task2\Chesterton";// Console.ReadLine();
            // Получить исходные данные
            LinqSolver lnqSolver = new LinqSolver(path);
            string result = lnqSolver.GetLongestWord(path);
            var result2 = lnqSolver.GetTopWords(path, 10);
            foreach (var item in result2)
            {
                Console.WriteLine(item.Word + " = " + item.Count);
            }
            Dictionary<int, int> resultishe = lnqSolver.GetStatisticOnWordsLenght();

            foreach (KeyValuePair<int, int> item in resultishe)
            {
                Console.WriteLine(item.Key + " = " + item.Value);
            }

            //Console.WriteLine(result);

        }
    }
}
