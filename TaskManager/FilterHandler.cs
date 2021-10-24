using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TaskManager
{
    class FilterHandler
    {
        public List<string> FilterData(string filter, List<string> data, string sender)
        {
            string[] filters = filter.Split('-');
            foreach (string filterChar in filters)
            {
                switch (filterChar)
                {
                    case "o":
                        data = OrderAlphabetically(data, sender);
                        break;

                    case "d":
                        data = ClearDuplicates(data, sender);
                        break;
                }
                if (filterChar.Contains("s\""))
                {
                    SaveOutput(data, filter);
                    return null;
                }
                if (filterChar.Contains("r\""))
                {
                    return RegexOutput(data, filter, true);
                }
                if (filterChar.Contains("R\""))
                {
                    return RegexOutput(data, filter, false);
                }
            }
            return data;
        }

        public List<string> RegexOutput(List<string> data, string filter, bool caseSensitive)
        {
            int endIndex = filter.IndexOf("-r") + 3;
            if (!caseSensitive)
            {
                endIndex = filter.IndexOf("-R") + 3;
            }

            string filterString = string.Empty;
            while (filter[endIndex] != '"')
            {
                filterString += filter[endIndex];
                endIndex++;
            }
            List<string> purgedData = new List<string>();
            foreach (string processName in data)
            {
                if (!caseSensitive)
                {
                    if (processName.ToUpper().Contains(filterString.ToUpper()))
                    {
                        purgedData.Add(processName);
                    }
                }
                else
                {
                    if (processName.Contains(filterString))
                    {
                        purgedData.Add(processName);
                    }
                }
            }
            return purgedData;
        }
        public void SaveOutput(List<string> data, string filter)
        {
            int endIndex = filter.IndexOf("-s") + 3;
            string directory = string.Empty;
            while (filter[endIndex] != '"')
            {
                directory += filter[endIndex];
                endIndex++;
            }
            try
            {
                File.WriteAllLines(@directory, data);
                Console.WriteLine("Successfully saved to " + directory + "\n");
            }
            catch (Exception)
            {
                Console.WriteLine("\nFailed to write to directory: " + directory);
            }
        }

        public List<string> OrderAlphabetically(List<string> data, string sender)
        {
            if (sender.Contains("nameiddump"))
            {
                List<string> allData = new List<string>();
                for (int i = 0; i < data.Count; i+=2)
                {
                    allData.Add(data[i] + "|" + data[i + 1]);
                }
                allData.Sort();
                data.Clear();
                foreach (string individualData in allData)
                {
                    string[] splitData = individualData.Split('|');
                    data.Add(splitData[0]);
                    data.Add(splitData[1]);
                }
                return data;
            }
            else
            {
                data.Sort();
                return data;
            }
        }

        public List<string> ClearDuplicates(List<string> data, string sender)
        {
            List<string> purgedData = new List<string>();

            if (sender.Contains("nameiddump"))
            {
                for (int i = 0; i < data.Count; i += 2)
                {
                    if (!purgedData.Contains(data[i]) && purgedData.IndexOf(data[i]) == -1)
                    {
                        purgedData.Add(data[i]);
                        purgedData.Add(data[i + 1]);
                    }
                }
            }
            else
            {
                foreach (string dataItem in data)
                {
                    if (!purgedData.Contains(dataItem))
                    {
                        purgedData.Add(dataItem);
                    }
                }
            }
            return purgedData;
        }
    }
}
