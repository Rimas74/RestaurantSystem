using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantSystem.BusinessLogicLayer.Interfaces;
using RestaurantSystem.Models;

namespace RestaurantSystem.BusinessLogicLayer
    {
    internal class OrderManager : IOrderManager
        {
        private int _nextOrderID = 1;

        public Order CreateOrder(int tableNumber, List<OrderItem> items)
            {
            var order = new Order
                {
                OrderId = _nextOrderID++,
                TableNumber = tableNumber,
                Items = items,
                OrderDate = System.DateTime.Now,
                };
            return order;
            }

        }
    }
