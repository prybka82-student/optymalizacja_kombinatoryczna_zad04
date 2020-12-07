using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie04
{
    public class Runner
    {
        private void Print(string msg, params (string, string)[] findReplace)
        {
            findReplace.ToList().ForEach(x => msg = msg.Replace(x.Item1, x.Item2));

            Console.WriteLine(msg);
        }

        private IEnumerable<double> InputSequence(int counter, 
            string sequencePrompt, string itemPrompt, string itemErrorMessage, double min, double max)
        {
            for (int i = 0; i < counter; i++)
            {
                yield return Input(itemPrompt.Replace("@NUM", (i+1).ToString()), itemErrorMessage, min, max);
            }
        }

        private double Input(string prompt, string errorMsg, double min, double max)
        {
            while (true)
            {
                Print(prompt);
                
                var input = Console.ReadLine();

                if (double.TryParse(input, out double val))
                    if (val >= min && val <= max)
                        return val;

                Print(errorMsg, ("@VAL", input));
            }
        }

        public async Task RunAsync(ISolver<int> solver)
        {
            int min = 3;
            int max = 200;

            var prompt = $"How many numbers are there in the set?\n(Provide a number greater or equal than {min} and less or equal than {max}";
            var errorMsg = ">>@VAL<< is not a valid number of items.";

            var n = (int)Input(prompt, errorMsg, min, max);

            //var form = n == 1 ? "liczbę" : "234".Contains(n.ToString().Last()) ? "liczby" : "liczb";
            var form = n == 1 ? "number" : "numbers";

            min = 1;
            max = 100;
            prompt = $"Enter {n} integer {form}, and press Enter after each number.)";

            var seq = InputSequence(n, prompt, "Provide item number @NUM:\n", errorMsg, min, max)
                .Select(x => (int)x)
                .ToList();

            var res = await solver.SolveAsync(seq);

            Print("\n\n-------------------\nThe answer is:\n\n@ANS\n\n", ("@ANS", res.ToString()));

            Print("Press any key to exit.");
            Console.ReadKey();

        }
    }
}
