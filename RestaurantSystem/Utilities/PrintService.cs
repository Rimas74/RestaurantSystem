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

        }

    }
