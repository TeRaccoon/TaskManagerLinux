using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TaskManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "CBTM";
            Console.WriteLine("CBTM Booting....\n\n");
            Run run = new Run();
            Console.ReadLine();
        }
    }
}
