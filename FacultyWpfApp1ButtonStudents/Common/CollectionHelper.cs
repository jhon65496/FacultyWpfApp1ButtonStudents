using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacultyWpfApp1ButtonStudents.Common
{
    public static class CollectionHelper
    {
        /// <summary>Режет последовательность на фрагменты.</summary>
        /// <typeparam name="T">Тип элемента входной и выходной последовательностей.</typeparam>
        /// <param name="collection">Входная последовательность.</param>
        /// <param name="fragmentLength">Длина фрагментов. Должна быть больше нуля.</param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> SplitFragments<T>(this IEnumerable<T> collection, int fragmentLength)
        {
            if (fragmentLength < 1)
                throw new ArgumentOutOfRangeException(nameof(fragmentLength));

            T[] fragment;
            var numerator = collection.GetEnumerator();
            int fragmentCount;
            bool moveNext = true;
            while (moveNext)
            {
                fragment = new T[fragmentLength];
                fragmentCount = 0;
                while (fragmentCount < fragmentLength && (moveNext = numerator.MoveNext()))
                {
                    fragment[fragmentCount] = numerator.Current;
                    fragmentCount++;
                }
                if (fragmentCount < fragmentLength)
                {
                    fragment = fragment.Take(fragmentCount).ToArray();
                }
                yield return Array.AsReadOnly(fragment);
            }
        }

    }
}
