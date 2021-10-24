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

        public void KillProcessID(string ID)
        {
            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                if (process.Id == Convert.ToInt32(ID))
                {
                    process.Kill();
                    break;
                }
            }
        }
    }
}
