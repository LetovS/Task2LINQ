using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
            
            Console.WriteLine("Введите путь к папке с текстовыми файлами в формате .txt: ");
            var userPath = Console.ReadLine();
            string path = @"..\..\Data\Chesterton";

            if (Directory.Exists(userPath))
                path = userPath;
            else
            {
                Console.WriteLine("Проблемы в указанном пути");
                var str = Environment.CurrentDirectory;
                Console.WriteLine($"Используется путь по умолчанию {str.Substring(0, str.Length - 9)}Data\\Chesterton");
            }
            var data = GetterDataWords.GetWords(path);

            ITextSolver obj = new LinqSolver(data);
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1 - Посмотреть вполнение задач.");
            Console.WriteLine("2 - Сравнить быстродействие.");
            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.D1)
            {
                Console.WriteLine("LINQ");
                Console.WriteLine("1) Самое длинное слово " + obj.FindLongestWord());
                Console.WriteLine("2) Статистика: ");
                foreach (var item in obj.GetStatisticWords(path))
                {
                    Console.WriteLine(item.Key + " " + item.Value);
                }
                Console.WriteLine("3) Часто Встречаемые слова: ");
                foreach (var item in obj.GetWordsFrequency(path, 10))
                {
                    Console.Write(item + " ");
                }
                Console.WriteLine("\n\nПоследовательные алгоритмы");
                obj = new SequentialSolver(data);
                Console.WriteLine("1) Самое длинное слово " + obj.FindLongestWord());
                Console.WriteLine("2) Статистика: ");
                foreach (var item in obj.GetStatisticWords(path))
                {
                    Console.WriteLine(item.Key + " " + item.Value);
                }
                Console.WriteLine("3) Часто Встречаемые слова: ");
                foreach (var item in obj.GetWordsFrequency(path, 10))
                {
                    Console.Write(item + " ");
                }
                Console.WriteLine("\n4) Имя файла с большим числом уникальных слов: " + obj.GetUniqueFileName(path));

                Console.WriteLine("\n\nParallel LINQ");
                obj = new ParallelLinqSolver(data);
                Console.WriteLine("1) Самое длинное слово " + obj.FindLongestWord());
                Console.WriteLine("2) Статистика: ");
                foreach (var item in obj.GetStatisticWords(path))
                {
                    Console.WriteLine(item.Key + " " + item.Value);
                }
                Console.WriteLine("3) Часто Встречаемые слова: ");
                foreach (var item in obj.GetWordsFrequency(path, 10))
                {
                    Console.Write(item + " ");
                }
                Console.WriteLine();
            }
            else
            {
                Stopwatch sw = new Stopwatch();
                Console.WriteLine("\nСравнение быстродействия: ");
                sw.Start();
                var statistic = obj.GetWordsFrequency(path, 10);
                sw.Stop();
                var res1 = sw.ElapsedMilliseconds;

                obj = new LinqSolver(data);

                sw.Start();
                statistic = obj.GetWordsFrequency(path, 10);
                sw.Stop();
                var res2 = sw.ElapsedMilliseconds;

                obj = new SequentialSolver(data);

                sw.Start();
                statistic = obj.GetWordsFrequency(path, 10);
                sw.Stop();
                var res3 = sw.ElapsedMilliseconds;

                Console.WriteLine($"Parallel Linq {res1}");
                Console.WriteLine($"LINQ {res2}");
                Console.WriteLine($"Последовательный {res3}");
            }
            Console.WriteLine("Конец работы");
            Console.ReadLine();
        }
    }
}
