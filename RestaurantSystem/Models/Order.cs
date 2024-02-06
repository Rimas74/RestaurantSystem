using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Models
    {
    public class Order
        {
        public int OrderId { get; set; }
        public int TableNumber { get; set; }
        public List<OrderItem> Items { get; set; }
        public DateTime OrderDate { get; set; }

        public decimal TotalAmount
            {
            get
                {
                return Items.Sum(item => item.Price * item.Quantity);
                }
            }
        }

    public class OrderItem
        {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public ProductCategory Category { get; set; }
        }
    }
