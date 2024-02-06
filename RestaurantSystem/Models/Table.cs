using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Models
    {
    public class Table
        {
        public int TableNumber { get; set; }
        public int NumberOfSeats { get; set; }
        public bool IsOccupied { get; set; }
        }
    }
