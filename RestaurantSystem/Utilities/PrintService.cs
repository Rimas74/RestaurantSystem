using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantSystem.Utilities.Interfaces;

namespace RestaurantSystem.Utilities
    {
    public class PrintService : IPrintService
        {
        public void PrintToConsole(string content)
            {
            Console.WriteLine(content);
            }

        //public void PrintToFile(string content, string filePath)
        //    {
        //    var directory=Path.GetDirectoryName(filePath);
        //    if (!Directory.Exists(directory))
        //    {
        //        Directory.CreateDirectory(directory);
        //    }
        //    File.WriteAllText(filePath, content);
        //    }
        }

    }
