using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TaskManager
{
    class ProcessHandler
    {
        public List<string> GetAllProcessNames()
        {
            List<string> processList = new List<string>();
            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                processList.Add(process.ProcessName);
            }
            return processList;
        }

        public List<string> GetAllProcessNamesWithIDS()
        {
            List<string> processList = new List<string>();
            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                processList.Add(process.ProcessName);
                processList.Add(process.Id.ToString());
            }
            return processList;
        }

        public List<string> GetAllProcessIDS()
        {
            List<string> processList = new List<string>();
            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                processList.Add(process.Id.ToString());
            }
            return processList;
        }

        public void KillProcessID(string ID)
        {
            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                if (process.Id == Convert.ToInt32(ID))
                {
                    try
                    {
                        process.Kill();
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Unable to kill process " + process.Id + ". Access denied!");
                    }
                    break;
                }
            }
        }
        public void KillProcessID(List<string> killList)
        {
            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                if (killList.Contains(process.Id.ToString()))
                {
                    try
                    {
                        process.Kill();
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Unable to kill process " + process.Id + ". Access denied!");
                    }
                }
            }
        }
        public void KillAll(string name)
        {
            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                if (process.ProcessName.Contains(name))
                {
                    process.Kill();
                }
            }
        }
    }
}
