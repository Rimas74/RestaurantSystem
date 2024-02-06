using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Models
    {
    public interface IVoucher
        {
        int OrderId { get; }
        int TableNumber { get; }
        List<OrderItem> Items { get; }
        decimal TotalAmount { get; }
        DateTime OrderDate { get; }
        string PrintVoucher();
        }
    }
