using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2LINQ.Comparers
{
    public abstract class BaseComparer
    {
        public abstract CompareField Field { get; set; }
        /// <summary>
        /// Тип сортировки.
        /// </summary>
        public enum CompareField : byte
        {
            /// <summary>
            /// По возрастанию.
            /// </summary>
            ByAscending =0,
            /// <summary>
            /// По убыванию.
            /// </summary>
            ByDescending
        }
    }
}
