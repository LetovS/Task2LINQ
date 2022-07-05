using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2LINQ.Comparers
{
    public class ComparerToStringAndInt : BaseComparer, IComparer<(string Name, int Count)>
    {
        public override CompareField Field { get ; set ; }

        public int Compare((string Name, int Count) x, (string Name, int Count) y)
        {
            switch (Field)
            {
                case CompareField.ByAscending:
                    return x.Count.CompareTo(y.Count);
                case CompareField.ByDescending:
                    return -x.Count.CompareTo(y.Count);
                default:
                    throw new ArgumentException(nameof(Field));
            }
        }
    }
}
