using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;

namespace RestaurantSystem.Models
    {
    public class RestaurantVoucher : IVoucher
        {
        public int VoucherId { get; set; }

        public int TableNumber { get; private set; }
        public List<OrderItem> Items { get; private set; }
        public decimal TotalAmount { get; private set; }
        public DateTime OrderDate { get; private set; }

        public RestaurantVoucher(Order order, int voucherId)
            {
            VoucherId = voucherId;
            TableNumber = order.TableNumber;
            Items = order.Items;
            TotalAmount = order.TotalAmount;
            OrderDate = order.OrderDate;
            }
        public string PrintVoucher()
            {
            var lithuanianCulture = new CultureInfo("lt-LT");
            var builder = new StringBuilder("Restaurant Voucher: ");
            builder.AppendLine();
            builder.AppendLine($"Voucher ID: {VoucherId}");
            builder.AppendLine($"Table Number: {TableNumber}");
            builder.AppendLine($"Ordered Items:");

            foreach (var item in Items)
                {
                string formattedPrice = item.Price.ToString("F2", lithuanianCulture) + " €";
                builder.AppendLine($"{item.Name} X {item.Quantity} X {formattedPrice} each");
                }
            string formattedTotal = TotalAmount.ToString("F2", lithuanianCulture) + " €";
            builder.AppendLine($"Total Amount: {formattedTotal}");
            builder.AppendLine($"Order Date: {OrderDate}");

            return builder.ToString();
            }

        }
    }
