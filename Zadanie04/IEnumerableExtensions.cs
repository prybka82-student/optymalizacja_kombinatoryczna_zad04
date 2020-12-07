using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zadanie04
{
    public static class IEnumerableExtensions
    {
        public static int Sum(this IEnumerable<int> sequence, int startIndex, int endIndex)
        {
            if (startIndex < 0)
                throw new IndexOutOfRangeException();

            if (endIndex >= sequence.Count())
                throw new IndexOutOfRangeException();

            var res = 0;

            for (int i = startIndex; i <= endIndex; i++)
                res += sequence.ElementAt(i);

            return res;
        }


        public static IEnumerable<T> RemoveAt<T>(this IEnumerable<T> sequence, int index)
        {
            if (index < 0 || index > sequence.Count() - 1)
                throw new IndexOutOfRangeException($"Index {index} is larger than the number of elements in sequence ({sequence.Count()}).");

            if (index == 0)
                return sequence.Skip(1);

            if (index == sequence.Count() - 1)
                return sequence.Take(sequence.Count() - 1);

            var part1 = sequence.Take(index);
            var part2 = sequence.Skip(index + 1);

            return part1.Concat(part2);
        }
    }
}
