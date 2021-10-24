using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TaskManager
{
    class Run
    {
        ProcessHandler processHandler = new ProcessHandler();
        public Run()
        {
            Console.WriteLine(File.ReadAllText(@"Manual.txt") + "\n\n~~~ Press enter to continue ~~~");
            Console.ReadLine();
            Console.Clear();
            DisplayMenu();
        }

        public void DisplayMenu()
        {
            string input = string.Empty;
            while (!input.Contains("QUIT"))
            {
                Console.Write("> ");
                string trueInput = Console.ReadLine();
                input = trueInput.Replace(" ", string.Empty);

                string menuSelection = input.Split('|')[0];
                string filter = string.Empty;
                try
                {
                    filter = input.Split('|')[1];
                }
                catch
                {

                }

                switch (menuSelection)
                {
                    case "namedump":
                        DisplayProcessNames(filter);
                        break;

                    case "nameiddump":
                        DisplayProcessNamesAndIDS(filter);
                        break;

                    case "Manual":

                        break;

                }
                if (menuSelection.Contains("killid"))
                {
                    KillProcessID(trueInput);
                }
                if (menuSelection.Contains("killall"))
                {
                    KillAll(trueInput);
                }
                Console.Write("\n");
            }
        }

        public void KillProcessID(string input)
        {
            try
            {
                processHandler.KillProcessID(input.Split(' ')[1]);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid Syntax!");
            }
        }
        public void DisplayProcessNames(string filter)
        {
            FilterHandler filterHandler = new FilterHandler();
            List<string> processNames = filterHandler.FilterData(filter, processHandler.GetAllProcessNames(), "namedump");

            if (processNames != null)
            {
                foreach (string name in processNames)
                {
                    Console.WriteLine(name);
                }
            }
        }

        public void DisplayProcessNamesAndIDS(string filter)
        {
            FilterHandler filterHandler = new FilterHandler();
            List<string> processNames = filterHandler.FilterData(filter, processHandler.GetAllProcessNamesWithIDS(), "nameiddump");
            int switcher = 0;

            if (processNames != null)
            {
                foreach (string value in processNames)
                {
                    if (switcher == 0)
                    {
                        Console.Write("Name - " + value);
                        for (int i = 0; i < 50 - value.Length; i++)
                        {
                            Console.Write(" ");
                        }
                        switcher = 1;
                    }
                    else
                    {
                        Console.WriteLine("ID - " + value);
                        switcher = 0;
                    }
                }
            }
        }
    }
}
