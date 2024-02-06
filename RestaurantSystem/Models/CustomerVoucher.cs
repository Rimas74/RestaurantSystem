using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Models
    {
    internal class CustomerVoucher : IVoucher
        {
        public int OrderId { get; private set; }
        public int TableNumber { get; private set; }
        public List<OrderItem> Items { get; private set; }
        public decimal TotalAmount { get; private set; }
        public DateTime OrderDate { get; private set; }

        public CustomerVoucher(Order order, int orderId)
            {
            OrderId = orderId;
            TableNumber = order.TableNumber;
            Items = order.Items;
            TotalAmount = order.TotalAmount;
            OrderDate = order.OrderDate;
            }

        public string PrintVoucher()
            {
            var builder = new StringBuilder("Customer Voucher");
            builder.AppendLine($"Order ID: {OrderId}");
            builder.AppendLine($"Table Number: {TableNumber}");
            builder.AppendLine($"Ordered Items:");

            foreach (var item in Items)
                {
                builder.AppendLine($"{item.Name} X {item.Quantity} X {item.Price:C} each");
                }
            builder.AppendLine($"Total Amount: {TotalAmount:C}");
            builder.AppendLine($"Order Date: {OrderDate}");

            return builder.ToString();
            }
        }

    }
