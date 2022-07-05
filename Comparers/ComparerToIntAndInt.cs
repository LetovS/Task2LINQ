using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2LINQ.Comparers
{
    public class ComparerToIntAndInt : BaseComparer, IComparer<(int LenghtName, int Value)>
    {
        public override CompareField Field { get ; set ; }

        public int Compare((int LenghtName, int Value) x, (int LenghtName, int Value) y)
        {
            switch (Field)
            {
                case CompareField.ByAscending:
                    return x.LenghtName.CompareTo(y.LenghtName);
                case CompareField.ByDescending:
                    return -x.LenghtName.CompareTo(y.LenghtName);
                default:
                    throw new ArgumentException(nameof(Field));
            }
        }
    }
}
