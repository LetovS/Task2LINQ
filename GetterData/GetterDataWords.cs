using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2LINQ.GetterData
{
    public static class GetterDataWords
    {
        private static List<string[]> _data;
        private static readonly string[] seporators = { " ", ".", ",", "!", "#", "$", "%", "^", "&", "*", "(", ")", "-", "_", "+", "/", "[", "]", ":", ";", "\"", "\r", "\n", "\t" };
        public static List<string[]> GetWords(string path)
        {
            //TODO ввести перечисленние шаблонов для выбора формата файлов.
            // Получить пути к файлам
            string[] some = Directory.EnumerateFiles(path, "*.txt").ToArray();
            // Получение слов
            _data = new List<string[]>();
            foreach (var item in some)
            {
                // Выборка слов с одного файла
                var res = File.ReadAllLines(item, Encoding.UTF8).SelectMany(x => x.Split(seporators, StringSplitOptions.RemoveEmptyEntries)).ToArray();
                // Добавляем слова в структуру
                _data.Add(res);
            }
            return _data;
        }
    }
}
