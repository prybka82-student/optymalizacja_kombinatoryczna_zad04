using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie04
{
    public class Solver : ISolver<int>
    {
        public async Task<int> SolveAsync(IEnumerable<int> sequence)
        {
            return await MaximizeSumAsync(sequence);
        }

        private async Task<int> MaximizeSumAsync(IEnumerable<int> sequence, int tripleSum =0)
        {
            if (sequence.Count() == 3)
                return sequence.Sum(x => x) + tripleSum;

            var tasks = new List<Task<int>>();

            for (int i = 1; i < sequence.Count() - 1; i++)
            {
                tasks.Add(MaximizeSumAsync(sequence.RemoveAt(i), tripleSum + sequence.Sum(i-1,i+1)));
            }

            await Task.WhenAll<int>(tasks);

            return tasks.Select(x => x.Result).Max();
        }


    }
}
