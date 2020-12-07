using System;
using System.Linq;
using System.Threading.Tasks;

namespace Zadanie04
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var runner = new Runner();
            
            await runner.RunAsync(new Solver());

            //2, 1, 5, 3, 4 możemy uzyskać maksymalnie 31 
        }
    }
}
